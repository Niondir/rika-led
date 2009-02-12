using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommunicationAPI.DataTypes;

namespace StoreClient
{
    /// <summary>
    /// Stellt einen Dialog dar, mit dem sich Produkte im Detail ansehen, bearbeiten und neu erstellen lassen
    /// </summary>
    public partial class FormAddProduct : Form
    {

        /// <summary>
        /// Gibt die dem Dialog entsprechende Werbung zurück oder legt diese fest
        /// </summary>
        public ProductData Value
        {
            get
            {
                SignData thisSign = new SignData(Convert.ToInt32(textBoxNumber.Text), regions[comboBoxGroup.SelectedIndex]);
                textBoxPrice.Text.Replace(',', '.');
                return new ProductData(thisSign, textBoxName.Text, Convert.ToDouble(textBoxPrice.Text));
            }
            set
            {
                textBoxName.Text = value.Name;
                textBoxNumber.Text = value.Sign.Id.ToString();
                textBoxPrice.Text = value.Price.ToString("0.00");
                comboBoxGroup.Text = value.Sign.Region.Name;
            }
        }

        private RegionData[] regions;
        
        /// <summary>
        /// Erstellt ein neues Dialogfenster für Produkte und initialisiert die Komponenten
        /// Zusätzlich werden die auf dem Server verfügbaren Regionen ind die ComboBox geladen
        /// </summary>
        public FormAddProduct()
        {
            InitializeComponent();
            SetRegions();
        }

        /// <summary>
        /// Erstellt ein neues Dialogfenster für Produkte und initialisiert die Komponenten
        /// Zusätzlich werden die Anzeigefelder mit den übergebenen Eigenschaften gefüllt
        /// </summary>
        /// <param name="editData">Das Produkt, das angezeigt werden soll</param>
        public FormAddProduct(ProductData editData)
            :this()
        {
            Value = editData;
        }

        /// <summary>
        /// Holt vom Server alle aktuell exisitierenden Produktgruppen und Stellt sie in der comboBoxGroup dar
        /// </summary>
        private void SetRegions()
        {
            regions = Connection.GetInstance().GetRegions();
            if (regions == null)
                return;
            comboBoxGroup.Items.Clear();
            foreach (RegionData i in regions)
            {
                comboBoxGroup.Items.Add(i.Name);
            }
        }

        /// <summary>
        /// Holt vom Server alle aktuell exisitierenden Produktgruppen und Stellt sie in der comboBoxGroup dar
        /// Zusätzlich wird die übergebene Produktgruppe selektiert
        /// </summary>
        /// <param name="selected">Produktgruppe, die selektiert werden soll</param>
        private void SetRegions(RegionData selected)
        {
            this.SetRegions();
            comboBoxGroup.SelectedIndex = comboBoxGroup.Items.Count - 1;
        }

        /// <summary>
        /// Übernimmt den aktuellen Status und schließt den Dialog wenn alle Eingaben korreckt sind mit dem DialogResult: OK
        /// </summary>
        private void buttonAccept_Click(object sender, EventArgs e)
        {
            if (Valid())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// Schließt den Dialog ohne die Änderungen zu übernehmen. DialogResult: CANCEL
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Überprüft, ob die Angaben im Dialog ein gültes Produkt ergeben und weist gegebenenfalls darauf hin, Einträge zu korrigieren.
        /// </summary>
        /// <returns>Gibt das Ergebnis der Validierung zurück</returns>
        private bool Valid()
        {
            string errorMsg = string.Empty;
            int i = 1;
            if (textBoxName.Text.Length < 1)
            {
                textBoxName.BackColor = Color.OrangeRed;
                errorMsg += i++.ToString()+". Bitte geben Sie eine gültige Produktbezeichnung ein." + Environment.NewLine;
            }
            int res;
            if (!int.TryParse(textBoxNumber.Text, out res))
            {
                textBoxNumber.BackColor = Color.OrangeRed;
                errorMsg += i++.ToString() + ". Bitte geben Sie eine gültige Produktnummer ein." + Environment.NewLine;
            }
            if(comboBoxGroup.Text.Length == 0)
            {
                comboBoxGroup.BackColor = Color.OrangeRed;
                errorMsg += i++.ToString() + ". Bitte wählen Sie eine Produktgruppe." + Environment.NewLine;
            }
            double result;
            if (!double.TryParse(textBoxPrice.Text, out result))
            {
                textBoxPrice.BackColor = Color.OrangeRed;
                errorMsg += i++.ToString() + ". Geben Sie einen gültigen Preis an." + Environment.NewLine;
            }
            if (comboBoxGroup.SelectedIndex == -1 && comboBoxGroup.Text.Length > 0)
            {
                if (MessageBox.Show("Die Warengruppe \"" + comboBoxGroup.Text + "\" existiert nicht, soll sie nun erstellt werden?", "Neue Gruppe erstellen?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    FormAddRegion f = new FormAddRegion(comboBoxGroup.Text);
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        Connection.GetInstance().Add(f.Value);
                        SetRegions(f.Value);
                    }
                    else
                    {
                        comboBoxGroup.BackColor = Color.OrangeRed;
                        errorMsg += i++.ToString() + ". Bitte wählen Sie eine Produktgruppe." + Environment.NewLine;
                    }
                }
                else
                {
                    comboBoxGroup.BackColor = Color.OrangeRed;
                    errorMsg += i++.ToString() + ". Bitte wählen Sie eine Produktgruppe." + Environment.NewLine;
                }
            }


            if (errorMsg.Length > 0)
            {
                MessageBox.Show(errorMsg);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Setzt die Hintergrundfarbe des Controls auf White. Dient dazu, bei Betreten eines zuvor rot gefärbten Controls, auf weiss zurück zu stellen
        /// </summary>
        /// <param name="sender">Control, das weisste Hintergrundfarbe fordert</param>
        private void ChangeBackToWhiteBackCol(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = SystemColors.Window;
        }

        /// <summary>
        /// Öffnet einen neuen Dialog, in dem eine neue Produktgruppe erstellt werden kann
        /// </summary>
        private void buttonNewGroup_Click(object sender, EventArgs e)
        {
            FormAddRegion addReg = new FormAddRegion();
            if (addReg.ShowDialog() == DialogResult.OK)
            {
                Connection.GetInstance().Add(addReg.Value);
                SetRegions(addReg.Value);
            }
        }

        /// <summary>
        /// Stellt zu jedem Eingabe Control im Dialog die passende Hilfe in die richTextBox
        /// </summary>
        /// <param name="sender">Control, zu dem die Hilfe angezeigt werden soll</param>
        private void EnterNewBox(object sender, EventArgs e)
        {
            string descString = "";
            if (sender == textBoxName)
                descString = "Geben Sie bitte die Produktbezeichnung ein\r\nDiese Bezeichnung erscheint ebenfalls auf dem Preisschild. Daher sind nur maximal 20 Zeichen pro Wort und 3 Worte erlaubt";
            if (sender == textBoxNumber)
                descString = "Geben Sie bitte die Nummer des Preisschildes, auf dem das Produkt dargestellt sein soll an";
            if (sender == textBoxPrice)
                descString = "Geben Sie bitte den Preis des Produkte in EUR an. Dezimalstellen können durch '.' und ',' getrennt werden";
            if (sender == comboBoxGroup)
                descString = "Wählen Sie bitte die Warengruppe, derer das Produkt zugehörig sein soll oder geben Sie eine neue ein";
            richTextBox1.Text = descString;
        }
    }
}

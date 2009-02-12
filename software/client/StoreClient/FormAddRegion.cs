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
    /// 
    /// </summary>
    public partial class FormAddRegion : Form
    {

        /// <summary>
        /// Gibt die dem Dialog entsprechende Produktgruppe zurück oder legt diese fest
        /// </summary>
        public RegionData Value
        {
            get { return new RegionData(textBoxID.Text, textBoxName.Text); }
            set
            {
                textBoxName.Text = value.Name;
                textBoxID.Text = value.Id;
            }
        }

        /// <summary>
        /// Erstellt ein neues Dialogfenster für Produktgruppen und initialisiert die Komponenten
        /// </summary>
        public FormAddRegion()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Erstellt ein neues Dialogfenster für Produktgruppen und initialisiert die Komponenten
        /// Zusätzlich wird der übergebene Name als Gruppenname eingetragen und der Focus für schnelle Eingabe auf das ID Textfeld gelegt
        /// <param name="name">Name der zu erstellenden Produktgruppe</param>
        /// </summary>
        public FormAddRegion(string name)
            : this()
        {
            textBoxName.Text = name;
            textBoxID.Focus();
        }

        /// <summary>
        /// Erstellt ein neues Dialogfenster für Produktgruppen und initialisiert die Komponenten
        /// Zusätzlich werden die Anzeigefelder mit den übergebenen Eigenschaften gefüllt
        /// </summary>
        /// <param name="editData">Die Produktgruppe, die angezeigt werden soll</param>
        public FormAddRegion(RegionData editData)
            :this()
        {
            this.Value = editData;
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
        /// Überprüft, ob die Angaben im Dialog eine gülte Produktgruppe ergeben und weist gegebenenfalls darauf hin, Einträge zu korrigieren.
        /// </summary>
        /// <returns>Gibt das Ergebnis der Validierung zurück</returns>
        private bool Valid()
        {
            string errorMsg = string.Empty;
            int i = 1;
            if (textBoxName.Text.Length < 1)
            {
                textBoxName.BackColor = Color.OrangeRed;
                errorMsg += i++.ToString() + ". Bitte geben Sie eine gütige Produktgruppen Nummer ein" + Environment.NewLine;
            }
            int res;
            if (!int.TryParse(textBoxID.Text, out res))
            {
                textBoxID.BackColor = Color.OrangeRed;
                errorMsg += i++.ToString() + ". Bitte geben Sie eine gütige Identifikationsnummer ein" + Environment.NewLine;
            }
            if (errorMsg.Length > 0)
            {
                MessageBox.Show(errorMsg);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Schließt den Dialog ohne die Änderungen zu übernehmen. DialogResult: CANCEL
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Setzt die Hintergrundfarbe des Controls auf White. Dient dazu, bei Betreten eines zuvor rot gefärbten Controls, auf weiss zurück zu stellen
        /// </summary>
        /// <param name="sender">Control, das weisste Hintergrundfarbe fordert</param>
        /// <param name="e">unbedeutend</param>
        private void ChangeBackToWhiteBackCol(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = SystemColors.Window;
        }


        /// <summary>
        /// Stellt zu jedem Eingabe Control im Dialog die passende Hilfe in die richTextBox
        /// </summary>
        /// <param name="sender">Control, zu dem die Hilfe angezeigt werden soll</param>
        /// <param name="e">unbedeutend</param>
        private void EnterNewBox(object sender, EventArgs e)
        {
            if(sender == textBoxName)
                richTextBox1.Text = "Bitte geben Sie eine für die neue Produktgruppe einen Namen ein";
            if(sender == textBoxID)
                richTextBox1.Text = "Bitte geben Sie die Identifikationsnummer der zugehörigen Lampe ein. Diese finden Sie auf der Rückseite der Steuerschaltung";
        }
    }
}

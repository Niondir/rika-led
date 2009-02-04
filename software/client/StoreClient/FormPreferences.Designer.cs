namespace StoreClient
{
    partial class FormPreferences
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPreferences));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxSaveUser = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.checkBoxPort = new System.Windows.Forms.CheckBox();
            this.textBoxServerAddr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.buttonSendID = new System.Windows.Forms.Button();
            this.textBoxNewID = new System.Windows.Forms.TextBox();
            this.textBoxOldID = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonShowID = new System.Windows.Forms.Button();
            this.imageListTabPages = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.ImageList = this.imageListTabPages;
            this.tabControl.Location = new System.Drawing.Point(3, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(416, 213);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBoxPassword);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.textBoxUsername);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.checkBoxSaveUser);
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(408, 186);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Benutzer";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(110, 72);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(100, 20);
            this.textBoxPassword.TabIndex = 4;
            this.textBoxPassword.UseSystemPasswordChar = true;
            this.textBoxPassword.Enter += new System.EventHandler(this.textBoxPassword_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Passwort";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(110, 46);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(100, 20);
            this.textBoxUsername.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Benutzername";
            // 
            // checkBoxSaveUser
            // 
            this.checkBoxSaveUser.AutoSize = true;
            this.checkBoxSaveUser.Location = new System.Drawing.Point(29, 25);
            this.checkBoxSaveUser.Name = "checkBoxSaveUser";
            this.checkBoxSaveUser.Size = new System.Drawing.Size(149, 17);
            this.checkBoxSaveUser.TabIndex = 0;
            this.checkBoxSaveUser.Text = "Benutzernamen speichern";
            this.checkBoxSaveUser.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.textBoxPort);
            this.tabPage2.Controls.Add(this.checkBoxPort);
            this.tabPage2.Controls.Add(this.textBoxServerAddr);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(408, 186);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Verbindung";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(396, 80);
            this.label4.TabIndex = 4;
            this.label4.Text = "Damit die Änderungen wirksam werden, müssen Sie sich zunächst auslogggen. [Datei " +
                "-> Verbindung trennen]\r\n";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Enabled = false;
            this.textBoxPort.Location = new System.Drawing.Point(162, 51);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(240, 20);
            this.textBoxPort.TabIndex = 3;
            // 
            // checkBoxPort
            // 
            this.checkBoxPort.AutoSize = true;
            this.checkBoxPort.Location = new System.Drawing.Point(9, 54);
            this.checkBoxPort.Name = "checkBoxPort";
            this.checkBoxPort.Size = new System.Drawing.Size(147, 17);
            this.checkBoxPort.TabIndex = 2;
            this.checkBoxPort.Text = "Anderen Port verwenden:";
            this.checkBoxPort.UseVisualStyleBackColor = true;
            this.checkBoxPort.CheckedChanged += new System.EventHandler(this.checkBoxPort_CheckedChanged);
            // 
            // textBoxServerAddr
            // 
            this.textBoxServerAddr.Location = new System.Drawing.Point(94, 12);
            this.textBoxServerAddr.Name = "textBoxServerAddr";
            this.textBoxServerAddr.Size = new System.Drawing.Size(308, 20);
            this.textBoxServerAddr.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Server Adresse:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.buttonSendID);
            this.tabPage3.Controls.Add(this.textBoxNewID);
            this.tabPage3.Controls.Add(this.textBoxOldID);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.buttonShowID);
            this.tabPage3.ImageIndex = 2;
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(408, 186);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Geräte";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // buttonSendID
            // 
            this.buttonSendID.Location = new System.Drawing.Point(174, 149);
            this.buttonSendID.Name = "buttonSendID";
            this.buttonSendID.Size = new System.Drawing.Size(75, 23);
            this.buttonSendID.TabIndex = 9;
            this.buttonSendID.Text = "Senden";
            this.buttonSendID.UseVisualStyleBackColor = true;
            this.buttonSendID.Click += new System.EventHandler(this.buttonSendID_Click);
            // 
            // textBoxNewID
            // 
            this.textBoxNewID.Location = new System.Drawing.Point(64, 151);
            this.textBoxNewID.MaxLength = 4;
            this.textBoxNewID.Name = "textBoxNewID";
            this.textBoxNewID.Size = new System.Drawing.Size(100, 20);
            this.textBoxNewID.TabIndex = 8;
            this.textBoxNewID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckHex);
            // 
            // textBoxOldID
            // 
            this.textBoxOldID.Location = new System.Drawing.Point(64, 129);
            this.textBoxOldID.MaxLength = 4;
            this.textBoxOldID.Name = "textBoxOldID";
            this.textBoxOldID.Size = new System.Drawing.Size(100, 20);
            this.textBoxOldID.TabIndex = 7;
            this.textBoxOldID.Text = "FFFF";
            this.textBoxOldID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckHex);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 154);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Neue ID";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 132);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Alte ID";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(395, 35);
            this.label6.TabIndex = 2;
            this.label6.Text = "Sie haben die Möglichkeit, die ID eines Schildes zu ändern. Hierzu benötigen Sie " +
                "die alte ID und tragen zusätzlich die neue ID ein.";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(5, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(398, 35);
            this.label5.TabIndex = 1;
            this.label5.Text = "Sie können alle Schilder veranlassen, ihre ID auf dem Display anzuzeigen.";
            // 
            // buttonShowID
            // 
            this.buttonShowID.Location = new System.Drawing.Point(8, 47);
            this.buttonShowID.Name = "buttonShowID";
            this.buttonShowID.Size = new System.Drawing.Size(241, 23);
            this.buttonShowID.TabIndex = 0;
            this.buttonShowID.Text = "Anzeigen der IDs auf dem Display veranlassen";
            this.buttonShowID.UseVisualStyleBackColor = true;
            this.buttonShowID.Click += new System.EventHandler(this.buttonShowID_Click);
            // 
            // imageListTabPages
            // 
            this.imageListTabPages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTabPages.ImageStream")));
            this.imageListTabPages.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTabPages.Images.SetKeyName(0, "user");
            this.imageListTabPages.Images.SetKeyName(1, "connection");
            this.imageListTabPages.Images.SetKeyName(2, "drive_rename.png");
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.tabControl, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.panelButtons, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(422, 251);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonCancel);
            this.panelButtons.Controls.Add(this.buttonSave);
            this.panelButtons.Controls.Add(this.buttonSaveAndClose);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.Location = new System.Drawing.Point(0, 219);
            this.panelButtons.Margin = new System.Windows.Forms.Padding(0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(422, 32);
            this.panelButtons.TabIndex = 1;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Image = global::StoreClient.Properties.Resources.cancel;
            this.buttonCancel.Location = new System.Drawing.Point(12, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(85, 26);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Abbrechen";
            this.buttonCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Image = global::StoreClient.Properties.Resources.page_save;
            this.buttonSave.Location = new System.Drawing.Point(225, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(95, 26);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Übernehmen";
            this.buttonSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Image = global::StoreClient.Properties.Resources.tick;
            this.buttonSaveAndClose.Location = new System.Drawing.Point(326, 3);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(84, 26);
            this.buttonSaveAndClose.TabIndex = 0;
            this.buttonSaveAndClose.Text = "Speichern";
            this.buttonSaveAndClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // FormPreferences
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(422, 251);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormPreferences";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Einstellungen";
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tableLayoutPanel.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ImageList imageListTabPages;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxSaveUser;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxServerAddr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.CheckBox checkBoxPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button buttonShowID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxNewID;
        private System.Windows.Forms.TextBox textBoxOldID;
        private System.Windows.Forms.Button buttonSendID;
    }
}
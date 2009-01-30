namespace StoreClient
{
    partial class ucUser
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxAllUsers = new System.Windows.Forms.GroupBox();
            this.listBoxUsers = new System.Windows.Forms.ListBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.groupBoxGroupsRights = new System.Windows.Forms.GroupBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBoxAds = new System.Windows.Forms.CheckBox();
            this.checkBoxProducts = new System.Windows.Forms.CheckBox();
            this.checkBoxUser = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.groupBoxSingleUser = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxNewPW = new System.Windows.Forms.TextBox();
            this.textBoxNewPWagain = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxGroup = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxOldPW = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonSaveUser = new System.Windows.Forms.Button();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.groupBoxAllUsers.SuspendLayout();
            this.groupBoxGroupsRights.SuspendLayout();
            this.groupBoxSingleUser.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxAllUsers
            // 
            this.groupBoxAllUsers.Controls.Add(this.listBoxUsers);
            this.groupBoxAllUsers.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBoxAllUsers.Location = new System.Drawing.Point(0, 0);
            this.groupBoxAllUsers.Name = "groupBoxAllUsers";
            this.groupBoxAllUsers.Size = new System.Drawing.Size(251, 455);
            this.groupBoxAllUsers.TabIndex = 0;
            this.groupBoxAllUsers.TabStop = false;
            this.groupBoxAllUsers.Text = "Aktuell registrierte Benutzer";
            // 
            // listBoxUsers
            // 
            this.listBoxUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxUsers.FormattingEnabled = true;
            this.listBoxUsers.Location = new System.Drawing.Point(3, 16);
            this.listBoxUsers.Name = "listBoxUsers";
            this.listBoxUsers.Size = new System.Drawing.Size(245, 433);
            this.listBoxUsers.TabIndex = 0;
            this.listBoxUsers.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(251, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 455);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // listBox2
            // 
            this.listBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(3, 16);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(194, 160);
            this.listBox2.TabIndex = 0;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // groupBoxGroupsRights
            // 
            this.groupBoxGroupsRights.Controls.Add(this.textBox5);
            this.groupBoxGroupsRights.Controls.Add(this.label7);
            this.groupBoxGroupsRights.Controls.Add(this.button1);
            this.groupBoxGroupsRights.Controls.Add(this.checkBoxAds);
            this.groupBoxGroupsRights.Controls.Add(this.checkBoxProducts);
            this.groupBoxGroupsRights.Controls.Add(this.checkBoxUser);
            this.groupBoxGroupsRights.Controls.Add(this.label6);
            this.groupBoxGroupsRights.Controls.Add(this.listBox2);
            this.groupBoxGroupsRights.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxGroupsRights.Location = new System.Drawing.Point(564, 0);
            this.groupBoxGroupsRights.Name = "groupBoxGroupsRights";
            this.groupBoxGroupsRights.Size = new System.Drawing.Size(200, 455);
            this.groupBoxGroupsRights.TabIndex = 6;
            this.groupBoxGroupsRights.TabStop = false;
            this.groupBoxGroupsRights.Text = "Gruppen und Rechte";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(50, 271);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(144, 20);
            this.textBox5.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 271);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Name:";
            // 
            // checkBoxAds
            // 
            this.checkBoxAds.AutoSize = true;
            this.checkBoxAds.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxAds.Location = new System.Drawing.Point(3, 244);
            this.checkBoxAds.Name = "checkBoxAds";
            this.checkBoxAds.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.checkBoxAds.Size = new System.Drawing.Size(194, 20);
            this.checkBoxAds.TabIndex = 4;
            this.checkBoxAds.Text = "Werbeverwaltung";
            this.checkBoxAds.UseVisualStyleBackColor = true;
            // 
            // checkBoxProducts
            // 
            this.checkBoxProducts.AutoSize = true;
            this.checkBoxProducts.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxProducts.Location = new System.Drawing.Point(3, 224);
            this.checkBoxProducts.Name = "checkBoxProducts";
            this.checkBoxProducts.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.checkBoxProducts.Size = new System.Drawing.Size(194, 20);
            this.checkBoxProducts.TabIndex = 3;
            this.checkBoxProducts.Text = "Produktverwaltung";
            this.checkBoxProducts.UseVisualStyleBackColor = true;
            // 
            // checkBoxUser
            // 
            this.checkBoxUser.AutoSize = true;
            this.checkBoxUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxUser.Location = new System.Drawing.Point(3, 204);
            this.checkBoxUser.Name = "checkBoxUser";
            this.checkBoxUser.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.checkBoxUser.Size = new System.Drawing.Size(194, 20);
            this.checkBoxUser.TabIndex = 1;
            this.checkBoxUser.Text = "Benutzerverwaltung";
            this.checkBoxUser.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(3, 176);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 10, 0, 5);
            this.label6.Size = new System.Drawing.Size(42, 28);
            this.label6.TabIndex = 2;
            this.label6.Text = "Rechte";
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(561, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 455);
            this.splitter2.TabIndex = 7;
            this.splitter2.TabStop = false;
            // 
            // groupBoxSingleUser
            // 
            this.groupBoxSingleUser.Controls.Add(this.tableLayoutPanel1);
            this.groupBoxSingleUser.Controls.Add(this.toolStrip1);
            this.groupBoxSingleUser.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBoxSingleUser.Location = new System.Drawing.Point(254, 0);
            this.groupBoxSingleUser.Name = "groupBoxSingleUser";
            this.groupBoxSingleUser.Size = new System.Drawing.Size(307, 455);
            this.groupBoxSingleUser.TabIndex = 2;
            this.groupBoxSingleUser.TabStop = false;
            this.groupBoxSingleUser.Text = "Ausgewählter Benutzer";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.textBoxName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxNewPW, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxNewPWagain, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxGroup, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.textBoxOldPW, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.buttonSaveUser, 1, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Enabled = false;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 41);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(301, 411);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // textBoxName
            // 
            this.textBoxName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxName.Location = new System.Drawing.Point(123, 3);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(175, 20);
            this.textBoxName.TabIndex = 0;
            this.textBoxName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxOldPW_KeyPress);
            // 
            // textBoxNewPW
            // 
            this.textBoxNewPW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxNewPW.Location = new System.Drawing.Point(123, 35);
            this.textBoxNewPW.Name = "textBoxNewPW";
            this.textBoxNewPW.Size = new System.Drawing.Size(175, 20);
            this.textBoxNewPW.TabIndex = 1;
            this.textBoxNewPW.UseSystemPasswordChar = true;
            this.textBoxNewPW.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxOldPW_KeyPress);
            // 
            // textBoxNewPWagain
            // 
            this.textBoxNewPWagain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxNewPWagain.Location = new System.Drawing.Point(123, 67);
            this.textBoxNewPWagain.Name = "textBoxNewPWagain";
            this.textBoxNewPWagain.Size = new System.Drawing.Size(175, 20);
            this.textBoxNewPWagain.TabIndex = 2;
            this.textBoxNewPWagain.UseSystemPasswordChar = true;
            this.textBoxNewPWagain.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxOldPW_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 27);
            this.label1.TabIndex = 4;
            this.label1.Text = "Benutzername";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 27);
            this.label2.TabIndex = 5;
            this.label2.Text = "Neues Passwort";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 69);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 27);
            this.label3.TabIndex = 6;
            this.label3.Text = "Passwort wiederholen";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 101);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 27);
            this.label4.TabIndex = 7;
            this.label4.Text = "Gruppe";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // comboBoxGroup
            // 
            this.comboBoxGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGroup.FormattingEnabled = true;
            this.comboBoxGroup.Location = new System.Drawing.Point(123, 99);
            this.comboBoxGroup.Name = "comboBoxGroup";
            this.comboBoxGroup.Size = new System.Drawing.Size(175, 21);
            this.comboBoxGroup.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 133);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 27);
            this.label5.TabIndex = 9;
            this.label5.Text = "Altes Passwort";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxOldPW
            // 
            this.textBoxOldPW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxOldPW.Location = new System.Drawing.Point(123, 131);
            this.textBoxOldPW.Name = "textBoxOldPW";
            this.textBoxOldPW.Size = new System.Drawing.Size(175, 20);
            this.textBoxOldPW.TabIndex = 10;
            this.textBoxOldPW.UseSystemPasswordChar = true;
            this.textBoxOldPW.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxOldPW_KeyPress);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonRefresh,
            this.toolStripSeparator2,
            this.toolStripButtonNew,
            this.toolStripButtonEdit,
            this.toolStripButtonDelete,
            this.toolStripSeparator1,
            this.toolStripButtonSave});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(301, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // button1
            // 
            this.button1.Image = global::StoreClient.Properties.Resources.page_save;
            this.button1.Location = new System.Drawing.Point(3, 308);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 26);
            this.button1.TabIndex = 5;
            this.button1.Text = "Speichern";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonSaveUser
            // 
            this.buttonSaveUser.Image = global::StoreClient.Properties.Resources.page_save;
            this.buttonSaveUser.Location = new System.Drawing.Point(123, 163);
            this.buttonSaveUser.Name = "buttonSaveUser";
            this.buttonSaveUser.Size = new System.Drawing.Size(102, 26);
            this.buttonSaveUser.TabIndex = 11;
            this.buttonSaveUser.Text = "Speichern";
            this.buttonSaveUser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonSaveUser.UseVisualStyleBackColor = true;
            this.buttonSaveUser.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRefresh.Image = global::StoreClient.Properties.Resources.arrow_refresh;
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRefresh.Text = "toolStripButton1";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.refreshContent);
            // 
            // toolStripButtonNew
            // 
            this.toolStripButtonNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNew.Image = global::StoreClient.Properties.Resources.page_add;
            this.toolStripButtonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNew.Name = "toolStripButtonNew";
            this.toolStripButtonNew.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonNew.Text = "toolStripButton1";
            this.toolStripButtonNew.Click += new System.EventHandler(this.toolStripButtonNew_Click);
            // 
            // toolStripButtonEdit
            // 
            this.toolStripButtonEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonEdit.Enabled = false;
            this.toolStripButtonEdit.Image = global::StoreClient.Properties.Resources.page_edit;
            this.toolStripButtonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEdit.Name = "toolStripButtonEdit";
            this.toolStripButtonEdit.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonEdit.Text = "toolStripButton2";
            this.toolStripButtonEdit.Click += new System.EventHandler(this.toolStripButtonEdit_Click);
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDelete.Enabled = false;
            this.toolStripButtonDelete.Image = global::StoreClient.Properties.Resources.page_delete;
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonDelete.Text = "toolStripButton3";
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSave.Enabled = false;
            this.toolStripButtonSave.Image = global::StoreClient.Properties.Resources.page_save;
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSave.Text = "toolStripButton4";
            this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // ucUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxGroupsRights);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.groupBoxSingleUser);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.groupBoxAllUsers);
            this.Name = "ucUser";
            this.Size = new System.Drawing.Size(764, 455);
            this.groupBoxAllUsers.ResumeLayout(false);
            this.groupBoxGroupsRights.ResumeLayout(false);
            this.groupBoxGroupsRights.PerformLayout();
            this.groupBoxSingleUser.ResumeLayout(false);
            this.groupBoxSingleUser.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxAllUsers;
        private System.Windows.Forms.ListBox listBoxUsers;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.GroupBox groupBoxGroupsRights;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.CheckBox checkBoxAds;
        private System.Windows.Forms.CheckBox checkBoxProducts;
        private System.Windows.Forms.CheckBox checkBoxUser;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBoxSingleUser;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxNewPW;
        private System.Windows.Forms.TextBox textBoxNewPWagain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxGroup;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxOldPW;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonNew;
        private System.Windows.Forms.ToolStripButton toolStripButtonEdit;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonSaveUser;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

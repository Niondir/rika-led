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
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.groupBoxGroupsRights = new System.Windows.Forms.GroupBox();
            this.textBoxGroupName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBoxAds = new System.Windows.Forms.CheckBox();
            this.checkBoxProducts = new System.Windows.Forms.CheckBox();
            this.checkBoxUser = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
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
            this.buttonSaveUser = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBoxNewGroup = new System.Windows.Forms.GroupBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonRefresh2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonNewGroup = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEditGroup = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDeleteGroup = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSaveGroup = new System.Windows.Forms.ToolStripButton();
            this.buttonSaveGroup = new System.Windows.Forms.Button();
            this.checkBoxRegions = new System.Windows.Forms.CheckBox();
            this.checkBoxStats = new System.Windows.Forms.CheckBox();
            this.checkBoxNetwork = new System.Windows.Forms.CheckBox();
            this.groupBoxAllUsers.SuspendLayout();
            this.groupBoxGroupsRights.SuspendLayout();
            this.groupBoxSingleUser.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBoxNewGroup.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxAllUsers
            // 
            this.groupBoxAllUsers.Controls.Add(this.listBoxUsers);
            this.groupBoxAllUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxAllUsers.Location = new System.Drawing.Point(0, 0);
            this.groupBoxAllUsers.Name = "groupBoxAllUsers";
            this.groupBoxAllUsers.Size = new System.Drawing.Size(466, 220);
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
            this.listBoxUsers.Size = new System.Drawing.Size(460, 199);
            this.listBoxUsers.TabIndex = 0;
            this.listBoxUsers.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // listBox2
            // 
            this.listBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(3, 16);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(288, 199);
            this.listBox2.TabIndex = 0;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // groupBoxGroupsRights
            // 
            this.groupBoxGroupsRights.Controls.Add(this.listBox2);
            this.groupBoxGroupsRights.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxGroupsRights.Location = new System.Drawing.Point(0, 0);
            this.groupBoxGroupsRights.Name = "groupBoxGroupsRights";
            this.groupBoxGroupsRights.Size = new System.Drawing.Size(294, 220);
            this.groupBoxGroupsRights.TabIndex = 6;
            this.groupBoxGroupsRights.TabStop = false;
            this.groupBoxGroupsRights.Text = "Gruppen und Rechte";
            // 
            // textBoxGroupName
            // 
            this.textBoxGroupName.Enabled = false;
            this.textBoxGroupName.Location = new System.Drawing.Point(94, 177);
            this.textBoxGroupName.Name = "textBoxGroupName";
            this.textBoxGroupName.Size = new System.Drawing.Size(144, 20);
            this.textBoxGroupName.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Enabled = false;
            this.label7.Location = new System.Drawing.Point(53, 182);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Name";
            // 
            // checkBoxAds
            // 
            this.checkBoxAds.AutoSize = true;
            this.checkBoxAds.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxAds.Enabled = false;
            this.checkBoxAds.Location = new System.Drawing.Point(16, 123);
            this.checkBoxAds.Name = "checkBoxAds";
            this.checkBoxAds.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.checkBoxAds.Size = new System.Drawing.Size(110, 20);
            this.checkBoxAds.TabIndex = 4;
            this.checkBoxAds.Text = "Werbeverwaltung";
            this.checkBoxAds.UseVisualStyleBackColor = true;
            // 
            // checkBoxProducts
            // 
            this.checkBoxProducts.AutoSize = true;
            this.checkBoxProducts.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxProducts.Enabled = false;
            this.checkBoxProducts.Location = new System.Drawing.Point(11, 97);
            this.checkBoxProducts.Name = "checkBoxProducts";
            this.checkBoxProducts.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.checkBoxProducts.Size = new System.Drawing.Size(115, 20);
            this.checkBoxProducts.TabIndex = 3;
            this.checkBoxProducts.Text = "Produktverwaltung";
            this.checkBoxProducts.UseVisualStyleBackColor = true;
            // 
            // checkBoxUser
            // 
            this.checkBoxUser.AutoSize = true;
            this.checkBoxUser.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxUser.Enabled = false;
            this.checkBoxUser.Location = new System.Drawing.Point(6, 71);
            this.checkBoxUser.Name = "checkBoxUser";
            this.checkBoxUser.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.checkBoxUser.Size = new System.Drawing.Size(120, 20);
            this.checkBoxUser.TabIndex = 1;
            this.checkBoxUser.Text = "Benutzerverwaltung";
            this.checkBoxUser.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Enabled = false;
            this.label6.Location = new System.Drawing.Point(6, 43);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 10, 0, 5);
            this.label6.Size = new System.Drawing.Size(42, 28);
            this.label6.TabIndex = 2;
            this.label6.Text = "Rechte";
            // 
            // groupBoxSingleUser
            // 
            this.groupBoxSingleUser.Controls.Add(this.tableLayoutPanel1);
            this.groupBoxSingleUser.Controls.Add(this.toolStrip1);
            this.groupBoxSingleUser.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxSingleUser.Location = new System.Drawing.Point(0, 220);
            this.groupBoxSingleUser.Name = "groupBoxSingleUser";
            this.groupBoxSingleUser.Size = new System.Drawing.Size(466, 235);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(460, 191);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // textBoxName
            // 
            this.textBoxName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxName.Location = new System.Drawing.Point(123, 3);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(334, 20);
            this.textBoxName.TabIndex = 0;
            this.textBoxName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxOldPW_KeyPress);
            // 
            // textBoxNewPW
            // 
            this.textBoxNewPW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxNewPW.Location = new System.Drawing.Point(123, 35);
            this.textBoxNewPW.Name = "textBoxNewPW";
            this.textBoxNewPW.Size = new System.Drawing.Size(334, 20);
            this.textBoxNewPW.TabIndex = 1;
            this.textBoxNewPW.UseSystemPasswordChar = true;
            this.textBoxNewPW.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxOldPW_KeyPress);
            // 
            // textBoxNewPWagain
            // 
            this.textBoxNewPWagain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxNewPWagain.Location = new System.Drawing.Point(123, 67);
            this.textBoxNewPWagain.Name = "textBoxNewPWagain";
            this.textBoxNewPWagain.Size = new System.Drawing.Size(334, 20);
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
            this.comboBoxGroup.Size = new System.Drawing.Size(334, 21);
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
            this.textBoxOldPW.Size = new System.Drawing.Size(334, 20);
            this.textBoxOldPW.TabIndex = 10;
            this.textBoxOldPW.UseSystemPasswordChar = true;
            this.textBoxOldPW.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxOldPW_KeyPress);
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
            this.toolStrip1.Size = new System.Drawing.Size(460, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRefresh.Image = global::StoreClient.Properties.Resources.arrow_refresh;
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRefresh.Text = "toolStripButton1";
            this.toolStripButtonRefresh.ToolTipText = "Aktualisieren";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.refreshContent);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonNew
            // 
            this.toolStripButtonNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNew.Image = global::StoreClient.Properties.Resources.page_add;
            this.toolStripButtonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNew.Name = "toolStripButtonNew";
            this.toolStripButtonNew.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonNew.Text = "toolStripButton1";
            this.toolStripButtonNew.ToolTipText = "Neuen Benutzer anlegen";
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
            this.toolStripButtonEdit.ToolTipText = "Ausgewählten Benutzer bearbeiten";
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
            this.toolStripButtonDelete.ToolTipText = "Ausgewählten Benutzer löschen";
            this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
            this.toolStripButtonSave.ToolTipText = "Benutzerdaten speichern";
            this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxAllUsers);
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxSingleUser);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBoxGroupsRights);
            this.splitContainer1.Panel2.Controls.Add(this.groupBoxNewGroup);
            this.splitContainer1.Size = new System.Drawing.Size(764, 455);
            this.splitContainer1.SplitterDistance = 466;
            this.splitContainer1.TabIndex = 7;
            // 
            // groupBoxNewGroup
            // 
            this.groupBoxNewGroup.Controls.Add(this.checkBoxNetwork);
            this.groupBoxNewGroup.Controls.Add(this.checkBoxStats);
            this.groupBoxNewGroup.Controls.Add(this.checkBoxRegions);
            this.groupBoxNewGroup.Controls.Add(this.toolStrip2);
            this.groupBoxNewGroup.Controls.Add(this.textBoxGroupName);
            this.groupBoxNewGroup.Controls.Add(this.label6);
            this.groupBoxNewGroup.Controls.Add(this.label7);
            this.groupBoxNewGroup.Controls.Add(this.checkBoxUser);
            this.groupBoxNewGroup.Controls.Add(this.buttonSaveGroup);
            this.groupBoxNewGroup.Controls.Add(this.checkBoxProducts);
            this.groupBoxNewGroup.Controls.Add(this.checkBoxAds);
            this.groupBoxNewGroup.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxNewGroup.Location = new System.Drawing.Point(0, 220);
            this.groupBoxNewGroup.Name = "groupBoxNewGroup";
            this.groupBoxNewGroup.Size = new System.Drawing.Size(294, 235);
            this.groupBoxNewGroup.TabIndex = 7;
            this.groupBoxNewGroup.TabStop = false;
            this.groupBoxNewGroup.Text = "Ausgewählte Gruppe";
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonRefresh2,
            this.toolStripSeparator3,
            this.toolStripButtonNewGroup,
            this.toolStripButtonEditGroup,
            this.toolStripButtonDeleteGroup,
            this.toolStripSeparator4,
            this.toolStripButtonSaveGroup});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip2.Location = new System.Drawing.Point(3, 16);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(288, 25);
            this.toolStrip2.TabIndex = 8;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButtonRefresh2
            // 
            this.toolStripButtonRefresh2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRefresh2.Image = global::StoreClient.Properties.Resources.arrow_refresh;
            this.toolStripButtonRefresh2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh2.Name = "toolStripButtonRefresh2";
            this.toolStripButtonRefresh2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRefresh2.Text = "toolStripButton1";
            this.toolStripButtonRefresh2.ToolTipText = "Aktualisieren";
            this.toolStripButtonRefresh2.Click += new System.EventHandler(this.refreshContent);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonNewGroup
            // 
            this.toolStripButtonNewGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNewGroup.Image = global::StoreClient.Properties.Resources.page_add;
            this.toolStripButtonNewGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNewGroup.Name = "toolStripButtonNewGroup";
            this.toolStripButtonNewGroup.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonNewGroup.Text = "toolStripButton1";
            this.toolStripButtonNewGroup.ToolTipText = "Neue Gruppe hinzufügen";
            this.toolStripButtonNewGroup.Click += new System.EventHandler(this.toolStripButtonNewGroup_Click);
            // 
            // toolStripButtonEditGroup
            // 
            this.toolStripButtonEditGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonEditGroup.Enabled = false;
            this.toolStripButtonEditGroup.Image = global::StoreClient.Properties.Resources.page_edit;
            this.toolStripButtonEditGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEditGroup.Name = "toolStripButtonEditGroup";
            this.toolStripButtonEditGroup.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonEditGroup.Text = "toolStripButton2";
            this.toolStripButtonEditGroup.ToolTipText = "Ausgewählte Gruppe bearbeiten";
            this.toolStripButtonEditGroup.Click += new System.EventHandler(this.toolStripButtonEditGroup_Click);
            // 
            // toolStripButtonDeleteGroup
            // 
            this.toolStripButtonDeleteGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDeleteGroup.Enabled = false;
            this.toolStripButtonDeleteGroup.Image = global::StoreClient.Properties.Resources.page_delete;
            this.toolStripButtonDeleteGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDeleteGroup.Name = "toolStripButtonDeleteGroup";
            this.toolStripButtonDeleteGroup.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonDeleteGroup.Text = "toolStripButton3";
            this.toolStripButtonDeleteGroup.ToolTipText = "Ausgewählte Gruppe löschen";
            this.toolStripButtonDeleteGroup.Click += new System.EventHandler(this.toolStripButtonDeleteGroup_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonSaveGroup
            // 
            this.toolStripButtonSaveGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSaveGroup.Enabled = false;
            this.toolStripButtonSaveGroup.Image = global::StoreClient.Properties.Resources.page_save;
            this.toolStripButtonSaveGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveGroup.Name = "toolStripButtonSaveGroup";
            this.toolStripButtonSaveGroup.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSaveGroup.Text = "toolStripButton4";
            this.toolStripButtonSaveGroup.ToolTipText = "Gruppendaten speichern";
            this.toolStripButtonSaveGroup.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonSaveGroup
            // 
            this.buttonSaveGroup.Enabled = false;
            this.buttonSaveGroup.Image = global::StoreClient.Properties.Resources.page_save;
            this.buttonSaveGroup.Location = new System.Drawing.Point(94, 203);
            this.buttonSaveGroup.Name = "buttonSaveGroup";
            this.buttonSaveGroup.Size = new System.Drawing.Size(102, 26);
            this.buttonSaveGroup.TabIndex = 5;
            this.buttonSaveGroup.Text = "Speichern";
            this.buttonSaveGroup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonSaveGroup.UseVisualStyleBackColor = true;
            this.buttonSaveGroup.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBoxRegions
            // 
            this.checkBoxRegions.AutoSize = true;
            this.checkBoxRegions.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxRegions.Location = new System.Drawing.Point(132, 74);
            this.checkBoxRegions.Name = "checkBoxRegions";
            this.checkBoxRegions.Size = new System.Drawing.Size(125, 17);
            this.checkBoxRegions.TabIndex = 9;
            this.checkBoxRegions.Text = "Regionen bearbeiten";
            this.checkBoxRegions.UseVisualStyleBackColor = true;
            // 
            // checkBoxStats
            // 
            this.checkBoxStats.AutoSize = true;
            this.checkBoxStats.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxStats.Location = new System.Drawing.Point(182, 100);
            this.checkBoxStats.Name = "checkBoxStats";
            this.checkBoxStats.Size = new System.Drawing.Size(75, 17);
            this.checkBoxStats.TabIndex = 11;
            this.checkBoxStats.Text = "Statistiken";
            this.checkBoxStats.UseVisualStyleBackColor = true;
            // 
            // checkBoxNetwork
            // 
            this.checkBoxNetwork.AutoSize = true;
            this.checkBoxNetwork.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxNetwork.Location = new System.Drawing.Point(199, 125);
            this.checkBoxNetwork.Name = "checkBoxNetwork";
            this.checkBoxNetwork.Size = new System.Drawing.Size(58, 17);
            this.checkBoxNetwork.TabIndex = 12;
            this.checkBoxNetwork.Text = "Geräte";
            this.checkBoxNetwork.UseVisualStyleBackColor = true;
            // 
            // ucUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucUser";
            this.Size = new System.Drawing.Size(764, 455);
            this.groupBoxAllUsers.ResumeLayout(false);
            this.groupBoxGroupsRights.ResumeLayout(false);
            this.groupBoxSingleUser.ResumeLayout(false);
            this.groupBoxSingleUser.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBoxNewGroup.ResumeLayout(false);
            this.groupBoxNewGroup.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxAllUsers;
        private System.Windows.Forms.ListBox listBoxUsers;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.GroupBox groupBoxGroupsRights;
        private System.Windows.Forms.CheckBox checkBoxAds;
        private System.Windows.Forms.CheckBox checkBoxProducts;
        private System.Windows.Forms.CheckBox checkBoxUser;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonSaveGroup;
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
        private System.Windows.Forms.TextBox textBoxGroupName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonSaveUser;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBoxNewGroup;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonNewGroup;
        private System.Windows.Forms.ToolStripButton toolStripButtonEditGroup;
        private System.Windows.Forms.ToolStripButton toolStripButtonDeleteGroup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveGroup;
        private System.Windows.Forms.CheckBox checkBoxRegions;
        private System.Windows.Forms.CheckBox checkBoxNetwork;
        private System.Windows.Forms.CheckBox checkBoxStats;
    }
}

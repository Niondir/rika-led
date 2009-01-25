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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.groupBoxSingleUser = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.groupBoxGroupsRights = new System.Windows.Forms.GroupBox();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.checkBoxUser = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxProducts = new System.Windows.Forms.CheckBox();
            this.checkBoxAds = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBoxAllUsers.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBoxSingleUser.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBoxGroupsRights.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxAllUsers
            // 
            this.groupBoxAllUsers.Controls.Add(this.listBox1);
            this.groupBoxAllUsers.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBoxAllUsers.Location = new System.Drawing.Point(0, 0);
            this.groupBoxAllUsers.Name = "groupBoxAllUsers";
            this.groupBoxAllUsers.Size = new System.Drawing.Size(251, 455);
            this.groupBoxAllUsers.TabIndex = 0;
            this.groupBoxAllUsers.TabStop = false;
            this.groupBoxAllUsers.Text = "Aktuell registrierte Benutzer";
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(3, 16);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(245, 433);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonNew,
            this.toolStripButtonEdit,
            this.toolStripButtonDelete,
            this.toolStripSeparator1,
            this.toolStripButtonSave});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(307, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonNew
            // 
            this.toolStripButtonNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNew.Image = global::StoreClient.Properties.Resources.page_add;
            this.toolStripButtonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNew.Name = "toolStripButtonNew";
            this.toolStripButtonNew.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonNew.Text = "toolStripButton1";
            // 
            // toolStripButtonEdit
            // 
            this.toolStripButtonEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
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
            this.toolStripButtonDelete.Image = global::StoreClient.Properties.Resources.page_delete;
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonDelete.Text = "toolStripButton3";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSave.Image = global::StoreClient.Properties.Resources.page_save;
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSave.Text = "toolStripButton4";
            // 
            // groupBoxSingleUser
            // 
            this.groupBoxSingleUser.Controls.Add(this.tableLayoutPanel1);
            this.groupBoxSingleUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSingleUser.Enabled = false;
            this.groupBoxSingleUser.Location = new System.Drawing.Point(0, 25);
            this.groupBoxSingleUser.Name = "groupBoxSingleUser";
            this.groupBoxSingleUser.Size = new System.Drawing.Size(307, 430);
            this.groupBoxSingleUser.TabIndex = 2;
            this.groupBoxSingleUser.TabStop = false;
            this.groupBoxSingleUser.Text = "Ausgewählter Benutzer";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.comboBox1, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.textBox4, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
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
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(123, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(175, 20);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Location = new System.Drawing.Point(123, 35);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(175, 20);
            this.textBox2.TabIndex = 1;
            // 
            // textBox3
            // 
            this.textBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox3.Location = new System.Drawing.Point(123, 67);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(175, 20);
            this.textBox3.TabIndex = 2;
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
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(123, 99);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(175, 21);
            this.comboBox1.TabIndex = 8;
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
            // textBox4
            // 
            this.textBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox4.Location = new System.Drawing.Point(123, 131);
            this.textBox4.Name = "textBox4";
            this.textBox4.PasswordChar = 'o';
            this.textBox4.Size = new System.Drawing.Size(175, 20);
            this.textBox4.TabIndex = 10;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(251, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 455);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBoxSingleUser);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(254, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(307, 455);
            this.panel1.TabIndex = 4;
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
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(561, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 455);
            this.splitter2.TabIndex = 7;
            this.splitter2.TabStop = false;
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
            // button1
            // 
            this.button1.Image = global::StoreClient.Properties.Resources.page_save;
            this.button1.Location = new System.Drawing.Point(3, 277);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 26);
            this.button1.TabIndex = 5;
            this.button1.Text = "Speichern";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ucUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxGroupsRights);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.groupBoxAllUsers);
            this.Name = "ucUser";
            this.Size = new System.Drawing.Size(764, 455);
            this.groupBoxAllUsers.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBoxSingleUser.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBoxGroupsRights.ResumeLayout(false);
            this.groupBoxGroupsRights.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxAllUsers;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonNew;
        private System.Windows.Forms.ToolStripButton toolStripButtonEdit;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox groupBoxSingleUser;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.GroupBox groupBoxGroupsRights;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.CheckBox checkBoxAds;
        private System.Windows.Forms.CheckBox checkBoxProducts;
        private System.Windows.Forms.CheckBox checkBoxUser;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
    }
}

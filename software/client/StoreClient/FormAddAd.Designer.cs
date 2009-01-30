namespace StoreClient
{
    partial class FormAddAd
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
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.dateTimePickerStartDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStopDate = new System.Windows.Forms.DateTimePicker();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelFrom = new System.Windows.Forms.Label();
            this.labelTo = new System.Windows.Forms.Label();
            this.labelStartDate = new System.Windows.Forms.Label();
            this.labelStopDate = new System.Windows.Forms.Label();
            this.labelCaption = new System.Windows.Forms.Label();
            this.dateTimePickerStartTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStopTime = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxAdLine4 = new System.Windows.Forms.TextBox();
            this.textBoxAdLine3 = new System.Windows.Forms.TextBox();
            this.textBoxAdLine2 = new System.Windows.Forms.TextBox();
            this.textBoxAdLine1 = new System.Windows.Forms.TextBox();
            this.groupBoxAppearance = new System.Windows.Forms.GroupBox();
            this.groupBoxInfos = new System.Windows.Forms.GroupBox();
            this.comboBoxGroup = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBoxAppearance.SuspendLayout();
            this.groupBoxInfos.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(81, 19);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(266, 20);
            this.textBoxName.TabIndex = 0;
            // 
            // dateTimePickerStartDate
            // 
            this.dateTimePickerStartDate.Location = new System.Drawing.Point(42, 76);
            this.dateTimePickerStartDate.Name = "dateTimePickerStartDate";
            this.dateTimePickerStartDate.Size = new System.Drawing.Size(305, 20);
            this.dateTimePickerStartDate.TabIndex = 4;
            this.dateTimePickerStartDate.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // dateTimePickerStopDate
            // 
            this.dateTimePickerStopDate.Location = new System.Drawing.Point(42, 103);
            this.dateTimePickerStopDate.Name = "dateTimePickerStopDate";
            this.dateTimePickerStopDate.Size = new System.Drawing.Size(305, 20);
            this.dateTimePickerStopDate.TabIndex = 5;
            // 
            // buttonOK
            // 
            this.buttonOK.Image = global::StoreClient.Properties.Resources.page_save;
            this.buttonOK.Location = new System.Drawing.Point(248, 477);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(124, 26);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "Speichern";
            this.buttonOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Image = global::StoreClient.Properties.Resources.cancel;
            this.buttonCancel.Location = new System.Drawing.Point(12, 477);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(116, 26);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Abbrechen";
            this.buttonCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(6, 150);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(102, 13);
            this.labelFrom.TabIndex = 9;
            this.labelFrom.Text = "In der Zeit zwischen";
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(216, 150);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(25, 13);
            this.labelTo.TabIndex = 11;
            this.labelTo.Text = "und";
            // 
            // labelStartDate
            // 
            this.labelStartDate.AutoSize = true;
            this.labelStartDate.Location = new System.Drawing.Point(8, 80);
            this.labelStartDate.Name = "labelStartDate";
            this.labelStartDate.Size = new System.Drawing.Size(25, 13);
            this.labelStartDate.TabIndex = 13;
            this.labelStartDate.Text = "von";
            // 
            // labelStopDate
            // 
            this.labelStopDate.AutoSize = true;
            this.labelStopDate.Location = new System.Drawing.Point(13, 107);
            this.labelStopDate.Name = "labelStopDate";
            this.labelStopDate.Size = new System.Drawing.Size(20, 13);
            this.labelStopDate.TabIndex = 14;
            this.labelStopDate.Text = "bis";
            // 
            // labelCaption
            // 
            this.labelCaption.AutoSize = true;
            this.labelCaption.Location = new System.Drawing.Point(6, 56);
            this.labelCaption.Name = "labelCaption";
            this.labelCaption.Size = new System.Drawing.Size(217, 13);
            this.labelCaption.TabIndex = 15;
            this.labelCaption.Text = "Werbeausstrahlung findet statt im Zeitraum...";
            // 
            // dateTimePickerStartTime
            // 
            this.dateTimePickerStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerStartTime.Location = new System.Drawing.Point(114, 145);
            this.dateTimePickerStartTime.Name = "dateTimePickerStartTime";
            this.dateTimePickerStartTime.ShowUpDown = true;
            this.dateTimePickerStartTime.Size = new System.Drawing.Size(95, 20);
            this.dateTimePickerStartTime.TabIndex = 16;
            // 
            // dateTimePickerStopTime
            // 
            this.dateTimePickerStopTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerStopTime.Location = new System.Drawing.Point(252, 145);
            this.dateTimePickerStopTime.Name = "dateTimePickerStopTime";
            this.dateTimePickerStopTime.ShowUpDown = true;
            this.dateTimePickerStopTime.Size = new System.Drawing.Size(95, 20);
            this.dateTimePickerStopTime.TabIndex = 17;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Black;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 297F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(341, 188);
            this.tableLayoutPanel1.TabIndex = 18;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.YellowGreen;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.textBoxAdLine4, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.textBoxAdLine3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.textBoxAdLine2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.textBoxAdLine1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(25, 27);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(291, 134);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // textBoxAdLine4
            // 
            this.textBoxAdLine4.BackColor = System.Drawing.Color.GreenYellow;
            this.textBoxAdLine4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxAdLine4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxAdLine4.Font = new System.Drawing.Font("Courier New", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAdLine4.Location = new System.Drawing.Point(3, 102);
            this.textBoxAdLine4.MaxLength = 20;
            this.textBoxAdLine4.Name = "textBoxAdLine4";
            this.textBoxAdLine4.Size = new System.Drawing.Size(285, 27);
            this.textBoxAdLine4.TabIndex = 3;
            // 
            // textBoxAdLine3
            // 
            this.textBoxAdLine3.BackColor = System.Drawing.Color.GreenYellow;
            this.textBoxAdLine3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxAdLine3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxAdLine3.Font = new System.Drawing.Font("Courier New", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAdLine3.Location = new System.Drawing.Point(3, 69);
            this.textBoxAdLine3.MaxLength = 20;
            this.textBoxAdLine3.Name = "textBoxAdLine3";
            this.textBoxAdLine3.Size = new System.Drawing.Size(285, 27);
            this.textBoxAdLine3.TabIndex = 2;
            // 
            // textBoxAdLine2
            // 
            this.textBoxAdLine2.BackColor = System.Drawing.Color.GreenYellow;
            this.textBoxAdLine2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxAdLine2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxAdLine2.Font = new System.Drawing.Font("Courier New", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAdLine2.Location = new System.Drawing.Point(3, 36);
            this.textBoxAdLine2.MaxLength = 20;
            this.textBoxAdLine2.Name = "textBoxAdLine2";
            this.textBoxAdLine2.Size = new System.Drawing.Size(285, 27);
            this.textBoxAdLine2.TabIndex = 1;
            // 
            // textBoxAdLine1
            // 
            this.textBoxAdLine1.BackColor = System.Drawing.Color.GreenYellow;
            this.textBoxAdLine1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxAdLine1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxAdLine1.Font = new System.Drawing.Font("Courier New", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAdLine1.Location = new System.Drawing.Point(3, 3);
            this.textBoxAdLine1.MaxLength = 20;
            this.textBoxAdLine1.Name = "textBoxAdLine1";
            this.textBoxAdLine1.Size = new System.Drawing.Size(285, 27);
            this.textBoxAdLine1.TabIndex = 0;
            // 
            // groupBoxAppearance
            // 
            this.groupBoxAppearance.Controls.Add(this.tableLayoutPanel1);
            this.groupBoxAppearance.Location = new System.Drawing.Point(12, 12);
            this.groupBoxAppearance.Name = "groupBoxAppearance";
            this.groupBoxAppearance.Size = new System.Drawing.Size(355, 224);
            this.groupBoxAppearance.TabIndex = 19;
            this.groupBoxAppearance.TabStop = false;
            this.groupBoxAppearance.Text = "Erscheinung";
            // 
            // groupBoxInfos
            // 
            this.groupBoxInfos.Controls.Add(this.comboBoxGroup);
            this.groupBoxInfos.Controls.Add(this.label2);
            this.groupBoxInfos.Controls.Add(this.label1);
            this.groupBoxInfos.Controls.Add(this.textBoxName);
            this.groupBoxInfos.Controls.Add(this.dateTimePickerStartDate);
            this.groupBoxInfos.Controls.Add(this.dateTimePickerStopTime);
            this.groupBoxInfos.Controls.Add(this.dateTimePickerStopDate);
            this.groupBoxInfos.Controls.Add(this.dateTimePickerStartTime);
            this.groupBoxInfos.Controls.Add(this.labelCaption);
            this.groupBoxInfos.Controls.Add(this.labelStopDate);
            this.groupBoxInfos.Controls.Add(this.labelStartDate);
            this.groupBoxInfos.Controls.Add(this.labelFrom);
            this.groupBoxInfos.Controls.Add(this.labelTo);
            this.groupBoxInfos.Location = new System.Drawing.Point(12, 242);
            this.groupBoxInfos.Name = "groupBoxInfos";
            this.groupBoxInfos.Size = new System.Drawing.Size(355, 229);
            this.groupBoxInfos.TabIndex = 20;
            this.groupBoxInfos.TabStop = false;
            this.groupBoxInfos.Text = "Zusatzinformationen";
            // 
            // comboBoxGroup
            // 
            this.comboBoxGroup.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxGroup.FormattingEnabled = true;
            this.comboBoxGroup.Location = new System.Drawing.Point(231, 190);
            this.comboBoxGroup.MaxDropDownItems = 20;
            this.comboBoxGroup.Name = "comboBoxGroup";
            this.comboBoxGroup.Size = new System.Drawing.Size(116, 21);
            this.comboBoxGroup.TabIndex = 20;
            this.comboBoxGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxGroup_SelectedIndexChanged);
            this.comboBoxGroup.Enter += new System.EventHandler(this.SetWhiteAgain);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(219, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Region, in der die Werbung ausgestrahlt wird";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Bezeichnung";
            // 
            // FormAddAd
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(384, 512);
            this.Controls.Add(this.groupBoxInfos);
            this.Controls.Add(this.groupBoxAppearance);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormAddAd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Werbung hinzufügen";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBoxAppearance.ResumeLayout(false);
            this.groupBoxInfos.ResumeLayout(false);
            this.groupBoxInfos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerStopDate;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.Label labelStartDate;
        private System.Windows.Forms.Label labelStopDate;
        private System.Windows.Forms.Label labelCaption;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerStopTime;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox textBoxAdLine1;
        private System.Windows.Forms.TextBox textBoxAdLine4;
        private System.Windows.Forms.TextBox textBoxAdLine3;
        private System.Windows.Forms.TextBox textBoxAdLine2;
        private System.Windows.Forms.GroupBox groupBoxAppearance;
        private System.Windows.Forms.GroupBox groupBoxInfos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxGroup;
    }
}
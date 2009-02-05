namespace StoreClient
{
    partial class ucAnalysisCharts
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
            this.components = new System.ComponentModel.Container();
            this.zedGraphControlMain = new ZedGraph.ZedGraphControl();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanelLower = new System.Windows.Forms.TableLayoutPanel();
            this.zedGraphPieView = new ZedGraph.ZedGraphControl();
            this.groupBoxRegions = new System.Windows.Forms.GroupBox();
            this.listBoxRegions = new System.Windows.Forms.ListBox();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.zedGraphControlFlee = new ZedGraph.ZedGraphControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxSummery = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.zedGraphPie = new ZedGraph.ZedGraphControl();
            this.groupBoxSetup = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBarYPrecision = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBarXPrecision = new System.Windows.Forms.TrackBar();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.tableLayoutPanelLower.SuspendLayout();
            this.groupBoxRegions.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBoxSummery.SuspendLayout();
            this.groupBoxSetup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarYPrecision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarXPrecision)).BeginInit();
            this.SuspendLayout();
            // 
            // zedGraphControlMain
            // 
            this.zedGraphControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControlMain.Location = new System.Drawing.Point(0, 0);
            this.zedGraphControlMain.Name = "zedGraphControlMain";
            this.zedGraphControlMain.ScrollGrace = 0;
            this.zedGraphControlMain.ScrollMaxX = 0;
            this.zedGraphControlMain.ScrollMaxY = 0;
            this.zedGraphControlMain.ScrollMaxY2 = 0;
            this.zedGraphControlMain.ScrollMinX = 0;
            this.zedGraphControlMain.ScrollMinY = 0;
            this.zedGraphControlMain.ScrollMinY2 = 0;
            this.zedGraphControlMain.Size = new System.Drawing.Size(929, 229);
            this.zedGraphControlMain.TabIndex = 0;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.zedGraphControlMain);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.tableLayoutPanelLower);
            this.splitContainerMain.Panel2.Controls.Add(this.panel1);
            this.splitContainerMain.Size = new System.Drawing.Size(929, 631);
            this.splitContainerMain.SplitterDistance = 229;
            this.splitContainerMain.TabIndex = 1;
            // 
            // tableLayoutPanelLower
            // 
            this.tableLayoutPanelLower.ColumnCount = 4;
            this.tableLayoutPanelLower.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelLower.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelLower.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelLower.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelLower.Controls.Add(this.zedGraphPieView, 0, 0);
            this.tableLayoutPanelLower.Controls.Add(this.groupBoxRegions, 3, 0);
            this.tableLayoutPanelLower.Controls.Add(this.zedGraphControl1, 1, 0);
            this.tableLayoutPanelLower.Controls.Add(this.zedGraphControlFlee, 2, 0);
            this.tableLayoutPanelLower.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelLower.Location = new System.Drawing.Point(0, 218);
            this.tableLayoutPanelLower.Name = "tableLayoutPanelLower";
            this.tableLayoutPanelLower.RowCount = 1;
            this.tableLayoutPanelLower.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelLower.Size = new System.Drawing.Size(929, 180);
            this.tableLayoutPanelLower.TabIndex = 4;
            // 
            // zedGraphPieView
            // 
            this.zedGraphPieView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphPieView.Location = new System.Drawing.Point(3, 3);
            this.zedGraphPieView.Name = "zedGraphPieView";
            this.zedGraphPieView.ScrollGrace = 0;
            this.zedGraphPieView.ScrollMaxX = 0;
            this.zedGraphPieView.ScrollMaxY = 0;
            this.zedGraphPieView.ScrollMaxY2 = 0;
            this.zedGraphPieView.ScrollMinX = 0;
            this.zedGraphPieView.ScrollMinY = 0;
            this.zedGraphPieView.ScrollMinY2 = 0;
            this.zedGraphPieView.Size = new System.Drawing.Size(226, 174);
            this.zedGraphPieView.TabIndex = 0;
            // 
            // groupBoxRegions
            // 
            this.groupBoxRegions.Controls.Add(this.listBoxRegions);
            this.groupBoxRegions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxRegions.Location = new System.Drawing.Point(699, 3);
            this.groupBoxRegions.Name = "groupBoxRegions";
            this.groupBoxRegions.Size = new System.Drawing.Size(227, 174);
            this.groupBoxRegions.TabIndex = 3;
            this.groupBoxRegions.TabStop = false;
            this.groupBoxRegions.Text = "Regionen";
            // 
            // listBoxRegions
            // 
            this.listBoxRegions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxRegions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxRegions.FormattingEnabled = true;
            this.listBoxRegions.ItemHeight = 16;
            this.listBoxRegions.Location = new System.Drawing.Point(3, 16);
            this.listBoxRegions.Name = "listBoxRegions";
            this.listBoxRegions.Size = new System.Drawing.Size(221, 148);
            this.listBoxRegions.TabIndex = 0;
            this.listBoxRegions.SelectedIndexChanged += new System.EventHandler(this.listBoxRegions_SelectedIndexChanged);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControl1.Location = new System.Drawing.Point(235, 3);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0;
            this.zedGraphControl1.ScrollMaxX = 0;
            this.zedGraphControl1.ScrollMaxY = 0;
            this.zedGraphControl1.ScrollMaxY2 = 0;
            this.zedGraphControl1.ScrollMinX = 0;
            this.zedGraphControl1.ScrollMinY = 0;
            this.zedGraphControl1.ScrollMinY2 = 0;
            this.zedGraphControl1.Size = new System.Drawing.Size(226, 174);
            this.zedGraphControl1.TabIndex = 1;
            // 
            // zedGraphControlFlee
            // 
            this.zedGraphControlFlee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControlFlee.Location = new System.Drawing.Point(467, 3);
            this.zedGraphControlFlee.Name = "zedGraphControlFlee";
            this.zedGraphControlFlee.ScrollGrace = 0;
            this.zedGraphControlFlee.ScrollMaxX = 0;
            this.zedGraphControlFlee.ScrollMaxY = 0;
            this.zedGraphControlFlee.ScrollMaxY2 = 0;
            this.zedGraphControlFlee.ScrollMinX = 0;
            this.zedGraphControlFlee.ScrollMinY = 0;
            this.zedGraphControlFlee.ScrollMinY2 = 0;
            this.zedGraphControlFlee.Size = new System.Drawing.Size(226, 174);
            this.zedGraphControlFlee.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBoxSummery);
            this.panel1.Controls.Add(this.zedGraphPie);
            this.panel1.Controls.Add(this.groupBoxSetup);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(929, 218);
            this.panel1.TabIndex = 5;
            // 
            // groupBoxSummery
            // 
            this.groupBoxSummery.Controls.Add(this.richTextBox1);
            this.groupBoxSummery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSummery.Location = new System.Drawing.Point(261, 0);
            this.groupBoxSummery.Name = "groupBoxSummery";
            this.groupBoxSummery.Padding = new System.Windows.Forms.Padding(5, 3, 5, 5);
            this.groupBoxSummery.Size = new System.Drawing.Size(318, 218);
            this.groupBoxSummery.TabIndex = 6;
            this.groupBoxSummery.TabStop = false;
            this.groupBoxSummery.Text = "Zusammenfassung";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(5, 16);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(308, 197);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // zedGraphPie
            // 
            this.zedGraphPie.Dock = System.Windows.Forms.DockStyle.Right;
            this.zedGraphPie.Location = new System.Drawing.Point(579, 0);
            this.zedGraphPie.Name = "zedGraphPie";
            this.zedGraphPie.ScrollGrace = 0;
            this.zedGraphPie.ScrollMaxX = 0;
            this.zedGraphPie.ScrollMaxY = 0;
            this.zedGraphPie.ScrollMaxY2 = 0;
            this.zedGraphPie.ScrollMinX = 0;
            this.zedGraphPie.ScrollMinY = 0;
            this.zedGraphPie.ScrollMinY2 = 0;
            this.zedGraphPie.Size = new System.Drawing.Size(350, 218);
            this.zedGraphPie.TabIndex = 5;
            // 
            // groupBoxSetup
            // 
            this.groupBoxSetup.Controls.Add(this.button1);
            this.groupBoxSetup.Controls.Add(this.label2);
            this.groupBoxSetup.Controls.Add(this.trackBarYPrecision);
            this.groupBoxSetup.Controls.Add(this.label1);
            this.groupBoxSetup.Controls.Add(this.trackBarXPrecision);
            this.groupBoxSetup.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBoxSetup.Location = new System.Drawing.Point(0, 0);
            this.groupBoxSetup.Name = "groupBoxSetup";
            this.groupBoxSetup.Padding = new System.Windows.Forms.Padding(5, 3, 5, 5);
            this.groupBoxSetup.Size = new System.Drawing.Size(261, 218);
            this.groupBoxSetup.TabIndex = 4;
            this.groupBoxSetup.TabStop = false;
            this.groupBoxSetup.Text = "Einstellungen";
            // 
            // button1
            // 
            this.button1.Image = global::StoreClient.Properties.Resources.arrow_refresh;
            this.button1.Location = new System.Drawing.Point(157, 108);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Aktualisieren";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(99, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 33);
            this.label2.TabIndex = 3;
            this.label2.Text = "Auflösung der Besucherachse";
            // 
            // trackBarYPrecision
            // 
            this.trackBarYPrecision.Location = new System.Drawing.Point(8, 62);
            this.trackBarYPrecision.Maximum = 4;
            this.trackBarYPrecision.Name = "trackBarYPrecision";
            this.trackBarYPrecision.Size = new System.Drawing.Size(85, 45);
            this.trackBarYPrecision.TabIndex = 2;
            this.trackBarYPrecision.Scroll += new System.EventHandler(this.trackBarYPrecision_Scroll);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(99, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 34);
            this.label1.TabIndex = 1;
            this.label1.Text = "Auflösung der Zeitachse";
            // 
            // trackBarXPrecision
            // 
            this.trackBarXPrecision.Location = new System.Drawing.Point(8, 16);
            this.trackBarXPrecision.Maximum = 4;
            this.trackBarXPrecision.Name = "trackBarXPrecision";
            this.trackBarXPrecision.Size = new System.Drawing.Size(85, 45);
            this.trackBarXPrecision.TabIndex = 0;
            this.trackBarXPrecision.Scroll += new System.EventHandler(this.trackBarXPrecision_Scroll);
            // 
            // ucAnalysisCharts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerMain);
            this.Name = "ucAnalysisCharts";
            this.Size = new System.Drawing.Size(929, 631);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.ResumeLayout(false);
            this.tableLayoutPanelLower.ResumeLayout(false);
            this.groupBoxRegions.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBoxSummery.ResumeLayout(false);
            this.groupBoxSetup.ResumeLayout(false);
            this.groupBoxSetup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarYPrecision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarXPrecision)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControlMain;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private ZedGraph.ZedGraphControl zedGraphPieView;
        private ZedGraph.ZedGraphControl zedGraphControlFlee;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.GroupBox groupBoxRegions;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelLower;
        private System.Windows.Forms.ListBox listBoxRegions;
        private System.Windows.Forms.Panel panel1;
        private ZedGraph.ZedGraphControl zedGraphPie;
        private System.Windows.Forms.GroupBox groupBoxSetup;
        private System.Windows.Forms.GroupBox groupBoxSummery;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TrackBar trackBarXPrecision;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBarYPrecision;
    }
}

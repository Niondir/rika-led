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
            this.zedGraphControl2 = new ZedGraph.ZedGraphControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.zedGraphCurrent = new ZedGraph.ZedGraphControl();
            this.groupBoxSummery = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.tableLayoutPanelLower.SuspendLayout();
            this.groupBoxRegions.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBoxSummery.SuspendLayout();
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
            this.zedGraphControlMain.Size = new System.Drawing.Size(900, 283);
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
            this.splitContainerMain.Size = new System.Drawing.Size(900, 512);
            this.splitContainerMain.SplitterDistance = 283;
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
            this.tableLayoutPanelLower.Controls.Add(this.zedGraphControl2, 2, 0);
            this.tableLayoutPanelLower.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelLower.Location = new System.Drawing.Point(0, 100);
            this.tableLayoutPanelLower.Name = "tableLayoutPanelLower";
            this.tableLayoutPanelLower.RowCount = 1;
            this.tableLayoutPanelLower.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelLower.Size = new System.Drawing.Size(900, 125);
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
            this.zedGraphPieView.Size = new System.Drawing.Size(219, 119);
            this.zedGraphPieView.TabIndex = 0;
            // 
            // groupBoxRegions
            // 
            this.groupBoxRegions.Controls.Add(this.listBoxRegions);
            this.groupBoxRegions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxRegions.Location = new System.Drawing.Point(678, 3);
            this.groupBoxRegions.Name = "groupBoxRegions";
            this.groupBoxRegions.Size = new System.Drawing.Size(219, 119);
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
            this.listBoxRegions.Size = new System.Drawing.Size(213, 100);
            this.listBoxRegions.TabIndex = 0;
            this.listBoxRegions.SelectedIndexChanged += new System.EventHandler(this.listBoxRegions_SelectedIndexChanged);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControl1.Location = new System.Drawing.Point(228, 3);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0;
            this.zedGraphControl1.ScrollMaxX = 0;
            this.zedGraphControl1.ScrollMaxY = 0;
            this.zedGraphControl1.ScrollMaxY2 = 0;
            this.zedGraphControl1.ScrollMinX = 0;
            this.zedGraphControl1.ScrollMinY = 0;
            this.zedGraphControl1.ScrollMinY2 = 0;
            this.zedGraphControl1.Size = new System.Drawing.Size(219, 119);
            this.zedGraphControl1.TabIndex = 1;
            // 
            // zedGraphControl2
            // 
            this.zedGraphControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControl2.Location = new System.Drawing.Point(453, 3);
            this.zedGraphControl2.Name = "zedGraphControl2";
            this.zedGraphControl2.ScrollGrace = 0;
            this.zedGraphControl2.ScrollMaxX = 0;
            this.zedGraphControl2.ScrollMaxY = 0;
            this.zedGraphControl2.ScrollMaxY2 = 0;
            this.zedGraphControl2.ScrollMinX = 0;
            this.zedGraphControl2.ScrollMinY = 0;
            this.zedGraphControl2.ScrollMinY2 = 0;
            this.zedGraphControl2.Size = new System.Drawing.Size(219, 119);
            this.zedGraphControl2.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.zedGraphCurrent);
            this.panel1.Controls.Add(this.groupBoxSummery);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 100);
            this.panel1.TabIndex = 5;
            // 
            // zedGraphCurrent
            // 
            this.zedGraphCurrent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphCurrent.Location = new System.Drawing.Point(454, 0);
            this.zedGraphCurrent.Name = "zedGraphCurrent";
            this.zedGraphCurrent.ScrollGrace = 0;
            this.zedGraphCurrent.ScrollMaxX = 0;
            this.zedGraphCurrent.ScrollMaxY = 0;
            this.zedGraphCurrent.ScrollMaxY2 = 0;
            this.zedGraphCurrent.ScrollMinX = 0;
            this.zedGraphCurrent.ScrollMinY = 0;
            this.zedGraphCurrent.ScrollMinY2 = 0;
            this.zedGraphCurrent.Size = new System.Drawing.Size(446, 100);
            this.zedGraphCurrent.TabIndex = 5;
            // 
            // groupBoxSummery
            // 
            this.groupBoxSummery.Controls.Add(this.richTextBox1);
            this.groupBoxSummery.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBoxSummery.Location = new System.Drawing.Point(0, 0);
            this.groupBoxSummery.Name = "groupBoxSummery";
            this.groupBoxSummery.Padding = new System.Windows.Forms.Padding(5, 3, 5, 5);
            this.groupBoxSummery.Size = new System.Drawing.Size(454, 100);
            this.groupBoxSummery.TabIndex = 4;
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
            this.richTextBox1.Size = new System.Drawing.Size(444, 79);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // ucAnalysisCharts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerMain);
            this.Name = "ucAnalysisCharts";
            this.Size = new System.Drawing.Size(900, 512);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.ResumeLayout(false);
            this.tableLayoutPanelLower.ResumeLayout(false);
            this.groupBoxRegions.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBoxSummery.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControlMain;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private ZedGraph.ZedGraphControl zedGraphPieView;
        private ZedGraph.ZedGraphControl zedGraphControl2;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.GroupBox groupBoxRegions;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelLower;
        private System.Windows.Forms.ListBox listBoxRegions;
        private System.Windows.Forms.Panel panel1;
        private ZedGraph.ZedGraphControl zedGraphCurrent;
        private System.Windows.Forms.GroupBox groupBoxSummery;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

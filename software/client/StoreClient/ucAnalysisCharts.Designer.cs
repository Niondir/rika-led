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
            this.zedGraphPieView = new ZedGraph.ZedGraphControl();
            this.splitContainerLower = new System.Windows.Forms.SplitContainer();
            this.groupBoxSummery = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.zedGraphCurrent = new ZedGraph.ZedGraphControl();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.zedGraphControl2 = new ZedGraph.ZedGraphControl();
            this.groupBoxRegions = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelLower = new System.Windows.Forms.TableLayoutPanel();
            this.listBoxRegions = new System.Windows.Forms.ListBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.splitContainerLower.Panel1.SuspendLayout();
            this.splitContainerLower.Panel2.SuspendLayout();
            this.splitContainerLower.SuspendLayout();
            this.groupBoxSummery.SuspendLayout();
            this.groupBoxRegions.SuspendLayout();
            this.tableLayoutPanelLower.SuspendLayout();
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
            this.zedGraphControlMain.Size = new System.Drawing.Size(575, 341);
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
            this.splitContainerMain.Panel1.Controls.Add(this.splitContainerLower);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.tableLayoutPanelLower);
            this.splitContainerMain.Size = new System.Drawing.Size(900, 512);
            this.splitContainerMain.SplitterDistance = 341;
            this.splitContainerMain.TabIndex = 1;
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
            this.zedGraphPieView.Size = new System.Drawing.Size(219, 161);
            this.zedGraphPieView.TabIndex = 0;
            // 
            // splitContainerLower
            // 
            this.splitContainerLower.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerLower.Location = new System.Drawing.Point(0, 0);
            this.splitContainerLower.Name = "splitContainerLower";
            // 
            // splitContainerLower.Panel1
            // 
            this.splitContainerLower.Panel1.Controls.Add(this.zedGraphControlMain);
            // 
            // splitContainerLower.Panel2
            // 
            this.splitContainerLower.Panel2.Controls.Add(this.splitter1);
            this.splitContainerLower.Panel2.Controls.Add(this.zedGraphCurrent);
            this.splitContainerLower.Panel2.Controls.Add(this.groupBoxSummery);
            this.splitContainerLower.Size = new System.Drawing.Size(900, 341);
            this.splitContainerLower.SplitterDistance = 575;
            this.splitContainerLower.TabIndex = 1;
            // 
            // groupBoxSummery
            // 
            this.groupBoxSummery.Controls.Add(this.richTextBox1);
            this.groupBoxSummery.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxSummery.Location = new System.Drawing.Point(0, 0);
            this.groupBoxSummery.Name = "groupBoxSummery";
            this.groupBoxSummery.Padding = new System.Windows.Forms.Padding(5, 3, 5, 5);
            this.groupBoxSummery.Size = new System.Drawing.Size(321, 127);
            this.groupBoxSummery.TabIndex = 2;
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
            this.richTextBox1.Size = new System.Drawing.Size(311, 106);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // zedGraphCurrent
            // 
            this.zedGraphCurrent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphCurrent.Location = new System.Drawing.Point(0, 127);
            this.zedGraphCurrent.Name = "zedGraphCurrent";
            this.zedGraphCurrent.ScrollGrace = 0;
            this.zedGraphCurrent.ScrollMaxX = 0;
            this.zedGraphCurrent.ScrollMaxY = 0;
            this.zedGraphCurrent.ScrollMaxY2 = 0;
            this.zedGraphCurrent.ScrollMinX = 0;
            this.zedGraphCurrent.ScrollMinY = 0;
            this.zedGraphCurrent.ScrollMinY2 = 0;
            this.zedGraphCurrent.Size = new System.Drawing.Size(321, 214);
            this.zedGraphCurrent.TabIndex = 3;
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
            this.zedGraphControl1.Size = new System.Drawing.Size(219, 161);
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
            this.zedGraphControl2.Size = new System.Drawing.Size(219, 161);
            this.zedGraphControl2.TabIndex = 2;
            // 
            // groupBoxRegions
            // 
            this.groupBoxRegions.Controls.Add(this.listBoxRegions);
            this.groupBoxRegions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxRegions.Location = new System.Drawing.Point(678, 3);
            this.groupBoxRegions.Name = "groupBoxRegions";
            this.groupBoxRegions.Size = new System.Drawing.Size(219, 161);
            this.groupBoxRegions.TabIndex = 3;
            this.groupBoxRegions.TabStop = false;
            this.groupBoxRegions.Text = "Regionen";
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
            this.tableLayoutPanelLower.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelLower.Name = "tableLayoutPanelLower";
            this.tableLayoutPanelLower.RowCount = 1;
            this.tableLayoutPanelLower.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelLower.Size = new System.Drawing.Size(900, 167);
            this.tableLayoutPanelLower.TabIndex = 4;
            // 
            // listBoxRegions
            // 
            this.listBoxRegions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxRegions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxRegions.FormattingEnabled = true;
            this.listBoxRegions.ItemHeight = 16;
            this.listBoxRegions.Location = new System.Drawing.Point(3, 16);
            this.listBoxRegions.Name = "listBoxRegions";
            this.listBoxRegions.Size = new System.Drawing.Size(213, 132);
            this.listBoxRegions.TabIndex = 0;
            this.listBoxRegions.SelectedIndexChanged += new System.EventHandler(this.listBoxRegions_SelectedIndexChanged);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 127);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(321, 3);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
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
            this.splitContainerLower.Panel1.ResumeLayout(false);
            this.splitContainerLower.Panel2.ResumeLayout(false);
            this.splitContainerLower.ResumeLayout(false);
            this.groupBoxSummery.ResumeLayout(false);
            this.groupBoxRegions.ResumeLayout(false);
            this.tableLayoutPanelLower.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControlMain;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private ZedGraph.ZedGraphControl zedGraphPieView;
        private System.Windows.Forms.SplitContainer splitContainerLower;
        private ZedGraph.ZedGraphControl zedGraphCurrent;
        private System.Windows.Forms.GroupBox groupBoxSummery;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private ZedGraph.ZedGraphControl zedGraphControl2;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.GroupBox groupBoxRegions;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelLower;
        private System.Windows.Forms.ListBox listBoxRegions;
        private System.Windows.Forms.Splitter splitter1;
    }
}

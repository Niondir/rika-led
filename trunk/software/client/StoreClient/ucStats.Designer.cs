namespace StoreClient
{
    partial class ucStats
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
            this.groupBoxMain = new System.Windows.Forms.GroupBox();
            this.labelStatsTraces = new System.Windows.Forms.Label();
            this.labelStatsAds = new System.Windows.Forms.Label();
            this.labelStatsProducts = new System.Windows.Forms.Label();
            this.labelStatsUser = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxMain
            // 
            this.groupBoxMain.Controls.Add(this.tableLayoutPanel1);
            this.groupBoxMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxMain.Location = new System.Drawing.Point(0, 0);
            this.groupBoxMain.Name = "groupBoxMain";
            this.groupBoxMain.Padding = new System.Windows.Forms.Padding(8, 3, 8, 8);
            this.groupBoxMain.Size = new System.Drawing.Size(722, 102);
            this.groupBoxMain.TabIndex = 0;
            this.groupBoxMain.TabStop = false;
            this.groupBoxMain.Text = "Statistiken";
            // 
            // labelStatsTraces
            // 
            this.labelStatsTraces.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelStatsTraces.Location = new System.Drawing.Point(439, 0);
            this.labelStatsTraces.Name = "labelStatsTraces";
            this.labelStatsTraces.Size = new System.Drawing.Size(264, 78);
            this.labelStatsTraces.TabIndex = 1;
            // 
            // labelStatsAds
            // 
            this.labelStatsAds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelStatsAds.Location = new System.Drawing.Point(260, 0);
            this.labelStatsAds.Name = "labelStatsAds";
            this.labelStatsAds.Size = new System.Drawing.Size(173, 78);
            this.labelStatsAds.TabIndex = 1;
            // 
            // labelStatsProducts
            // 
            this.labelStatsProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelStatsProducts.Location = new System.Drawing.Point(127, 0);
            this.labelStatsProducts.Name = "labelStatsProducts";
            this.labelStatsProducts.Size = new System.Drawing.Size(127, 78);
            this.labelStatsProducts.TabIndex = 1;
            // 
            // labelStatsUser
            // 
            this.labelStatsUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelStatsUser.Location = new System.Drawing.Point(3, 0);
            this.labelStatsUser.Name = "labelStatsUser";
            this.labelStatsUser.Size = new System.Drawing.Size(118, 78);
            this.labelStatsUser.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.56374F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.83853F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.49575F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.10198F));
            this.tableLayoutPanel1.Controls.Add(this.labelStatsTraces, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelStatsProducts, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelStatsUser, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelStatsAds, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(706, 78);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // ucStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxMain);
            this.Name = "ucStats";
            this.Size = new System.Drawing.Size(722, 102);
            this.groupBoxMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxMain;
        private System.Windows.Forms.Label labelStatsUser;
        private System.Windows.Forms.Label labelStatsTraces;
        private System.Windows.Forms.Label labelStatsAds;
        private System.Windows.Forms.Label labelStatsProducts;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

namespace StoreClient
{
    partial class ucProducts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucProducts));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonFilterGroups = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.GridProducts = new OutlookStyleControls.OutlookGrid();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonFilterGroups,
            this.toolStripButtonRefresh,
            this.toolStripSeparator1,
            this.toolStripButtonPNew,
            this.toolStripButtonPEdit,
            this.toolStripButtonPDelete});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(809, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripButtonFilterGroups
            // 
            this.toolStripButtonFilterGroups.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFilterGroups.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonFilterGroups.Image")));
            this.toolStripButtonFilterGroups.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFilterGroups.Name = "toolStripButtonFilterGroups";
            this.toolStripButtonFilterGroups.Size = new System.Drawing.Size(29, 22);
            this.toolStripButtonFilterGroups.Text = "toolStripDropDownButton1";
            this.toolStripButtonFilterGroups.ToolTipText = "Produktgruppen filtern";
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRefresh.Image")));
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRefresh.Text = "toolStripButton1";
            this.toolStripButtonRefresh.ToolTipText = "Aktualisieren";
            // 
            // GridProducts
            // 
            this.GridProducts.AllowUserToAddRows = false;
            this.GridProducts.AllowUserToOrderColumns = true;
            this.GridProducts.AllowUserToResizeRows = false;
            this.GridProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GridProducts.CollapseIcon = null;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridProducts.DefaultCellStyle = dataGridViewCellStyle2;
            this.GridProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridProducts.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.GridProducts.ExpandIcon = null;
            this.GridProducts.Location = new System.Drawing.Point(0, 25);
            this.GridProducts.Name = "GridProducts";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridProducts.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.GridProducts.Size = new System.Drawing.Size(809, 511);
            this.GridProducts.TabIndex = 1;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonPNew
            // 
            this.toolStripButtonPNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPNew.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPNew.Image")));
            this.toolStripButtonPNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPNew.Name = "toolStripButtonPNew";
            this.toolStripButtonPNew.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPNew.Text = "toolStripButton1";
            this.toolStripButtonPNew.ToolTipText = "Neues Produkt hinzufügen";
            // 
            // toolStripButtonPEdit
            // 
            this.toolStripButtonPEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPEdit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPEdit.Image")));
            this.toolStripButtonPEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPEdit.Name = "toolStripButtonPEdit";
            this.toolStripButtonPEdit.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPEdit.Text = "toolStripButton2";
            this.toolStripButtonPEdit.ToolTipText = "Ausgewähltes Produkt bearbeiten";
            // 
            // toolStripButtonPDelete
            // 
            this.toolStripButtonPDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPDelete.Image")));
            this.toolStripButtonPDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPDelete.Name = "toolStripButtonPDelete";
            this.toolStripButtonPDelete.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPDelete.Text = "toolStripButton3";
            this.toolStripButtonPDelete.ToolTipText = "Ausgewähltes Produkt löschen";
            // 
            // ucProducts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GridProducts);
            this.Controls.Add(this.toolStrip);
            this.Name = "ucProducts";
            this.Size = new System.Drawing.Size(809, 536);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButtonFilterGroups;
        private OutlookStyleControls.OutlookGrid GridProducts;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonPNew;
        private System.Windows.Forms.ToolStripButton toolStripButtonPEdit;
        private System.Windows.Forms.ToolStripButton toolStripButtonPDelete;
    }
}

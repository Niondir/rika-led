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
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonFilterGroups = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPDelete = new System.Windows.Forms.ToolStripButton();
            this.GridProducts = new System.Windows.Forms.DataGridView();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Group = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.toolStripButtonFilterGroups.Image = global::StoreClient.Properties.Resources._33;
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
            this.toolStripButtonRefresh.Text = "Aktualisieren";
            this.toolStripButtonRefresh.ToolTipText = "Aktualisieren";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonPNew
            // 
            this.toolStripButtonPNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPNew.Image = global::StoreClient.Properties.Resources._23;
            this.toolStripButtonPNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPNew.Name = "toolStripButtonPNew";
            this.toolStripButtonPNew.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPNew.Text = "Neues Produkt erstellen";
            this.toolStripButtonPNew.ToolTipText = "Neues Produkt hinzufügen";
            this.toolStripButtonPNew.Click += new System.EventHandler(this.toolStripButtonPNew_Click);
            // 
            // toolStripButtonPEdit
            // 
            this.toolStripButtonPEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPEdit.Image = global::StoreClient.Properties.Resources._24;
            this.toolStripButtonPEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPEdit.Name = "toolStripButtonPEdit";
            this.toolStripButtonPEdit.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPEdit.Text = "Ausgewähltes Produkt bearbeiten";
            this.toolStripButtonPEdit.ToolTipText = "Ausgewähltes Produkt bearbeiten";
            // 
            // toolStripButtonPDelete
            // 
            this.toolStripButtonPDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPDelete.Image = global::StoreClient.Properties.Resources._12;
            this.toolStripButtonPDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPDelete.Name = "toolStripButtonPDelete";
            this.toolStripButtonPDelete.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPDelete.Text = "Ausgewähltes Produkt löschen";
            this.toolStripButtonPDelete.ToolTipText = "Ausgewähltes Produkt löschen";
            // 
            // GridProducts
            // 
            this.GridProducts.AllowUserToAddRows = false;
            this.GridProducts.AllowUserToOrderColumns = true;
            this.GridProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GridProducts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductName,
            this.ProductID,
            this.Group,
            this.Price});
            this.GridProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridProducts.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.GridProducts.Location = new System.Drawing.Point(0, 25);
            this.GridProducts.Name = "GridProducts";
            this.GridProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridProducts.Size = new System.Drawing.Size(809, 511);
            this.GridProducts.TabIndex = 1;
            // 
            // ProductName
            // 
            this.ProductName.HeaderText = "Name";
            this.ProductName.Name = "ProductName";
            // 
            // ProductID
            // 
            this.ProductID.HeaderText = "Produktnummer";
            this.ProductID.Name = "ProductID";
            // 
            // Group
            // 
            this.Group.HeaderText = "Produktgruppe";
            this.Group.Name = "Group";
            // 
            // Price
            // 
            this.Price.HeaderText = "Preis";
            this.Price.Name = "Price";
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
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonPNew;
        private System.Windows.Forms.ToolStripButton toolStripButtonPEdit;
        private System.Windows.Forms.ToolStripButton toolStripButtonPDelete;
        private System.Windows.Forms.DataGridView GridProducts;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Group;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
    }
}

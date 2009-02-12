namespace StoreClient
{
    /// <summary>
    /// Entkoppelte Anzeige der Produktgruppen
    /// <seealso cref="ucGroups"/>
    /// </summary>
    partial class FormGroupManagement
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
            this.ucGroups1 = new StoreClient.ucGroups();
            this.SuspendLayout();
            // 
            // ucGroups1
            // 
            this.ucGroups1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGroups1.Location = new System.Drawing.Point(0, 0);
            this.ucGroups1.Name = "ucGroups1";
            this.ucGroups1.Size = new System.Drawing.Size(355, 342);
            this.ucGroups1.TabIndex = 0;
            this.ucGroups1.RegionChanged += new System.EventHandler(this.ucGroups1_RegionChanged);
            // 
            // FormGroupManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 342);
            this.Controls.Add(this.ucGroups1);
            this.Name = "FormGroupManagement";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Produktgruppen";
            this.ResumeLayout(false);

        }

        #endregion

        private ucGroups ucGroups1;


    }
}
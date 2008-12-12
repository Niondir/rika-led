namespace StoreClient
{
    partial class FormMain
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verbindungHerstellenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.einstellungenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.einstellungenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fensterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.produkteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.produktgruppenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.werbungToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.benutzerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hilfeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMain = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripConnectionStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.einstellungenToolStripMenuItem,
            this.fensterToolStripMenuItem,
            this.hilfeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(823, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verbindungHerstellenToolStripMenuItem,
            this.beendenToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // verbindungHerstellenToolStripMenuItem
            // 
            this.verbindungHerstellenToolStripMenuItem.Name = "verbindungHerstellenToolStripMenuItem";
            this.verbindungHerstellenToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.verbindungHerstellenToolStripMenuItem.Text = "Verbindung herstellen";
            this.verbindungHerstellenToolStripMenuItem.Click += new System.EventHandler(this.verbindungHerstellenToolStripMenuItem_Click);
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.beendenToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.beendenToolStripMenuItem.Text = "Beenden";
            // 
            // einstellungenToolStripMenuItem
            // 
            this.einstellungenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.einstellungenToolStripMenuItem1});
            this.einstellungenToolStripMenuItem.Name = "einstellungenToolStripMenuItem";
            this.einstellungenToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.einstellungenToolStripMenuItem.Text = "Extras";
            // 
            // einstellungenToolStripMenuItem1
            // 
            this.einstellungenToolStripMenuItem1.Name = "einstellungenToolStripMenuItem1";
            this.einstellungenToolStripMenuItem1.Size = new System.Drawing.Size(145, 22);
            this.einstellungenToolStripMenuItem1.Text = "Einstellungen";
            // 
            // fensterToolStripMenuItem
            // 
            this.fensterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.produkteToolStripMenuItem,
            this.produktgruppenToolStripMenuItem,
            this.toolStripSeparator1,
            this.werbungToolStripMenuItem,
            this.benutzerToolStripMenuItem});
            this.fensterToolStripMenuItem.Name = "fensterToolStripMenuItem";
            this.fensterToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.fensterToolStripMenuItem.Text = "Fenster";
            // 
            // produkteToolStripMenuItem
            // 
            this.produkteToolStripMenuItem.Name = "produkteToolStripMenuItem";
            this.produkteToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.produkteToolStripMenuItem.Text = "Produkte";
            this.produkteToolStripMenuItem.Click += new System.EventHandler(this.produkteToolStripMenuItem_Click);
            // 
            // produktgruppenToolStripMenuItem
            // 
            this.produktgruppenToolStripMenuItem.Name = "produktgruppenToolStripMenuItem";
            this.produktgruppenToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.produktgruppenToolStripMenuItem.Text = "Produktgruppen";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(158, 6);
            // 
            // werbungToolStripMenuItem
            // 
            this.werbungToolStripMenuItem.Name = "werbungToolStripMenuItem";
            this.werbungToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.werbungToolStripMenuItem.Text = "Werbung";
            this.werbungToolStripMenuItem.Click += new System.EventHandler(this.werbungToolStripMenuItem_Click);
            // 
            // benutzerToolStripMenuItem
            // 
            this.benutzerToolStripMenuItem.Name = "benutzerToolStripMenuItem";
            this.benutzerToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.benutzerToolStripMenuItem.Text = "Benutzer";
            // 
            // hilfeToolStripMenuItem
            // 
            this.hilfeToolStripMenuItem.Name = "hilfeToolStripMenuItem";
            this.hilfeToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.hilfeToolStripMenuItem.Text = "Hilfe";
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 24);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(823, 482);
            this.panelMain.TabIndex = 4;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripConnectionStatus,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 506);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(823, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripConnectionStatus
            // 
            this.toolStripConnectionStatus.Name = "toolStripConnectionStatus";
            this.toolStripConnectionStatus.Size = new System.Drawing.Size(72, 17);
            this.toolStripConnectionStatus.Text = "Verbindung:";
            this.toolStripConnectionStatus.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 528);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(230, 150);
            this.Name = "FormMain";
            this.Text = "iShop";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verbindungHerstellenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beendenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem einstellungenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem einstellungenToolStripMenuItem1;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.ToolStripMenuItem fensterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem produkteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem produktgruppenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem werbungToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem benutzerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hilfeToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripConnectionStatus;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
    }
}


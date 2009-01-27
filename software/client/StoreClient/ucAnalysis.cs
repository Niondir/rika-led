using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace StoreClient
{
    public partial class ucAnalysis : UserControl
    {
        public ucAnalysis()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            ZedGraphControl gc = new ZedGraphControl();

            this.Controls.Add(gc);
        }
    }
}

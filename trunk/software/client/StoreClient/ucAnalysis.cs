using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using CommunicationAPI.DataTypes;

namespace StoreClient
{
    public partial class ucAnalysis : UserControl
    {
        public ucAnalysis()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            dateTimePicker1_ValueChanged(null, null);
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            panelEmpty.BackColor = SystemColors.Control;
            TraceData[] traces = Connection.GetInstance().GetTraces();
            panelEmpty.Controls.Clear();
            panelEmpty.Controls.Add(new ucAnalysisCharts(traces, dateTimePickerStart.Value, dateTimePickerStop.Value));
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerStop.MinDate = dateTimePickerStart.Value + TimeSpan.FromDays(1);
        }
    }
}

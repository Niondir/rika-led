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
        /// <summary>
        /// Stellt die Laufwege in verschiedenen Graphen dar.
        /// Das Control beinhaltet zum einen ein ucAnalysisControl, das für die Visualisierung zuständig ist.
        /// Zum anderen lässt sich damit der Zeitraum der Analyse bestimmen
        /// </summary>
        public ucAnalysis()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            dateTimePicker1_ValueChanged(null, null);
        }

        /// <summary>
        /// Führt die Analyse im ausgewählten Bereich durch ([dateTimePickerStop] [dateTimePickerStart])
        /// und zeigt diese in einem ucAnalysisCharts an
        /// </summary>
        /// <seealso cref="ucAnalysisCharts"/>
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            panelEmpty.BackColor = SystemColors.Control;
            TraceData[] traces = Connection.GetInstance().GetTraces(dateTimePickerStart.Value, dateTimePickerStop.Value);
            panelEmpty.Controls.Clear();
            panelEmpty.Controls.Add(new ucAnalysisCharts(traces, dateTimePickerStart.Value, dateTimePickerStop.Value));
        }

        /// <summary>
        /// Verhindert, dass eine Stopzeit ausgewählt werden kann, die vor der Startzeit oder zu dicht an ihr liegt
        /// </summary>
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerStop.MinDate = dateTimePickerStart.Value + TimeSpan.FromDays(1);
        }
    }
}

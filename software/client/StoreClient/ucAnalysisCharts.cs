using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CommunicationAPI.DataTypes;
using ZedGraph;

namespace StoreClient
{
    public partial class ucAnalysisCharts : UserControl
    {
        RegionData[] regions;
        TraceData[] traces;
        DateTime start, stop;

        /// <summary>
        /// Gibt die aktuelle Auflösung der graphen auf der X Achse an oder legt sie fest
        /// </summary>
        private GraphPrecision xAxisPrec
        {
            set
            {
                trackBarXPrecision.Value = (int)value;
                label1.Text = "Auflösung der Zeitachse\r\n" + value.ToString();
            }
            get
            {
                return (GraphPrecision)trackBarXPrecision.Value;
            }
        }

        /// <summary>
        /// Gibt die aktuelle Auflösung der graphen auf der Y Achse an oder legt sie fest
        /// </summary>
        private GraphPrecision yAxisPrec
        {
            set
            {
                trackBarYPrecision.Value = (int)value;
                label2.Text = "Auflösung der Zeitachse\r\n" + value.ToString();
            }
            get
            {
                return (GraphPrecision)trackBarYPrecision.Value;
            }
        }

        /// <summary>
        /// Erstellt die visuelle Darstellung in einem Control abhängig von den Übergabeparametern
        /// </summary>
        /// <param name="traces">Liste der Kunden Laufwege, die analysiert werden sollen</param>
        /// <param name="start">Startzeit der Analyse</param>
        /// <param name="stop">Stopzeit der Analyse</param>
        public ucAnalysisCharts(TraceData[] traces, DateTime start, DateTime stop)
        {
            regions = Connection.GetInstance().GetRegions();
            this.traces = (TraceData[])traces.Clone();
            
            this.start = start.Date;
            if (stop == stop.Date)
                this.stop = stop;
            else
                this.stop = stop.Date + TimeSpan.FromDays(1);

            InitializeComponent();
            this.Dock = DockStyle.Fill;
            zedGraphControlMain.GraphPane.XAxis.Type = AxisType.Date;
            SetMainGraph(GraphPrecision.Tage, GraphPrecision.Minuten);
            richTextBox1.Text = GetSummary();
            SetRegions();
        }

        /// <summary>
        /// Aktualisiert die Regionen und schreibt sie als Elemente in die List Box
        /// </summary>
        private void SetRegions()
        {
            listBoxRegions.Items.Clear();
            foreach (RegionData i in regions)
            {
                listBoxRegions.Items.Add(i.Name);
            }
        }

        /// <summary>
        /// Gibt eine kurze Zusammenfassung der Analysierten Daten zurück
        /// </summary>
        /// <returns>Zusammenfassung der Daten in einem String</returns>
        private string GetSummary()
        {
            string ret = "";


            int a1 = traces.Length;
            TimeSpan a2 = new TimeSpan();
            foreach (TraceData i in traces)
                for (int j = 0; j < i.Locations.Length - 1; j++)
                    a2 += (i.Locations[j + 1].Time - i.Locations[j].Time);
            int a3 = regions.Length;
            ret += String.Format(ucStats.traceStats, a1, a2.ToString(), a3);
            ret += "\r\n\r\n";
            
            return ret;
        }

        /// <summary>
        /// Zeigt die Daten, abhängig von der Genauigkeit im Hauptgraphen und im Tortendiagramm an.
        /// </summary>
        /// <param name="xAxis">Auflösung der x Achse</param>
        /// <param name="yAxis">Auflösung der y Achse</param>
        public void SetMainGraph(GraphPrecision xAxis, GraphPrecision yAxis)
        {
            regions = Connection.GetInstance().GetRegions();
            int steps;
            TimeSpan step = new TimeSpan();

            this.xAxisPrec = xAxis;
            this.yAxisPrec = yAxis;
            
            switch (xAxis)
            {
                case GraphPrecision.Millisekunden:
                    steps = (int)(stop - start).TotalMilliseconds;
                    step = TimeSpan.FromMilliseconds(1);
                    break;
                case GraphPrecision.Sekunden:
                    steps = (int)(stop - start).TotalSeconds; 
                    step = TimeSpan.FromSeconds(1);
                    break;
                case GraphPrecision.Minuten:
                    steps = (int)(stop - start).TotalMinutes;
                    step = TimeSpan.FromMinutes(1);
                    break;
                case GraphPrecision.Stunden:
                    steps = (int)(stop - start).TotalHours;
                    step = TimeSpan.FromHours(1);
                    break;
                case GraphPrecision.Tage:
                    steps = (int)(stop - start).TotalDays;
                    step = TimeSpan.FromDays(1);
                    break;
                default: steps = 0; break;
            }


            // Zeichnen
            PointPairList[] mainLines = new PointPairList[regions.Length];

            for(int i = 0; i<mainLines.Length; i++)
            {
                DateTime lowerBound = start;
                DateTime upperBound = start + step;
                mainLines[i] = new PointPairList();
                for (int j = 0; j < steps; j++)
                {
                    TimeSpan ts = GetRestInRegion(lowerBound, upperBound, traces, regions[i]);
                    double xVal = (double)(new XDate(lowerBound));
                    switch(yAxis){
                        case GraphPrecision.Stunden: mainLines[i].Add((double)xVal, ts.TotalHours); break;
                        case GraphPrecision.Minuten: mainLines[i].Add((double)xVal, ts.TotalMinutes); break;
                        case GraphPrecision.Sekunden: mainLines[i].Add((double)xVal, ts.TotalSeconds); break;
                        default: mainLines[i].Add((double)xVal, ts.TotalMinutes); break;
                }

                    lowerBound = upperBound;
                    upperBound += step;
                }
            }

            zedGraphControlMain.GraphPane.CurveList.Clear();
            ColorRandomizer.Reset();
            for(int i=0; i<regions.Length; i++)
            {
                LineItem li = zedGraphControlMain.GraphPane.AddCurve(regions[i].Name, mainLines[i], ColorRandomizer.NextColor(), SymbolType.Circle);
                li.Line.Width = 3;
            }
            zedGraphControlMain.GraphPane.Title.Text = "Regionenbesuche nach Zeit";
            zedGraphControlMain.GraphPane.XAxis.Title.Text = "Zeit ("+this.xAxisPrec.ToString()+")";
            zedGraphControlMain.GraphPane.YAxis.Title.Text = "Besuchzeit ("+this.yAxisPrec.ToString()+")";
            zedGraphControlMain.AxisChange();
            zedGraphControlMain.Refresh();


            // Pie
            ColorRandomizer.Reset();
            zedGraphPie.GraphPane.CurveList.Clear();
            zedGraphPie.GraphPane.YAxis.IsVisible = zedGraphPie.GraphPane.XAxis.IsVisible = false;
            foreach(RegionData i in regions)
            {
                zedGraphPie.GraphPane.AddPieSlice(SumUp(i.Id), ColorRandomizer.NextColor(), Color.White, 45f, 0, i.Name);
            }
            zedGraphPie.Refresh();
        }

        /// <summary>
        /// Summiert alle Zeitspannen auf, die innerhalb des untersuchten Intervalls bei der übergebenen Produktregion angelaufen wurden
        /// </summary>
        /// <param name="regionName">Name der Region</param>
        /// <returns></returns>
        private double SumUp(string regionName)
        {
            double ret = 0;
            foreach (TraceData i in traces)
            {
                for (int j = 0; j < i.Locations.Length-1; j++)
                {
                    if (i.Locations[j].LampId == regionName)
                        ret += (i.Locations[j].Time - i.Locations[j + 1].Time).TotalMinutes;
                }
                // letzter wird mit dem Abgabe Datetime verglichen
                if (i.Locations[i.Locations.Length - 1].LampId == regionName)
                    ret += (i.Timestamp - i.Locations[i.Locations.Length - 1].Time).TotalMinutes;
            }
            return ret;
        }

        /// <summary>
        /// Berechnet die Zeit, die in einem bestimmten Zeitintervall von allen übergebenen Laufwegen in der bestimmten Region verbracht wurde
        /// </summary>
        /// <param name="start">Startpunkt des Zeitrahmens</param>
        /// <param name="stop">Endpunkt des Zeitrahmens</param>
        /// <param name="traces">Laufwege, die untersucht werden sollen</param>
        /// <param name="region">Region, die untersucht werden soll</param>
        /// <returns>Verweildauer in dergion [region]</returns>
        private TimeSpan GetRestInRegion(DateTime start, DateTime stop, TraceData[] traces, RegionData region)
        {
            TimeSpan ret = new TimeSpan();
            foreach (TraceData i in traces)
            {
                // für alle Regionen bis ausschließlich der letzten, da die mit der abgabe der daten verrechnet werden muss
                for(int j=0; j<i.Locations.Length - 1; j++)
                {
                    if (i.Locations[j].Time < stop && i.Locations[j].Time >= start && i.Locations[j].LampId == region.Id)
                    {
                        ret += i.Locations[j + 1].Time - i.Locations[j].Time;
                    }
                }
                // letzter abschnitt
                int last = i.Locations.Length - 1;
                if (i.Locations[last].Time < stop && i.Locations[last].Time > start && i.Locations[last].LampId == region.Id)
                    ret += i.Timestamp - i.Locations[last].Time;
            }
            return ret;
        }

        /// <summary>
        /// Zeigt in den unteren Graphen, die Daten der neu ausgewählten Region an
        /// </summary>
        /// <seealso cref="UpdateSubGraphs"/>
        private void listBoxRegions_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxRegions.Tag = regions[listBoxRegions.SelectedIndex];

            UpdateSubGraphs(regions[listBoxRegions.SelectedIndex]);
        }

        /// <summary>
        /// Berechnet die Daten abhängig von dem Übergabeparameter für die unteren Graphen und stellt sie dar
        /// </summary>
        /// <param name="region">Region, die dargestellt werden soll</param>
        private void UpdateSubGraphs(RegionData region)
        {
            // Fluchtgraph
            double[] destinations = new double[regions.Length];
            for (int i = 0; i < destinations.Length; i++)
                destinations[i] = 0.0;
            foreach (TraceData i in traces)
            {
                for (int j = 0; j < i.Locations.Length - 1; j++)
                {
                    if (i.Locations[j].LampId == region.Id)
                        for (int k = 0; k < regions.Length; k++)
                            if (i.Locations[j+1].LampId == regions[k].Id && i.Locations[j].LampId != i.Locations[j+1].LampId)
                                destinations[k]++;
                }
            }
            zedGraphControlFlee.GraphPane.Title.Text = "Regionenflucht von " + region.Name;
            zedGraphControlFlee.GraphPane.XAxis.IsVisible = false;
            zedGraphControlFlee.GraphPane.YAxis.IsVisible = false;
            zedGraphControlFlee.GraphPane.CurveList.Clear();
            ColorRandomizer.Reset();
            for(int i=0; i<destinations.Length; i++)
            {
                zedGraphControlFlee.GraphPane.AddPieSlice(destinations[i], ColorRandomizer.NextColor(), Color.White, 45f, 0, regions[i].Name);
            }
            zedGraphControlFlee.Refresh();


            //Tageszeitgraph
            TimeSpan step = TimeSpan.FromMinutes(1.0);
            int steps = 24 * 60;
            DateTime lowerBound = start;
            DateTime upperBound = start + step;
            PointPairList dayTimeLine = new PointPairList();
            for (int i = 0; i < steps; i++)
            {
                TimeSpan ts = new TimeSpan();

                for (int j = 0; j < (stop - start).TotalDays; j++)
                {
                    ts += GetRestInRegion(lowerBound + TimeSpan.FromDays(j), upperBound + TimeSpan.FromDays(j), traces, region);
                }
                double xVal = (double)(new XDate(lowerBound));
                dayTimeLine.Add(xVal, ts.TotalMinutes);
                lowerBound = upperBound;
                upperBound += step;
            }
            zedGraphControlDayTime.GraphPane.CurveList.Clear();
            zedGraphControlDayTime.GraphPane.XAxis.Type = AxisType.Date;
            zedGraphControlDayTime.GraphPane.XAxis.Scale.Min = (double)new XDate(start);
            zedGraphControlDayTime.GraphPane.XAxis.Scale.Max = (double)new XDate(start + TimeSpan.FromHours(24));
            LineItem li = zedGraphControlDayTime.GraphPane.AddCurve(region.Name, dayTimeLine, Color.Black, SymbolType.None);
            li.Line.Width = 3f;
            zedGraphControlDayTime.GraphPane.Title.Text = "Tageszeitverteilung";
            zedGraphControlDayTime.AxisChange();
            zedGraphControlDayTime.Refresh();
        }

        /// <summary>
        /// Passt die Auflösung der y Achse an
        /// </summary>
        private void trackBarYPrecision_Scroll(object sender, EventArgs e)
        {
            this.yAxisPrec = (GraphPrecision)trackBarYPrecision.Value;
        }

        /// <summary>
        /// Passt die Auflösung der x Achse an
        /// </summary>
        private void trackBarXPrecision_Scroll(object sender, EventArgs e)
        {
            this.xAxisPrec = (GraphPrecision)trackBarXPrecision.Value;
        }

        /// <summary>
        /// Aktualisiert die Anzeige
        /// </summary>
        /// <seealso cref="SetMainGraph"/>
        private void button1_Click(object sender, EventArgs e)
        {
            SetMainGraph((GraphPrecision)trackBarXPrecision.Value, (GraphPrecision)trackBarYPrecision.Value);
        }
    }

    /// <summary>
    /// Auflistung der möglichen Auflösungen
    /// </summary>
    public enum GraphPrecision
    {
        /// <summary>
        /// 
        /// </summary>
        Millisekunden,
        /// <summary>
        /// 
        /// </summary>
        Sekunden,
        /// <summary>
        /// 
        /// </summary>
        Minuten,
        /// <summary>
        /// 
        /// </summary>
        Stunden,
        /// <summary>
        /// 
        /// </summary>
        Tage
    }

    /// <summary>
    /// Vereinfachter Zugriff auf Farben
    /// </summary>
    public class ColorRandomizer
    {
        static private Dictionary<int, Color> colors = new Dictionary<int, Color>();
        static private bool inited = false;
        static private Random rnd;
        static private int _i = 0;
        static private int i { get { return _i++; } }
        static private int key = 0;
        
        /// <summary>
        /// 
        /// </summary>
        static public void Reset()
        {
            key = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static public Color NextColor()
        {
            if (!inited)
                Init();
            Color ret = colors[key++];
            return ret;
        }

        private static void Init()
        {
            rnd = new Random((int)DateTime.Now.Ticks);

            colors.Add(i, Color.Red);
            colors.Add(i, Color.Blue);
            colors.Add(i, Color.Green);
            colors.Add(i, Color.Pink);
            colors.Add(i, Color.Orange);
            colors.Add(i, Color.Crimson);
            colors.Add(i, Color.DarkBlue);
            
            inited = true;
        }
    }
}

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
            SetMainGraph(GraphPrecision.Hours);
            richTextBox1.Text = GetSummary();
            SetRegions();
        }

        private void SetRegions()
        {
            listBoxRegions.Items.Clear();
            foreach (RegionData i in regions)
            {
                listBoxRegions.Items.Add(i.Name);
            }
        }

        private string GetSummary()
        {
            /*
            int traceCount = 0;
            List<string> aktRegions = new List<string>();
            List<String> traceRegions = new List<string>();
            foreach (RegionData i in regions)
                aktRegions.Add(i.Name);
            foreach (TraceData i in traces)
                foreach (LocationData j in i.Locations)
                    if(traceRegions.Contains(j.
            foreach (TraceData i in traces)
            {
                // Used traces
                if (i.Timestamp > start && i.Timestamp < stop)
                    traceCount++;

                //unvisited regions and deleted regions in traces

            }
            */
            return "";
            


        }

        public void SetMainGraph(GraphPrecision precision)
        {
            regions = Connection.GetInstance().GetRegions();
            int steps;
            TimeSpan step = new TimeSpan();
            
            
            switch (precision)
            {
                case GraphPrecision.MilliSeconds:
                    steps = (int)(stop - start).TotalMilliseconds;
                    step = TimeSpan.FromMilliseconds(1);
                    break;
                case GraphPrecision.Seconds:
                    steps = (int)(stop - start).TotalSeconds; 
                    step = TimeSpan.FromSeconds(1);
                    break;
                case GraphPrecision.Minutes:
                    steps = (int)(stop - start).TotalMinutes;
                    step = TimeSpan.FromMinutes(1);
                    break;
                case GraphPrecision.Hours:
                    steps = (int)(stop - start).TotalHours;
                    step = TimeSpan.FromHours(1);
                    break;
                case GraphPrecision.Days:
                    steps = (int)(stop - start).TotalDays;
                    step = TimeSpan.FromDays(1);
                    break;
                default: steps = 0; break;
            }



            PointPairList[] mainLines = new PointPairList[regions.Length];

            for(int i = 0; i<mainLines.Length; i++)
            {
                DateTime lowerBound = start;
                DateTime upperBound = start + step;
                mainLines[i] = new PointPairList();
                for (int j = 0; j < steps; j++)
                {
                    TimeSpan ts = GetRestInRegion(lowerBound, upperBound, traces, regions[i]);
                    mainLines[i].Add((double)j, ts.TotalMinutes);

                    lowerBound = upperBound;
                    upperBound += step;
                }
            }
            Random rnd = new Random();
            for(int i=0; i<regions.Length; i++)
            {
                LineItem li = zedGraphControlMain.GraphPane.AddCurve(regions[i].Name, mainLines[i], ColorRandomizer.NextColor(), SymbolType.Circle);
                li.Line.Width = 3;
            }
            zedGraphControlMain.AxisChange();
            zedGraphControlMain.Refresh();
        }
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

        private void listBoxRegions_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxRegions.Tag = regions[listBoxRegions.SelectedIndex];
        }
    }
    public enum GraphPrecision
    {
        MilliSeconds,
        Seconds,
        Minutes,
        Hours,
        Days,
    }
    public class ColorRandomizer
    {
        static private Dictionary<int, Color> colors = new Dictionary<int, Color>();
        static private bool inited = false;
        static private Random rnd;
        static private int _i = 0;
        static private int i { get { return _i++; } }
        static public Color NextColor()
        {
            if (!inited)
                Init();
            int key = rnd.Next(colors.Count - 1);
            Color ret = colors[key];
            colors.Remove(key);
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
            
            inited = true;
        }
    }
}

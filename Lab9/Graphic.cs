using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab9
{
    public partial class Graphic: Form
    {
        public Graphic(List<int> rList, List<int> nList)
        {
            InitializeComponent();
            chart1.Series.Add(new Series());
            chart1.ChartAreas[0].AxisX.Title = "R";
            chart1.ChartAreas[0].AxisX.Minimum = rList.Min();
            chart1.ChartAreas[0].AxisX.Maximum = rList.Max();
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 1;
            chart1.ChartAreas[0].AxisY.Title = "N";
            chart1.ChartAreas[0].AxisY.Minimum = nList.Min();
            chart1.ChartAreas[0].AxisY.Maximum = nList.Max();
            chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 1;
            chart1.Series[0].Color = Color.Green;
            chart1.Series[1].Color = Color.Black;
            chart1.Series[0].ChartType = SeriesChartType.Line;
            chart1.Series[1].ChartType = SeriesChartType.Point;

            for (int i = 0; i < rList.Count; i++)
            {
                chart1.Series[0].Points.AddXY(rList[i], nList[i]);
                chart1.Series[1].Points.AddXY(rList[i], nList[i]);
            }
        }
    }
}

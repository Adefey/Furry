using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Furry
{
    public partial class MainForm : Form
    {
        private Function F;
        private FourierTransformer FT;
        public MainForm()
        {
            InitializeComponent();
        }

        private void inputButton_Click(object sender, EventArgs e)
        {
            F = new Function((x) => Math.Sin(1 / x), 5);
        }

        private async void workButton_Click(object sender, EventArgs e)
        {
            FT = new FourierTransformer(F, 0.01, 20);

            plotChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            plotChart.Series[0].Color = Color.DarkViolet;

            string textResult = "";
            List<Point2D> result = new List<Point2D>();
            await Task.Run(() =>
            {
                result = FT.MakeValueArr();
                foreach (Point2D po in result)
                {
                    textResult += $"Fourier series in point x={po.X} : {FT.MakeSeriesString(po.X)} = {po.Y} \r\n";
                }
            });
            textBox.Text = textResult;
            foreach (Point2D po in result)
            {
                plotChart.Series[0].Points.AddXY(po.X, po.Y);
            }
        }
    }
}

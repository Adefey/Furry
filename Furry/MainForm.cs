using System;
using System.Collections.Generic;
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
            F = new Function((x) => Math.Pow(x + 1, 2), 3);
        }

        private async void workButton_Click(object sender, EventArgs e)
        {
            FT = new FourierTransformer(F, 0.01, 100);

            plotChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            string textResult = "";
            List<Point2D> result = new List<Point2D>();
            await Task.Run(() =>
            {
                result = FT.MakeValueArr();
            });
            textBox.Text = textResult;
            foreach (Point2D po in result)
            {
                plotChart.Series[0].Points.AddXY(po.X, po.Y);
            }
        }
    }
}

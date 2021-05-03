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
        private List<Point2D> result = new List<Point2D>();
        public MainForm()
        {
            InitializeComponent();
        }

        private async void inputButton_Click(object sender, EventArgs e)
        {
            F = new Function((x) => (decimal)Math.Pow((double)x + 1, 3), 5);
            FT = new FourierTransformer(F, 0.02m, 20);

            plotChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            plotChart.Series[0].Color = Color.DarkViolet;

            string textResult = "";

            await Task.Run(() =>
            {
                result = FT.MakeValueArr();
                foreach (Point2D po in result)
                {
                    textResult += $"Fourier series in point x={po.X} : {FT.MakeSeriesString(po.X)} = {Math.Round(po.Y, 3)} \r\n";
                }
            });
            textBox.Text = textResult;
            inputTextBox.Text = "Done";
        }

        private void workButton_Click(object sender, EventArgs e)
        {
            if (inputTextBox.Text == "Done")
            {
                foreach (Point2D po in result)
                {
                    plotChart.Series[0].Points.AddXY(po.X, po.Y);
                }
            }
            else
            {
                MessageBox.Show("Wait", "Not ready");
            }
        }
    }
}

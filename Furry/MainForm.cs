using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Furry
{
    public delegate decimal func(decimal x);

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
            #region init values
            int l = (int)numericUpDown1.Value;
            decimal eps = numericUpDown2.Value;
            int n = (int)numericUpDown3.Value;
            func function = new func(MathParser.MakeExpr(inputTextBox.Text));
            #endregion

            F = new Function(function, l);
            FT = new FourierTransformer(F, eps, n);

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
        }

        private void workButton_Click(object sender, EventArgs e)
        {
            {
                foreach (Point2D po in result)
                {
                    plotChart.Series[0].Points.AddXY(po.X, po.Y);
                }
            }
        }
    }
}

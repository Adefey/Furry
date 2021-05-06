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
        private List<Point2D> originalFunction = new List<Point2D>();

        public MainForm()
        {
            InitializeComponent();
            plotChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            plotChart.Series[0].Color = Color.DarkRed;
            plotChart.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            plotChart.Series[1].Color = Color.DarkViolet;
            plotChart.ChartAreas[0].AxisX.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.SharpTriangle;
            plotChart.ChartAreas[0].AxisY.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.SharpTriangle;        
        }

        private async void inputButton_Click(object sender, EventArgs e)
        {
            #region init values
            int l = (int)numericUpDown1.Value;
            double eps = (double)numericUpDown2.Value;
            int n = (int)numericUpDown3.Value;
            Func<double, double> function;
            try
            {
                function = new Func<double, double>(MathParser.MakeExpr(inputTextBox.Text));
            }
            catch (Exception)
            {
                MessageBox.Show("Input error, call math functions using class Math", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            F = new Function(function, l);
            FT = new FourierTransformer(F, eps, n);

            string textResult = "";

            await Task.Run(() =>
            {
                try
                {
                    result = FT.MakeValueArr();
                    originalFunction = FT.MakeOriginalValueArr();
                }
                catch (Exception)
                {
                    MessageBox.Show($"Math error. May be your function is not determided somewhere from {-l} to {l}", "Math Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                foreach (Point2D po in result)
                {
                    textResult += $"Fourier series in point x={po.X} : {FT.MakeSeriesString(po.X)} = {Math.Round(po.Y, 3)} \r\n\r\n";
                }
            });
            textBox.Text = textResult;
        }

        private void workButton_Click(object sender, EventArgs e)
        {
            plotChart.ChartAreas[0].AxisX.Minimum = -(double)numericUpDown1.Value;
            plotChart.ChartAreas[0].AxisX.Maximum = (double)numericUpDown1.Value;
            plotChart.Series[0].Points.Clear();
            plotChart.Series[1].Points.Clear();
            foreach (Point2D po in result)
            {
                plotChart.Series[0].Points.AddXY(po.X, po.Y);
            }
            foreach (Point2D po in originalFunction)
            {
                plotChart.Series[1].Points.AddXY(po.X, po.Y);
            }                   
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().Show();
        }
    }
}

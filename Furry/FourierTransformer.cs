using System;
using System.Collections.Generic;

namespace Furry
{
    internal class FourierTransformer
    {
        private Function F;
        private int maxN;
        private double eps;

        public FourierTransformer(Function F, double eps, int maxN)
        {
            this.F = F;
            this.eps = eps;
            this.maxN = maxN;
        }

        private double EvaluateA0()
        {
            double res = F.Integrate(eps);
            return 1 / F.GetL() * res;
        }

        private double EvaluateAn(int n)
        {
            double res = 0;
            for (double x = -F.GetL(); x <= F.GetL(); x += eps)
            {
                res += F.GetF()(x) * eps * CosN(x, n, F.GetL());
            }
            return res * (1 / F.GetL());
        }

        private double CosN(double x, int n, double l)
        {
            return Math.Cos((Math.PI * n * x) / l);
        }

        private double EvaluateBn(int n)
        {
            double res = 0;
            for (double x = -F.GetL(); x <= F.GetL(); x += eps)
            {
                res += F.GetF()(x) * eps * SinN(x, n, F.GetL());
            }
            return res * (1 / F.GetL());
        }

        private double SinN(double x, int n, double l)
        {
            return Math.Sin((Math.PI * n * x) / l);
        }

        public double EvaluateFunction(double x)
        {
            double res = EvaluateA0() / 2;
            for (int n = 1; n <= maxN; n++)
            {
                double cosAdd = EvaluateAn(n) * CosN(x, n, F.GetL());
                double sinAdd = EvaluateBn(n) * SinN(x, n, F.GetL());
                res += cosAdd + sinAdd;
            }
            return res;
        }

        public string MakeSeriesString(double x)
        {
            string res = (EvaluateA0() / 2).ToString() + "+";
            for (int n = 1; n <= maxN; n++)
            {
                double cosAdd = EvaluateAn(n) * CosN(x, n, F.GetL());
                double sinAdd = EvaluateBn(n) * SinN(x, n, F.GetL());
                res += "(" + cosAdd.ToString() + "+" + sinAdd.ToString() + ")+";
            }
            res = res.TrimEnd('+');
            return res;
        }

        public List<Point2D> MakeValueArr()
        {
            List<Point2D> listRes = new List<Point2D>();
            for (double x = -F.GetL(); x <= F.GetL(); x += eps)
            {
                double res = EvaluateA0() / 2;
                for (int n = 1; n <= maxN; n++)
                {
                    double cosAdd = EvaluateAn(n) * CosN(x, n, F.GetL());
                    double sinAdd = EvaluateBn(n) * SinN(x, n, F.GetL());
                    res += cosAdd + sinAdd;
                }
                listRes.Add(new Point2D(x, res));
            }
            return listRes;
        }
    }

    internal struct Point2D
    {
        public double X;
        public double Y;
        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}

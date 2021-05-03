using System;
using System.Collections.Generic;

namespace Furry
{
    internal class FourierTransformer
    {
        private Function F;
        private int maxN;
        private decimal eps;

        public FourierTransformer(Function F, decimal eps, int maxN)
        {
            this.F = F;
            this.eps = eps;
            this.maxN = maxN;
        }

        private decimal EvaluateA0()
        {
            decimal res = F.Integrate(eps);
            return 1 / F.GetL() * res;
        }

        private decimal EvaluateAn(int n)
        {
            decimal res = 0;
            for (decimal x = -F.GetL(); x <= F.GetL(); x += eps)
            {
                res += F.GetF()(x) * eps * CosN(x, n, F.GetL());
            }
            return res * (1 / F.GetL());
        }

        private decimal CosN(decimal x, int n, decimal l)
        {
            return (decimal)(Math.Cos((Math.PI * n * (double)x) / (double)l));
        }

        private decimal EvaluateBn(int n)
        {
            decimal res = 0;
            for (decimal x = -F.GetL(); x <= F.GetL(); x += eps)
            {
                res += F.GetF()(x) * eps * SinN(x, n, F.GetL());
            }
            return res * (1 / F.GetL());
        }

        private decimal SinN(decimal x, int n, decimal l)
        {
            return (decimal)(Math.Sin((Math.PI * n * (double)x) / (double)l));
        }

        public decimal EvaluateFunction(decimal x)
        {
            decimal res = EvaluateA0() / 2;
            for (int n = 1; n <= maxN; n++)
            {
                decimal cosAdd = EvaluateAn(n) * CosN(x, n, F.GetL());
                decimal sinAdd = EvaluateBn(n) * SinN(x, n, F.GetL());
                res += cosAdd + sinAdd;
            }
            return res;
        }

        public string MakeSeriesString(decimal x)
        {
            string res = $"{ Math.Round(EvaluateA0() / 2)}  + ";
            for (int n = 1; n <= maxN; n++)
            {
                decimal cosAdd = EvaluateAn(n) * CosN(x, n, F.GetL());
                decimal sinAdd = EvaluateBn(n) * SinN(x, n, F.GetL());
                res += $"( {Math.Round(cosAdd, 3)} + {Math.Round(sinAdd, 3)} ) + ";
            }
            res = res.TrimEnd('+', ' ');
            return res;
        }

        public List<Point2D> MakeValueArr()
        {
            List<Point2D> listRes = new List<Point2D>();
            for (decimal x = -F.GetL(); x <= F.GetL(); x += eps)
            {
                decimal res = EvaluateA0() / 2;
                for (int n = 1; n <= maxN; n++)
                {
                    decimal cosAdd = EvaluateAn(n) * CosN(x, n, F.GetL());
                    decimal sinAdd = EvaluateBn(n) * SinN(x, n, F.GetL());
                    res += cosAdd + sinAdd;
                }
                listRes.Add(new Point2D(x, res));
            }
            return listRes;
        }
    }

    internal struct Point2D
    {
        public decimal X;
        public decimal Y;
        public Point2D(decimal x, decimal y)
        {
            X = x;
            Y = y;
        }
    }
}
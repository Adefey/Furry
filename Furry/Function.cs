﻿namespace Furry
{
    internal class Function
    {
        public delegate double func(double x);

        private func f;
        private double l;

        public Function()
        {
            l = 0;
            f = null;
        }

        public Function(func f, double l)
        {
            this.f = f;
            this.l = l;
        }

        public void SetL(double l)
        {
            this.l = l;
        }

        public double GetL()
        {
            return l;
        }

        public void SetFunction(func f)
        {
            this.f = f;
        }

        public func GetF()
        {
            return f;
        }

        public double Integrate(double eps)
        {
            double res = 0;
            for (double x = -l; x <= l; x += eps)
            {
                res += f(x) * eps;
            }
            return res;
        }

        public static double Integrate(Function func, double eps)
        {
            double res = 0;
            for (double x = -func.l; x <= func.l; x += eps)
            {
                res += func.f(x) * eps;
            }
            return res;
        }
    }
}
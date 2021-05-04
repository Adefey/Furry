﻿using System;

namespace Furry
{
    internal class Function
    {
        private Func<decimal, decimal> f;
        private decimal l;

        public Function()
        {
            l = 0;
            f = null;
        }

        public Function(Func<decimal, decimal> f, decimal l)
        {
            this.f = f;
            this.l = l;
        }

        public void SetL(decimal l)
        {
            this.l = l;
        }

        public decimal GetL()
        {
            return l;
        }

        public void SetFunction(Func<decimal, decimal> f)
        {
            this.f = f;
        }

        public Func<decimal, decimal> GetF()
        {
            return f;
        }

        public decimal Integrate(decimal eps)
        {
            decimal res = 0;
            for (decimal x = -l; x <= l; x += eps)
            {
                res += f(x) * eps;
            }
            return res;
        }

        public static decimal Integrate(Function func, decimal eps)
        {
            decimal res = 0;
            for (decimal x = -func.l; x <= func.l; x += eps)
            {
                res += func.f(x) * eps;
            }
            return res;
        }
    }
}

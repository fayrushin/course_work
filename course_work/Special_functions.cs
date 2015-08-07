using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace course_work
{
    class Special_functions
    {

        public long fact(int num)
        {            
            return (num == 0) ? 1 : num * fact(num - 1);
        }
        public long fact_n(int num, int n)
        {
            return (num == 0) ? 1 : (num + n) * fact_n(num - 1,n);
        }
        private double plnm_lejandro(double x, int n, int m1)
        {
            int m = Math.Abs(m1);
            double qk = 1, s = 0;
            double a = Math.Pow((1 - x * x), (m / 2.0)) / Math.Pow(2, n);
            double ak = fact(2 * n) * Math.Pow(x, n - m) /
                Convert.ToDouble(fact(n) * fact(n - m));
            for (int k = 0; k <= (n - m) / 2; k++)
            {
                ak *= qk;
                s += ak;
                qk = (-1) * (n - k) * (n - m - 2 * k - 1) * (n - m - 2 * k) /
                    Convert.ToDouble((k + 1) * (2 * n - 2 * k) * (2 * n - 2 * k - 1) * x * x);
            }
            return s * a;
        }
        public Complex fet(double fi, int m)
        {
            return Complex.Exp(Complex.ImaginaryOne * m * fi) /
                Convert.ToDouble(Math.Sqrt(2 * Math.PI));
        }
        public double tetta(double tet, int l, int m)
        {
            double t;
            double cos_tet = Math.Cos(tet);
            t = Math.Sqrt((2 * l + 1) / 2.0 * fact(l - Math.Abs(m)) / Convert.ToDouble(fact(l + Math.Abs(m)))) *
                plnm_lejandro(cos_tet, l, m);
            return t;
        }
        private double plnm_cheb(double x, int n, int alpha)
        {
            double ln0 = 1.0, ln1 = -x + alpha + 1.0;
            double ln = 0;
            if (n == 0)
                return ln0;
            if (n == 1)
                return ln1;

            for (int i = 1; i < n; i++)
            {
                ln = ((alpha + 2 * i + 1 - x) * ln1 - (alpha + i) * ln0) /
                    Convert.ToDouble(i + 1);
                ln0 = ln1;
                ln1 = ln;
            }

            return ln;

        }
        public double rad_ur(double r, int nr, int l)
        {
            double t = Math.Pow(2, l + 1) / Math.Pow(nr + l + 1, l + 2) * Math.Sqrt(fact(nr) /
                Convert.ToDouble(fact(nr + 2 * l + 1))) * Math.Pow(r, l) *
                Math.Exp(-r / Convert.ToDouble(nr + l + 1)) *
                plnm_cheb(2 * r / Convert.ToDouble(nr + l + 1), nr, 2 * l + 1);
            return t;
        }
        public double rad_ur2(double r, int nr, int l)
        {
            double t = Math.Pow(2, l + 1) / Math.Pow(nr + l + 1, l + 2) * Math.Sqrt(1.0 /
                Convert.ToDouble(fact_n(2*l+1,nr))) * Math.Pow(r, l) *
                Math.Exp(-r / Convert.ToDouble(nr + l + 1)) *
                plnm_cheb(2 * r / Convert.ToDouble(nr + l + 1), nr, 2 * l + 1);
            return t;
        }
    }
}

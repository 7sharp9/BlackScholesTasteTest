using System;

namespace CsBs
{
    public class Options
    {
        public enum Style
        {
            Call,
            Put
        }

        public static double BlackScholes(Style callPut, double s, double x, double t, double r, double v)
        {
            double result = 0.0;

            var d1 = (Math.Log(s/x) + (r + v*v/2.0)*t)/(v*Math.Sqrt(t));
            var d2 = d1 - v*Math.Sqrt(t);
            switch (callPut)
            {
                case Style.Call:
                    result = s*Cnd(d1) - x*Math.Exp(-r*t)*Cnd(d2);
                    break;
                case Style.Put:
                    result = x*Math.Exp(-r*t)*Cnd(-d2) - s*Cnd(-d1);
                    break;
            }
            return result;
        }

        private static double Cnd(double x)
        {
            const double a1 = 0.31938153;
            const double a2 = -0.356563782;
            const double a3 = 1.781477937;
            const double a4 = -1.821255978;
            const double a5 = 1.330274429;
            var l = Math.Abs(x);
            var k = 1.0/(1.0 + 0.2316419*l);
            var w = 1.0 - 1.0/Math.Sqrt(2*Math.PI)* 
                Math.Exp(-l*l/2.0)* (a1*k + a2*k*k + a3* 
                    Math.Pow(k, 3) + a4*Math.Pow(k, 4) + a5*Math.Pow(k, 5));
            if (x < 0)
            {
                return 1.0 - w;
            }
            return w;
        }
    }
}
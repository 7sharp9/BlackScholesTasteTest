using System;
using System.Collections.Generic;

namespace CsBs
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            var d = new double[50000000];
            var i = 0;
            while (i < 50000000)
            {
                d[i] = Options.BlackScholes(Options.Style.Call, 60.0, 65.0, 0.25, 0.08, 0.3);
                i++;
            }
            sw.Stop();

            Console.WriteLine("50M black scholes: {0}", sw.Elapsed);
            Console.WriteLine("Output = {0}", Options.BlackScholes(Options.Style.Call, 60.0, 65.0, 0.25, 0.08, 0.3));
            Console.ReadKey();
        }
    }
}
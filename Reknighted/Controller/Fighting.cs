using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Reknighted.Controller
{
    internal class Fighting
    {
        public static double Fight(double[] first, double[] second)
        {
            double d1 = first[0];
            double p1 = first[1];
            double f1 = first[2];
            double h1 = first[3];

            double d2 = second[0];
            double p2 = second[1];
            double f2 = second[2];
            double h2 = second[3];

            double s1 = (d1 + p1 + f1) * Math.Sqrt(h1);
            double s2 = (d2 + p2 + f2) * Math.Sqrt(h2);

            double sq1 = Math.Pow(s1, 1.25);
            double sq2 = Math.Pow(s2, 1.25);

            double x1 = sq1 / (sq2 + sq1);

            return x1;
        }
    }
}

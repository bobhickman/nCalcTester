using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculation
{
    public static class CoreCalculations 
    {
        public static double Sum(double[] values)
        {
            double sum = 0.0;
            foreach (double d in values)
                sum += d;
            return sum;
        }

        public static double Average(double[] values)
        {
            return Sum(values) / values.Length;
        }
    }
}

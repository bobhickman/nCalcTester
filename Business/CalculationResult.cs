using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class CalculationResult
    {
        public string CalculatorName { get; set; }
        public string CalculationName { get; set; }
        public double[] Results;

        public override string ToString()
        {
            return CalculationName;
        }
    }
}

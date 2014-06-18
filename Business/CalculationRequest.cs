using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class CalculationRequest
    {
        public string CalculatorName { get; set; }
        public List<string> Ids { get; set; }
        public List<string> CalculationNames { get; set; }

        public override string ToString()
        {
            return CalculatorName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculation
{
    public class ReflectionCalculator : ICalculator
    {
        public string Category
        {
            get { throw new NotImplementedException(); }
        }

        public List<string> GetNamesOfCalculations()
        {
            throw new NotImplementedException();
        }

        public double[] Calculate(string calculationName, List<Tuple<string, double[]>> parameters)
        {
            throw new NotImplementedException();
        }
    }
}

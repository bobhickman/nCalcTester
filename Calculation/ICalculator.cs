using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculation
{
    public interface ICalculator
    {
        string Category { get; }

        List<string> GetNamesOfCalculations();

        double[] Calculate(string calculationName, List<Tuple<string,double[]>> parameters);
    }
}

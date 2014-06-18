using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculation
{
    public interface ICalculationProvider
    {
        List<CalculationDefinition> Calculations { get; }
        IEnumerable<CalculationDefinition> GetCalculations(string categoryName);
        CalculationDefinition GetCalculation(string calculationName);
        List<string> GetParameterNames(string calculationName);
        List<string> GetNamesOfCalculators();
    }
}

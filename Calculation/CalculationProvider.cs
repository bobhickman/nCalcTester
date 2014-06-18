using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Calculation
{
    public abstract class CalculationProvider : ICalculationProvider
    {
        public virtual List<CalculationDefinition> Calculations { get; protected set; }

        public virtual IEnumerable<CalculationDefinition> GetCalculations(string categoryName)
        {
            return Calculations.Where(x => x.Category == categoryName);
        }

        public virtual CalculationDefinition GetCalculation(string calculationName)
        {
            return Calculations.Find(x => x.Name == calculationName);
        }

        public virtual List<string> GetParameterNames(string calculationName)
        {
            return Calculations.Find(x => x.Name == calculationName).Parameters.ToList();
        }

        public virtual List<string> GetNamesOfCalculators()
        {
            return Calculations.Select(x => x.Category).Distinct().ToList();
        }
    }
}

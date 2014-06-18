using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculation
{
    public class CalculationDefinition
    {
        // TODO: make instances of this immutable

        public string Category { get; set; }

        public string Formula { get; set; }

        public string Name { get; set; }

        public List<string> Parameters { get; set; }

        public override string ToString()
        {
            return string.Format("{0} : {1}", Category, Name);
        }
    }
}

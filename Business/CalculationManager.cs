using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Calculation;
using DataAccess;

namespace Business
{
    public interface ICalculationManager
    {
        List<CalculationResult> Calculate(CalculationRequest request);

        List<string> GetNamesOfCalculators();
        List<string> GetNamesOfCalculations(string calculatorName);
    }

    public class CalculationManager : ICalculationManager
    {
        private ICalculationProvider _calculationProvider;
        private IDataProvider _dataProvider;

        public CalculationManager(ICalculationProvider calculationProvider,
            IDataProvider dataProvider)
        {
            _calculationProvider = calculationProvider;
            _dataProvider = dataProvider;
        }

        public List<string> GetNamesOfCalculators()
        {
            return _calculationProvider.GetNamesOfCalculators();
        }

        public List<string> GetNamesOfCalculations(string calculatorName)
        {
            return GetCalculator(calculatorName).GetNamesOfCalculations();
        }

        public List<CalculationResult> Calculate(CalculationRequest request)
        {
            List<CalculationResult> results = new List<CalculationResult>();

            ICalculator calculator = GetCalculator(request.CalculatorName);
            foreach (string calculationName in request.CalculationNames)
            {
                List<string> parameterNames = _calculationProvider.GetParameterNames(calculationName);
                List<Tuple<string, double[]>> parameters = new List<Tuple<string, double[]>>();
                foreach (var parameterName in parameterNames)
                {
                    var values = _dataProvider.GetValues(parameterName);
                    parameters.Add(new Tuple<string,double[]>(parameterName, values));
                }

                var result = calculator.Calculate(calculationName, parameters);
                results.Add(new CalculationResult
                {
                    CalculationName = calculationName,
                    CalculatorName = request.CalculatorName,
                    Results = result
                });
            }
            
            return results;
        }

        private ICalculator GetCalculator(string calculatorName)
        {
            // TODO: Do this with reflection
            ICalculator calculator = null;
            switch (calculatorName)
            {
                case "Profiles":
                    calculator = new ProfilesNCalcCalculator(_calculationProvider);
                    break;
                case "Performance":
                    calculator = new PerformanceNCalcCalculator(_calculationProvider);
                    break;
            }
            return calculator;
        }
    }
}

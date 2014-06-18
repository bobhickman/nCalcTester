using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Business;
using Calculation;
using DataAccess;

namespace nCalcTester
{
    class Program
    {
        static void Main(string[] args)
        {
            // Convert these to IoC
            ICalculationProvider calculationProvider = NCalcCalculationProvider.Instance;// new NCalcCalculationProvider();
            IDataProvider dataProvider = new StaticDataProvider();
            ICalculationManager calculationManager = new CalculationManager(calculationProvider, dataProvider);

            foreach (string calculatorName in calculationManager.GetNamesOfCalculators())
            {
                List<string> calculationNames = calculationManager.GetNamesOfCalculations(calculatorName);
                CalculationRequest calculationRequest = new CalculationRequest
                {
                    CalculatorName = calculatorName,
                    CalculationNames = calculationNames,
                    Ids = new List<string>() { "id0", "id1", "id2", "id3", "id4", "id5", "id6", "id7" }
                };
                List<CalculationResult> calculationResults = calculationManager.Calculate(calculationRequest);
                Console.WriteLine("CALCULATOR: " + calculatorName);
                foreach (CalculationResult calculationResult in calculationResults)
                {
                    Console.WriteLine("CALCULATION: " + calculationResult.CalculationName);
                    foreach (double result in calculationResult.Results)
                        Console.WriteLine(result.ToString("N3"));
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            Console.WriteLine("Press any key to continue ... ");
            Console.ReadLine();
        }
    }
}

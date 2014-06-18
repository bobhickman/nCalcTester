using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NCalc;

namespace Calculation
{
    public abstract class NCalcCalculator : ICalculator
    {
        protected ICalculationProvider _calculationProvider = null;

        public NCalcCalculator(ICalculationProvider calculationProvider)
        {
            _calculationProvider = calculationProvider;
        }

        public virtual string Category { get; protected set; }
        
        public virtual List<string> GetNamesOfCalculations()
        {
            return _calculationProvider.GetCalculations(Category).Select(x => x.Name).ToList();
        }

        public abstract double[] Calculate(string calculationName, List<Tuple<string,double[]>> parameters);

        protected NCalc.Expression BuildExpression(CalculationDefinition calcdef, List<Tuple<string,double[]>> parameters)
        {
            NCalc.Expression e = new NCalc.Expression(calcdef.Formula);
            foreach (var parm in parameters)
                e.Parameters[parm.Item1] = parm.Item2;
            e.EvaluateFunction += delegate(string name, NCalc.FunctionArgs args)
            {
                NCalcFunctions.EvaluateFunction(name, args);
            };
            return e;
        }
    }

    public class ProfilesNCalcCalculator : NCalcCalculator
    {
        private static readonly string _category = "Profiles";

        public ProfilesNCalcCalculator(ICalculationProvider calculationProvider)
            :base(calculationProvider)
        {
            Category = _category;
        }

        public override double[] Calculate(string calculationName, List<Tuple<string,double[]>> parameters)
        {
            var calculation = _calculationProvider.GetCalculation(calculationName);
            Expression e = BuildExpression(calculation, parameters);
            e.Options = EvaluateOptions.IterateParameters;
            double[] results = ((List<object>)e.Evaluate()).Cast<double>().ToArray();
            return results;
        }
    }

    public class PerformanceNCalcCalculator : NCalcCalculator
    {
        private static readonly string _category = "Performance";

        public PerformanceNCalcCalculator(ICalculationProvider calculationProvider)
            : base(calculationProvider)
        {
            Category = _category;
        }

        public override double[] Calculate(string calculationName, List<Tuple<string, double[]>> parameters)
        {
            var calculation = _calculationProvider.GetCalculation(calculationName);
            Expression e = BuildExpression(calculation, parameters);
            var v = e.Evaluate();
            double[] results = new double[] { (double)e.Evaluate() };
            return results;
        }
    }

    // nCalc specific functions
    internal static class NCalcFunctions
    {
        internal static void EvaluateFunction(string name, FunctionArgs args)
        {
            switch (name)
            {
                case "Average":
                    Average(args);
                    break;
                case "Count":
                    Count(args);
                    break;
                case "Dabs":
                    Dabs(args);
                    break;
                case "Sum":
                    Sum(args);
                    break;
            }
        }

        private static void Average(FunctionArgs args)
        {
            double[] values = (double[])args.Parameters[0].Parameters.Values.First();
            args.Result = CoreCalculations.Average(values);
        }

        private static void Count(FunctionArgs args)
        {
            double count = ((double[])args.Parameters[0].Parameters.Values.First()).Count();
            args.Result = count;
        }

        /// <summary>
        /// Provide a Double override for Math.Abs because nCalc only uses the Decimal form
        /// </summary>
        /// <param name="args"></param>
        private static void Dabs(FunctionArgs args)
        {
            args.Parameters[0].Options = EvaluateOptions.None; // Bug? Doesn't work if IterateParameters is set
            args.Result = Math.Abs((double)args.Parameters[0].Evaluate());
        }

        private static void Sum(FunctionArgs args)
        {
            double[] values = (double[])args.Parameters[0].Parameters.Values.First();
            args.Result = CoreCalculations.Sum(values);
        }
    }
}

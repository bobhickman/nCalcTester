using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IDataProvider
    {
        double[] GetValues(string parameterName);
    }

    public class StaticDataProvider: IDataProvider
    {
        public static double NAN = double.NaN;

        public double[] GetValues(string parameterName)
        {
            double[] values = { };
            switch (parameterName)
            {
                // For IBESImpliedStandardDeviationOfFiscalYear1Estimates
                case "FY1_STD_DEV":
                    values = new double[] { 0.154, 0.132, 0.086, 0.081, 0.095, 1, NAN, 1};
                    break;
                case "ANNUAL_ACTUAL_EPS":
                    values = new double[] { 1.080, 1.820, 0.836, 0.836, 0.836, -1, 1, NAN};
                    break;
                // For IBESRolling12MonthEstimatedGrowthMedian
                case "MONTH_DIFF":
                    values = new double[] { 1, 5, 6, 7, 10, 11, 12, 13 };
                    break;
                case "FY1_MED_ESTIMATE":
                    values = new double[] { 1.080, 0.910, 1.672, 0.836, 0.836, 1, 1, 1 };
                    break;
                case "FY2_MED_ESTIMATE":
                    values = new double[] { NAN, NAN, NAN, 0.636, 0.936, -1, 2, 0.500 };
                    break;
                case "RETURN":
                    values = new double[] { 0.154, 0.132, 0.086, 0.081, 0.095, 0.010, 0.050, 0.001 };
                    break;
            }
            return values;
        }
    }
}

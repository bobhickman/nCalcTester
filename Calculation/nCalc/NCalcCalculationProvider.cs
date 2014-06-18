using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Calculation
{
    /// <summary>
    /// nCalc specific calculation provider
    /// </summary>
    public class NCalcCalculationProvider : CalculationProvider
    {
        private static readonly Lazy<NCalcCalculationProvider> _instance =
            new Lazy<NCalcCalculationProvider>(() => new NCalcCalculationProvider());

        private static readonly string _defaultXmlFile = "CalculationDefinitions.xml";

        private NCalcCalculationProvider()
        {
            //SaveXml();
            LoadXml();
        }

        public static ICalculationProvider Instance
        {
            get { return _instance.Value; }
        }

        private void LoadXml()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<CalculationDefinition>));
            using (FileStream stream = new FileStream(_defaultXmlFile, FileMode.Open))
            {
                Calculations = (List<CalculationDefinition>)serializer.Deserialize(stream);
            }
        }

        private void SaveXml()
        {
            //CreateCalculations();
            XmlSerializer serializer = new XmlSerializer(typeof(List<CalculationDefinition>));
            using (FileStream stream = new FileStream(_defaultXmlFile, FileMode.Create))
            {
                serializer.Serialize(stream, Calculations);
            }
        }

        private void CreateCalculations()
        {
            // TODO: Load from xml
            Calculations = new List<CalculationDefinition>()
            {
                {
                    new CalculationDefinition 
                    {
                        Category = "Profiles",
                        Formula = "100 * (FY1_STD_DEV/Dabs(ANNUAL_ACTUAL_EPS))", 
                        Name = "IBESImpliedStandardDeviationOfFiscalYear1Estimates", 
                        Parameters = new List<string> { "FY1_STD_DEV", "ANNUAL_ACTUAL_EPS" }
                    }
                },
                {
                    new CalculationDefinition
                    {
                        Category = "Profiles",
                        Formula = "100 * if( MONTH_DIFF < 7, (FY1_MED_ESTIMATE/ANNUAL_ACTUAL_EPS)-1, if( MONTH_DIFF < 12, ((FY1_MED_ESTIMATE+FY2_MED_ESTIMATE)/(ANNUAL_ACTUAL_EPS+FY1_MED_ESTIMATE))-1, (FY2_MED_ESTIMATE/FY1_MED_ESTIMATE)-1 ))",
                        Name = "IBESRolling12MonthEstimatedGrowthMedian",
                        Parameters = new List<string> { "MONTH_DIFF", "FY1_MED_ESTIMATE", "FY2_MED_ESTIMATE", "ANNUAL_ACTUAL_EPS" }
                    }    
                },
                {
                    new CalculationDefinition
                    {
                        Category = "Performance",
                        Formula = "100 * (Sum(RETURN)/Count(RETURN))",
                        Name = "AverageReturn",
                        Parameters = new List<string> { "RETURN" }
                    }
                }
            };
        }
    }
}

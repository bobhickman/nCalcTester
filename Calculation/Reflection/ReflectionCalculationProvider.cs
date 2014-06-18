using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculation
{
    /// <summary>
    /// Provides calculators by reflection loading them from assemblies
    /// </summary>
    public class ReflectionCalculationProvider : CalculationProvider
    {
        private static readonly Lazy<ReflectionCalculationProvider> _instance =
           new Lazy<ReflectionCalculationProvider>(() => new ReflectionCalculationProvider());
    
        private static readonly string _defaultFolder = @".\CalculationDefinitions";

        private ReflectionCalculationProvider()
        {
            // Reflection load all the assemblies 
            // Find all the ReflectionCalculators defined and hold onto them
            // Create the CalculationDefinitions for each one
            // Attach a file watcher in case a new one gets dropped in
        }

        public static ICalculationProvider Instance
        {
            get { return _instance.Value; }
        }
    }
}

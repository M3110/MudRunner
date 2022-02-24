using System.IO;

namespace MudRunner.Suspension.Core.Models
{
    /// <summary>
    /// It contains the base paths used in the application.
    /// </summary>
    public static class BasePaths
    {
        /// <summary>
        /// The application base path.
        /// </summary>
        public static string Application => Directory.GetCurrentDirectory().Replace("\\src\\Suspension.Application", "\\");

        /// <summary>
        /// The base path to solution response files.
        /// </summary>
        public static string Solution => Path.Combine(BasePaths.Application, "solutions");

        /// <summary>
        /// The base path to response files of analysis operations.
        /// </summary>
        public static string Analysis => Path.Combine(BasePaths.Solution, "Analysis");

        /// <summary>
        /// The base path to response files of dynamic analysis operations.
        /// </summary>
        public static string DynamicAnalysis => Path.Combine(BasePaths.Analysis, "Dynamic");

        /// <summary>
        /// The base path to response files of dynamic analysis operations that considers half car.
        /// </summary>
        public static string HalfCarAnalysis => Path.Combine(BasePaths.DynamicAnalysis, "Half Car");

        /// <summary>
        /// The base path to response files of dynamic analysis operations that considers half car and six degrees of freedom.
        /// </summary>
        public static string HalfCarSixDofAnalysis => Path.Combine(BasePaths.HalfCarAnalysis, "6 Degrees of Freedom");

        /// <summary>
        /// The base path to response files of dynamic amplitude analysis operations that considers half car and six degrees of freedom.
        /// </summary>
        public static string HalfCarSixDofAmplitudeAnalysis => Path.Combine(BasePaths.HalfCarSixDofAnalysis, "Amplitude");
    }
}

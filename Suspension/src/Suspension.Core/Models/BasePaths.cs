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
        public static string Application => Directory.GetCurrentDirectory().Replace("\\src\\SoftTissue.Application", "\\");

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
    }
}

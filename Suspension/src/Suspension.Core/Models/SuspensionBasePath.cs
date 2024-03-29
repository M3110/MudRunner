﻿using System.IO;

namespace MudRunner.Suspension.Core.Models
{
    /// <summary>
    /// It contains the base paths used in the suspension project.
    /// </summary>
    public static class SuspensionBasePath
    {
        /// <summary>
        /// The application base path.
        /// </summary>
        public static string Application => Directory.GetCurrentDirectory().Replace("\\src\\Suspension.Application", "\\");

        /// <summary>
        /// The base path to files generated by application.
        /// </summary>
        public static string Files => Path.Combine(SuspensionBasePath.Application, "files");

        /// <summary>
        /// The base path to response files of analysis operations.
        /// </summary>
        public static string Analysis => Path.Combine(SuspensionBasePath.Files, "Analysis");

        /// <summary>
        /// The base path to response files of dynamic analysis operations.
        /// </summary>
        public static string DynamicAnalysis => Path.Combine(SuspensionBasePath.Analysis, "Dynamic");

        /// <summary>
        /// The base path to response files of dynamic analysis operations that considers half car.
        /// </summary>
        public static string HalfCarAnalysis => Path.Combine(SuspensionBasePath.DynamicAnalysis, "Half Car");

        /// <summary>
        /// The base path to response files of dynamic analysis operations that considers half car and six degrees of freedom.
        /// </summary>
        public static string HalfCarSixDofAnalysis => Path.Combine(SuspensionBasePath.HalfCarAnalysis, "6 Degrees of Freedom");

        /// <summary>
        /// The base path to response files of dynamic amplitude analysis operations that considers half car and six degrees of freedom.
        /// </summary>
        public static string HalfCarSixDofAmplitudeAnalysis => Path.Combine(SuspensionBasePath.HalfCarSixDofAnalysis, "Amplitude");
    }
}

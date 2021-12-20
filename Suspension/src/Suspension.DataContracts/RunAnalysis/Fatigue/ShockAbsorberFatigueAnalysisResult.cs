using Suspension.DataContracts.Models;

namespace Suspension.DataContracts.RunAnalysis.Fatigue
{
    /// <summary>
    /// It contains the essential fatigue analysis results for shock absorber.
    /// </summary>
    public class ShockAbsorberFatigueAnalysisResult
    {
        /// <summary>
        /// Unit: N (Newton).
        /// </summary>
        public Force MeanForce { get; set; }

        /// <summary>
        /// Unit: N (Newton).
        /// </summary>
        public Force ForceAmplitude { get; set; }
    }
}

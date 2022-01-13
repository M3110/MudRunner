using MudRunner.Commons.DataContracts.Operation;
using System.Collections.Generic;
using System.Linq;

namespace MudRunner.Suspension.DataContracts.RunAnalysis.Fatigue
{
    /// <summary>
    /// It represents the 'data' content of RunFatigueAnalysis operation response.
    /// </summary>
    public class RunFatigueAnalysisResponseData : OperationResponseData
    {
        /// <summary>
        /// True, if analysis failed. False, otherwise.
        /// </summary>
        public bool AnalisysFailed => SafetyFactor < 1;

        /// <summary>
        /// The safety factor.
        /// </summary>
        public double SafetyFactor => new List<double> 
        { 
            (UpperWishboneResult?.SafetyFactor).GetValueOrDefault(), 
            (LowerWishboneResult?.SafetyFactor).GetValueOrDefault(),
            (TieRodResult?.SafetyFactor).GetValueOrDefault()
        }.Min();

        /// <summary>
        /// The Von-Misses equivalent stress safety factor.
        /// </summary>
        public double StressSafetyFactor => new List<double>
        {
            (UpperWishboneResult?.StressSafetyFactor).GetValueOrDefault(),
            (LowerWishboneResult?.StressSafetyFactor).GetValueOrDefault(),
            (TieRodResult?.StressSafetyFactor).GetValueOrDefault()
        }.Min();

        /// <summary>
        /// The buckling safety factor.
        /// </summary>
        public double BucklingSafetyFactor => new List<double>
        {
            (UpperWishboneResult?.BucklingSafetyFactor).GetValueOrDefault(),
            (LowerWishboneResult?.BucklingSafetyFactor).GetValueOrDefault(),
            (TieRodResult?.BucklingSafetyFactor).GetValueOrDefault()
        }.Min();

        /// <summary>
        /// The fatigue safety factor.
        /// </summary>
        public double FatigueSafetyFactor => new List<double>
        {
            (UpperWishboneResult?.FatigueSafetyFactor).GetValueOrDefault(),
            (LowerWishboneResult?.FatigueSafetyFactor).GetValueOrDefault(),
            (TieRodResult?.FatigueSafetyFactor).GetValueOrDefault()
        }.Min();

        /// <summary>
        /// The fatigue number of cycles.
        /// </summary>
        public double FatigueNumberOfCycles => new List<double>
        {
            (UpperWishboneResult?.FatigueNumberOfCycles).GetValueOrDefault(),
            (LowerWishboneResult?.FatigueNumberOfCycles).GetValueOrDefault(),
            (TieRodResult?.FatigueNumberOfCycles).GetValueOrDefault()
        }.Min();

        /// <summary>
        /// The force reactions at shock absorber.
        /// </summary>
        public ShockAbsorberFatigueAnalysisResult ShockAbsorberResult { get; set; }

        /// <summary>
        /// The analysis result to upper wishbone.
        /// </summary>
        public WishboneFatigueAnalysisResult UpperWishboneResult { get; set; }

        /// <summary>
        /// The analysis result to lower wishbone.
        /// </summary>
        public WishboneFatigueAnalysisResult LowerWishboneResult { get; set; }

        /// <summary>
        /// The analysis result to tie rod.
        /// </summary>
        public SingleComponentFatigueAnalysisResult TieRodResult { get; set; }
    }
}

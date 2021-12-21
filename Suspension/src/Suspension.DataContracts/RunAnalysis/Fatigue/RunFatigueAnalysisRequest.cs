using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using MudRunner.Suspension.DataContracts.Models.Enums;
using MudRunner.Suspension.DataContracts.Models.Profiles;
using MudRunner.Suspension.DataContracts.Models.SuspensionComponents;
using MudRunner.Suspension.DataContracts.OperationBase;

namespace MudRunner.Suspension.DataContracts.RunAnalysis.Fatigue
{
    /// <summary>
    /// It represents the request content of RunFatigueAnalysis operation.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class RunFatigueAnalysisRequest<TProfile> : OperationRequestBase
        where TProfile : Profile
    {
        /// <summary>
        /// True, if result should be rounded. False, otherwise.
        /// </summary>
        public bool ShouldRoundResults { get; set; }

        /// <summary>
        /// The number of decimals that should be rounded in results.
        /// </summary>
        public int? NumberOfDecimalsToRound { get; set; }

        /// <summary>
        /// The material.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public MaterialType Material { get; set; }

        /// <summary>
        /// The origin considered to analysis.
        /// </summary>
        /// <example>x,y,z</example>
        public string Origin { get; set; }

        /// <summary>
        /// The maximum applied force.
        /// Unit: N (Newton).
        /// </summary>
        /// <example>x,y,z</example>
        public string MaximumAppliedForce { get; set; }

        /// <summary>
        /// The minimum applied force.
        /// Unit: N (Newton).
        /// </summary>
        /// <example>x,y,z</example>
        public string MinimumAppliedForce { get; set; }

        /// <summary>
        /// The fatigue stress concentration factor.
        /// Dimensionless.
        /// </summary>
        public double FatigueStressConcentrationFactor { get; set; }

        /// <summary>
        /// The fatigue limit (Se').
        /// Unit: MPa (Mega Pascal).
        /// </summary>
        public double FatigueLimit { get; set; }

        /// <summary>
        /// The fatigue limit fraction (f).
        /// Dimensionless.
        /// </summary>
        public double FatigueLimitFraction { get; set; }

        /// <summary>
        /// True, if is rotative section. False, otherwise.
        /// </summary>
        public bool IsRotativeSection { get; set; }

        /// <summary>
        /// The reliability for project to fatigue analysis.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Reliability Reliability { get; set; }

        /// <summary>
        /// The surface finish for fatigue analysis.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public SurfaceFinish SurfaceFinish { get; set; }

        /// <summary>
        /// The shock absorber.
        /// </summary>
        public ShockAbsorber ShockAbsorber { get; set; }

        /// <summary>
        /// The suspension A-arm upper.
        /// </summary>
        public SuspensionAArm<TProfile> SuspensionAArmUpper { get; set; }

        /// <summary>
        /// The suspension A-arm lower.
        /// </summary>
        public SuspensionAArm<TProfile> SuspensionAArmLower { get; set; }

        /// <summary>
        /// The tie rod.
        /// </summary>
        public TieRod<TProfile> TieRod { get; set; }
    }
}

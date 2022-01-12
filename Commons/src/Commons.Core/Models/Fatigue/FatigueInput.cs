using MudRunner.Commons.DataContracts.Models.Enums;
using MudRunner.Commons.DataContracts.Models.Profiles;

namespace MudRunner.Commons.Core.Models.Fatigue
{
    /// <summary>
    /// It contains the input for fatigue analysis.
    /// </summary>
    public class FatigueInput<TProfile>
        where TProfile : Profile
    {
        /// <summary>
        /// The minimum applied stress in component.
        /// Unit: MPa (Mega Pascal).
        /// </summary>
        public double MinimumAppliedStress { get; set; }

        /// <summary>
        /// The maximum applied stress in component.
        /// Unit: MPa (Mega Pascal).
        /// </summary>
        public double MaximumAppliedStress { get; set; }

        /// <summary>
        /// Sut.
        /// Unit: MPa (Mega Pascal).
        /// </summary>
        public double TensileStress { get; set; }
        
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
        public Reliability Reliability { get; set; }

        /// <summary>
        /// The surface finish for fatigue analysis.
        /// </summary>
        public SurfaceFinish SurfaceFinish { get; set; }

        /// <summary>
        /// The loading types for fatigue analysis.
        /// </summary>
        public LoadingType LoadingType { get; set; }

        /// <summary>
        /// Unit: ºC (Celsius).
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// The profile.
        /// </summary>
        public TProfile Profile { get; set; }
    }
}

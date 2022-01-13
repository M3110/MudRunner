using MudRunner.Commons.DataContracts.Models.Profiles;

namespace MudRunner.Suspension.Core.Models.SuspensionComponents
{
    /// <summary>
    /// It represents the suspension system.
    /// </summary>
    public class SuspensionSystem
    {
        /// <summary>
        /// The shock absorber.
        /// </summary>
        public ShockAbsorber ShockAbsorber { get; set; }

        /// <summary>
        /// The upper wishbone.
        /// </summary>
        public Wishbone UpperWishbone { get; set; }

        /// <summary>
        /// The lower wishbone.
        /// </summary>
        public Wishbone LowerWishbone { get; set; }

        /// <summary>
        /// The tie rod.
        /// </summary>
        public TieRod TieRod { get; set; }
    }

    /// <summary>
    /// It represents the suspension system.
    /// </summary>
    public class SuspensionSystem<TProfile>
        where TProfile : Profile
    {
        /// <summary>
        /// The shock absorber.
        /// </summary>
        public ShockAbsorber ShockAbsorber { get; set; }

        /// <summary>
        /// The upper wishbone.
        /// </summary>
        public Wishbone<TProfile> UpperWishbone { get; set; }

        /// <summary>
        /// The lower wishbone.
        /// </summary>
        public Wishbone<TProfile> LowerWishbone { get; set; }

        /// <summary>
        /// The tie rod.
        /// </summary>
        public TieRod<TProfile> TieRod { get; set; }
    }
}

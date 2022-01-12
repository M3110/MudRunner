using MudRunner.Commons.DataContracts.Models.Profiles;

namespace MudRunner.Suspension.DataContracts.Models.SuspensionComponents
{
    /// <summary>
    /// It represents the tie rod.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class TieRod<TProfile> : TieRodPoint
        where TProfile : Profile
    {
        /// <summary>
        /// The profile.
        /// </summary>
        public TProfile Profile { get; set; }
    }
}

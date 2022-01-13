using MudRunner.Commons.DataContracts.Models.Profiles;

namespace MudRunner.Suspension.DataContracts.Models.SuspensionComponents
{
    /// <summary>
    /// It represents the wishbone.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class Wishbone<TProfile> : WishbonePoint
        where TProfile : Profile
    {
        /// <summary>
        /// The profile.
        /// </summary>
        public TProfile Profile { get; set; }
    }
}

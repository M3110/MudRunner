using Suspension.DataContracts.Models.Profiles;

namespace Suspension.DataContracts.Models.SuspensionComponents
{
    /// <summary>
    /// It represents the suspension A-arm.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class SuspensionAArm<TProfile> : SuspensionAArmPoint
        where TProfile : Profile
    {
        /// <summary>
        /// The profile.
        /// </summary>
        public TProfile Profile { get; set; }
    }
}

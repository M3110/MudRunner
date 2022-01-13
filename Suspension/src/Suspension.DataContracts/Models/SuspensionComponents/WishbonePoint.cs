using MudRunner.Commons.DataContracts.Models.Profiles;

namespace MudRunner.Suspension.DataContracts.Models.SuspensionComponents
{
    /// <summary>
    /// It represents the point to wishbone.
    /// </summary>
    public class WishbonePoint
    {
        /// <summary>
        /// The point of fastening with steering knuckle.
        /// </summary>
        public string OuterBallJoint { get; set; }

        /// <summary>
        /// The front pivot point.
        /// This geometry has two pivot point.
        /// </summary>
        public string FrontPivot { get; set; }

        /// <summary>
        /// The rear pivot point.
        /// This geometry has two pivot point.
        /// </summary>
        public string RearPivot { get; set; }

        /// <summary>
        /// This method creates a <see cref="WishbonePoint"/> based on <see cref="Wishbone{TProfile}"/>.
        /// </summary>
        /// <typeparam name="TProfile"></typeparam>
        /// <param name="wishbone"></param>
        /// <returns></returns>
        public static WishbonePoint Create<TProfile>(Wishbone<TProfile> wishbone)
            where TProfile : Profile
        {
            return new WishbonePoint
            {
                OuterBallJoint = wishbone.OuterBallJoint,
                FrontPivot = wishbone.FrontPivot,
                RearPivot = wishbone.RearPivot
            };
        }
    }
}

using MudRunner.Commons.Core.ExtensionMethods;
using MudRunner.Commons.DataContracts.Models;
using MudRunner.Commons.DataContracts.Models.Enums;
using MudRunner.Commons.DataContracts.Models.Profiles;
using MudRunner.Suspension.DataContracts.Models.SuspensionComponents;
using DataContract = MudRunner.Suspension.DataContracts.Models.SuspensionComponents;

namespace MudRunner.Suspension.Core.Models.SuspensionComponents
{
    /// <summary>
    /// It represents the wishbone.
    /// </summary>
    public class Wishbone
    {
        /// <summary>
        /// The absolut applied force to one segment of wishbone.
        /// </summary>
        public double AppliedForce1 { get; set; }

        /// <summary>
        /// The absolut applied force to another segment of wishbone.
        /// </summary>
        public double AppliedForce2 { get; set; }

        /// <summary>
        /// The point of fastening with steering knuckle.
        /// </summary>
        public Point3D OuterBallJoint { get; set; }

        /// <summary>
        /// The front pivot point.
        /// This geometry has two pivot point.
        /// </summary>
        public Point3D FrontPivot { get; set; }

        /// <summary>
        /// The rear pivot point.
        /// This geometry has two pivot point.
        /// </summary>
        public Point3D RearPivot { get; set; }

        /// <summary>
        /// The vector that represents the direction of wishbone to one segment of wishbone.
        /// </summary>
        public Vector3D VectorDirection1 => Vector3D.Create(this.FrontPivot, this.OuterBallJoint);

        /// <summary>
        /// The vector that represents the direction of wishbone to another segment of wishbone.
        /// </summary>
        public Vector3D VectorDirection2 => Vector3D.Create(this.RearPivot, this.OuterBallJoint);

        /// <summary>
        /// The normalized vector that represents the direction of wishbone to one segment of wishbone.
        /// </summary>
        public Vector3D NormalizedDirection1 => this.VectorDirection1.Normalize();

        /// <summary>
        /// The normalized vector that represents the direction of wishbone to anoher segment of wishbone.
        /// </summary>
        public Vector3D NormalizedDirection2 => this.VectorDirection2.Normalize();

        /// <summary>
        /// The length to one segment of wishbone.
        /// </summary>
        public double Length1 => this.VectorDirection1.Length;

        /// <summary>
        /// The length to another segment of wishbone.
        /// </summary>
        public double Length2 => this.VectorDirection2.Length;

        /// <summary>
        /// This method creates a <see cref="Wishbone"/> based on <see cref="WishbonePoint"/>.
        /// </summary>
        /// <param name="wishbone"></param>
        /// <param name="appliedForce1"></param>
        /// <param name="appliedForce2"></param>
        /// <returns></returns>
        public static Wishbone Create(WishbonePoint wishbone, double appliedForce1 = 0, double appliedForce2 = 0)
        {
            return new Wishbone
            {
                OuterBallJoint = Point3D.Create(wishbone.OuterBallJoint),
                FrontPivot = Point3D.Create(wishbone.FrontPivot),
                RearPivot = Point3D.Create(wishbone.RearPivot),
                AppliedForce1 = appliedForce1,
                AppliedForce2 = appliedForce2
            };
        }
    }

    /// <summary>
    /// It represents the wishbone.
    /// </summary>
    public class Wishbone<TProfile> : Wishbone
        where TProfile : Profile
    {
        /// <summary>
        /// The material.
        /// </summary>
        public Material Material { get; set; }

        /// <summary>
        /// The profile.
        /// </summary>
        public TProfile Profile { get; set; }

        /// <summary>
        /// This method creates a <see cref="Wishbone{TProfile}"/> based on <see cref="DataContract.Wishbone{TProfile}"/>.
        /// </summary>
        /// <param name="wishbone"></param>
        /// <param name="material"></param>
        /// <param name="appliedForce1"></param>
        /// <param name="appliedForce2"></param>
        /// <returns></returns>
        public static Wishbone<TProfile> Create(DataContract.Wishbone<TProfile> wishbone, MaterialType material, double appliedForce1 = 0, double appliedForce2 = 0)
        {
            return new Wishbone<TProfile>
            {
                OuterBallJoint = Point3D.Create(wishbone.OuterBallJoint),
                FrontPivot = Point3D.Create(wishbone.FrontPivot),
                RearPivot = Point3D.Create(wishbone.RearPivot),
                Profile = wishbone.Profile,
                AppliedForce1 = appliedForce1,
                AppliedForce2 = appliedForce2,
                Material = Material.Create(material)
            };
        }
    }
}

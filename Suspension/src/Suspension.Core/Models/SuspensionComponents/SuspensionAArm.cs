using SuspensionAnalysis.Core.ExtensionMethods;
using SuspensionAnalysis.DataContracts.Models;
using SuspensionAnalysis.DataContracts.Models.Enums;
using SuspensionAnalysis.DataContracts.Models.Profiles;
using DataContract = SuspensionAnalysis.DataContracts.Models.SuspensionComponents;

namespace SuspensionAnalysis.Core.Models.SuspensionComponents
{
    /// <summary>
    /// It represents the suspension wishbone.
    /// </summary>
    public class SuspensionWishbone
    {
        /// <summary>
        /// The absolut applied force to one segment of suspension wishbone.
        /// </summary>
        public double AppliedForce1 { get; set; }

        /// <summary>
        /// The absolut applied force to another segment of suspension wishbone.
        /// </summary>
        public double AppliedForce2 { get; set; }

        /// <summary>
        /// The point of fastening with steering knuckle.
        /// </summary>
        public Point3D OuterBallJoint { get; set; }

        /// <summary>
        /// The pivot point.
        /// This geometry has two pivot point.
        /// </summary>
        public Point3D FrontPivot { get; set; }

        /// <summary>
        /// The pivot point.
        /// This geometry has two pivot point.
        /// </summary>
        public Point3D RearPivot { get; set; }

        /// <summary>
        /// The vector that represents the direction of suspension wishbone to one segment of suspension wishbone.
        /// </summary>
        public Vector3D VectorDirection1 => Vector3D.Create(this.FrontPivot, this.OuterBallJoint);
        
        /// <summary>
        /// The vector that represents the direction of suspension wishbone to another segment of suspension wishbone.
        /// </summary>
        public Vector3D VectorDirection2 => Vector3D.Create(this.RearPivot, this.OuterBallJoint);

        /// <summary>
        /// The normalized vector that represents the direction of suspension wishbone to one segment of suspension wishbone.
        /// </summary>
        public Vector3D NormalizedDirection1 => this.VectorDirection1.Normalize();
        
        /// <summary>
        /// The normalized vector that represents the direction of suspension wishbone to anoher segment of suspension wishbone.
        /// </summary>
        public Vector3D NormalizedDirection2 => this.VectorDirection2.Normalize();

        /// <summary>
        /// The length to one segment of suspension wishbone.
        /// </summary>
        public double Length1 => this.VectorDirection1.Length;

        /// <summary>
        /// The length to another segment of suspension wishbone.
        /// </summary>
        public double Length2 => this.VectorDirection2.Length;

        /// <summary>
        /// This method creates a <see cref="SuspensionWishbone"/> based on <see cref="DataContract.SuspensionWishbonePoint"/>.
        /// </summary>
        /// <param name="suspensionWishbone"></param>
        /// <param name="appliedForce1"></param>
        /// <param name="appliedForce2"></param>
        /// <returns></returns>
        public static SuspensionWishbone Create(DataContract.SuspensionWishbonePoint suspensionWishbone, double appliedForce1 = 0, double appliedForce2 = 0)
        {
            return new SuspensionWishbone
            {
                OuterBallJoint = Point3D.Create(suspensionWishbone.OuterBallJoint),
                FrontPivot = Point3D.Create(suspensionWishbone.FrontPivot),
                RearPivot = Point3D.Create(suspensionWishbone.RearPivot),
                AppliedForce1 = appliedForce1,
                AppliedForce2 = appliedForce2
            };
        }
    }

    /// <summary>
    /// It represents the suspension wishbone.
    /// </summary>
    public class SuspensionWishbone<TProfile> : SuspensionWishbone
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
        /// This method creates a <see cref="SuspensionWishbone{TProfile}"/> based on <see cref="DataContract.SuspensionWishbone{TProfile}"/>.
        /// </summary>
        /// <param name="suspensionWishbone"></param>
        /// <param name="material"></param>
        /// <param name="appliedForce1"></param>
        /// <param name="appliedForce2"></param>
        /// <returns></returns>
        public static SuspensionWishbone<TProfile> Create(DataContract.SuspensionWishbone<TProfile> suspensionWishbone, MaterialType material, double appliedForce1 = 0, double appliedForce2 = 0)
        {
            return new SuspensionWishbone<TProfile>
            {
                OuterBallJoint = Point3D.Create(suspensionWishbone.OuterBallJoint),
                FrontPivot = Point3D.Create(suspensionWishbone.FrontPivot),
                RearPivot = Point3D.Create(suspensionWishbone.RearPivot),
                Profile = suspensionWishbone.Profile,
                AppliedForce1 = appliedForce1,
                AppliedForce2 = appliedForce2,
                Material = Material.Create(material)
            };
        }
    }
}

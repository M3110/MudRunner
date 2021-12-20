using Suspension.Core.ExtensionMethods;
using Suspension.DataContracts.Models;
using Suspension.DataContracts.Models.Profiles;

namespace Suspension.Core.Models.SuspensionComponents
{
    /// <summary>
    /// It contains the points to a single suspension component.
    /// </summary>
    public class SingleComponent
    {
        /// <summary>
        /// The absolut applied force.
        /// </summary>
        public double AppliedForce { get; set; }

        /// <summary>
        /// The pivot point at chassis.
        /// </summary>
        public Point3D PivotPoint { get; set; }

        /// <summary>
        /// The fastening point.
        /// </summary>
        public Point3D FasteningPoint { get; set; }

        /// <summary>
        /// The vector that represents the direction of single component.
        /// </summary>
        public Vector3D VectorDirection => Vector3D.Create(this.PivotPoint, this.FasteningPoint);

        /// <summary>
        /// The normalized vector that represents the direction of single component.
        /// </summary>
        public Vector3D NormalizedDirection => this.VectorDirection.Normalize();

        /// <summary>
        /// The length.
        /// </summary>
        public double Length => this.VectorDirection.Length;
    }

    /// <summary>
    /// It contains the points to a single suspension component.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class SingleComponent<TProfile> : SingleComponent
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
    }
}

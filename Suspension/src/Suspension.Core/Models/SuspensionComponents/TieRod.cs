using Suspension.DataContracts.Models;
using Suspension.DataContracts.Models.Enums;
using Suspension.DataContracts.Models.Profiles;
using DataContract = Suspension.DataContracts.Models.SuspensionComponents;

namespace Suspension.Core.Models.SuspensionComponents
{
    /// <summary>
    /// It represents the tie rod.
    /// </summary>
    public class TieRod : SingleComponent
    {
        /// <summary>
        /// This method creates a <see cref="TieRod"/> based on <see cref="DataContract.TieRodPoint"/>.
        /// </summary>
        /// <param name="tieRod"></param>
        /// <param name="appliedForce"></param>
        /// <returns></returns>
        public static TieRod Create(DataContract.TieRodPoint tieRod, double appliedForce = 0)
        {
            return new TieRod
            {
                FasteningPoint = Point3D.Create(tieRod.FasteningPoint),
                PivotPoint = Point3D.Create(tieRod.PivotPoint),
                AppliedForce = appliedForce
            };
        }
    }

    /// <summary>
    /// It represents the tie rod.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class TieRod<TProfile> : SingleComponent<TProfile>
        where TProfile : Profile
    {
        /// <summary>
        /// This method creates a <see cref="TieRod{TProfile}"/> based on <see cref="DataContract.TieRod{TProfile}"/>.
        /// </summary>
        /// <param name="tieRod"></param>
        /// <param name="material"></param>
        /// <param name="appliedForce"></param>
        /// <returns></returns>
        public static TieRod<TProfile> Create(DataContract.TieRod<TProfile> tieRod, MaterialType material, double appliedForce = 0)
        {
            return new TieRod<TProfile>
            {
                FasteningPoint = Point3D.Create(tieRod.FasteningPoint),
                PivotPoint = Point3D.Create(tieRod.PivotPoint),
                AppliedForce = appliedForce,
                Profile = tieRod.Profile,
                Material = Material.Create(material)
            };
        }
    }
}

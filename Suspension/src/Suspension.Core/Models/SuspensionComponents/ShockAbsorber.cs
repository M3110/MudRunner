using MudRunner.Commons.DataContracts.Models;
using DataContract = MudRunner.Suspension.DataContracts.Models.SuspensionComponents;

namespace MudRunner.Suspension.Core.Models.SuspensionComponents
{
    /// <summary>
    /// It represents the shock absorber.
    /// </summary>
    public class ShockAbsorber : SingleComponent 
    {
        /// <summary>
        /// This method creates a <see cref="ShockAbsorber"/> based on <see cref="DataContract.ShockAbsorberPoint"/>.
        /// </summary>
        /// <param name="shockAbsorber"></param>
        /// <param name="appliedForce"></param>
        /// <returns></returns>
        public static ShockAbsorber Create(DataContract.ShockAbsorberPoint shockAbsorber, double appliedForce = 0)
        {
            return new ShockAbsorber
            {
                FasteningPoint = Point3D.Create(shockAbsorber.FasteningPoint),
                PivotPoint = Point3D.Create(shockAbsorber.PivotPoint),
                AppliedForce = appliedForce
            };
        }
    }
}

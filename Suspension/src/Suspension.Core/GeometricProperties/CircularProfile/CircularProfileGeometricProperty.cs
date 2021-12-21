using System;
using DataContract = MudRunner.Suspension.DataContracts.Models.Profiles;

namespace MudRunner.Suspension.Core.GeometricProperties.CircularProfile
{
    /// <summary>
    /// It is responsible to calculate the geometric properties to circular profile.
    /// </summary>
    public class CircularProfileGeometricProperty : GeometricProperty<DataContract.CircularProfile>, ICircularProfileGeometricProperty
    {
        /// <inheritdoc/>
        public override double CalculateArea(DataContract.CircularProfile profile)
        {
            return profile.Thickness.HasValue ?
                (Math.PI / 4) * (Math.Pow(profile.Diameter, 2) - Math.Pow(profile.Diameter - 2 * profile.Thickness.Value, 2)) 
                : (Math.PI / 4) * Math.Pow(profile.Diameter, 2);
        }

        /// <inheritdoc/>
        public override double CalculateMomentOfInertia(DataContract.CircularProfile profile)
        {
            return profile.Thickness.HasValue ?
                (Math.PI / 64) * (Math.Pow(profile.Diameter, 4) - Math.Pow(profile.Diameter - 2 * profile.Thickness.Value, 4))
                : (Math.PI / 64) * Math.Pow(profile.Diameter, 4);
        }
    }
}

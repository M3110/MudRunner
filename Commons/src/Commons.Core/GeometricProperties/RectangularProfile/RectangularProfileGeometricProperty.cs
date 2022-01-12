using DataContract = MudRunner.Commons.DataContracts.Models.Profiles;
using System;

namespace MudRunner.Commons.Core.GeometricProperties.RectangularProfile
{
    /// <summary>
    /// It is responsible to calculate the geometric properties to rectangular profile.
    /// </summary>
    public class RectangularProfileGeometricProperty : GeometricProperty<DataContract.RectangularProfile>, IRectangularProfileGeometricProperty
    {
        /// <inheritdoc/>
        public override double CalculateArea(DataContract.RectangularProfile profile)
        {
            return profile.Thickness.HasValue ?
                profile.Width * profile.Height - (profile.Width - 2 * profile.Thickness.Value) * (profile.Height - 2 * profile.Thickness.Value)
                : profile.Width * profile.Height;
        }

        /// <inheritdoc/>
        public override double CalculateMomentOfInertia(DataContract.RectangularProfile profile)
        {
            return profile.Thickness.HasValue ?
                (Math.Pow(profile.Height, 3) * profile.Width - Math.Pow(profile.Height - 2 * profile.Thickness.Value, 3) * (profile.Width - 2 * profile.Thickness.Value)) / 12
                : Math.Pow(profile.Height, 3) * profile.Width / 12;
        }
    }
}

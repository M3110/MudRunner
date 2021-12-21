using Suspension.Core.Models;
using Suspension.DataContracts.Models.Profiles;
using System;

namespace Suspension.Core.GeometricProperties
{
    /// <summary>
    /// It is responsible to calculate the geometric properties to a profile.
    /// </summary>
    public class GeometricProperty
    {
        /// <summary>
        /// This method validates a geometric property.
        /// Parameters that can be validated: length, area, moment of inertia.
        /// </summary>
        /// <param name="geometricProperty"></param>
        /// <param name="nameOfVariable"></param>
        public static void Validate(double geometricProperty, string nameOfVariable)
        {
            if (geometricProperty <= 0 || Constants.InvalidValues.Contains(geometricProperty))
            {
                throw new ArgumentOutOfRangeException(nameof(geometricProperty), $"The {nameOfVariable} cannot be equals to {geometricProperty}. The {nameOfVariable} must be grether than zero.");
            }
        }
    }

    /// <summary>
    /// It is responsible to calculate the geometric properties to a profile.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public abstract class GeometricProperty<TProfile> : GeometricProperty, IGeometricProperty<TProfile>
        where TProfile : Profile
    {
        /// <inheritdoc/>
        public abstract double CalculateArea(TProfile profile);

        /// <inheritdoc/>
        public abstract double CalculateMomentOfInertia(TProfile profile);
    }
}

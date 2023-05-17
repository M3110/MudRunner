using MudRunner.Suspension.DataContracts.Models.Enums;
using System;

namespace MudRunner.Suspension.Core.Models.SuspensionComponents.SteeringKnuckle
{
    /// <summary>
    /// It contains the necessary information about whatever bearing.
    /// </summary>
    public struct Bearing
    {
        /// <summary>
        /// It contains the necessary information about bearing 1. 
        /// </summary>
        public static readonly Bearing Bearing1 = new(effectiveRadius: 45.3e-3, axialLoadFactor: 1, radialLoadFactor: 1.6);

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="effectiveRadius"></param>
        /// <param name="axialLoadFactor"></param>
        /// <param name="radialLoadFactor"></param>
        private Bearing(double effectiveRadius, double axialLoadFactor, double radialLoadFactor)
        {
            EffectiveRadius = effectiveRadius;
            AxialLoadFactor = axialLoadFactor;
            RadialLoadFactor = radialLoadFactor;
        }

        /// <summary>
        /// Unit: m (meter).
        /// </summary>
        public double EffectiveRadius { get; }

        /// <summary>
        /// Dimensionless.
        /// </summary>
        public double AxialLoadFactor { get; }

        /// <summary>
        /// Dimensionless.
        /// </summary>
        public double RadialLoadFactor { get; }

        /// <summary>
        /// This method creates a new instance of <see cref="Bearing"/> based on <see cref="BearingType"/>.
        /// </summary>
        /// <param name="bearingType"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Bearing Create(BearingType bearingType)
        {
            return bearingType switch
            {
                BearingType.Bearing1 => Bearing1,
                _ => throw new Exception($"Invalid bearing: '{bearingType}'.")
            };
        }
    }
}
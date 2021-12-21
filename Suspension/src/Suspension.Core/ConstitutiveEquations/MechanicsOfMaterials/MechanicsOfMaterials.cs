using MudRunner.Suspension.Core.GeometricProperties;
using MudRunner.Suspension.DataContracts.Models.Enums;
using System;

namespace MudRunner.Suspension.Core.ConstitutiveEquations.MechanicsOfMaterials
{
    /// <summary>
    /// It contains the Mechanics of Materials constitutive equations.
    /// </summary>
    public class MechanicsOfMaterials : IMechanicsOfMaterials
    {
        /// <inheritdoc/>
        public double CalculateEquivalentStress(double normalStress = 0, double flexuralStress = 0, double shearStress = 0, double torsionalStress = 0)
        {
            return Math.Sqrt(Math.Pow(normalStress + flexuralStress, 2) + 3 * Math.Pow(shearStress + torsionalStress, 2));
        }

        /// <inheritdoc/>
        public double CalculateNormalStress(double normalForce, double area)
        {
            GeometricProperty.Validate(area, nameof(area));

            return normalForce / area;
        }

        /// <inheritdoc/>
        public double CalculateCriticalBucklingForce(double youngModulus, double momentOfInertia, double length, FasteningType fasteningType = FasteningType.BothEndPinned)
        {
            GeometricProperty.Validate(momentOfInertia, nameof(momentOfInertia));
            GeometricProperty.Validate(length, nameof(length));

            // It was necessary to multiply the result by 10^-6 to convert it to Newton.
            return (Math.Pow(Math.PI, 2) * youngModulus * momentOfInertia / Math.Pow(length * this.CalculateColumnEffectiveLengthFactor(fasteningType), 2)) * Math.Pow(10, -6);
        }

        /// <inheritdoc/>
        public double CalculateColumnEffectiveLengthFactor(FasteningType fasteningType)
        {
            return fasteningType switch
            {
                FasteningType.BothEndPinned => 1,
                FasteningType.BothEndFixed => 0.5,
                FasteningType.OneEndFixedOneEndPinned => Math.Sqrt(2) / 2,
                FasteningType.OneEndFixed => 2,
                _ => throw new ArgumentOutOfRangeException($"Invalid fastening type: {fasteningType}.")
            };
        }
    }
}

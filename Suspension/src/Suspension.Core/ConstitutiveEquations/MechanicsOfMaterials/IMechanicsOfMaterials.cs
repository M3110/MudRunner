using Suspension.DataContracts.Models.Enums;

namespace Suspension.Core.ConstitutiveEquations.MechanicsOfMaterials
{
    /// <summary>
    /// It contains the Mechanics of Materials constitutive equations.
    /// </summary>
    public interface IMechanicsOfMaterials
    {
        /// <summary>
        /// This method calcultes the equivalent stress using Von-Misses method.
        /// </summary>
        /// <param name="normalStress">Normal stress in MPa (Mega Pascal).</param>
        /// <param name="flexuralStress">Flexural stress in MPa (Mega Pascal).</param>
        /// <param name="shearStress">Shear stress in MPa (Mega Pascal).</param>
        /// <param name="torsionalStress">Torsion stress in MPa (Mega Pascal).</param>
        /// <returns>The equivalent stress. Unit: MPa (Mega Pascal).</returns>
        double CalculateEquivalentStress(double normalStress = 0, double flexuralStress = 0, double shearStress = 0, double torsionalStress = 0);

        /// <summary>
        /// This method calculates the normal stress.
        /// </summary>
        /// <param name="normalForce">Normal force in N (Newton).</param>
        /// <param name="area">Area in mm² (milimeters squared).</param>
        /// <returns>The normal stress. Unit: MPa (Mega Pascal).</returns>
        double CalculateNormalStress(double normalForce, double area);

        /// <summary>
        /// This method calculates the critical buckling force.
        /// </summary>
        /// <param name="youngModulus">Young's modulus in MPa (Mega Pascal).</param>
        /// <param name="momentOfInertia">Moment of inertia in mm^4 (milimeters raised by four).</param>
        /// <param name="length">Length in m (meter).</param>
        /// <param name="fasteningType">The fastening type of component.</param>
        /// <returns>The critical buckling force. Unit: N (Newton).</returns>
        double CalculateCriticalBucklingForce(double youngModulus, double momentOfInertia, double length, FasteningType fasteningType = FasteningType.BothEndPinned);

        /// <summary>
        /// This method calculates the effective length factor to buckling analysis based on fastening type.
        /// </summary>
        /// <param name="fasteningType">The fastening type of component.</param>
        /// <returns>The effective length factor. Dimensionless.</returns>
        double CalculateColumnEffectiveLengthFactor(FasteningType fasteningType);
    }
}
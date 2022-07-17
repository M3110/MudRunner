using MudRunner.Commons.DataContracts.Models.Enums;
using System;

namespace MudRunner.Commons.DataContracts.Models
{
    /// <summary>
    /// It contains the necessary information about each material that could be used in project.
    /// </summary>
    public struct Material
    {
        /// <summary>
        /// It contains the necessary information about Steel SAE 1020.
        /// </summary>
        public static readonly Material Steel1020 = new(youngModulus: 205e3, yieldStrength: 350, tensileStress: 470, specificMass: 7850);

        /// <summary>
        /// It contains the necessary information about Steel SAE 1045.
        /// </summary>
        public static readonly Material Steel1045 = new(youngModulus: 200e3, yieldStrength: 450, tensileStress: 738, specificMass: 7850);

        /// <summary>
        /// It contains the necessary information about Steel SAE 4130.
        /// </summary>
        public static readonly Material Steel4130 = new(youngModulus: 200e3, yieldStrength: 552, tensileStress: 860, specificMass: 7850);

        /// <summary>
        /// It contains the necessary information about Aluminum 6061-T6.
        /// </summary>
        public static readonly Material Aluminum6061T6 = new(youngModulus: 70e3, yieldStrength: 310, tensileStress: 290, specificMass: 2710);

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="youngModulus"></param>
        /// <param name="yieldStrength"></param>
        /// <param name="tensileStress"></param>
        /// <param name="specificMass"></param>
        private Material(double youngModulus, double yieldStrength, double tensileStress, double specificMass)
        {
            this.YoungModulus = youngModulus;
            this.YieldStrength = yieldStrength;
            this.TensileStress = tensileStress;
            this.SpecificMass = specificMass;
        }

        /// <summary>
        /// Young modulus. 
        /// Unit: MPa (Pascal).
        /// </summary>
        public double YoungModulus { get; }

        /// <summary>
        /// Yield strength. 
        /// Unit: MPa (Pascal).
        /// </summary>
        public double YieldStrength { get; }

        /// <summary>
        /// Tje tensile stress (Sut).
        /// Unit: MPa (Mega Pascal).
        /// </summary>
        public double TensileStress { get; set; }

        /// <summary>
        /// Specific mass. 
        /// Unit: kg/m³ (kilogram per cubic meters).
        /// </summary>
        public double SpecificMass { get; }

        /// <summary>
        /// This method creates an instance of class <seealso cref="Material"/>.
        /// </summary>
        /// <param name="materialType"></param>
        /// <returns></returns>
        public static Material Create(MaterialType materialType)
        {
            return materialType switch
            {
                MaterialType.Steel1020 => Material.Steel1020,
                MaterialType.Steel1045 => Material.Steel1045,
                MaterialType.Steel4130 => Material.Steel4130,
                MaterialType.Aluminum6061T6 => Material.Aluminum6061T6,

                _ => throw new Exception($"Invalid material: '{materialType}'.")
            };
        }
    }
}

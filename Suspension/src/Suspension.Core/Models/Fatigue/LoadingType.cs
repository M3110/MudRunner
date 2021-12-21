namespace MudRunner.Suspension.Core.Models.Fatigue
{
    /// <summary>
    /// It represents the loading types for fatigue analysis.
    /// </summary>
    public enum LoadingType
    {
        /// <summary>
        /// Loading by bending involves applying a load in a manner that causes a material to curve and 
        /// results in compressing the material on one side and stretching it on the other. 
        /// </summary>
        Bending = 1,

        /// <summary>
        /// It can be tension and compression.
        /// Tension is the type of loading in which the two sections of material on either side of a 
        /// plane tend to be pulled apart or elongated. Compression is the reverse of tensile loading 
        /// and involves pressing the material together
        /// </summary>
        Axial = 2,

        /// <summary>
        /// Torsion is the application of a force that causes twisting in a material.
        /// A twisting action applied to a generally shaft-like, cylindrical, or tubular member. The 
        /// twisting may be either reversed (back and forth) or unidirectional (one way).
        /// </summary>
        Torsion = 3
    }
}

namespace Suspension.DataContracts.Models.Profiles
{
    /// <summary>
    /// It represents the circular profile.
    /// </summary>
    public class CircularProfile : Profile
    {
        /// <summary>
        /// The diameter.
        /// Unit: mm (milimeter).
        /// </summary>
        public double Diameter { get; set; }
    }
}

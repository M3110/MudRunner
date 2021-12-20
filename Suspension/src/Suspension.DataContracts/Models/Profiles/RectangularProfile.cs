namespace Suspension.DataContracts.Models.Profiles
{
    /// <summary>
    /// It represents the rectangular profile.
    /// </summary>
    public class RectangularProfile : Profile
    {
        /// <summary>
        /// The width.
        /// Unit: mm (milimeter).
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// The height.
        /// Unit: mm (milimeter).
        /// </summary>
        public double Height { get; set; }
    }
}

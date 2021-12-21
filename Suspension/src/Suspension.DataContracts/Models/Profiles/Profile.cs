namespace MudRunner.Suspension.DataContracts.Models.Profiles
{
    /// <summary>
    /// It represents the generic profile.
    /// </summary>
    public abstract class Profile
    {
        /// <summary>
        /// The thickness.
        /// Unit: mm (milimeter).
        /// </summary>
        public double? Thickness { get; set; }
    }
}

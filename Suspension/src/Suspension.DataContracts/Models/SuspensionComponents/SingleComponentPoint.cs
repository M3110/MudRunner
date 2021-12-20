namespace Suspension.DataContracts.Models.SuspensionComponents
{
    /// <summary>
    /// It contains the points to a single suspension component.
    /// </summary>
    public abstract class SingleComponentPoint
    {
        /// <summary>
        /// The pivot point at chassis in milimeters (mm).
        /// </summary>
        /// <example>x,y,z</example>
        public string PivotPoint { get; set; }

        /// <summary>
        /// The fastening point in milimeters (mm).
        /// </summary>
        /// <example>x,y,z</example>
        public string FasteningPoint { get; set; }
    }
}

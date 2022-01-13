using MudRunner.Commons.DataContracts.Models;

namespace MudRunner.Suspension.Core.Models.SuspensionComponents.SteeringKnuckle
{
    /// <summary>
    /// It represents the steering knuckle.
    /// </summary>
    public class SteeringKnuckle
    {
        /// <summary>
        /// The point of fastening with wishbone upper. 
        /// </summary>
        public Point3D UpperWishbonePoint { get; set; }

        /// <summary>
        /// The point of fastening with wishbone lower.
        /// </summary>
        public Point3D LowerWishbonePoint { get; set; }

        /// <summary>
        /// The point of fastening with tie rod.
        /// </summary>
        public Point3D TieRodPoint { get; set; }
    }
}

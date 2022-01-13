using MudRunner.Commons.DataContracts.Models;
using DataContract = MudRunner.Suspension.DataContracts.Models.SuspensionComponents.SteeringKnuckle;

namespace MudRunner.Suspension.Core.Models.SuspensionComponents.SteeringKnuckle
{
    /// <summary>
    /// It represents the brake caliper support. 
    /// </summary>
    public class BrakeCaliperSupportPoint
    {
        /// <summary>
        /// The first pivot point of brake caliper support.
        /// </summary>
        public Point3D Point1 { get; set; }

        /// <summary>
        /// The second pivot point of brake caliper support. 
        /// </summary>
        public Point3D Point2 { get; set; }

        /// <summary>
        /// This method creates a new instance of <see cref="BrakeCaliperSupportPoint"/>.
        /// </summary>
        /// <param name="brakeCaliperSupportPoint"></param>
        /// <returns></returns>
        public static BrakeCaliperSupportPoint Create(DataContract.BrakeCaliperSupportPoint brakeCaliperSupportPoint)
        {
            return new BrakeCaliperSupportPoint
            {
                Point1 = Point3D.Create(brakeCaliperSupportPoint.Point1),
                Point2 = Point3D.Create(brakeCaliperSupportPoint.Point2)
            };
        }
    }
}

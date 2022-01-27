using MudRunner.Suspension.DataContracts.Models.Enums;
using System.Collections.Generic;

namespace MudRunner.Suspension.DataContracts.Models
{
    /// <summary>
    /// It contains the variables to calculste the base excitation at the system.
    /// </summary>
    public class BaseExcitation
    {
        /// <summary>
        /// The constants to describe the equation for base excitation.
        /// </summary>
        public List<double> Constants { get; set; }

        /// <summary>
        /// The curve type that describe the constant.
        /// </summary>
        public CurveType CurveType { get; set; }

        /// <summary>
        /// The limit times that indicates when the curve for base excitation changes.
        /// This propery is only used when <see cref="CurveType"/> is <see cref="CurveType.Cosine"/>.
        /// </summary>
        public List<double> LimitTimes { get; set; }

        /// <summary>
        /// This propery is only used when <see cref="CurveType"/> is <see cref="CurveType.Cosine"/>.
        /// Unit: m (meter).
        /// </summary>
        public double ObstacleWidth { get; set; }

        /// <summary>
        /// The speed at which the car is traveling down the track.
        /// Unit: km/h (kilometer per hour).
        /// </summary>
        public double CarSpeed { get; set; }
    }
}

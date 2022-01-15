namespace MudRunner.Commons.Core.Models
{
    /// <summary>
    /// It contains the constants used in the project.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The invalid values for double parameters.
        /// </summary>
        public static List<double> InvalidValues => new()
        { 
            double.NaN, 
            double.PositiveInfinity, 
            double.NegativeInfinity, 
            double.MaxValue, 
            double.MinValue 
        };

        /// <summary>
        /// Unit: m/s² (meter per squared second).
        /// </summary>
        public static double GravityAcceleration => 9.80665;
    }
}

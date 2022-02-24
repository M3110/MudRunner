namespace MudRunner.Commons.Core.Models
{
    /// <summary>
    /// It is responsible to convert units.
    /// </summary>
    public static class UnitConverter
    {
        /// <summary>
        /// This method converts a velocity from kilometers per hour to meters per second.
        /// </summary>
        /// <param name="valueInKmh"></param>
        /// <returns></returns>
        public static double FromKmhToMs(double valueInKmh) => valueInKmh / 3.6;

        /// <summary>
        /// This method converts a frequency from revolutions per minute to radian per second.
        /// </summary>
        /// <param name="valueInRpm"></param>
        /// <returns></returns>
        public static double FromRpmToRads(double valueInRpm) => valueInRpm * Math.PI / 30;
    }
}

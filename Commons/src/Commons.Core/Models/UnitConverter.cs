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
        public static double FromKmHToMS(double valueInKmh)
        {
            return valueInKmh / 3.6;
        }
    }
}

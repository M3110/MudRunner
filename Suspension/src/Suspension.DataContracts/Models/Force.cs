using System;

namespace Suspension.DataContracts.Models
{
    /// <summary>
    /// It represents the force.
    /// </summary>
    public class Force
    {
        /// <summary>
        /// the absolut value to force.
        /// </summary>
        public double AbsolutValue { get; set; }

        /// <summary>
        /// The force at axis X.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// The force at axis Y.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// The force at axis Z.
        /// </summary>
        public double Z { get; set; }

        /// <summary>
        /// This method rounds each value at <see cref="Force"/> to a specified number of fractional
        /// digits, and rounds midpoint values to the nearest even number.
        /// </summary>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public Force Round(int decimals)
        {
            return new Force
            {
                AbsolutValue = Math.Round(this.AbsolutValue, decimals),
                X = Math.Round(this.X, decimals),
                Y = Math.Round(this.Y, decimals),
                Z = Math.Round(this.Z, decimals)
            };
        }

        /// <summary>
        /// This method sums two forces.
        /// </summary>
        /// <param name="force"></param>
        /// <returns></returns>
        public Force Sum(Force force)
        {
            Force result = new()
            {
                X = this.X + force.X,
                Y = this.Y + force.Y,
                Z = this.Z + force.Z
            };
            result.AbsolutValue = CalculateAbsolutValue(result);

            return result;
        }

        /// <summary>
        /// This method subtracts two forces.
        /// </summary>
        /// <param name="force"></param>
        /// <returns></returns>
        public Force Subtract(Force force)
        {
            Force result = new()
            {
                X = this.X - force.X,
                Y = this.Y - force.Y,
                Z = this.Z - force.Z
            };
            result.AbsolutValue = CalculateAbsolutValue(result);

            return result;
        }

        /// <summary>
        /// This method subtracts two forces.
        /// </summary>
        /// <param name="force"></param>
        /// <returns></returns>
        public Force Divide(int value)
        {
            return new()
            {
                AbsolutValue = this.AbsolutValue / value,
                X = this.X / value,
                Y = this.Y / value,
                Z = this.Z / value
            };
        }

        /// <summary>
        /// This method returns a new force with the ablsolute value of each axis.
        /// </summary>
        /// <returns></returns>
        public Force Abs()
        {
            return new()
            {
                X = Math.Abs(this.X),
                Y = Math.Abs(this.Y),
                Z = Math.Abs(this.Z),
                AbsolutValue = this.AbsolutValue
            };
        }

        /// <summary>
        /// This method creates the <see cref="Force"/> based on the absolut value and the normalized direction.
        /// </summary>
        /// <param name="absolutValue"></param>
        /// <param name="normalizedDirection"></param>
        /// <returns></returns>
        public static Force Create(double absolutValue, Vector3D normalizedDirection)
        {
            return new Force
            {
                AbsolutValue = absolutValue,
                X = absolutValue * normalizedDirection.X,
                Y = absolutValue * normalizedDirection.Y,
                Z = absolutValue * normalizedDirection.Z
            };
        }

        /// <summary>
        /// This method calculates the absolut value based on axis x, y and z.
        /// </summary>
        /// <param name="force"></param>
        /// <returns></returns>
        private double CalculateAbsolutValue(Force force)
        {
            return Math.Sqrt(Math.Pow(force.X, 2) + Math.Pow(force.Y, 2) + Math.Pow(force.Z, 2));
        }
    }
}

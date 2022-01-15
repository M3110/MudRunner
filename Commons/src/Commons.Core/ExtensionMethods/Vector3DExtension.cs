using MudRunner.Commons.DataContracts.Models;

namespace MudRunner.Commons.Core.ExtensionMethods
{
    /// <summary>
    /// It contains the extension methods to Vector3D.
    /// </summary>
    public static class Vector3DExtension
    {
        /// <summary>
        /// This method normalizes the vector.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector3D Normalize(this Vector3D vector)
            => new()
            {
                X = vector.X / vector.Length,
                Y = vector.Y / vector.Length,
                Z = vector.Z / vector.Length
            };

        /// <summary>
        /// This method calculates the cross product between two vectors.
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns></returns>
        public static Vector3D CrossProduct(this Vector3D vector1, Vector3D vector2)
            => new()
            {
                X = vector1.Y * vector2.Z - vector1.Z * vector2.Y,
                Y = vector1.Z * vector2.X - vector1.X * vector2.Z,
                Z = vector1.X * vector2.Y - vector1.Y * vector2.X
            };

        /// <summary>
        /// This method calculates the dot product between two vectors.
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns></returns>
        public static double DotProduct(this Vector3D vector1, Vector3D vector2)
            => vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;

        /// <summary>
        /// This method returns true if all axis of vector is equals to zero and false, otherwise.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static bool IsZero(this Vector3D vector) => vector.X == 0 && vector.Y == 0 && vector.Z == 0;
    }
}

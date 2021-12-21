using MudRunner.Suspension.DataContracts.Models.Profiles;

namespace MudRunner.Suspension.Core.GeometricProperties
{
    /// <summary>
    /// It is responsible to calculate the geometric properties to a profile.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public interface IGeometricProperty<TProfile>
        where TProfile : Profile
    {
        /// <summary>
        /// This method calculates the area.
        /// Unit: mm² (millimeters squared). 
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        double CalculateArea(TProfile profile);

        /// <summary>
        /// This method calculates the moment of inertia.
        /// Unit: mm^4 (milimeters raised by four).
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        double CalculateMomentOfInertia(TProfile profile);
    }
}
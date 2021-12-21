using DataContract = Suspension.DataContracts.Models.Profiles;

namespace Suspension.Core.GeometricProperties.CircularProfile
{
    /// <summary>
    /// It is responsible to calculate the geometric properties to circular profile.
    /// </summary>
    public interface ICircularProfileGeometricProperty : IGeometricProperty<DataContract.CircularProfile> { }
}
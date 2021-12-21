using Suspension.Core.ConstitutiveEquations.MechanicsOfMaterials;
using Suspension.Core.GeometricProperties.CircularProfile;
using Suspension.Core.Mapper;
using Suspension.Core.Operations.CalculateReactions;
using DataContract = Suspension.DataContracts.Models.Profiles;

namespace Suspension.Core.Operations.RunAnalysis.Static.CircularProfile
{
    /// <summary>
    /// It is responsible to run the static analysis to suspension system considering circular profile.
    /// </summary>
    public class RunCircularProfileStaticAnalysis : RunStaticAnalysis<DataContract.CircularProfile>, IRunCircularProfileStaticAnalysis
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="calculateReactions"></param>
        /// <param name="mechanicsOfMaterials"></param>
        /// <param name="geometricProperty"></param>
        /// <param name="mappingResolver"></param>
        public RunCircularProfileStaticAnalysis(
            ICalculateReactions calculateReactions,
            IMechanicsOfMaterials mechanicsOfMaterials,
            ICircularProfileGeometricProperty geometricProperty,
            IMappingResolver mappingResolver)
            : base(calculateReactions, mechanicsOfMaterials, geometricProperty, mappingResolver)
        { }
    }
}

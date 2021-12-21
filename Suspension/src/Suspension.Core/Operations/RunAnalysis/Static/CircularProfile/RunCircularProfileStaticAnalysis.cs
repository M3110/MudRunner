using MudRunner.Suspension.Core.ConstitutiveEquations.MechanicsOfMaterials;
using MudRunner.Suspension.Core.GeometricProperties.CircularProfile;
using MudRunner.Suspension.Core.Mapper;
using MudRunner.Suspension.Core.Operations.CalculateReactions;
using DataContract = MudRunner.Suspension.DataContracts.Models.Profiles;

namespace MudRunner.Suspension.Core.Operations.RunAnalysis.Static.CircularProfile
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

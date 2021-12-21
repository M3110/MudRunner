using MudRunner.Commons.Core.ConstitutiveEquations.MechanicsOfMaterials;
using MudRunner.Commons.Core.GeometricProperties.RectangularProfile;
using MudRunner.Suspension.Core.Mapper;
using MudRunner.Suspension.Core.Operations.CalculateReactions;
using DataContract = MudRunner.Commons.DataContracts.Models.Profiles;

namespace MudRunner.Suspension.Core.Operations.RunAnalysis.Static.RectangularProfile
{
    /// <summary>
    /// It is responsible to run the static analysis to suspension system considering rectangular profile.
    /// </summary>
    public class RunRectangularProfileStaticAnalysis : RunStaticAnalysis<DataContract.RectangularProfile>, IRunRectangularProfileStaticAnalysis
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="calculateReactions"></param>
        /// <param name="mechanicsOfMaterials"></param>
        /// <param name="mappingResolver"></param>
        public RunRectangularProfileStaticAnalysis(
            ICalculateReactions calculateReactions,
            IMechanicsOfMaterials mechanicsOfMaterials,
            IRectangularProfileGeometricProperty geometricProperty,
            IMappingResolver mappingResolver)
            : base(calculateReactions, mechanicsOfMaterials, geometricProperty, mappingResolver)
        { }
    }
}

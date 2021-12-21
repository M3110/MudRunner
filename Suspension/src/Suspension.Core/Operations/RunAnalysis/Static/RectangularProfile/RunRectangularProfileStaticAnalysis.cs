using Suspension.Core.ConstitutiveEquations.MechanicsOfMaterials;
using Suspension.Core.GeometricProperties.RectangularProfile;
using Suspension.Core.Mapper;
using Suspension.Core.Operations.CalculateReactions;
using DataContract = Suspension.DataContracts.Models.Profiles;

namespace Suspension.Core.Operations.RunAnalysis.Static.RectangularProfile
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

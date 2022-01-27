using MudRunner.Commons.Core.ConstitutiveEquations.MechanicsOfMaterials;
using MudRunner.Commons.Core.GeometricProperties.RectangularProfile;
using MudRunner.Suspension.Core.Mapper;
using MudRunner.Suspension.Core.Operations.CalculateReactions;
using MudRunner.Suspension.DataContracts.RunAnalysis.Static;
using System.Threading.Tasks;
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
        /// <param name="geometricProperty"></param>
        /// <param name="mappingResolver"></param>
        public RunRectangularProfileStaticAnalysis(
            ICalculateReactions calculateReactions,
            IMechanicsOfMaterials mechanicsOfMaterials,
            IRectangularProfileGeometricProperty geometricProperty,
            IMappingResolver mappingResolver)
            : base(calculateReactions, mechanicsOfMaterials, geometricProperty, mappingResolver)
        { }

        /// <summary>
        /// Asynchronously, this method validates the <see cref="RunStaticAnalysisRequest{RectangularProfile}"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override Task<RunStaticAnalysisResponse> ValidateOperationAsync(RunStaticAnalysisRequest<DataContract.RectangularProfile> request)
        {
            RunStaticAnalysisResponse response = new();
            response.SetSuccessOk();

            return Task.FromResult(response);
        }
    }
}

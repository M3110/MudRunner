using MudRunner.Commons.Core.Operation;
using MudRunner.Commons.DataContracts.Models.Profiles;
using MudRunner.Suspension.DataContracts.RunAnalysis.Fatigue;

namespace MudRunner.Suspension.Core.Operations.RunAnalysis.Fatigue
{
    /// <summary>
    /// It is responsible to run the fatigue analysis to suspension system.
    /// </summary>
    public interface IRunFatigueAnalysis<TProfile> : IOperationBase<RunFatigueAnalysisRequest<TProfile>, RunFatigueAnalysisResponse>
        where TProfile : Profile
    { }
}
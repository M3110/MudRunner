using Suspension.Core.Operations.Base;
using Suspension.DataContracts.Models.Profiles;
using Suspension.DataContracts.RunAnalysis.Fatigue;

namespace Suspension.Core.Operations.RunAnalysis.Fatigue
{
    /// <summary>
    /// It is responsible to run the fatigue analysis to suspension system.
    /// </summary>
    public interface IRunFatigueAnalysis<TProfile> : IOperationBase<RunFatigueAnalysisRequest<TProfile>, RunFatigueAnalysisResponse>
        where TProfile : Profile
    { }
}
using MudRunner.Suspension.DataContracts.RunAnalysis.Dynamic.QuarterCar.OneDegreeOfFreedom;

namespace MudRunner.Suspension.Core.Operations.RunAnalysis.Dynamic.QuarterCar.OneDegreeOfFreedom
{
    /// <summary>
    /// It is responsible to run the dynamic analysis to suspension system focusing in the amplitude of the system 
    /// considering quarter car and one degrees of freedom.
    /// </summary>
    public interface IRunQuarterCarOneDofAmplitudeDynamicAnalysis : IRunAmplitudeDynamicAnalysis<
        RunQuarterCarOneDofAmplitudeDynamicAnalysisRequest,
        RunQuarterCarOneDofDynamicAnalysisRequest>
    { }
}

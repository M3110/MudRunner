using MudRunner.Suspension.DataContracts.RunAnalysis.Dynamic.HalfCar.SixDegreeOfFreedom;

namespace MudRunner.Suspension.Core.Operations.RunAnalysis.Dynamic.HalfCar.SixDegreeOfFreedom
{
    /// <summary>
    /// It is responsible to run the dynamic analysis to suspension system focusing in the amplitude of the system 
    /// considering half car and six degrees of freedom.
    /// </summary>
    public interface IRunHalfCarSixDofAmplitudeDynamicAnalysis : IRunAmplitudeDynamicAnalysis<
        RunHalfCarSixDofAmplitudeDynamicAnalysisRequest, 
        RunHalfCarSixDofDynamicAnalysisRequest> 
    { }
}

using MudRunner.Commons.Core.Operation;
using MudRunner.Commons.DataContracts.Models;
using MudRunner.Commons.DataContracts.Operation;
using MudRunner.Suspension.DataContracts.CalculateSteeringKnuckleReactions;
using MudRunner.Suspension.DataContracts.Models.Enums;

namespace MudRunner.Suspension.Core.Operations.CalculateStearingKnuckleReactions
{
    public interface ICalculateSteeringKnuckleReactions : IOperationBase<CalculateSteeringKnuckleReactionsRequest, OperationResponse<CalculateSteeringKnuckleReactionsResponseData>>
    {
        public Force CalculateTieRodReactions(Force tieRodReaction, string steeringWheelForce, SuspensionPosition suspensionPosition);

        public double CalculateBearingReaction(CalculateSteeringKnuckleReactionsRequest request);

        public (Force Reaction1, Force Reaction2) CalculateBrakeCaliperReactions(CalculateSteeringKnuckleReactionsRequest request);
    }
}

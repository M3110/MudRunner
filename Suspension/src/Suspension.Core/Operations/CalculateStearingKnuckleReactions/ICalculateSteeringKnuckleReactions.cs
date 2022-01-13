using MudRunner.Commons.Core.Operation;
using MudRunner.Commons.DataContracts.Models;
using MudRunner.Suspension.DataContracts.CalculateSteeringKnuckleReactions;
using MudRunner.Suspension.DataContracts.Models.Enums;

namespace MudRunner.Suspension.Core.Operations.CalculateStearingKnuckleReactions
{
    public interface ICalculateSteeringKnuckleReactions : IOperationBase<CalculateSteeringKnuckleReactionsRequest, CalculateSteeringKnuckleReactionsResponse>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tieRodReaction"></param>
        /// <param name="steeringWheelForce"></param>
        /// <param name="suspensionPosition"></param>
        /// <returns></returns>
        public Force CalculateTieRodReactions(Force tieRodReaction, string steeringWheelForce, SuspensionPosition suspensionPosition);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public double CalculateBearingReaction(CalculateSteeringKnuckleReactionsRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public (Force Reaction1, Force Reaction2) CalculateBrakeCaliperReactions(CalculateSteeringKnuckleReactionsRequest request);
    }
}

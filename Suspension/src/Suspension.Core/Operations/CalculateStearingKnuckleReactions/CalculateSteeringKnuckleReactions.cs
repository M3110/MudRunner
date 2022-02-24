using MudRunner.Commons.Core.ExtensionMethods;
using MudRunner.Commons.Core.Operation;
using MudRunner.Commons.DataContracts.Models;
using MudRunner.Suspension.Core.Models.SuspensionComponents;
using MudRunner.Suspension.Core.Models.SuspensionComponents.SteeringKnuckle;
using MudRunner.Suspension.Core.Operations.CalculateReactions;
using MudRunner.Suspension.DataContracts.CalculateReactions;
using MudRunner.Suspension.DataContracts.CalculateSteeringKnuckleReactions;
using MudRunner.Suspension.DataContracts.Models.Enums;
using System.Threading.Tasks;

namespace MudRunner.Suspension.Core.Operations.CalculateStearingKnuckleReactions
{
    /// <summary>
    /// It is responsible to calculate the reactions to steering knuckle. 
    /// </summary>
    public class CalculateSteeringKnuckleReactions : OperationBase<CalculateSteeringKnuckleReactionsRequest, CalculateSteeringKnuckleReactionsResponse>, ICalculateSteeringKnuckleReactions
    {
        private readonly ICalculateReactions _calculateReactions;

        public CalculateSteeringKnuckleReactions(ICalculateReactions calculateReactions)
        {
            this._calculateReactions = calculateReactions;
        }

        public Force CalculateTieRodReactions(Force tieRodReaction, string steeringWheelForce, SuspensionPosition suspensionPosition)
        {
            if (suspensionPosition == SuspensionPosition.Rear)
                return tieRodReaction;

            return tieRodReaction.Sum(Force.Create(steeringWheelForce));
        }

        public double CalculateBearingReaction(CalculateSteeringKnuckleReactionsRequest request)
        {
            var bearing = Bearing.Create(request.BearingType);

            return (request.BearingTorque / bearing.EffectiveRadius) * bearing.RadialLoadFactor;
        }

        public (Force Reaction1, Force Reaction2) CalculateBrakeCaliperReactions(CalculateSteeringKnuckleReactionsRequest request)
        {
            var brakeCaliperSupport = BrakeCaliperSupportPoint.Create(request.SteeringKnuckle.BrakeCaliperSupportPoint);

            var inertialForce = Vector3D.Create(request.InertialForce);
            var inertialForceCoordinate = Point3D.Create(request.InertialForceCoordinate);

            var vector1 = Vector3D.Create(brakeCaliperSupport.Point2, brakeCaliperSupport.Point1);
            var vector2 = Vector3D.Create(inertialForceCoordinate, brakeCaliperSupport.Point1);

            double[] effort = new double[6]
            {
                inertialForce.X,
                inertialForce.Y,
                inertialForce.Z,
                -vector2.X * inertialForce.Y + vector2.Y * inertialForce.X,
                -vector2.Y * inertialForce.Z + vector2.Z * inertialForce.Y,
                -vector2.Z * inertialForce.X + vector2.X * inertialForce.Z
            };

            double[,] displacement = new double[6, 6]
            {
                { 1, 0, 0, 1, 0, 0 },
                { 0, 1, 0, 0, 1, 0 },
                { 0, 0, 1, 0, 0, 1 },
                { 0, 0, 0, -vector1.Y, vector1.X, 0 },
                { 0, 0, 0, 0, -vector1.Z, vector1.Y },
                { 0, 0, 0, vector1.Z, 0, -vector1.X }
            };

            double[] result = displacement
                .InverseMatrix()
                .Multiply(effort);

            return (new Force(result[0], result[1], result[2]), new Force(result[3], result[4], result[5]));
        }

        protected override async Task<CalculateSteeringKnuckleReactionsResponse> ProcessOperationAsync(CalculateSteeringKnuckleReactionsRequest request)
        {
            var response = new CalculateSteeringKnuckleReactionsResponse();

            CalculateReactionsResponseData suspensionSystemEfforts;
            if (request.CalculateReactionsResponseData != null)
            {
                suspensionSystemEfforts = request.CalculateReactionsResponseData;
            }
            else
            {
                var calculateReactionsResponse = await this._calculateReactions.ProcessAsync(request.CalculateReactionsRequest).ConfigureAwait(false);
                if (calculateReactionsResponse.Success == false)
                {
                    response.AddErrors(calculateReactionsResponse.Errors);
                    response.SetInternalServerError("Occurred error while calculating reactions on suspension system.");
                    return response;
                }

                suspensionSystemEfforts = calculateReactionsResponse.Data;
            }

            // Step X - Calculates the reactions.
            response.Data.UpperWishboneReaction = suspensionSystemEfforts.UpperWishboneReaction1.Sum(suspensionSystemEfforts.UpperWishboneReaction2);
            response.Data.LowerWishboneReaction = suspensionSystemEfforts.LowerWishboneReaction1.Sum(suspensionSystemEfforts.LowerWishboneReaction2);
            response.Data.TieRodReaction = this.CalculateTieRodReactions(suspensionSystemEfforts.TieRodReaction, request.SteeringWheelForce, request.SuspensionPosition);
            response.Data.BearingReaction = this.CalculateBearingReaction(request);
            (response.Data.BrakeCaliperSupportReaction1, response.Data.BrakeCaliperSupportReaction2) = this.CalculateBrakeCaliperReactions(request);

            return response;
        }

        protected override Task<CalculateSteeringKnuckleReactionsResponse> ValidateOperationAsync(CalculateSteeringKnuckleReactionsRequest request)
        {
            CalculateSteeringKnuckleReactionsResponse response = new();
            response.SetSuccessOk();

            if (request.CalculateReactionsRequest == null && request.CalculateReactionsResponseData == null)
            {
                response.SetBadRequestError("The forces applied to the steering knukle or the suspension points must be passed on request");
            }

            return Task.FromResult(response);
        }
    }
}

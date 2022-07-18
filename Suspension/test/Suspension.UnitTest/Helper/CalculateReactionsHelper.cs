using MudRunner.Commons.DataContracts.Models;
using MudRunner.Commons.DataContracts.Operation;
using MudRunner.Suspension.Core.Models.SuspensionComponents;
using MudRunner.Suspension.DataContracts.CalculateReactions;
using MudRunner.Suspension.DataContracts.Models.SuspensionComponents;
using Newtonsoft.Json.Linq;
using ShockAbsorber = MudRunner.Suspension.Core.Models.SuspensionComponents.ShockAbsorber;

namespace MudRunner.Suspension.UnitTest.Helper
{
    /// <summary>
    /// It contains method and properties to help testing the CalculateReactions operation.
    /// </summary>
    public static class CalculateReactionsHelper
    {
        public static Point3D CreateOrigin()
        {
            return new Point3D { X = 0, Y = 0.75, Z = 0 };
        }

        public static SuspensionSystem CreateSuspensionSystem()
        {
            return new SuspensionSystem
            {
                ShockAbsorber = new ShockAbsorber
                {
                    FasteningPoint = new Point3D { X = -0.005, Y = 0.645, Z = 0.180 },
                    PivotPoint = new Point3D { X = -0.005, Y = 0.485, Z = 0.430 }
                },
                LowerWishbone = new Wishbone
                {
                    OuterBallJoint = new Point3D { X = -0.012, Y = 0.685, Z = 0.150 },
                    FrontPivot = new Point3D { X = -0.100, Y = 0.350, Z = 0.130 },
                    RearPivot = new Point3D { X = 0.250, Y = 0.350, Z = 0.150 }
                },
                UpperWishbone = new Wishbone
                {
                    OuterBallJoint = new Point3D { X = 0.012, Y = 0.660, Z = 0.410 },
                    FrontPivot = new Point3D { X = -0.080, Y = 0.450, Z = 0.362 },
                    RearPivot = new Point3D { X = 0.200, Y = 0.450, Z = 0.362 }
                },
                TieRod = new TieRod
                {
                    FasteningPoint = new Point3D { X = -0.120, Y = 0.668, Z = 0.200 },
                    PivotPoint = new Point3D { X = -0.125, Y = 0.370, Z = 0.176 }
                }
            };
        }


        public static CalculateReactionsRequest CreateRequest()
        {
            return new CalculateReactionsRequest
            {
                Origin = "0,0.75,0",
                ShouldRoundResults = false,
                AppliedForce = "1000,-1000,1000",
                ShockAbsorber = new ShockAbsorberPoint
                {
                    FasteningPoint = "-0.005,0.645,0.180",
                    PivotPoint = "-0.005,0.485,0.430"
                },
                LowerWishbone = new WishbonePoint
                {
                    OuterBallJoint = "-0.012,0.685,0.150",
                    FrontPivot = "-0.100,0.350,0.130",
                    RearPivot = "0.250,0.350,0.150"
                },
                UpperWishbone = new WishbonePoint
                {
                    OuterBallJoint = "0.012,0.660,0.410",
                    FrontPivot = "-0.080,0.450,0.362",
                    RearPivot = "0.200,0.450,0.362"
                },
                TieRod = new TieRodPoint
                {
                    FasteningPoint = "-0.120,0.668,0.200",
                    PivotPoint = "-0.125,0.370,0.176"
                }
            };
        }

        public static JToken CreateDisplacementMatrixAsJToken()
        {
            return JToken.Parse(
               @"[
                  [
                    0.2536444394535612,
                    -0.616054625333608,
                    0.39276091431921983,
                    -0.6575356150740176,
                    0.0,
                    0.016722033940405615
                  ],
                  [
                    0.9655782638288978,
                    0.7877034331555677,
                    0.8965194783373497,
                    0.7344812721571474,
                    0.5390536964233674,
                    0.9966332228481739
                  ],
                  [
                    0.05764646351217296,
                    0.0,
                    0.20491873790567985,
                    0.16788143363591934,
                    -0.8422714006615114,
                    0.08026576291394695
                  ],
                  [
                    -0.10539214691613032,
                    -0.11815551497333515,
                    -0.2324803081539939,
                    -0.19046148645995056,
                    -0.6396630152323849,
                    -0.14576931426530368
                  ],
                  [
                    0.03873842348018025,
                    -0.0924081938000412,
                    0.15857295001601196,
                    -0.27160417938397824,
                    -0.004211357003307557,
                    0.012976298337754757
                  ],
                  [
                    -0.18514314686204603,
                    0.4120829361866545,
                    -0.2481693990248975,
                    0.44229412950343194,
                    -0.002695268482116837,
                    -0.1307537638885165
                  ]
                ]");
        }

        public static OperationResponse<CalculateReactionsResponseData> CreateResponse()
        {
            OperationResponse<CalculateReactionsResponseData> response = new() { Data = CreateResponseData() };
            response.SetSuccessOk();

            return response;
        }

        public static CalculateReactionsResponseData CreateResponseData()
        {
            return new CalculateReactionsResponseData
            {
                LowerWishboneReaction1 = new Force
                {
                    AbsolutValue = -706.844136886457
                },
                LowerWishboneReaction2 = new Force
                {
                    AbsolutValue = 2318.54871728814
                },
                UpperWishboneReaction1 = new Force
                {
                    AbsolutValue = 410.390832183452
                },
                UpperWishboneReaction2 = new Force
                {
                    AbsolutValue = -693.435739188224
                },
                ShockAbsorberReaction = new Force
                {
                    AbsolutValue = 1046.35331054682
                },
                TieRodReaction = new Force
                {
                    AbsolutValue = -568.377481091224
                }
            };
        }
    }
}
﻿using MudRunner.Suspension.DataContracts.Models.Enums;
using MudRunner.Suspension.DataContracts.Models.Profiles;
using MudRunner.Suspension.DataContracts.Models.SuspensionComponents;
using MudRunner.Suspension.DataContracts.RunAnalysis.Static;

namespace MudRunner.Suspension.UnitTest.Helper.DataContracts
{
    /// <summary>
    /// It contains method and properties to help the testing the RunAnalysis operation.
    /// </summary>
    public static class RunAnalysisHelper
    {
        public static RunStaticAnalysisRequest<CircularProfile> CreateCircularProfileRequest()
        {
            return new RunStaticAnalysisRequest<CircularProfile>
            {
                Origin = "0,0.75,0",
                NumberOfDecimalsToRound = 2,
                ShouldRoundResults = true,
                Material = MaterialType.Steel1020,
                AppliedForce = "0,0,1000",
                ShockAbsorber = new ShockAbsorber
                {
                    FasteningPoint = "-0.005,0.645,0.180",
                    PivotPoint = "-0.005,0.485,0.430"
                },
                SuspensionAArmLower = new SuspensionAArm<CircularProfile>
                {
                    Profile = new CircularProfile
                    {
                        Diameter = 25.4,
                        Thickness = 0.9
                    },
                    KnucklePoint = "-0.012,0.685,0.150",
                    PivotPoint1 = "0.250,0.350,0.150",
                    PivotPoint2 = "-0.100,0.350,0.130"
                },
                SuspensionAArmUpper = new SuspensionAArm<CircularProfile>
                {
                    Profile = new CircularProfile
                    {
                        Diameter = 25.4,
                        Thickness = 0.9
                    },
                    KnucklePoint = "0.012,0.660,0.410",
                    PivotPoint1 = "0.200,0.450,0.362",
                    PivotPoint2 = "-0.080,0.450,0.362"
                },
                TieRod = new TieRod<CircularProfile>
                {
                    Profile = new CircularProfile
                    {
                        Diameter = 25.4,
                        Thickness = 0.9
                    },
                    PivotPoint = "-0.125,0.370,0.176",
                    FasteningPoint = "-0.120,0.668,0.200"
                }
            };
        }

        public static RunStaticAnalysisRequest<RectangularProfile> CreateRectangularProfileRequest()
        {
            return new RunStaticAnalysisRequest<RectangularProfile>
            {
                Origin = "0,0,0",
                NumberOfDecimalsToRound = 2,
                ShouldRoundResults = true,
                Material = MaterialType.Steel1020,
                AppliedForce = "0,0,1000",
                ShockAbsorber = new ShockAbsorber
                {
                    FasteningPoint = "-0.005,0.645,0.180",
                    PivotPoint = "-0.005,0.485,0.430"
                },
                SuspensionAArmLower = new SuspensionAArm<RectangularProfile>
                {
                    Profile = new RectangularProfile
                    {
                        Height = 10,
                        Width = 10,
                        Thickness = 1
                    },
                    KnucklePoint = "-0.012,0.685,0.150",
                    PivotPoint1 = "0.250,0.350,0.150",
                    PivotPoint2 = "-0.100,0.350,0.130"
                },
                SuspensionAArmUpper = new SuspensionAArm<RectangularProfile>
                {
                    Profile = new RectangularProfile
                    {
                        Height = 10,
                        Width = 10,
                        Thickness = 1
                    },
                    KnucklePoint = "0.012,0.660,0.410",
                    PivotPoint1 = "0.200,0.450,0.362",
                    PivotPoint2 = "-0.080,0.450,0.362"
                },
                TieRod = new TieRod<RectangularProfile>
                {
                    Profile = new RectangularProfile
                    {
                        Height = 10,
                        Width = 10,
                        Thickness = 1
                    },
                    PivotPoint = "-0.125,0.370,0.176",
                    FasteningPoint = "-0.120,0.668,0.200"
                }
            };
        }
    }
}

# MudRunner.Suspension App
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## UNRELEASED
### Fixed
 - RunDynamicAnalysis operation that was not building correctly the equivalent force vector.
### Changed
 - RunDynamicAnalysis operations to calculate the system deformation.
 - Nuget package MudRunner.Commons.Core to 1.3.0.
 - Nuget package MudRunner.Commons.DataContracts to 1.3.0.
### Added
 - Enum CurveType.
 - Class BaseExcitation.
 - Class DynamicAnalysisResult.
 - Properties 'ConsiderLargeDisplacements', 'BaseExcitation' and 'CarSpeed' on class RunDynamicAnalysisRequest.
 - Properties 'MaximumResult' and 'MaximumDeformationResult' on class RunDynamicAnalysisResponseData.
 - Operations RunAmplitudeDynamicAnalysis, RunHalfCarSixDofDynamicAnalysis and RunHalfCarSixDofAmplitudeDynamicAnalysis.
 - Nuget package MudRunner.Commons.Application to 1.3.0.

## [1.2.0] - 2022-01-13
### Changed
 - Framework to NET6.0.
 - Nuget package MudRunner.Commons.Core to 1.2.0.
 - Nuget package MudRunner.Commons.DataContracts to 1.2.0.
 - Renamed suspension A-Arm to wishbone in all application.  
 - Renamed PivotPoint1 to FrontPivot in all application.
 - Renamed PivotPoint2 to RearPivot in all application.
 - Renamed KnucklePoint to OuterBallJoint in all application. 
### Removed
 - Codes that was in MudRunner.Commons.
### Added
 - Operation CalculateSteeringKnuckleReactions. 
 - Operation RunDynamicAnalysis.
 - Class BasePaths with the base paths for application.

## [1.1.0] - 2021-12-20
### Added
 - File Directory.Build.Props.
 - Fatigue constitutive equations and operation.
 - Methods 'Abs', 'Sum' and 'Subtract' to class Force.
 - Properties BucklingSafetyFactor and StressSafetyFactor to RunStaticAnalysisResponseData.
 - Extension method 'IsZero' for Vector3D.
### Changed
 - Renamed application from Suspension to MudRunner.Suspension.
 - Renamed RunAnalysis operation to RunStaticAnalysis and refactored it.
 - Renamed property 'ForceApplied' to 'AppliedForce' in class RunStaticAnalysisRequest.
 - Point3D to receive the point as string at milimeters instead of meters.
 - Profiles to receive the values in milimeters.
 - GeometricProperty to calculate the area in milimeters squared and moment of inertia in milimeters raised by four.
 - MechanicsOfMaterials to be concreate and do not receive the profile type.
 - Renamed class TieRodAnalysisResult to SingleComponentAnalysisResult.
 - File .gitignore to ignore the folder build.
 - Nuget Package Coverlet.collector to 3.1.0.
 - Nuget Package Moq to 4.16.1.
 - Nuget Package Microsoft.NET.Test.Sdk to 17.0.0.
 - Nuget Package FluentAssertions to 6.2.0.
 - Nuget Package Newtonsoft.Json to 13.0.1.
 - Nuget Package Swashbuckle.AspNetCore to 6.2.3.
 - Nuget Package Swashbuckle.AspNetCore.Newtonsoft to 6.2.3.
### Removed
 - Classes CircularProfileMechanicsOfMaterials and RectangularProfileMechanicsOfMaterials.
 - Classes OperationError, OperationErroCode, OperationRequestData and OperationRequest<TData>.

## [1.0.1] - 2021-11-23
### Changed
 - Renamed SuspensionAnalysis to Suspension.
 - Moved files CHANGELOG.MD and README.MD to main folder.

## [1.0.0] - 2021-04-20
### Added
 - First version of the program.
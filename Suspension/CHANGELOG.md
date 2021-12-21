# MudRunner.Suspension App
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## UNRELEASED
### Added
 - File Directory.Build.Props.
 - Fatigue constitutive equations and operation.
 - Methods 'Abs', 'Sum' and 'Subtract' to class Force.
 - Properties BucklingSafetyFactor and StressSafetyFactor to RunStaticAnalysisResponseData.
 - Extension method 'IsZero' for Vector3D.
### Changed
 - Renamed application from Suspensionto MudRunner.Suspension.
 - Renamed RunAnalysis operation to RunStaticAnalysis and refactored it.
 - Point3D to receive the point as string at milimeters instead of meters.
 - Profiles to receive the values in milimeters.
 - GeometricProperty to calculate the area in milimeters squared and moment of inertia in milimeters raised by four.
 - Renamed property 'ForceApplied' to 'AppliedForce' in class RunAnalysisRequest.
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
 - OperationError and OperationErroCode.

## [1.0.1] - 2021-11-23
### Changed
 - Renamed SuspensionAnalysis to Suspension.
 - Moved files CHANGELOG.MD and README.MD to main folder.

## [1.0.0] - 2021-04-20
### Added
 - First version of the program.
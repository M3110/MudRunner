# Mud Runner App
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## UNRELEASED
### Added
 - Fatigue constitutive equations and operation.
 - Methods 'Abs', 'Sum' and 'Subtract' to class Force.
 - Properties BucklingSafetyFactor and StressSafetyFactor to RunStaticAnalysisResponseData.
 - Extension method 'IsZero' for Vector3D.
### Changed
 - Renamed RunAnalysis operation to RunStaticAnalysis and refactored it.
 - Point3D to receive the point as string at milimeters instead of meters.
 - Profiles to receive the values in milimeters.
 - GeometricProperty to calculate the area in milimeters squared and moment of inertia in milimeters raised by four.
 - Renamed property 'ForceApplied' to 'AppliedForce' in class RunAnalysisRequest.
 - MechanicsOfMaterials to be concreate and do not receive the profile type.
 - Renamed class TieRodAnalysisResult to SingleComponentAnalysisResult.
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
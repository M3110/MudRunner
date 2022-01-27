# MudRunner.Commons App
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## UNRELEASED
### Added
 - Project MudRunner.Commons.Application.
 - Property 'InitialTime' in class Constants.
 - Class UnitConverter.
 - Class DifferentialEquationMethodFactory.
### Removed
 - Property 'InitialTime' in class NumericalMethodInput.
 - Classes NewmarkMethodInput and NewmarkBetaMethodInput.
### Changed
 - Refactored numerical methods.
### Fixed
 - Class OperationBase that was not processing the operation correctly.
 - Method 'AddErros' in class OperationResponseBase that was not adding errors when HttpStatusCode is not success.

## [1.2.0] - 2022-01-15
### Added
 - Newmark and Newmark-Beta numerical methods.
 - Methods 'Multiply', 'Sum', 'Subtract' and 'MathOperation' in class ArrayExtension.
 - Method 'SetConflictError' in class OperationResponseBase.
 - Method 'ValidateAsync' in class OperationBase and interface IOperationBase.
 - Method 'AddErrors' receiving an OperationResponseBase in class OperationResponseBase.
 - Method 'IsHttpStatusCode' class OperationResponseBase.
 - Property 'GravityAcceleration' in class Constants.
### Changed
 - Method 'ValidateOperationAsync' to be abstract.

## [1.1.0] - 2022-01-13
### Added
 - Method 'Create' in class Force to create a new instance of Force based on a string.
 - Constructor for class Force that receives the axis x, y and z.

## [1.0.0] - 2021-12-20
### Added
 - First version of the program.
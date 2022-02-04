# MudRunner.Commons App
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [1.3.0] - 2022-02-04
### Added
 - Project MudRunner.Commons.Application.
 - Property 'InitialTime' on class Constants.
 - Class UnitConverter.
 - Class DifferentialEquationMethodFactory.
 - Constructor for class NumericalMethodResult that receives the number of boundary conditions.
 - Method 'SetSuccessPartialContent' on class OperationResponseBase.
### Removed
 - Property 'InitialTime' on class NumericalMethodInput.
 - Classes NewmarkMethodInput and NewmarkBetaMethodInput.
### Changed
 - Refactored numerical methods.
### Fixed
 - Class OperationBase that was not processing the operation correctly.
 - Method 'AddErros' on class OperationResponseBase that was not adding errors when HttpStatusCode is not success.
 - Method 'CalculateEquivalentForceAsync' on class NewmarkMethod that was returning null vectors.
 - Method 'CalculateEquivalentForceAsync' on class NewmarkBetaMethod that was returning null vectors.
 - Class NewmarkBetaMethod that was not calculating the acceleration acording to Newmark-Beta method.

## [1.2.0] - 2022-01-15
### Added
 - Newmark and Newmark-Beta numerical methods.
 - Methods 'Multiply', 'Sum', 'Subtract' and 'MathOperation' on class ArrayExtension.
 - Method 'SetConflictError' on class OperationResponseBase.
 - Method 'ValidateAsync' on class OperationBase and interface IOperationBase.
 - Method 'AddErrors' receiving an OperationResponseBase on class OperationResponseBase.
 - Method 'IsHttpStatusCode' class OperationResponseBase.
 - Property 'GravityAcceleration' on class Constants.
### Changed
 - Method 'ValidateOperationAsync' to be abstract.

## [1.1.0] - 2022-01-13
### Added
 - Method 'Create' on class Force to create a new instance of Force based on a string.
 - Constructor for class Force that receives the axis x, y and z.

## [1.0.0] - 2021-12-20
### Added
 - First version of the program.
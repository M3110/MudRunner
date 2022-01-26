namespace MudRunner.Suspension.DataContracts.RunAnalysis.Dynamic.HalfCar.SixDegreeOfFreedom
{
    /// <summary>
    /// It represents the request content of RunHalfCarSixDofDynamicAnalysis operation.
    /// </summary>
    public class RunHalfCarSixDofDynamicAnalysisRequest : RunDynamicAnalysisRequest
    {
        #region Car parameters.

        /// <summary>
        /// The mass distribution in the front.
        /// Dimensionless.
        /// </summary>
        public double FrontMassDistribution { get; set; }

        /// <summary>
        /// The mass distribution in the rear.
        /// Dimensionless.
        /// </summary>
        public double RearMassDistribution { get; set; }

        /// <summary>
        /// The car mass without the engine mass and unsprung masses.
        /// Unit: kg (kilogram).
        /// </summary>
        public double CarMass { get; set; }

        /// <summary>
        /// The moment of inertia of the car without the engine mass and unsprung masses.
        /// Unit: kg/m² (kilogram per squared meter).
        /// </summary>
        public double CarMomentOfInertia { get; set; }

        #endregion

        #region Engine parameters.

        /// <summary>
        /// Unit: kg (kilogram).
        /// </summary>
        public double EngineMass { get; set; }

        /// <summary>
        /// Unit: N/m (Newton per meter).
        /// </summary>
        public double EngineMountStiffness { get; set; }

        /// <summary>
        /// The distance between gravity center and engine.
        /// Unit: m (meter).
        /// </summary>
        public double EngineDistance { get; set; } 

        /// <summary>
        /// Unit: N (Newton).
        /// </summary>
        public double EngineForce { get; set; }

        /// <summary>
        /// Unit: rad/s (radian per second).
        /// </summary>
        public double EngineFrequency { get; set; }

        #endregion

        #region Rear parameters.

        /// <summary>
        /// Unit: N.s/m (Newton-second per meter).
        /// </summary>
        public double RearDamping { get; set; }

        /// <summary>
        /// Unit: N/m (Newton per meter).
        /// </summary>
        public double RearStiffness { get; set; }

        /// <summary>
        /// Unit: N/m (Newton per meter).
        /// </summary>
        public double RearTireStiffness { get; set; }

        /// <summary>
        /// The distance between gravity center and rear of the car.
        /// Unit: m (meter).
        /// </summary>
        public double RearDistance { get; set; }

        #endregion

        #region Front parameters.

        /// <summary>
        /// Unit: N.s/m (Newton-second per meter).
        /// </summary>
        public double FrontDamping { get; set; }

        /// <summary>
        /// Unit: N/m (Newton per meter).
        /// </summary>
        public double FrontStiffness { get; set; }

        /// <summary>
        /// Unit: N/m (Newton per meter).
        /// </summary>
        public double FrontTireStiffness { get; set; }

        /// <summary>
        /// The distance between gravity center and front of the car.
        /// Unit: m (meter).
        /// </summary>
        public double FrontDistance { get; set; } 

        #endregion

        #region Unsprung masses.

        /// <summary>
        /// Unit: kg (kilogram).
        /// </summary>
        public double RearUnsprungMass { get; set; }

        /// <summary>
        /// Unit: kg (kilogram).
        /// </summary>
        public double FrontUnsprungMass { get; set; } 

        #endregion

        #region Driver parameters.

        /// <summary>
        /// Unit: kg (kilogram).
        /// </summary>
        public double DriverMass { get; set; }

        /// <summary>
        /// Unit: N/m (Newton per meter).
        /// </summary>
        public double SeatStiffness { get; set; }

        /// <summary>
        /// The distance between gravity center and driver.
        /// Unit: m (meter).
        /// </summary>
        public double DriverDistance { get; set; } 

        #endregion
    }
}

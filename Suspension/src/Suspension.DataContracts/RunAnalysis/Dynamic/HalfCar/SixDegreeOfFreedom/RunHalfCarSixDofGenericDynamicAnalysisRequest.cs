namespace MudRunner.Suspension.DataContracts.RunAnalysis.Dynamic.HalfCar.SixDegreeOfFreedom
{
    /// <summary>
    /// It represents the generic request content of half car dynamic analysis with 6 degree of freedom operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RunHalfCarSixDofGenericDynamicAnalysisRequest<T> : RunGenericDynamicAnalysisRequest
    {
        #region Car parameters.

        /// <summary>
        /// The mass distribution in the front.
        /// Dimensionless.
        /// </summary>
        public T FrontMassDistribution { get; set; }

        /// <summary>
        /// The mass distribution in the rear.
        /// Dimensionless.
        /// </summary>
        public T RearMassDistribution { get; set; }

        /// <summary>
        /// The car mass without the engine mass and unsprung masses.
        /// Unit: kg (kilogram).
        /// </summary>
        public T CarMass { get; set; }

        /// <summary>
        /// The moment of inertia of the car without the engine mass and unsprung masses.
        /// Unit: kg/m² (kilogram per squared meter).
        /// </summary>
        public T CarMomentOfInertia { get; set; }

        #endregion

        #region Engine parameters.

        /// <summary>
        /// Unit: kg (kilogram).
        /// </summary>
        public T EngineMass { get; set; }

        /// <summary>
        /// Unit: N/m (Newton per meter).
        /// </summary>
        public T EngineMountStiffness { get; set; }

        /// <summary>
        /// The distance between gravity center and engine.
        /// Unit: m (meter).
        /// </summary>
        public T EngineDistance { get; set; } 

        /// <summary>
        /// Unit: N (Newton).
        /// </summary>
        public T EngineForce { get; set; }

        /// <summary>
        /// Unit: rad/s (radian per second).
        /// </summary>
        public T EngineFrequency { get; set; }

        #endregion

        #region Rear parameters.

        /// <summary>
        /// Unit: N.s/m (Newton-second per meter).
        /// </summary>
        public T RearDamping { get; set; }

        /// <summary>
        /// Unit: N/m (Newton per meter).
        /// </summary>
        public T RearStiffness { get; set; }

        /// <summary>
        /// Unit: N/m (Newton per meter).
        /// </summary>
        public T RearTireStiffness { get; set; }

        /// <summary>
        /// The distance between gravity center and rear of the car.
        /// Unit: m (meter).
        /// </summary>
        public T RearDistance { get; set; }

        #endregion

        #region Front parameters.

        /// <summary>
        /// Unit: N.s/m (Newton-second per meter).
        /// </summary>
        public T FrontDamping { get; set; }

        /// <summary>
        /// Unit: N/m (Newton per meter).
        /// </summary>
        public T FrontStiffness { get; set; }

        /// <summary>
        /// Unit: N/m (Newton per meter).
        /// </summary>
        public T FrontTireStiffness { get; set; }

        /// <summary>
        /// The distance between gravity center and front of the car.
        /// Unit: m (meter).
        /// </summary>
        public T FrontDistance { get; set; } 

        #endregion

        #region Unsprung masses.

        /// <summary>
        /// Unit: kg (kilogram).
        /// </summary>
        public T RearUnsprungMass { get; set; }

        /// <summary>
        /// Unit: kg (kilogram).
        /// </summary>
        public T FrontUnsprungMass { get; set; } 

        #endregion

        #region Driver parameters.

        /// <summary>
        /// Unit: kg (kilogram).
        /// </summary>
        public T DriverMass { get; set; }

        /// <summary>
        /// Unit: N/m (Newton per meter).
        /// </summary>
        public T SeatStiffness { get; set; }

        /// <summary>
        /// The distance between gravity center and driver.
        /// Unit: m (meter).
        /// </summary>
        public T DriverDistance { get; set; } 

        #endregion
    }
}

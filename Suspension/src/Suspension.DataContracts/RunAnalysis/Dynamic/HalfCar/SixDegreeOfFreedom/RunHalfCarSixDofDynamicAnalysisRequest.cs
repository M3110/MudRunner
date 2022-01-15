namespace MudRunner.Suspension.DataContracts.RunAnalysis.Dynamic.HalfCar.SixDegreeOfFreedom
{
    /// <summary>
    /// It represents the request content of RunHalfCarSixDofDynamicAnalysis operation.
    /// </summary>
    public class RunHalfCarSixDofDynamicAnalysisRequest
    {
        /// <summary>
        /// The car mass without the engine mass.
        /// Unit: kg (kilogram).
        /// </summary>
        public double CarMass { get; set; }

        /// <summary>
        /// The mass distribution in the front.
        /// </summary>
        public double FrontMassDistribution { get; set; }

        /// <summary>
        /// The mass distribution in the rear.
        /// </summary>
        public double RearMassDistribution { get; set; }
    }
}

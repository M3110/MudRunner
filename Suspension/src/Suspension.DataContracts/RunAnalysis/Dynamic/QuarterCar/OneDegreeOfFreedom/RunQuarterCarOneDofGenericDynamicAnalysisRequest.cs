namespace MudRunner.Suspension.DataContracts.RunAnalysis.Dynamic.QuarterCar.OneDegreeOfFreedom
{
    /// <summary>
    /// It represents the generic request content of quarter car dynamic analysis with 1 degree of freedom operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RunQuarterCarOneDofGenericDynamicAnalysisRequest<T> : RunGenericDynamicAnalysisRequest
    {
        /// <summary>
        /// Unit: kg (kilogram).
        /// </summary>
        public T Mass { get; set; }

        /// <summary>
        /// Unit: N.s/m (Newton-second per meter).
        /// </summary>
        public T Damping { get; set; }

        /// <summary>
        /// Unit: N/m (Newton per meter).
        /// </summary>
        public T Stiffness { get; set; }

        /// <summary>
        /// Unit: N (Newton).
        /// </summary>
        public T Force { get; set; }

        /// <summary>
        /// Unit: Hz (Hertz).
        /// </summary>
        public T Frequency { get; set; }
    }
}

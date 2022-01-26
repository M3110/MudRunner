namespace MudRunner.Suspension.DataContracts.Models.Enums
{
    /// <summary>
    /// It contains the accepted curve types for curve fitting.
    /// </summary>
    public enum CurveType
    {
        /// <summary>
        /// Equation: f(x) = a_0 + a_1*x + a_2*x^2 + ... + a_n*x^n
        /// </summary>
        Polinomial = 1,

        /// <summary>
        /// Equation: f(x) = a_0 * exp(a_1 * x) + a_2 * exp(a_3 * x) + ... + a_n-1 * exp(a_n * x)
        /// </summary>
        Exponencial = 2,

        /// <summary>
        /// Equation: 
        ///     f(x) = (a_0 / 2) * (a_1 + a_2 * cos(w * x)), with x = [t_0, t_1]
        ///     f(x) = (a_3 / 2) * (a_4 + a_5 * cos(w * x)), with x = [t_2, t_3]
        ///     ...
        ///     f(x) = (a_n-2 / 2) * (a_n-1 + a_n * cos(w * x)), with x = [t_m-1, t_m]
        /// </summary>
        Cosine = 3,
    }
}

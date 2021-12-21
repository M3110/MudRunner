using MudRunner.Commons.Core.Models.Fatigue;
using MudRunner.Commons.DataContracts.Models.Profiles;

namespace MudRunner.Commons.Core.ConstitutiveEquations.Fatigue
{
    /// <summary>
    /// It contains the Mechanical Fatigue constitutive equations.
    /// </summary>
    public interface IFatigue<TProfile>
        where TProfile : Profile
    {
        /// <summary>
        /// This method calculates the result for fatigue analysis.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        FatigueResult CalculateFatigueResult(FatigueInput<TProfile> input);

        /// <summary>
        /// This method calculates the modified fatigue stress.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        double CalculateModifiedFatigueStress(FatigueInput<TProfile> input);
    }
}
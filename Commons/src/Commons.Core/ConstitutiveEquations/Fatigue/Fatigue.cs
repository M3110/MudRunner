using MudRunner.Commons.Core.Models.Fatigue;
using MudRunner.Commons.DataContracts.Models.Enums;
using MudRunner.Commons.DataContracts.Models.Profiles;

namespace MudRunner.Commons.Core.ConstitutiveEquations.Fatigue
{
    /// <summary>
    /// It contains the Mechanical Fatigue constitutive equations.
    /// </summary>
    public class Fatigue<TProfile> : IFatigue<TProfile>
        where TProfile : Profile
    {
        /// <summary>
        /// This method calculates the result for fatigue analysis.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public FatigueResult CalculateFatigueResult(FatigueInput<TProfile> input)
        {
            double stressAmplitude = Math.Abs((input.MaximumAppliedStress - input.MinimumAppliedStress) / 2);
            double meanStress = (input.MaximumAppliedStress + input.MinimumAppliedStress) / 2;
            double equivalentStress = stressAmplitude / (1 - (meanStress / input.TensileStress));
            double modifiedFatigueStress = CalculateModifiedFatigueStress(input);

            double a = Math.Pow(input.FatigueLimitFraction * input.TensileStress, 2) / modifiedFatigueStress;
            double b = -Math.Log10(input.FatigueLimitFraction * input.TensileStress / modifiedFatigueStress) / 3;
            double numberOfCycles = Math.Pow(equivalentStress / a, 1 / b);

            return new()
            {
                StressAmplitude = stressAmplitude,
                MeanStress = meanStress,
                EquivalentStress = equivalentStress,
                NumberOfCycles = numberOfCycles > 1e6 ? 1e6 : numberOfCycles,
                SafetyFactor = Math.Pow(stressAmplitude / modifiedFatigueStress + meanStress / input.TensileStress, -1)
            };
        }

        /// <summary>
        /// This method calculates the modified fatigue stress (Se).
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public double CalculateModifiedFatigueStress(FatigueInput<TProfile> input)
        {
            return input.FatigueLimit
                * CalculateSurfaceFactor(input.TensileStress, input.SurfaceFinish)
                * CalculateSizeFactor(input.Profile, input.LoadingType, input.IsRotativeSection)
                * CalculateLoadingFactor(input.LoadingType)
                * CalculateTemperatureFactor(input.Temperature)
                * CalculateReliabilityFactor(input.Reliability);
        }

        /// <summary>
        /// This method calculates the fatigue surface factor.
        /// </summary>
        /// <param name="tensileStress"></param>
        /// <param name="surfaceFinish"></param>
        /// <returns></returns>
        private double CalculateSurfaceFactor(double tensileStress, SurfaceFinish surfaceFinish)
        {
            (double a, double b) = surfaceFinish switch
            {
                SurfaceFinish.Retified => (1.58, -0.085),
                SurfaceFinish.Machined => (4.51, -0.265),
                SurfaceFinish.ColdRolled => (4.51, -0.265),
                SurfaceFinish.HotRolled => (57.7, -0.718),
                SurfaceFinish.Wrought => (272, -0.995),
                _ => throw new ArgumentOutOfRangeException(nameof(surfaceFinish))
            };

            return a * Math.Pow(tensileStress, b);
        }

        /// <summary>
        /// This method calculates the fatigue size factor.
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="loadingType"></param>
        /// <returns></returns>
        private double CalculateSizeFactor(TProfile profile, LoadingType loadingType, bool isRotativeSection)
        {
            if (loadingType == LoadingType.Axial)
            {
                return 1;
            }

            double equivalentDiameter = 0;
            if (profile is CircularProfile)
            {
                CircularProfile circularProfile = profile as CircularProfile;
                equivalentDiameter = isRotativeSection ? circularProfile.Diameter : 0.37 * circularProfile.Diameter;
            }
            else if (profile is RectangularProfile)
            {
                RectangularProfile rectangularProfile = profile as RectangularProfile;
                equivalentDiameter = isRotativeSection ? rectangularProfile.Height : 0.808 * Math.Sqrt(rectangularProfile.Width * rectangularProfile.Height);
            }

            if (2.79 <= equivalentDiameter * 1000 && equivalentDiameter <= 51)
            {
                return Math.Pow(equivalentDiameter / 7.62, -0.107);
            }
            else if (51 < equivalentDiameter * 1000 && equivalentDiameter <= 254)
            {
                return 1.51 * Math.Pow(equivalentDiameter, -0.157);
            }

            throw new ArgumentOutOfRangeException(nameof(equivalentDiameter));
        }

        /// <summary>
        /// This method calculates the loading factor.
        /// </summary>
        /// <param name="loadingType"></param>
        /// <returns></returns>
        private double CalculateLoadingFactor(LoadingType loadingType)
        {
            return loadingType switch
            {
                LoadingType.Bending => 1,
                LoadingType.Axial => 0.85,
                LoadingType.Torsion => 0.59,
                _ => throw new ArgumentOutOfRangeException(nameof(loadingType))
            };
        }

        /// <summary>
        /// This method calculates the temperature factor.
        /// </summary>
        /// <param name="temperature"></param>
        /// <returns></returns>
        private double CalculateTemperatureFactor(double temperature)
        {
            // It always retuns 1 because for Baja SAE analysis, this property does not affect and
            // does not variate significantly.
            return 1;
        }

        /// <summary>
        /// This method calculates the reliability factor.
        /// </summary>
        /// <param name="reliability"></param>
        /// <returns></returns>
        private double CalculateReliabilityFactor(Reliability reliability)
        {
            return reliability switch
            {
                Reliability.Fifty => 1,
                Reliability.Ninety => 0.897,
                Reliability.NinetyFive => 0.868,
                Reliability.NinetyNine => 0.814,
                Reliability.NinetyNinePointNine => 0.753,
                _ => throw new ArgumentOutOfRangeException(nameof(reliability))
            };
        }
    }
}

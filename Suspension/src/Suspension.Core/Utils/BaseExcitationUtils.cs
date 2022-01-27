using MudRunner.Commons.Core.Models;
using MudRunner.Suspension.DataContracts.Models;
using MudRunner.Suspension.DataContracts.Models.Enums;
using System;
using System.Collections.Generic;

namespace MudRunner.Suspension.Core.Utils
{
    /// <summary>
    /// It contains useful methods for <see cref="BaseExcitation"/>.
    /// </summary>
    public static class BaseExcitationUtils
    {
        /// <summary>
        /// This method calculates the value for the base excitation to a specific time.
        /// </summary>
        /// <param name="baseExcitation"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static double Calculate(BaseExcitation baseExcitation, double time)
        {
            CurveType curveType = baseExcitation.CurveType;
            List<double> constants = baseExcitation.Constants;
            List<double> limitTimes = baseExcitation.LimitTimes;

            if (curveType == CurveType.Polinomial)
            {
                double result = 0;
                for (int i = 0; i < constants.Count; i++)
                {
                    result += constants[i] * Math.Pow(time, i);
                }

                return result;
            }

            if (curveType == CurveType.Exponencial)
            {
                double result = 0;
                for (int i = 0; i < constants.Count / 2; i++)
                {
                    result += constants[2 * i] * Math.Exp(constants[2 * i + 1] * time);
                }

                return result;
            }

            if (curveType == CurveType.Cosine)
            {
                // The speed of the car is in kilometers per hour when recieved in the request and it must be converted to meters per second
                // because all calculations must be done with the units according to International System of Units.
                double carSpeed = UnitConverter.FromKmHToMS(baseExcitation.CarSpeed);

                double frequency = 2 * Math.PI * carSpeed / baseExcitation.ObstacleWidth;

                double result = 0;
                if (limitTimes == null)
                {
                    if (Constants.InitialTime <= time && time <= Constants.InitialTime + baseExcitation.ObstacleWidth / carSpeed)
                    {
                        result = constants[0] / 2 * (constants[1] + constants[2] * Math.Cos(frequency * time));
                    }
                }
                else
                {
                    for (int i = 0; i < constants.Count / 3; i++)
                    {
                        if (limitTimes[2 * i] <= time && time < limitTimes[2 * i + 1])
                        {
                            result = constants[3 * i] / 2 * (constants[3 * i + 1] + constants[3 * i + 2] * Math.Cos(frequency * time));
                        }
                    }
                }

                return result;
            }

            throw new ArgumentOutOfRangeException(nameof(curveType), $"Invalid curve type: '{curveType}'.");
        }
    }
}

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
    // TODO: Generalizar método internamente pois todos os métodos seguem a mesma estrutura.
    public static class BaseExcitationUtils
    {
        /// <summary>
        /// This method calculates the displacement for the base excitation to a specific time.
        /// </summary>
        /// <param name="baseExcitation"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static double CalculateDisplacement(BaseExcitation baseExcitation, double time)
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

        /// <summary>
        /// This method calculates the velocity for the base excitation to a specific time.
        /// </summary>
        /// <param name="baseExcitation"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static double CalculateVelocity(BaseExcitation baseExcitation, double time)
        {
            CurveType curveType = baseExcitation.CurveType;
            List<double> constants = baseExcitation.Constants;
            List<double> limitTimes = baseExcitation.LimitTimes;

            if (curveType == CurveType.Polinomial)
            {
                double result = 0;
                for (int i = 1; i < constants.Count; i++)
                {
                    result += constants[i] * i * Math.Pow(time, i - 1);
                }

                return result;
            }

            if (curveType == CurveType.Exponencial)
            {
                double result = 0;
                for (int i = 0; i < constants.Count / 2; i++)
                {
                    result += constants[2 * i] * constants[2 * i + 1] * Math.Exp(constants[2 * i + 1] * time);
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
                        result = (constants[0] / 2) * frequency * constants[2] * (-Math.Sin(frequency * time));
                    }
                }
                else
                {
                    for (int i = 0; i < constants.Count / 3; i++)
                    {
                        if (limitTimes[2 * i] <= time && time < limitTimes[2 * i + 1])
                        {
                            result = (constants[3 * i] / 2) * frequency * constants[3 * i + 2] * (-Math.Sin(frequency * time));
                        }
                    }
                }

                return result;
            }

            throw new ArgumentOutOfRangeException(nameof(curveType), $"Invalid curve type: '{curveType}'.");
        }

        /// <summary>
        /// This method calculates the acceleration for the base excitation to a specific time.
        /// </summary>
        /// <param name="baseExcitation"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static double CalculateAcceleration(BaseExcitation baseExcitation, double time)
        {
            CurveType curveType = baseExcitation.CurveType;
            List<double> constants = baseExcitation.Constants;
            List<double> limitTimes = baseExcitation.LimitTimes;

            if (curveType == CurveType.Polinomial)
            {
                double result = 0;
                for (int i = 2; i < constants.Count; i++)
                {
                    result += constants[i] * i * (i - 1) * Math.Pow(time, i - 2);
                }

                return result;
            }

            if (curveType == CurveType.Exponencial)
            {
                double result = 0;
                for (int i = 0; i < constants.Count / 2; i++)
                {
                    result += constants[2 * i] * Math.Pow(constants[2 * i + 1], 2) * Math.Exp(constants[2 * i + 1] * time);
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
                        result = (constants[0] / 2) * Math.Pow(frequency, 2) * constants[2] * (-Math.Cos(frequency * time));
                    }
                }
                else
                {
                    for (int i = 0; i < constants.Count / 3; i++)
                    {
                        if (limitTimes[2 * i] <= time && time < limitTimes[2 * i + 1])
                        {
                            result = (constants[3 * i] / 2) * Math.Pow(frequency, 2) * constants[3 * i + 2] * (-Math.Cos(frequency * time));
                        }
                    }
                }

                return result;
            }

            throw new ArgumentOutOfRangeException(nameof(curveType), $"Invalid curve type: '{curveType}'.");
        }
    }
}

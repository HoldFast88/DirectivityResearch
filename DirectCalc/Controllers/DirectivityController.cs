using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics;
using MathNet;
using MathNet.Numerics.Integration;

namespace DirectCalc
{
    class DirectivityController
    {
        /////////////////////////////////////////////////////////////////////////////////////////////

        static public double CountDirectivityRate(MicrophoneType microphoneType, double frequency, double deltha, Int32 count, double diameter)
        {
            double result = Integrate.OnClosedInterval(x => (Math.Pow(DirectivityController.CountDirectivityDependence(microphoneType, frequency, deltha, count, diameter, x), 2) * Math.Sin(x)), 0, Math.PI);

            double resultForReturning = 10 * Math.Log10(2 / result);
            return resultForReturning;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        static public double CountDirectivityDependence(MicrophoneType microphoneType, double frequency, double deltha, Int32 count, double diameter, double theta)
        {
            double wavelenght = (double)331 / (double)frequency;
            double angle = theta;
            /*
             * Микрофон органного типа
             * */
            //*
            double first = 0.0;
            double second = 0.0;

            switch (microphoneType)
            {
                case MicrophoneType.MicrophoneTypeOrgan:
                    {
                    mark1: ;
                    first = Math.Sin((count * Math.PI * deltha * (1 - Math.Cos(angle))) / (wavelenght));
                    second = count * Math.Sin((Math.PI * deltha * (1 - Math.Cos(angle))) / (wavelenght));
                        if (second == 0.0)
                        {
                            // Судя по всему, функция Math.Cos() достаточно грубо округляет значения и считает что Math.Cos(0.00000000000001) = 1.
                            // Looks like Math.Cos() function round value pretty crude so we need to increase this value manually.
                            angle += 0.00000001;
                            goto mark1;
                        }
                    }
                    break;

                case MicrophoneType.MicrophoneTypeLinear:
                    {
                    mark2: ;
                    first = Math.Sin(Math.Sin(angle) * Convert.ToDouble(count) * Math.PI * deltha / wavelenght);
                    second = Convert.ToDouble(count) * Math.Sin(Math.Sin(angle) * Math.PI * deltha / wavelenght);
                        if (second == 0.0)
                        {
                            angle = Math.PI;
                            goto mark2;
                        }
                    }
                    break;

                case MicrophoneType.MicrophoneTypeParabolic:
                    {
                        double radius = diameter / 2;
                    mark2: ;
                        first = (1 + Math.Cos(angle)) * alglib.besselj1((2 * Math.PI * radius * Math.Sin(angle)) / wavelenght) * wavelenght;
                        second = 4 * Math.PI * radius * Math.Sin(angle);

                        /*
                        double k = 2 * Math.PI / wavelenght;

                        first = 2 * alglib.besselj1(k * radius * Math.Sin(angle));
                        second = k * radius * Math.Sin(angle);
                        */

                        if (first == 0.0)
                        {
                            angle += 0.000001;
                            goto mark2;
                        }
                    }
                    break;

                default:
                    {
                    }
                    break;
            }

            double result = Math.Abs(first / second);
            return result;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectCalc
{
    class IntelligibilityController
    {
        static public double CountIntelligibility(MicrophoneType microphoneType, double deltha, Int32 count, double diameter)
        {
            /*
             * Set center and border frequencies for bands.
             */

            double[] frequencyBorders = {/* 90.0, 175.0,*/
                                                    175.0, 355.0,
                                                    355.0, 710.0,
                                                    710.0, 1400.0,
                                                    1400.0, 2800.0,
                                                    2800.0, 5600.0,
                                                   /* 5600.0, 11200.0 */};

            double[] centerFrequencies = {/* 125.0, */250.0, 500.0, 1000.0, 2000.0, 4000.0/*, 8000.0 */};

            double[] weightCoefficients = {/* 0.01, */0.03, 0.12, 0.20, 0.30, 0.26/*, 0.07 */};

            double[] formantParameters = {/* 25.0, */18.0, 14.0, 9.0, 6.0, 5.0/*, 4.0 */};

            /*
             * Count coefficients of perception for each band.
             */

            List<double> coefficientsOfPerception = new List<double>();

            for (int i = 0; i < centerFrequencies.Length; i++)
            {
                double frequency = centerFrequencies[i];
                double SNR = DirectivityController.CountDirectivityRate(microphoneType, frequency, deltha, count, diameter); // dB

                double val = formantParameters[i];
                double Q = SNR - val;

                double coefficientOfPerception = 0.0;

                double qAbsValue = Math.Abs(Q);
                double constDependance = (0.78 + 5.46 * Math.Exp(-4.3 * Math.Pow(10, -3) * (27.3 - qAbsValue * qAbsValue))) / (1 + Math.Pow(10, 0.1 * qAbsValue));

                if (Q <= 0)
                {
                    coefficientOfPerception = constDependance;
                }
                else if (Q > 0)
                {
                    coefficientOfPerception = 1 - constDependance;
                }

                coefficientsOfPerception.Add(coefficientOfPerception);
            }

            /*
             * Count spectral indexes of articulation for each band.
             */

            List<double> spectralIndexesOfArticulation = new List<double>();

            for (int i = 0; i < centerFrequencies.Length; i++)
            {
                double coefficientOfPerception = coefficientsOfPerception[i];
                double weightCoefficient = weightCoefficients[i];

                double spectralIndexOfArticulation = coefficientOfPerception * weightCoefficient;
                spectralIndexesOfArticulation.Add(spectralIndexOfArticulation);
            }

            /*
             * Count integral index of articulation for each band.
             */

            double integralIndexOfArticulation = 0.0;

            for (int i = 0; i < spectralIndexesOfArticulation.Count; i++)
            {
                double index = spectralIndexesOfArticulation[i];
                integralIndexOfArticulation += index;
            }

            /*
             * Count syllable intelligibility
             */

            double syllableIntelligibility = 0.0;
            
            if (integralIndexOfArticulation <= 0.15)
            {
                syllableIntelligibility = Math.Pow(4 * integralIndexOfArticulation, 1.43);
            }
            else if ( (integralIndexOfArticulation > 0.15) && (integralIndexOfArticulation <= 0.7) )
            {
                syllableIntelligibility = 1.1 * (1 - 1.17 * Math.Exp(-2.9 * integralIndexOfArticulation));
            }
            else if (integralIndexOfArticulation > 0.7)
            {
                syllableIntelligibility = 1.01 * (1 - 9.1 * Math.Exp(-6.9 * integralIndexOfArticulation));
            }

            double wordsIntelligibility = 1.05 * (1 - Math.Exp((-6.15 * syllableIntelligibility) / (1 + syllableIntelligibility)));
            
            return wordsIntelligibility;
        }
    }
}

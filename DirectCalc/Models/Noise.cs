using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectCalc
{
    class Noise
    {
        public NoiseType type;
        public List<double> noiseArray;
        public Microphone sourceMicrophone;
        public double rate; // dB
        public UInt32 noiseDirection;

        public Noise(NoiseType _type)
        {
            type = _type;
        }

        ///////////////////

        private double ConvertToDB(double value)
        {
            return 10 * Math.Log10(value);
        }

        private double ConvertFromDB(double value)
        {
            return 2 / Math.Pow(10, value/10);
        }

        ///////////////////

        public void FormNoise()
        {
            if (noiseArray == null)
            {
                noiseArray = new List<double>();
            }
            else
            {
                noiseArray.Clear();
            }

            switch (type)
            {
                case NoiseType.NoiseTypeBrown:
                    {
                        for (int i = 0; i < sourceMicrophone.frequinciesList.Length; i++)
                        {
                            double frequency = sourceMicrophone.frequinciesList[i];

                            // нужно для учёта направления прихода звука
                            double[] coefficients = (double[])sourceMicrophone.createArrayForDirectivityPlot(Convert.ToUInt32(frequency));
                            //double waveAngleCoefficient = coefficients[noiseDirection];
                            double waveAngleCoefficientdB = 0;//ConvertToDB(waveAngleCoefficient); // dB value
                            //double result = ConvertToDB(1 / (frequency * frequency)) + (rate + waveAngleCoefficientdB);
                            double res = (1 / (frequency * frequency)) * ConvertFromDB(rate + waveAngleCoefficientdB);
                            //double result = ConvertToDB(res);

                            noiseArray.Add(res);
                        }
                    }
                    break;

                default:
                    {
                    }
                    break;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectCalc
{
    enum NoiseType
    {
        NoiseTypeBrown
    }

    class Noise
    {
        public NoiseType type;
        public List<double> noiseArray;
        public Microphone sourceMicrophone;
        public double rate; // dB

        public Noise(NoiseType _type)
        {
            type = _type;
        }

        private double ConvertToDB(double value)
        {
            return 10 * Math.Log10(value);
        }

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

                            double result = ConvertToDB(1 / (frequency * frequency)) + rate;

                            noiseArray.Add(result);
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

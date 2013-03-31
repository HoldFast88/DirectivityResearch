using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectCalc
{
    class Microphone
    {
        public MicrophoneProperties properties;
        public String title;
        public double[] frequinciesList;
        public double[] directivityValuesList;

        public Microphone() { }

        public Microphone(String _title, MicrophoneProperties _properties) 
        {
            title = _title;
            properties = _properties;
        }

        public void AddNoise(Noise noise)
        {
            noise.sourceMicrophone = this;
            noise.FormNoise();

            for (int i = 0; i < noise.noiseArray.Count; i++)
            {
                directivityValuesList[i] += noise.noiseArray[i];
            }
        }

        public System.Array createArrayForDirectivityPlot(UInt32 frequency)
        {
            int count = 360;

            double[] array = new double[count];
            double pi = Math.PI;

            double maxValue = 0;
            for (int i = 0; i < count; i++)
            {
                double wavelenght = (double)((double)320 / (double)frequency);
                double angle = ((double)i / (double)count) * 2 * pi;

                double value = DirectivityController.CountDirectivityDependence(this.properties.microphoneType, frequency, this.properties.deltha, this.properties.count, this.properties.diameter, angle);

                array[i] = value;
                if (value > maxValue)
                    maxValue = value;
            }

            for (int i = 0; i < array.Length; i++)
            {
                double element = array[i];
                array[i] = element / maxValue;
            }

            return array;
        }


        public void buildDirectivityDepencity(double minimumFrequency, double maximumFrequency)
        {
            int count = 1000;

            double deltha = maximumFrequency - minimumFrequency;
            Int32 num = this.properties.count;
            double d = this.properties.deltha;
            MicrophoneType microphoneType = this.properties.microphoneType;
            double diameter = this.properties.diameter;

            frequinciesList = new double[count];
            directivityValuesList = new double[count];

            Array[] arrayForReturn = new Array[2];

            switch (microphoneType)
            {
                case MicrophoneType.MicrophoneTypeLinear:
                    {
                        for (int i = 0; i < count; i++)
                        {
                            double freq = minimumFrequency + (deltha / count) * (i + 1);
                            directivityValuesList[i] = (DirectivityController.CountDirectivityRate(microphoneType, freq, d, num, diameter));
                            frequinciesList[i] = freq;
                        }
                    }
                    break;

                case MicrophoneType.MicrophoneTypeOrgan:
                    {
                        for (int i = 0; i < count; i++)
                        {
                            double freq = minimumFrequency + (deltha / count) * (i + 1);
                            directivityValuesList[i] = DirectivityController.CountDirectivityRate(microphoneType, freq, d, num, diameter);
                            frequinciesList[i] = freq;
                        }
                    }
                    break;

                case MicrophoneType.MicrophoneTypeParabolic:
                    {
                        for (int i = 0; i < count; i++)
                        {
                            double freq = minimumFrequency + (deltha / count) * (i + 1);
                            double theta = Math.PI / (i / count);

                            double wavelenght = (double)331 / (double)freq;

                            double first = 5 * Math.PI * ((diameter / 2) * (diameter / 2));
                            double second = wavelenght * wavelenght; // wavelenght ^ 2

                            directivityValuesList[i] = 10 * Math.Log10(Math.Abs(first / second));
                            frequinciesList[i] = freq;
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

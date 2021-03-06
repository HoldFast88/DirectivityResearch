﻿using System;
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

        ///////////////////

        private double ConvertToDB(double value)
        {
            return 10 * Math.Log10(value);
        }

        private double ConvertFromDB(double value)
        {
            return 2 / Math.Pow(10, value / 10);
        }

        ///////////////////

        public void AddNoise(Noise noise)
        {
            noise.sourceMicrophone = this;
            noise.FormNoise();

            for (int i = 0; i < noise.noiseArray.Count; i++)
            {
                double directivityPlotValue = directivityValuesList[i];
                double valueFromDB = ConvertFromDB(directivityPlotValue);
                double resultValue = valueFromDB - noise.noiseArray[i];
                double resultValueDB = ConvertToDB(resultValue);
                directivityValuesList[i] = resultValueDB;
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

                            double radius = (diameter / 2);

                            double first = 4 * Math.PI * Math.PI * radius * radius;
                            double second = wavelenght * wavelenght;

                            directivityValuesList[i] = 10 * Math.Log10(Math.Abs(first / second));
                             
                            //directivityValuesList[i] = DirectivityController.CountDirectivityRate(microphoneType, freq, d, num, diameter);
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

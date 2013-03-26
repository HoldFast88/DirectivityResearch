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

        public Microphone() { }

        public Microphone(String _title, MicrophoneProperties _properties) 
        {
            title = _title;
            properties = _properties;
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
    }
}

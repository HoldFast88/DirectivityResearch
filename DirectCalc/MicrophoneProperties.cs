using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectCalc
{
    class MicrophoneProperties
    {
        public MicrophoneType microphoneType;
        public double deltha;
        public Int32 count;
        public double diameter;

        public MicrophoneProperties() {}

        public MicrophoneProperties(MicrophoneType _microphoneType, double _deltha, Int32 _count, double _diameter)
        {
            microphoneType = _microphoneType;
            deltha = _deltha;
            count = _count;
            diameter = _diameter;
        }
    }
}

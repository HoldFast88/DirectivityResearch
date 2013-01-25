using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mic
{
    class PlotController
    {
        public static System.Array DrawPlot(MicType type, uint frequency, uint count, uint tubesNumber, double deltha)
        {
            double[] array = new double[count];
            switch (type)
            {
                case (MicType.MicTypeAllDirectional):
                    {
                        for (int i = 0; i < count; i++)
                        {
                            array[i] = (double)1;
                        }
                    }
                    break;

                case (MicType.MicTypeOrgan):
                    {
                        double pi = Math.PI;
                        for (int i = 0; i < count; i++)
                        {
                            double wavelenght = (double)((double)320 / (double)frequency);
                            double x = pi * deltha * (1 - Math.Cos(i * pi / 180)) / wavelenght;
                            double angle = ((double)i / (double)count) * 2 * pi;
                            /*
                             * Органный микрофон.
                             */
                            double first = Math.Sin((tubesNumber * Math.PI * deltha * (1 - Math.Cos(angle))) / (wavelenght));
                            double second = tubesNumber * Math.Sin((Math.PI * deltha * (1 - Math.Cos(angle))) / (wavelenght));
                            double value = first / second;
                            /*
                             * Линейная группа микрофонов
                             * TODO: вынести в отдельный case и сделать вьюшки
                             */
                            /*
                            mark:;
                            double first = Math.Sin(Math.Sin(angle) * tubesNumber * Math.PI * deltha / wavelenght);
                            double second = tubesNumber * Math.Sin(Math.Sin(angle) * Math.PI * deltha / wavelenght);
                            double value = first / second;
                            if (second == 0)
                            {
                                angle = Math.PI;
                                goto mark;
                            }
                             */
                            double val = Math.Abs(value);
                            array[i] = val;
                        }

                        double maxValue = 0;
                        for (int i = 0; i < array.Length; i++)
                        {
                            if (array[i] > maxValue)
                                maxValue = array[i];
                        }

                        for (int i = 0; i < array.Length; i++)
                        {
                            double element = array[i];
                            array[i] = element / maxValue;
                        }
                    }
                    break;

                default:
                    {
                    }
                    break;
            }
            return array;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////

        public static System.Array DrawNoisePlot(float median, float delta, float step, uint count)
        {
            return null;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////
    }
}

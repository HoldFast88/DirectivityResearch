using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MathNet.Numerics;
using MathNet;
using MathNet.Numerics.Integration;

namespace DirectCalc
{
    enum MicrophoneType
    {
        MicrophoneTypeOrgan = 0,
        MicrophoneTypeLinear = 1,
        MicrophoneTypeParabolic = 2
    };

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private void button1_Click(object sender, EventArgs e)
        {
            bool isFrequencyFieldIsEmpty = frequencyField.Text.Length == 0;
            bool isMinimumFrequencyFieldIsEmpty = frequencyTextField.Text.Length == 0;
            bool isMaximumFrequencyFieldIsEmpty = frequencyMaxTextBox.Text.Length == 0;
            bool isDiameterFieldIsEmpty = diameterTextBox.Text.Length == 0;

            Form2 newMDIChild = new Form2();

            int numberOfCheckedCheckboxes = Convert.ToInt32(checkBox1.Checked) + Convert.ToInt32(checkBox2.Checked) + Convert.ToInt32(checkBox3.Checked);
            Array[] arrayOfXPoints = new Array[numberOfCheckedCheckboxes];
            Array[] arrayOfYPoints = new Array[numberOfCheckedCheckboxes];
            String[] arrayOfTitles = new String[numberOfCheckedCheckboxes];

            int index = 0;

            if (checkBox1.Checked) // linear mic
            {
                Array[] array = buildDirectivityDepencity(MicrophoneType.MicrophoneTypeLinear);
                arrayOfYPoints[index] = array[0];
                arrayOfXPoints[index] = array[1];
                arrayOfTitles[index] = "Линейная группа микрофонов";
                index++;
            }

            if (checkBox2.Checked) // organ mic
            {
                Array[] array = buildDirectivityDepencity(MicrophoneType.MicrophoneTypeOrgan);
                arrayOfYPoints[index] = array[0];
                arrayOfXPoints[index] = array[1];
                arrayOfTitles[index] = "Микрофон органного типа";
                index++;
            }

            if (checkBox3.Checked) // organ mic
            {
                Array[] array = buildDirectivityDepencity(MicrophoneType.MicrophoneTypeParabolic);
                arrayOfYPoints[index] = array[0];
                arrayOfXPoints[index] = array[1];
                arrayOfTitles[index] = "Параболический микрофон";
                index++;
            }

            newMDIChild.arrayOfXValues = arrayOfXPoints;
            newMDIChild.arrayOfYValues = arrayOfYPoints;
            newMDIChild.arrayOfTitles = arrayOfTitles;
            newMDIChild.Show();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private void button2_Click(object sender, EventArgs e)
        {
            bool isFrequencyFieldIsEmpty = frequencyField.Text.Length == 0;

            if (!isFrequencyFieldIsEmpty)
            {
                double[] array = null;// = (double[])createArrayForDirectivityPlot();
                System.String plotTitle = "";

                if (organRadioButton.Checked)
                {
                    array = (double[])createArrayForDirectivityPlot(MicrophoneType.MicrophoneTypeOrgan);
                    plotTitle = "Микрофон органного типа";
                }
                else if (lineGroupRadioButton.Checked)
                {
                    array = (double[])createArrayForDirectivityPlot(MicrophoneType.MicrophoneTypeLinear);
                    plotTitle = "Линейная группа микрофонов";
                }
                else if (parabolicRadioButton.Checked)
                {
                    array = (double[])createArrayForDirectivityPlot(MicrophoneType.MicrophoneTypeParabolic);
                    plotTitle = "Параболический микрофон";
                }

                /*
                 * Вычисление отношения площадей главного лепестка ДН и всех остальных
                 * 
                double minValue = 1.0;
                double firstZeroAngle = 0.0;
                int sss = array.Length;
                for (int i = 0; i < array.Length; i++)
                {
                    double angle = ((double)i / (double)360) * 2 * Math.PI;
                    double value = dependence(250, 40, 0.02, angle, MicrophoneType.MicrophoneTypeOrgan);

                    if (value < minValue)
                    {
                        minValue = array[i];
                    }

                    if (value > minValue)
                    {
                        firstZeroAngle = angle;
                        break;
                    }
                }



                double mainDirection = Integrate.OnClosedInterval(x => dependence(250, 40, 0.02, x, MicrophoneType.MicrophoneTypeOrgan), -firstZeroAngle, firstZeroAngle);
                double allDirections = Integrate.OnClosedInterval(x => dependence(250, 40, 0.02, x, MicrophoneType.MicrophoneTypeOrgan), 0, 2 * Math.PI);
                double res = mainDirection / allDirections;
                */


                Form3 newMDIChild = new Form3();

                newMDIChild.pointsArray = array;
                newMDIChild.plotTitle = plotTitle;
                newMDIChild.Show();
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private System.Array createArrayForDirectivityPlot(MicrophoneType microphoneType) // using for building directivity plot
        {
            int frequency = Convert.ToInt32(frequencyField.Text);
            Int32 tubesNumber = 0;// = Convert.ToInt32(numberTextField.Text);
            double deltha = 0;// = Convert.ToInt32(deltaTextField.Text) / (double)100;

            if (numberTextField.Text.Length > 0 && deltaTextField.Text.Length > 0)
            {
                tubesNumber = Convert.ToInt32(numberTextField.Text);
                deltha = Convert.ToInt32(deltaTextField.Text) / (double)100;
            }

            int count = 360;

            double[] array = new double[count];
            double pi = Math.PI;

            progressBar1.Maximum = count;
            double maxValue = 0;
            for (int i = 0; i < count; i++)
            {
                double wavelenght = (double)((double)320 / (double)frequency);
                //double x = pi * deltha * (1 - Math.Cos(i * pi / 180)) / wavelenght;
                double angle = ((double)i / (double)count) * 2 * pi;

                double diameter = 0.0;

                if (microphoneType == MicrophoneType.MicrophoneTypeParabolic)
                {
                    diameter = Convert.ToDouble(diameterTextBox.Text) / (float)100;
                }

                double value = DirectivityController.CountDirectivityDependence(microphoneType, frequency, deltha, tubesNumber, diameter, angle);

                array[i] = value;
                if (value > maxValue)
                    maxValue = value;

                progressBar1.Value = i;
            }

            for (int i = 0; i < array.Length; i++)
            {
                double element = array[i];
                array[i] = element / maxValue;
            }

            progressBar1.Value = 0;
            return array;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private Array[] buildDirectivityDepencity(MicrophoneType microphoneType)
        {
            int count = 1000;
            progressBar1.Maximum = count;
            double[] array = new double[count];
            double[] yAxisValues = new double[count];

            double minFrequency = Convert.ToDouble(frequencyTextField.Text);
            double maxFrequency = Convert.ToDouble(frequencyMaxTextBox.Text);
            double deltha = maxFrequency - minFrequency;

            Int32 num = Convert.ToInt32(numberTextField.Text);

            double d = Convert.ToInt32(deltaTextField.Text) / (double)100;
            double diameter = 0.0;

            if (microphoneType == MicrophoneType.MicrophoneTypeParabolic)
            {
                diameter = Convert.ToDouble(diameterTextBox.Text) / (float)100;
            }

            Array[] arrayForReturn = new Array[2];

            switch (microphoneType)
            {
                case MicrophoneType.MicrophoneTypeLinear:
                    {
                        for (int i = 0; i < count; i++)
                        {
                            double freq = minFrequency + (deltha / count) * (i + 1);
                            array[i] = DirectivityController.CountDirectivityRate(microphoneType, freq, d, num, diameter);

                            yAxisValues[i] = freq;
                            progressBar1.Value = i + 1;
                        }
                    }
                    break;

                case MicrophoneType.MicrophoneTypeOrgan:
                    {
                        for (int i = 0; i < count; i++)
                        {
                            double freq = minFrequency + (deltha / count) * (i + 1);
                            array[i] = DirectivityController.CountDirectivityRate(microphoneType, freq, d, num, diameter);

                            yAxisValues[i] = freq;
                            progressBar1.Value = i + 1;
                        }
                    }
                    break;

                case MicrophoneType.MicrophoneTypeParabolic:
                    {
                        for (int i = 0; i < count; i++)
                        {
                            double freq = minFrequency + (deltha / count) * (i + 1);
                            double theta = Math.PI / (i / count);

                            double wavelenght = (double)331 / (double)freq;

                            double first = 5 * Math.PI * Math.Pow(diameter / 2, 2.0);
                            double second = Math.Pow(wavelenght, 2.0);

                            array[i] = 10 * Math.Log10(Math.Abs(first / second));

                            yAxisValues[i] = freq;
                            progressBar1.Value = i + 1;
                        }
                    }
                    break;

                default:
                    {
                    }
                    break;
            }

            progressBar1.Value = 0;

            arrayForReturn[0] = array;
            arrayForReturn[1] = yAxisValues;

            return arrayForReturn;
            //System.String plotTitle = "";
            /*
            newMDIChild.pointsArray = array;
            newMDIChild.yAxisValues = yAxisValues;
            newMDIChild.plotTitle = plotTitle;
            newMDIChild.Show();
             */
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        /*
        private double dependence(double frequency, Int32 n, double d, double theta, MicrophoneType microphoneType)
        {
            double wavelenght = (double)331 / (double)frequency;
            double angle = theta;
            /*
             * Микрофон органного типа
             * */
        /*
            double first = 0.0;
            double second = 0.0;

            switch (microphoneType)
            {
                case MicrophoneType.MicrophoneTypeOrgan:
                    {
                    mark1: ;
                        first = Math.Sin((n * Math.PI * d * (1 - Math.Cos(angle))) / (wavelenght));
                        second = n * Math.Sin((Math.PI * d * (1 - Math.Cos(angle))) / (wavelenght));
                        if (second == 0.0)
                        {
                            // Судя по всему, функция Math.Cos() достаточно грубо округляет значения и считает что Math.Cos(0.00000000000001) = 1
                            angle += 0.00000001;
                            goto mark1;
                        }
                    }
                    break;

                case MicrophoneType.MicrophoneTypeLinear:
                    {
                    mark2: ;
                        first = Math.Sin(Math.Sin(angle) * Convert.ToDouble(n) * Math.PI * d / wavelenght);
                        second = Convert.ToDouble(n) * Math.Sin(Math.Sin(angle) * Math.PI * d / wavelenght);
                        if (second == 0.0)
                        {
                            angle = Math.PI;
                            goto mark2;
                            //return 0.0F;
                        }
                    }
                    break;

                case MicrophoneType.MicrophoneTypeParabolic:
                    {
                        double diameter = Convert.ToDouble(diameterTextBox.Text) / (float)100;
                        double radius = diameter / 2;
                    mark2: ;
                        first = (1 + Math.Cos(angle)) * alglib.besselj1((2 * Math.PI * radius * Math.Sin(angle)) / wavelenght) * wavelenght;
                        second = 4 * Math.PI * radius * Math.Sin(angle);

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
         * */

        /////////////////////////////////////////////////////////////////////////////////////////////

        /*
        private double directivity(double f, MicrophoneType microphoneType)
        {
            Int32 num = Convert.ToInt32(numberTextField.Text);

            double d = Convert.ToInt32(deltaTextField.Text) / (double)100;
            double diameter = 0.0;

            if (microphoneType == MicrophoneType.MicrophoneTypeParabolic)
            {
                diameter = Convert.ToDouble(diameterTextBox.Text) / (float)100;
            }

            double result = Integrate.OnClosedInterval(x => (Math.Pow(DirectivityController.CountDirectivityDependence(microphoneType, f, d, num, diameter, x), 2) * Math.Sin(x)), 0, Math.PI);

            double dd = 10 * Math.Log10(2 / result);
            return dd;
        }
         * */

        /////////////////////////////////////////////////////////////////////////////////////////////

        private void Form1_Load(object sender, EventArgs e)
        {
            updateInterface();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private void lineGroupRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            updateInterface();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private void organRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            updateInterface();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private void parabolicRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            updateInterface();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private void updateInterface()
        {
            int numberOfCheckedCheckboxes = Convert.ToInt32(checkBox1.Checked) + Convert.ToInt32(checkBox2.Checked) + Convert.ToInt32(checkBox3.Checked);

            if (numberOfCheckedCheckboxes == 0)
                button1.Enabled = false;
            else
                button1.Enabled = true;

            if (lineGroupRadioButton.Checked || organRadioButton.Checked)
            {
                // labels
                label1.Show();
                label2.Show();
                label3.Show();
                label4.Show();
                label5.Show();
                label6.Show();
                label7.Show();
                label8.Hide();
                label9.Hide();

                // text fields
                frequencyField.Show();
                frequencyTextField.Show();
                frequencyMaxTextBox.Show();
                numberTextField.Show();
                deltaTextField.Show();
                diameterTextBox.Hide();

            }
            else if (parabolicRadioButton.Checked)
            {
                // labels
                label1.Show();
                label2.Hide();
                label3.Hide();
                label4.Show();
                label5.Hide();
                label6.Show();
                //label7.Hide();
                label7.Show();
                label8.Show();
                label9.Show();

                // text fields
                //frequencyField.Hide();
                frequencyField.Show();
                frequencyTextField.Show();
                frequencyMaxTextBox.Show();
                numberTextField.Hide();
                deltaTextField.Hide();
                diameterTextBox.Show();

                // buttons
                //button2.Enabled = false;
            }

            // buttons
            if (numberOfCheckedCheckboxes > 1 || numberOfCheckedCheckboxes < 1)
            {
                button2.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            updateInterface();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            updateInterface();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            updateInterface();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
    }
}

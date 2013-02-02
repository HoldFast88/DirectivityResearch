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
    enum MicrophoneType {
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

            if (lineGroupRadioButton.Checked || organRadioButton.Checked)
            {
                if (!isMinimumFrequencyFieldIsEmpty && !isMaximumFrequencyFieldIsEmpty)
                {
                    if (lineGroupRadioButton.Checked)
                        buildDirectivityDepencity(MicrophoneType.MicrophoneTypeLinear);
                    else if (organRadioButton.Checked)
                        buildDirectivityDepencity(MicrophoneType.MicrophoneTypeOrgan);
                }
            }
            else if (parabolicRadioButton.Enabled)
            {
                if (!isDiameterFieldIsEmpty)
                    buildDirectivityDepencity(MicrophoneType.MicrophoneTypeParabolic);
            }
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
            Int32 tubesNumber = Convert.ToInt32(numberTextField.Text);
            double deltha = Convert.ToInt32(deltaTextField.Text) / (double)100;
            int count = 360;

            double[] array = new double[count];
            double pi = Math.PI;

            progressBar1.Maximum = count;
            for (int i = 0; i < count; i++)
            {
                double wavelenght = (double)((double)320 / (double)frequency);
                double x = pi * deltha * (1 - Math.Cos(i * pi / 180)) / wavelenght;
                double angle = ((double)i / (double)count) * 2 * pi;

                double value = dependence(frequency, tubesNumber, deltha, angle, microphoneType);
                array[i] = value;
                progressBar1.Value = i;
            }

            // Нормирование показаний
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

            progressBar1.Value = 0;
            return array;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private void buildDirectivityDepencity(MicrophoneType microphoneType)
        {
            Form2 newMDIChild = new Form2();

            int count = 1000;
            progressBar1.Maximum = count;
            double[] array = new double[count];
            double[] yAxisValues = new double[count];

            double minFrequency = Convert.ToDouble(frequencyTextField.Text);
            double maxFrequency = Convert.ToDouble(frequencyMaxTextBox.Text);
            double deltha = maxFrequency - minFrequency;

            switch (microphoneType)
            {
                case MicrophoneType.MicrophoneTypeLinear:
                    {
                        for (int i = 0; i < count; i++)
                        {
                            double freq = minFrequency + (deltha / count) * (i + 1);
                            array[i] = directivity(freq, microphoneType);
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
                            array[i] = directivity(freq, microphoneType);
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
                            array[i] = 10 * Math.Log10(dependence(freq, 0, 0, theta, microphoneType));

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
            System.String plotTitle = "";
            newMDIChild.pointsArray = array;
            newMDIChild.yAxisValues = yAxisValues;
            newMDIChild.plotTitle = plotTitle;
            newMDIChild.Show();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private double dependence(double frequency, Int32 n, double d, double theta, MicrophoneType microphoneType)
        {
            double wavelenght = (double)331 / (double)frequency;
            double angle = theta;
            /*
             * Микрофон органного типа
             * */
            //*
            double first = 0.0;
            double second = 0.0;
            if (organRadioButton.Checked)
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
            /*
             * Линейная группа микрофонов
             */
            //*
            else if (lineGroupRadioButton.Checked)
            {
            mark2: ;
                first = Math.Sin(Math.Sin(angle) * n * Math.PI * d / wavelenght);
                second = n * Math.Sin(Math.Sin(angle) * Math.PI * d / wavelenght);
                if (second == 0.0)
                {
                    angle = Math.PI;
                    goto mark2;
                    //return 0.0F;
                }
            }
            //*/
            else if (parabolicRadioButton.Checked)
            {
                double diameter = Convert.ToDouble(diameterTextBox.Text) / (float)100;
                first = 5 * Math.PI * Math.Pow(diameter / 2, 2.0);
                second = Math.Pow(wavelenght, 2.0);
            }
            double result = Math.Abs(first / second);
            return result;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private double directivity(double f, MicrophoneType microphoneType)
        {
            Int32 num = Convert.ToInt32(numberTextField.Text);

            double d = Convert.ToInt32(deltaTextField.Text) / (double)100;
            double result;

            result = Integrate.OnClosedInterval(x => (Math.Pow(dependence(f, num, d, x, microphoneType), 2) * Math.Sin(x)), 0, Math.PI);

            double dd = 10 * Math.Log10(2 / result);
            return dd;
        }

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

            if (lineGroupRadioButton.Checked || organRadioButton.Checked){
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

                // buttons
                if (numberOfCheckedCheckboxes > 1 || numberOfCheckedCheckboxes < 1){
                    button2.Enabled = false;
                } else {
                    button2.Enabled = true;
                }
            } else if (parabolicRadioButton.Checked) {
                // labels
                label1.Show();
                label2.Hide();
                label3.Hide();
                label4.Show();
                label5.Hide();
                label6.Show();
                label7.Hide();
                label8.Show();
                label9.Show();

                // text fields
                frequencyField.Hide();
                frequencyTextField.Show();
                frequencyMaxTextBox.Show();
                numberTextField.Hide();
                deltaTextField.Hide();
                diameterTextBox.Show();

                // buttons
                button2.Enabled = false;
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

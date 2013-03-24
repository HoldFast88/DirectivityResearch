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

    public partial class MainForm : Form
    {
        public MainForm()
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

            DirectivityDependence newMDIChild = new DirectivityDependence();

            int numberOfCheckedCheckboxes = Convert.ToInt32(checkBox1.Checked) + Convert.ToInt32(checkBox2.Checked) + Convert.ToInt32(checkBox3.Checked);
            Array[] arrayOfXPoints = new Array[numberOfCheckedCheckboxes];
            Array[] arrayOfYPoints = new Array[numberOfCheckedCheckboxes];
            List<MicrophoneProperties> arrayOfProperties = new List<MicrophoneProperties>();
            String[] arrayOfTitles = new String[numberOfCheckedCheckboxes];

            int index = 0;

            if (checkBox1.Checked) // linear mic
            {
                MicrophoneProperties properties = new MicrophoneProperties(MicrophoneType.MicrophoneTypeLinear, (Convert.ToInt32(deltaTextField.Text) / (double)100), Convert.ToInt32(numberTextField.Text), 0);
                arrayOfProperties.Add(properties);

                Array[] array = buildDirectivityDepencity(MicrophoneType.MicrophoneTypeLinear);
                arrayOfYPoints[index] = array[0];
                arrayOfXPoints[index] = array[1];
                arrayOfTitles[index] = "Линейная группа микрофонов";
                index++;
            }

            if (checkBox2.Checked) // organ mic
            {
                MicrophoneProperties properties = new MicrophoneProperties(MicrophoneType.MicrophoneTypeOrgan, (Convert.ToInt32(deltaTextField.Text) / (double)100), Convert.ToInt32(numberTextField.Text), 0);
                arrayOfProperties.Add(properties);

                Array[] array = buildDirectivityDepencity(MicrophoneType.MicrophoneTypeOrgan);
                arrayOfYPoints[index] = array[0];
                arrayOfXPoints[index] = array[1];
                arrayOfTitles[index] = "Микрофон органного типа";
                index++;
            }

            if (checkBox3.Checked) // parabolic mic
            {
                double diameter = Convert.ToDouble(diameterTextBox.Text) / (float)100;
                MicrophoneProperties properties = new MicrophoneProperties(MicrophoneType.MicrophoneTypeParabolic, 0.0, 0, diameter);
                arrayOfProperties.Add(properties);

                Array[] array = buildDirectivityDepencity(MicrophoneType.MicrophoneTypeParabolic);
                arrayOfYPoints[index] = array[0];
                arrayOfXPoints[index] = array[1];
                arrayOfTitles[index] = "Параболический микрофон";
                index++;
            }

            newMDIChild.arrayOfProperties = arrayOfProperties;
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

                DirectivityDiagram newMDIChild = new DirectivityDiagram();

                newMDIChild.pointsArray = array;
                newMDIChild.plotTitle = plotTitle;
                newMDIChild.Show();
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private System.Array createArrayForDirectivityPlot(MicrophoneType microphoneType) // using for building directivity plot
        {
            int frequency = Convert.ToInt32(frequencyField.Text);
            Int32 tubesNumber = 0;
            double deltha = 0;

            if (numberTextField.Text.Length > 0 && deltaTextField.Text.Length > 0)
            {
                tubesNumber = Convert.ToInt32(numberTextField.Text);
                deltha = Convert.ToInt32(deltaTextField.Text) / (double)100;
            }

            double diameter = 0.0;

            if (microphoneType == MicrophoneType.MicrophoneTypeParabolic)
            {
                diameter = Convert.ToDouble(diameterTextBox.Text) / (float)100;
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
            Int32 num = 0;
            double d = 0.0;

            if (microphoneType != MicrophoneType.MicrophoneTypeParabolic)
            {
                num = Convert.ToInt32(numberTextField.Text);

                d = Convert.ToInt32(deltaTextField.Text) / (double)100;
            }
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

﻿using System;
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
                    buildDirectivityDepencity();
            }
            else if (parabolicRadioButton.Enabled)
            {
                if (!isDiameterFieldIsEmpty)
                    buildDirectivityDepencity();
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private void button2_Click(object sender, EventArgs e)
        {
            bool isFrequencyFieldIsEmpty = frequencyField.Text.Length == 0;
            bool isMinimumFrequencyFieldIsEmpty = frequencyTextField.Text.Length == 0;
            bool isMaximumFrequencyFieldIsEmpty = frequencyMaxTextBox.Text.Length == 0;

            if (!isMinimumFrequencyFieldIsEmpty && !isMaximumFrequencyFieldIsEmpty)
            {
                double[] array = (double[])createArrayForDirectivityPlot();
                System.String plotTitle = "";

                if (organRadioButton.Checked)
                    plotTitle = "Микрофон органного типа";
                else if (lineGroupRadioButton.Checked)
                    plotTitle = "Линейная группа микрофонов";
                else if (parabolicRadioButton.Checked)
                    plotTitle = "Параболический микрофон";

                Form3 newMDIChild = new Form3();

                newMDIChild.pointsArray = array;
                newMDIChild.plotTitle = plotTitle;
                newMDIChild.ShowDialog();
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private System.Array createArrayForDirectivityPlot()
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

                double value = dependence(frequency, tubesNumber, deltha, angle);
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

        private void buildDirectivityDepencity()
        {
            Form2 newMDIChild = new Form2();

            int count = 1000;
            progressBar1.Maximum = count;
            double[] array = new double[count];
            double[] yAxisValues = new double[count];

            double minFrequency = Convert.ToDouble(frequencyTextField.Text);
            double maxFrequency = Convert.ToDouble(frequencyMaxTextBox.Text);
            double deltha = maxFrequency - minFrequency;

            for (int i = 0; i < count; i++)
            {
                double freq = minFrequency + (deltha / count) * (i + 1);

                if (lineGroupRadioButton.Checked || organRadioButton.Checked) {
                    array[i] = directivity(freq);
                } else if (parabolicRadioButton.Enabled) {
                    double theta = Math.PI / (i / count);
                    array[i] = 10 * Math.Log10(dependence(freq, 0, 0, theta));
                }

                yAxisValues[i] = freq;
                progressBar1.Value = i + 1;
            }

            progressBar1.Value = 0;
            System.String plotTitle = "";
            newMDIChild.pointsArray = array;
            newMDIChild.yAxisValues = yAxisValues;
            newMDIChild.plotTitle = plotTitle;
            newMDIChild.ShowDialog();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private double dependence(double frequency, Int32 n, double d, double theta)
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

        private double directivity(double f)
        {
            Int32 num = Convert.ToInt32(numberTextField.Text);

            double d = Convert.ToInt32(deltaTextField.Text) / (double)100;
            double result;

            result = Integrate.OnClosedInterval(x => (Math.Pow(dependence(f, num, d, x), 2) * Math.Sin(x)), 0, Math.PI);

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
                button2.Enabled = true;
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
    }
}
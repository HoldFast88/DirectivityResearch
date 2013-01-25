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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private void button1_Click(object sender, EventArgs e)
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
                double freq = minFrequency + (deltha/count)*(i + 1);
                array[i] = directivity(freq);
                yAxisValues[i] = freq;
                progressBar1.Value = i + 1;
            }

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
            /*
        mark: ;
            double first = Math.Sin((n * Math.PI * d * (1 - Math.Cos(angle))) / (wavelenght));
            double second = n * Math.Sin((Math.PI * d * (1 - Math.Cos(angle))) / (wavelenght));
            if (second == 0.0)
            {
                // Судя по всему функция Math.Cos() достаточно грубо округляет значения и считает что Math.Cos(0.00000000000001) = 1
                angle += 0.00000001;
                goto mark;
            }
            /*
             * Линейная группа микрофонов
             */
            //*
        mark: ;
            double first = Math.Sin(Math.Sin(angle)*n*Math.PI*d / wavelenght);
            double second = n * Math.Sin(Math.Sin(angle)*Math.PI*d / wavelenght);
            if (second == 0.0)
            {
                angle = Math.PI;
                goto mark;
                //return 0.0F;
            }
            //*/
            double result = Math.Abs(first / second);
            return result;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private double directivity(double f)
        {
            Int32 num = Convert.ToInt32(numberTextField.Text);
            double d = Convert.ToInt32(deltaTextField.Text) / (double)100;
            double result = Integrate.OnClosedInterval(x => (Math.Pow(dependence(f, num, d, x),2)*Math.Sin(x)), 0, Math.PI);
            double dd = 10 * Math.Log10(2 / result);
            return dd;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /////////////////////////////////////////////////////////////////////////////////////////////
    }
}

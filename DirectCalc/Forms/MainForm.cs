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

    partial class MainForm : Form
    {
        public bool isOpenedForAddingGraph;
        public DirectivityDependence dependenceForm;

        public MainForm()
        {
            isOpenedForAddingGraph = false;
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private void button1_Click(object sender, EventArgs e)
        {
            bool isMinimumFrequencyFieldIsEmpty = frequencyTextField.Text.Length == 0;
            bool isMaximumFrequencyFieldIsEmpty = frequencyMaxTextBox.Text.Length == 0;
            bool isDiameterFieldIsEmpty = diameterTextBox.Text.Length == 0;

            if (isMinimumFrequencyFieldIsEmpty || isMaximumFrequencyFieldIsEmpty)
            {
                return;
            }

            DirectivityDependence newMDIChild = new DirectivityDependence();

            int numberOfCheckedCheckboxes = Convert.ToInt32(checkBox1.Checked) + Convert.ToInt32(checkBox2.Checked) + Convert.ToInt32(checkBox3.Checked);

            List<Microphone> microphonesList = new List<Microphone>();

            int index = 0;

            if (checkBox1.Checked) // linear mic
            {
                MicrophoneProperties properties = new MicrophoneProperties(MicrophoneType.MicrophoneTypeLinear, (Convert.ToDouble(deltaTextField.Text) / (double)100), Convert.ToInt32(numberTextField.Text), 0);
                String title = "Линейная группа микрофонов";

                Microphone microphone = new Microphone(title, properties);

                microphonesList.Add(microphone);
            }

            if (checkBox2.Checked) // organ mic
            {
                MicrophoneProperties properties = new MicrophoneProperties(MicrophoneType.MicrophoneTypeOrgan, (Convert.ToDouble(deltaTextField.Text) / (double)100), Convert.ToInt32(numberTextField.Text), 0);
                String title = "Микрофон органного типа";

                Microphone microphone = new Microphone(title, properties);

                microphonesList.Add(microphone);
            }

            if (checkBox3.Checked) // parabolic mic
            {
                double diameter = Convert.ToDouble(diameterTextBox.Text) / (float)100;
                MicrophoneProperties properties = new MicrophoneProperties(MicrophoneType.MicrophoneTypeParabolic, 0.0, 0, diameter);
                String title = "Параболический микрофон";

                Microphone microphone = new Microphone(title, properties);

                microphonesList.Add(microphone);
            }

            newMDIChild.maxFrequency = Convert.ToDouble(frequencyMaxTextBox.Text);
            newMDIChild.minFrequency = Convert.ToDouble(frequencyTextField.Text);
            if (isOpenedForAddingGraph && dependenceForm != null)
            {
                foreach (Microphone mic in microphonesList)
                {
                    mic.buildDirectivityDepencity(newMDIChild.minFrequency, newMDIChild.maxFrequency);
                    dependenceForm.microphonesList.Add(mic);
                }

                dependenceForm.RebuildGraph();
                this.Close();
                return;
            }
            else
            {
                newMDIChild.microphonesList = microphonesList;
            }
            newMDIChild.Show();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        private void button2_Click(object sender, EventArgs e) // build directivity diagram
        {
            double[] array = null;
            System.String plotTitle = "";
            Microphone microphone;
            MicrophoneProperties properties = null;

            if (organRadioButton.Checked)
            {
                plotTitle = "Микрофон органного типа";
                properties = new MicrophoneProperties(MicrophoneType.MicrophoneTypeOrgan, 0, 0, 0);
            }
            else if (lineGroupRadioButton.Checked)
            {
                plotTitle = "Линейная группа микрофонов";
                properties = new MicrophoneProperties(MicrophoneType.MicrophoneTypeLinear, 0, 0, 0);
            }
            else if (parabolicRadioButton.Checked)
            {
                plotTitle = "Параболический микрофон";
                properties = new MicrophoneProperties(MicrophoneType.MicrophoneTypeParabolic, 0, 0, 0);
            }

            microphone = new Microphone(plotTitle, properties);
            

            DirectivityDiagram newMDIChild = new DirectivityDiagram();

            newMDIChild.microphone = microphone;
            newMDIChild.Show();
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
                label8.Hide();
                label9.Hide();

                // text fields
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
                label8.Show();
                label9.Show();

                // text fields
                frequencyTextField.Show();
                frequencyMaxTextBox.Show();
                numberTextField.Hide();
                deltaTextField.Hide();
                diameterTextBox.Show();
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

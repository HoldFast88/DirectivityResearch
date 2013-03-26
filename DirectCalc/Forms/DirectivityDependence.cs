using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace DirectCalc
{
    partial class DirectivityDependence : Form
    {
        public Array[] arrayOfXValues;
        public Array[] arrayOfYValues;
        public List<MicrophoneProperties> arrayOfProperties;
        public String[] arrayOfTitles;
        public List<double> arrayOfMaxWorkingFrequecies;
        public System.String plotTitle;

        public DirectivityDependence()
        {
            InitializeComponent();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Form2_Load(object sender, EventArgs e)
        {
            // Setup the graph
            CreateGraph(zedGraphControl1);
            // Size the control to fill the form with a margin
            SetSize();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Form2_Resize(object sender, EventArgs e)
        {
            SetSize();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////

        private void CreateGraph(ZedGraphControl zgc)
        {
            // get a reference to the GraphPane
            GraphPane myPane = zgc.GraphPane;
            zedGraphControl1.GraphPane.CurveList.Clear();

            // Set the Titles
            myPane.Title.Text = plotTitle;
            myPane.XAxis.Title.Text = "Frequency, Hz";
            myPane.YAxis.Title.Text = "Directivity, dB";
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            arrayOfMaxWorkingFrequecies = new List<double>();

            for (int i = 0; i < arrayOfXValues.Length; i++)
            {
                double[] xValuesArray = (double[])arrayOfXValues[i];
                double[] yValuesArray = (double[])arrayOfYValues[i];
                String title = arrayOfTitles[i];

                // search for biggest Y value, which represents max working frequency
                double maxYValue = 0;
                double xValue;
                int index = 0;
                for (int q = 0; q < yValuesArray.Length; q++)
                {
                    if (yValuesArray[q] > maxYValue)
                    {
                        maxYValue = yValuesArray[q];
                        index = q;
                    }
                }
                xValue = xValuesArray[index];

                // save max working freq for this microphone type
                arrayOfMaxWorkingFrequecies.Add(xValue);

                PointPairList list = new PointPairList(xValuesArray, yValuesArray);
                LineItem curve = myPane.AddCurve(title, list, (i == 0 ? Color.Black : i == 1 ? Color.Blue : Color.Red), (i == 0 ? SymbolType.Square : i == 1 ? SymbolType.Diamond : SymbolType.Triangle));
                curve.Symbol.Fill.Color = Color.Blue;
                curve.Symbol.Fill.Type = FillType.Solid;
                curve.Symbol.Size = 2;
                curve.Line.Width = 1.0F;

                // adding to curve mark, which shows max working frequency
                PointPairList maxWorkingFrequencyMark = new PointPairList();
                maxWorkingFrequencyMark.Add(xValue, maxYValue);
                LineItem selectedPoint = myPane.AddCurve("", maxWorkingFrequencyMark, Color.Black, SymbolType.XCross);
                selectedPoint.Symbol.Fill.Color = Color.Black;
                selectedPoint.Symbol.Fill.Type = FillType.Solid;
                selectedPoint.Symbol.Size = 15;
            }

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            zedGraphControl1.Refresh();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////

        private void SetSize()
        {
            zedGraphControl1.Location = new Point(10, 10);
            // Leave a small margin around the outside of the control
            zedGraphControl1.Size = new Size(ClientRectangle.Width - 220,
                                    ClientRectangle.Height - 20);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////

        private void ShowAlert()
        {
            MessageBox.Show("Максимальная рабочая частота для рассчитываемого типа микрофона должна быть больше или равна 5600 Гц.", "Ошибка", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////

        private void button1_Click(object sender, EventArgs e) // count and show intelligibility
        {
            int count = arrayOfProperties.Count;

            if (count >= 1)
            {
                if (arrayOfMaxWorkingFrequecies[0] < 5600.0)
                {
                    ShowAlert();
                    return;
                }

                MicrophoneProperties property = arrayOfProperties[0];
                double value = IntelligibilityController.CountIntelligibility(property.microphoneType, property.deltha, property.count, property.diameter);

                firstTypeValue.Visible = true;
                firstTypeMicrophoneNameLabel.Visible = true;

                firstTypeMicrophoneNameLabel.Text = arrayOfTitles[0];
                firstTypeValue.Text = Convert.ToString(value);
            }

            if (count >= 2)
            {
                if (arrayOfMaxWorkingFrequecies[1] < 5600.0)
                {
                    ShowAlert();
                    return;
                }

                MicrophoneProperties property = arrayOfProperties[1];
                double value = IntelligibilityController.CountIntelligibility(property.microphoneType, property.deltha, property.count, property.diameter);

                secondTypeValue.Visible = true;
                secondTypeMicrophoneNameLabel.Visible = true;

                secondTypeMicrophoneNameLabel.Text = arrayOfTitles[1];
                secondTypeValue.Text = Convert.ToString(value);
            }

            if (count >= 3)
            {
                if (arrayOfMaxWorkingFrequecies[2] < 5600.0)
                {
                    ShowAlert();
                    return;
                }

                MicrophoneProperties property = arrayOfProperties[2];
                double value = IntelligibilityController.CountIntelligibility(property.microphoneType, property.deltha, property.count, property.diameter);

                thirdTypeValue.Visible = true;
                thirdTypeMicrophoneNameLabel.Visible = true;

                thirdTypeMicrophoneNameLabel.Text = arrayOfTitles[2];
                thirdTypeValue.Text = Convert.ToString(value);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

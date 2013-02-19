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
    public partial class Form2 : Form
    {
        public Array[] arrayOfXValues;
        public Array[] arrayOfYValues;
        public String[] arrayOfTitles;
        public System.String plotTitle;

        public Form2()
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
            zedGraphControl1.Size = new Size(ClientRectangle.Width - 20,
                                    ClientRectangle.Height - 20);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

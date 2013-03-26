using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace DirectCalc
{
    partial class DirectivityDiagram : Form
    {
        public double[] pointsArray;
        public Microphone microphone;
        public UInt32 frequency;

        public DirectivityDiagram()
        {
            InitializeComponent();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////

        private void Form3_Load(object sender, EventArgs e)
        {
            RedrawPlot();

            frequencyNumeric.Value = frequency;
            if (microphone.properties.microphoneType == MicrophoneType.MicrophoneTypeParabolic)
            {
                countNumeric.Enabled = false;
                delthaNumeric.Enabled = false;
                diameterNumeric.Enabled = true;
            }
            else
            {
                countNumeric.Enabled = true;
                delthaNumeric.Enabled = true;
                diameterNumeric.Enabled = false;
            }

            countNumeric.Value = microphone.properties.count;
            delthaNumeric.Value = Convert.ToDecimal(microphone.properties.deltha);
            diameterNumeric.Value = Convert.ToDecimal(microphone.properties.diameter);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////

        private void RedrawPlot()
        {
            GraphPane pane = zedGraphControl1.GraphPane;

            // Если есть что удалять
            if (pane.CurveList.Count > 0)
            {
                for (int i = 0; i < pane.CurveList.Count; i++)
                {
                    // Удалим кривую по индексу
                    pane.CurveList.RemoveAt(i);

                    // Обновим график
                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Invalidate();
                }
            }

            // Setup the graph
            CreateGraph(zedGraphControl1);
            // Size the control to fill the form with a margin
            SetSize();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////

        private void Form3_Resize(object sender, EventArgs e)
        {
            SetSize();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////

        private void SetSize()
        {
            zedGraphControl1.Location = new Point(10, 10);
            // Leave a small margin around the outside of the control
            zedGraphControl1.Size = new Size(ClientRectangle.Width - 217,
                                    ClientRectangle.Height - 20);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////

        // Build the Chart
        private void CreateGraph(ZedGraphControl zgc)
        {
            // get a reference to the GraphPane
            GraphPane myPane = zgc.GraphPane;

            // Set the Titles
            myPane.Title.Text = microphone.title;
            myPane.XAxis.Title.Text = "";
            myPane.YAxis.Title.Text = "";
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsZeroLine = false;

            myPane.XAxis.Scale.Min = -1;
            myPane.XAxis.Scale.Max = 1;

            // По оси Y установим автоматический подбор масштаба
            myPane.YAxis.Scale.MinAuto = true;
            myPane.YAxis.Scale.MaxAuto = true;

            // Создаем список точек 
            RadarPointList points = new RadarPointList();
            // Т.к. в списке будет 4 точки, то и окружность будет разбиваться на 4 части
            // Обход точек будет осуществляться по часовой стрелке
            points.Clockwise = true;
            // Первая точка - сверху над началом координат. Расстояние до центра = 1. Второй параметр в большинстве случаев не используется
            for (int i = 0; i < pointsArray.Length; i++)
            {
                points.Add(pointsArray[i], 1);
            }

            // Добавляем кривую по этим четырем точкам
            LineItem myCurve = myPane.AddCurve("", points, Color.Black, SymbolType.None);
            // Отметим начало координат черным квадратиком
            BoxObj box = new BoxObj(-0.005, 0.005, 0.01, 0.01, Color.Black, Color.Black);
            myPane.GraphObjList.Add(box);

            zgc.AxisChange();
            zgc.Invalidate();
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            microphone.properties.count = Convert.ToInt32(countNumeric.Value);
            microphone.properties.deltha = Convert.ToDouble(delthaNumeric.Value) / 100.0;
            microphone.properties.diameter = Convert.ToDouble(diameterNumeric.Value) / 100.0;
            frequency = Convert.ToUInt32(frequencyNumeric.Value);

            pointsArray = (double[])microphone.createArrayForDirectivityPlot(frequency);

            RedrawPlot();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////

    }
}

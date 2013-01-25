using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace Mic
{
    public partial class Form2 : Form
    {
        public double[] pointsArray;
        public System.String plotTitle;
        public uint micType;
        public uint frequency;
        public uint count;
        public uint tubesNumber;
        public double deltha;

        public Form2()
        {
            InitializeComponent();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////

        private void Form2_Load(object sender, EventArgs e)
        {
           // Setup the graph
            CreateGraph(zedGraphControl1);
            // Size the control to fill the form with a margin
            SetSize();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////

        private void Form1_Resize(object sender, EventArgs e)
        {
            SetSize();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////

        private void SetSize()
        {
            zedGraphControl1.Location = new Point(10, 10);
            // Leave a small margin around the outside of the control
            zedGraphControl1.Size = new Size(ClientRectangle.Width - 20,
                                    ClientRectangle.Height - 20);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////

        // Build the Chart
        private void CreateGraph(ZedGraphControl zgc)
        {
            // get a reference to the GraphPane
            GraphPane myPane = zgc.GraphPane;

            // Set the Titles
            myPane.Title.Text = plotTitle;
            myPane.XAxis.Title.Text = "";
            myPane.YAxis.Title.Text = "";
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;
            /*
             * ДН в декартовой системе координат
             * 
            // Make up some data arrays based on the Sine function
            double x, y;
            PointPairList list1 = new PointPairList();
            for (int i = 0; i < 360; i++)
            {
                x = (double)i;
                y = 1);
                list1.Add(x, y);
            }

            // Generate a red curve with diamond
            // symbols, and "Porsche" in the legend
            LineItem myCurve = myPane.AddCurve("AllDirectional",
                  list1, Color.Red, SymbolType.Diamond);

            // Tell ZedGraph to refigure the
            // axes since the data have changed
            zgc.AxisChange();
             */

            myPane.YAxis.MajorGrid.IsZeroLine = false;
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

        ///////////////////////////////////////////////////////////////////////////////////////////////

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Используйте grad-and-drop левой кнопкой мыши для масштабирования графика по оси Y, зажатым колёсиком для перемешения экрана видимости и вращение колёсика для уменьшения/уменьшения масшатаба.");
            /*
            for (uint i = 20; i <= 20000; i += 20)
            {
                frequency = i;
                redrawPlot();
                System.Threading.Thread.Sleep(500); // ms
            }
             */
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////

        private void redrawPlot()
        {
            double[] array = (double[])PlotController.DrawPlot((MicType)micType,
                                                               frequency,
                                                               count,
                                                               tubesNumber,
                                                               deltha);
            this.pointsArray = array;
            // Setup the graph
            CreateGraph(zedGraphControl1);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////

    }
}

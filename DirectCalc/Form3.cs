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
    public partial class Form3 : Form
    {
        public double[] pointsArray;
        public System.String plotTitle;
        public uint micType;
        public uint frequency;
        public uint count;
        public uint tubesNumber;
        public double deltha;

        public Form3()
        {
            InitializeComponent();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////

        private void Form3_Load(object sender, EventArgs e)
        {
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

        ///////////////////////////////////////////////////////////////////////////////////////////////

    }
}

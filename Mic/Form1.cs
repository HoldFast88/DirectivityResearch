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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 newMDIChild = new Form2();

            System.String plotTitle = "";
            MicType micType = MicType.MicTypeAllDirectional;
            uint frequency = 0;
            uint count = 0;
            uint tubesNumber = 0;
            double deltha = 0;

            if (allDirectionalRadioButton.Checked)
            {
                micType = MicType.MicTypeAllDirectional;
                frequency = 1;
                count = 360;
                tubesNumber = 0;
                deltha = 0;
                plotTitle = "Всенаправленный микрофон";
            }
            else if (organRadioButton.Checked)
            {
                if (frequencyField.Text.Length == 0 || quantityTextBox.Text.Length == 0 || delthaTextBox.Text.Length == 0)
                {
                    MessageBox.Show("Введите все данные!");
                    return;
                }
                micType = MicType.MicTypeOrgan;
                frequency = Convert.ToUInt32(frequencyField.Text);
                count = 360;
                tubesNumber = Convert.ToUInt32(quantityTextBox.Text);
                deltha = Convert.ToDouble(delthaTextBox.Text) / (double)100;
                plotTitle = "Микрофон органного типа";
            }

            double[] array = (double[])PlotController.DrawPlot(micType,
                                                               frequency,
                                                               count,
                                                               tubesNumber,
                                                               deltha);
            newMDIChild.pointsArray = array;
            newMDIChild.plotTitle = plotTitle;
            newMDIChild.ShowDialog();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////

        private void frequencyField_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////

        private void stepTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                //if (e.KeyChar != (char)Keys.Back && !((e.KeyChar == '.') && (stepTextBox.Text.IndexOf(".") == -1)))
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////

        private void organRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked) // нажали
            {
                quantityLabel.Visible = true;
                quantityTextBox.Visible = true;

                delthaLabel.Visible = true;
                delthaTextBox.Visible = true;
            }
            else // отжали
            {
                quantityLabel.Visible = false;
                quantityTextBox.Visible = false;

                delthaLabel.Visible = false;
                delthaTextBox.Visible = false;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*
            if (e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
             */
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////

        private void delthaTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*
            if (e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
             */
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////
    }
}

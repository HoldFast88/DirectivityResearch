namespace Mic
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.allDirectionalRadioButton = new System.Windows.Forms.RadioButton();
            this.organRadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.frequencyField = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.quantityLabel = new System.Windows.Forms.Label();
            this.quantityTextBox = new System.Windows.Forms.TextBox();
            this.delthaTextBox = new System.Windows.Forms.TextBox();
            this.delthaLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // allDirectionalRadioButton
            // 
            this.allDirectionalRadioButton.AutoSize = true;
            this.allDirectionalRadioButton.Location = new System.Drawing.Point(30, 31);
            this.allDirectionalRadioButton.Name = "allDirectionalRadioButton";
            this.allDirectionalRadioButton.Size = new System.Drawing.Size(173, 17);
            this.allDirectionalRadioButton.TabIndex = 0;
            this.allDirectionalRadioButton.TabStop = true;
            this.allDirectionalRadioButton.Text = "Всенаправленный микрофон";
            this.allDirectionalRadioButton.UseVisualStyleBackColor = true;
            // 
            // organRadioButton
            // 
            this.organRadioButton.AutoSize = true;
            this.organRadioButton.Location = new System.Drawing.Point(30, 55);
            this.organRadioButton.Name = "organRadioButton";
            this.organRadioButton.Size = new System.Drawing.Size(159, 17);
            this.organRadioButton.TabIndex = 1;
            this.organRadioButton.TabStop = true;
            this.organRadioButton.Text = "Микрофон органного типа";
            this.organRadioButton.UseVisualStyleBackColor = true;
            this.organRadioButton.CheckedChanged += new System.EventHandler(this.organRadioButton_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Частота";
            // 
            // frequencyField
            // 
            this.frequencyField.Location = new System.Drawing.Point(107, 135);
            this.frequencyField.Name = "frequencyField";
            this.frequencyField.Size = new System.Drawing.Size(116, 20);
            this.frequencyField.TabIndex = 3;
            this.frequencyField.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frequencyField_KeyPress);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(348, 110);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 41);
            this.button1.TabIndex = 4;
            this.button1.Text = "Построить диаграмму направленности";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // quantityLabel
            // 
            this.quantityLabel.AutoSize = true;
            this.quantityLabel.Location = new System.Drawing.Point(27, 112);
            this.quantityLabel.Name = "quantityLabel";
            this.quantityLabel.Size = new System.Drawing.Size(103, 13);
            this.quantityLabel.TabIndex = 7;
            this.quantityLabel.Text = "Количество трубок";
            this.quantityLabel.Visible = false;
            // 
            // quantityTextBox
            // 
            this.quantityTextBox.Location = new System.Drawing.Point(137, 110);
            this.quantityTextBox.Name = "quantityTextBox";
            this.quantityTextBox.Size = new System.Drawing.Size(86, 20);
            this.quantityTextBox.TabIndex = 8;
            this.quantityTextBox.Visible = false;
            this.quantityTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // delthaTextBox
            // 
            this.delthaTextBox.Location = new System.Drawing.Point(151, 84);
            this.delthaTextBox.Name = "delthaTextBox";
            this.delthaTextBox.Size = new System.Drawing.Size(72, 20);
            this.delthaTextBox.TabIndex = 10;
            this.delthaTextBox.Visible = false;
            this.delthaTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.delthaTextBox_KeyPress);
            // 
            // delthaLabel
            // 
            this.delthaLabel.AutoSize = true;
            this.delthaLabel.Location = new System.Drawing.Point(27, 87);
            this.delthaLabel.Name = "delthaLabel";
            this.delthaLabel.Size = new System.Drawing.Size(119, 13);
            this.delthaLabel.TabIndex = 9;
            this.delthaLabel.Text = "Разность длин трубок";
            this.delthaLabel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 163);
            this.Controls.Add(this.delthaTextBox);
            this.Controls.Add(this.delthaLabel);
            this.Controls.Add(this.quantityTextBox);
            this.Controls.Add(this.quantityLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.frequencyField);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.organRadioButton);
            this.Controls.Add(this.allDirectionalRadioButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton allDirectionalRadioButton;
        private System.Windows.Forms.RadioButton organRadioButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox frequencyField;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label quantityLabel;
        private System.Windows.Forms.TextBox quantityTextBox;
        private System.Windows.Forms.TextBox delthaTextBox;
        private System.Windows.Forms.Label delthaLabel;


    }
}


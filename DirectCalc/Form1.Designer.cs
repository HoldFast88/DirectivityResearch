﻿namespace DirectCalc
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
            this.button1 = new System.Windows.Forms.Button();
            this.frequencyTextField = new System.Windows.Forms.TextBox();
            this.numberTextField = new System.Windows.Forms.TextBox();
            this.deltaTextField = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.frequencyMaxTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.frequencyField = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.organRadioButton = new System.Windows.Forms.RadioButton();
            this.lineGroupRadioButton = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 176);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "Построить зависимость КН от частоты";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frequencyTextField
            // 
            this.frequencyTextField.Location = new System.Drawing.Point(148, 90);
            this.frequencyTextField.Name = "frequencyTextField";
            this.frequencyTextField.Size = new System.Drawing.Size(39, 20);
            this.frequencyTextField.TabIndex = 1;
            this.frequencyTextField.Text = "20";
            // 
            // numberTextField
            // 
            this.numberTextField.Location = new System.Drawing.Point(148, 117);
            this.numberTextField.Name = "numberTextField";
            this.numberTextField.Size = new System.Drawing.Size(100, 20);
            this.numberTextField.TabIndex = 2;
            this.numberTextField.Text = "25";
            // 
            // deltaTextField
            // 
            this.deltaTextField.Location = new System.Drawing.Point(148, 144);
            this.deltaTextField.Name = "deltaTextField";
            this.deltaTextField.Size = new System.Drawing.Size(100, 20);
            this.deltaTextField.TabIndex = 3;
            this.deltaTextField.Text = "5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Диапазон частот";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Количество трубок";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Разность длин трубок";
            // 
            // frequencyMaxTextBox
            // 
            this.frequencyMaxTextBox.Location = new System.Drawing.Point(210, 90);
            this.frequencyMaxTextBox.Name = "frequencyMaxTextBox";
            this.frequencyMaxTextBox.Size = new System.Drawing.Size(38, 20);
            this.frequencyMaxTextBox.TabIndex = 8;
            this.frequencyMaxTextBox.Text = "5000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(190, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "to";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 227);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(309, 23);
            this.progressBar1.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(252, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "см";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(253, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Гц";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(174, 176);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(147, 34);
            this.button2.TabIndex = 17;
            this.button2.Text = "Построить диаграмму направленности";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frequencyField
            // 
            this.frequencyField.Location = new System.Drawing.Point(148, 64);
            this.frequencyField.Name = "frequencyField";
            this.frequencyField.Size = new System.Drawing.Size(100, 20);
            this.frequencyField.TabIndex = 16;
            this.frequencyField.Text = "500";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(89, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Частота";
            // 
            // organRadioButton
            // 
            this.organRadioButton.AutoSize = true;
            this.organRadioButton.Location = new System.Drawing.Point(14, 36);
            this.organRadioButton.Name = "organRadioButton";
            this.organRadioButton.Size = new System.Drawing.Size(159, 17);
            this.organRadioButton.TabIndex = 14;
            this.organRadioButton.TabStop = true;
            this.organRadioButton.Text = "Микрофон органного типа";
            this.organRadioButton.UseVisualStyleBackColor = true;
            // 
            // lineGroupRadioButton
            // 
            this.lineGroupRadioButton.AutoSize = true;
            this.lineGroupRadioButton.Checked = true;
            this.lineGroupRadioButton.Location = new System.Drawing.Point(14, 12);
            this.lineGroupRadioButton.Name = "lineGroupRadioButton";
            this.lineGroupRadioButton.Size = new System.Drawing.Size(179, 17);
            this.lineGroupRadioButton.TabIndex = 13;
            this.lineGroupRadioButton.TabStop = true;
            this.lineGroupRadioButton.Text = "Линейная группа микрофонов";
            this.lineGroupRadioButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 262);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.frequencyField);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.organRadioButton);
            this.Controls.Add(this.lineGroupRadioButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.frequencyMaxTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.deltaTextField);
            this.Controls.Add(this.numberTextField);
            this.Controls.Add(this.frequencyTextField);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox frequencyTextField;
        private System.Windows.Forms.TextBox numberTextField;
        private System.Windows.Forms.TextBox deltaTextField;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox frequencyMaxTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox frequencyField;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton organRadioButton;
        private System.Windows.Forms.RadioButton lineGroupRadioButton;
    }
}


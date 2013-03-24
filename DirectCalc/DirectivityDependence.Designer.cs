namespace DirectCalc
{
    partial class DirectivityDependence
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
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.button1 = new System.Windows.Forms.Button();
            this.firstTypeMicrophoneNameLabel = new System.Windows.Forms.Label();
            this.secondTypeMicrophoneNameLabel = new System.Windows.Forms.Label();
            this.thirdTypeMicrophoneNameLabel = new System.Windows.Forms.Label();
            this.firstTypeValue = new System.Windows.Forms.Label();
            this.secondTypeValue = new System.Windows.Forms.Label();
            this.thirdTypeValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(12, 12);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(560, 504);
            this.zedGraphControl1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(578, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(175, 39);
            this.button1.TabIndex = 1;
            this.button1.Text = "Разборчивость речи";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // firstTypeMicrophoneNameLabel
            // 
            this.firstTypeMicrophoneNameLabel.AutoSize = true;
            this.firstTypeMicrophoneNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.firstTypeMicrophoneNameLabel.Location = new System.Drawing.Point(578, 71);
            this.firstTypeMicrophoneNameLabel.Name = "firstTypeMicrophoneNameLabel";
            this.firstTypeMicrophoneNameLabel.Size = new System.Drawing.Size(41, 13);
            this.firstTypeMicrophoneNameLabel.TabIndex = 2;
            this.firstTypeMicrophoneNameLabel.Text = "label1";
            this.firstTypeMicrophoneNameLabel.Visible = false;
            // 
            // secondTypeMicrophoneNameLabel
            // 
            this.secondTypeMicrophoneNameLabel.AutoSize = true;
            this.secondTypeMicrophoneNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.secondTypeMicrophoneNameLabel.Location = new System.Drawing.Point(578, 131);
            this.secondTypeMicrophoneNameLabel.Name = "secondTypeMicrophoneNameLabel";
            this.secondTypeMicrophoneNameLabel.Size = new System.Drawing.Size(41, 13);
            this.secondTypeMicrophoneNameLabel.TabIndex = 3;
            this.secondTypeMicrophoneNameLabel.Text = "label2";
            this.secondTypeMicrophoneNameLabel.Visible = false;
            // 
            // thirdTypeMicrophoneNameLabel
            // 
            this.thirdTypeMicrophoneNameLabel.AutoSize = true;
            this.thirdTypeMicrophoneNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.thirdTypeMicrophoneNameLabel.Location = new System.Drawing.Point(578, 200);
            this.thirdTypeMicrophoneNameLabel.Name = "thirdTypeMicrophoneNameLabel";
            this.thirdTypeMicrophoneNameLabel.Size = new System.Drawing.Size(41, 13);
            this.thirdTypeMicrophoneNameLabel.TabIndex = 4;
            this.thirdTypeMicrophoneNameLabel.Text = "label3";
            this.thirdTypeMicrophoneNameLabel.Visible = false;
            // 
            // firstTypeValue
            // 
            this.firstTypeValue.AutoSize = true;
            this.firstTypeValue.Location = new System.Drawing.Point(578, 100);
            this.firstTypeValue.Name = "firstTypeValue";
            this.firstTypeValue.Size = new System.Drawing.Size(35, 13);
            this.firstTypeValue.TabIndex = 5;
            this.firstTypeValue.Text = "label1";
            this.firstTypeValue.Visible = false;
            // 
            // secondTypeValue
            // 
            this.secondTypeValue.AutoSize = true;
            this.secondTypeValue.Location = new System.Drawing.Point(578, 166);
            this.secondTypeValue.Name = "secondTypeValue";
            this.secondTypeValue.Size = new System.Drawing.Size(35, 13);
            this.secondTypeValue.TabIndex = 6;
            this.secondTypeValue.Text = "label1";
            this.secondTypeValue.Visible = false;
            // 
            // thirdTypeValue
            // 
            this.thirdTypeValue.AutoSize = true;
            this.thirdTypeValue.Location = new System.Drawing.Point(578, 236);
            this.thirdTypeValue.Name = "thirdTypeValue";
            this.thirdTypeValue.Size = new System.Drawing.Size(35, 13);
            this.thirdTypeValue.TabIndex = 7;
            this.thirdTypeValue.Text = "label1";
            this.thirdTypeValue.Visible = false;
            // 
            // DirectivityDependence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 533);
            this.Controls.Add(this.thirdTypeValue);
            this.Controls.Add(this.secondTypeValue);
            this.Controls.Add(this.firstTypeValue);
            this.Controls.Add(this.thirdTypeMicrophoneNameLabel);
            this.Controls.Add(this.secondTypeMicrophoneNameLabel);
            this.Controls.Add(this.firstTypeMicrophoneNameLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.zedGraphControl1);
            this.Name = "DirectivityDependence";
            this.Text = "График зависимостей КН микрофонов от частоты";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.Resize += new System.EventHandler(this.Form2_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label firstTypeMicrophoneNameLabel;
        private System.Windows.Forms.Label secondTypeMicrophoneNameLabel;
        private System.Windows.Forms.Label thirdTypeMicrophoneNameLabel;
        private System.Windows.Forms.Label firstTypeValue;
        private System.Windows.Forms.Label secondTypeValue;
        private System.Windows.Forms.Label thirdTypeValue;
    }
}
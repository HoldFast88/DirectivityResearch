namespace DirectCalc
{
    partial class DirectivityDiagram
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
            this.components = new System.ComponentModel.Container();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.delthaNumeric = new System.Windows.Forms.NumericUpDown();
            this.countNumeric = new System.Windows.Forms.NumericUpDown();
            this.frequencyNumeric = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.diameterNumeric = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.delthaNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frequencyNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diameterNumeric)).BeginInit();
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
            this.zedGraphControl1.Size = new System.Drawing.Size(512, 514);
            this.zedGraphControl1.TabIndex = 1;
            // 
            // delthaNumeric
            // 
            this.delthaNumeric.DecimalPlaces = 1;
            this.delthaNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.delthaNumeric.Location = new System.Drawing.Point(550, 427);
            this.delthaNumeric.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.delthaNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.delthaNumeric.Name = "delthaNumeric";
            this.delthaNumeric.Size = new System.Drawing.Size(120, 20);
            this.delthaNumeric.TabIndex = 2;
            this.delthaNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.delthaNumeric.ValueChanged += new System.EventHandler(this.ValueChanged);
            this.delthaNumeric.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValueChanged);
            // 
            // countNumeric
            // 
            this.countNumeric.Location = new System.Drawing.Point(550, 352);
            this.countNumeric.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.countNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.countNumeric.Name = "countNumeric";
            this.countNumeric.Size = new System.Drawing.Size(120, 20);
            this.countNumeric.TabIndex = 3;
            this.countNumeric.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.countNumeric.ValueChanged += new System.EventHandler(this.ValueChanged);
            this.countNumeric.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValueChanged);
            // 
            // frequencyNumeric
            // 
            this.frequencyNumeric.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.frequencyNumeric.Location = new System.Drawing.Point(550, 275);
            this.frequencyNumeric.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.frequencyNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.frequencyNumeric.Name = "frequencyNumeric";
            this.frequencyNumeric.Size = new System.Drawing.Size(120, 20);
            this.frequencyNumeric.TabIndex = 4;
            this.frequencyNumeric.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.frequencyNumeric.ValueChanged += new System.EventHandler(this.ValueChanged);
            this.frequencyNumeric.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(547, 259);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Частота, Гц";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(547, 336);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Кол-во трубок (микрофонов)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(547, 411);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Шаг приращения, см";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(547, 490);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Диаметр зеркала, см";
            // 
            // diameterNumeric
            // 
            this.diameterNumeric.Location = new System.Drawing.Point(550, 506);
            this.diameterNumeric.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.diameterNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.diameterNumeric.Name = "diameterNumeric";
            this.diameterNumeric.Size = new System.Drawing.Size(120, 20);
            this.diameterNumeric.TabIndex = 8;
            this.diameterNumeric.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.diameterNumeric.ValueChanged += new System.EventHandler(this.ValueChanged);
            this.diameterNumeric.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValueChanged);
            // 
            // DirectivityDiagram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 538);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.diameterNumeric);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.frequencyNumeric);
            this.Controls.Add(this.countNumeric);
            this.Controls.Add(this.delthaNumeric);
            this.Controls.Add(this.zedGraphControl1);
            this.Name = "DirectivityDiagram";
            this.Text = "Диаграмма направленности";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.Resize += new System.EventHandler(this.Form3_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.delthaNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frequencyNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diameterNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.NumericUpDown delthaNumeric;
        private System.Windows.Forms.NumericUpDown countNumeric;
        private System.Windows.Forms.NumericUpDown frequencyNumeric;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown diameterNumeric;

    }
}
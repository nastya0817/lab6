namespace lab6
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbFlipYZ = new System.Windows.Forms.CheckBox();
            this.cbFlipXZ = new System.Windows.Forms.CheckBox();
            this.cbFlipXY = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtScaleZ = new System.Windows.Forms.TextBox();
            this.txtScaleY = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtScaleX = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRotationZ = new System.Windows.Forms.TextBox();
            this.txtRotationY = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtRotationX = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOffsetZ = new System.Windows.Forms.TextBox();
            this.txtOffsetY = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOffsetX = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(864, 648);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbFlipYZ);
            this.panel1.Controls.Add(this.cbFlipXZ);
            this.panel1.Controls.Add(this.cbFlipXY);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.txtScaleZ);
            this.panel1.Controls.Add(this.txtScaleY);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.txtScaleX);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtRotationZ);
            this.panel1.Controls.Add(this.txtRotationY);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtRotationX);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtOffsetZ);
            this.panel1.Controls.Add(this.txtOffsetY);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtOffsetX);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(664, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 648);
            this.panel1.TabIndex = 1;
            // 
            // cbFlipYZ
            // 
            this.cbFlipYZ.AutoSize = true;
            this.cbFlipYZ.Location = new System.Drawing.Point(25, 499);
            this.cbFlipYZ.Name = "cbFlipYZ";
            this.cbFlipYZ.Size = new System.Drawing.Size(138, 24);
            this.cbFlipYZ.TabIndex = 23;
            this.cbFlipYZ.Text = "Отразить по YZ";
            this.cbFlipYZ.UseVisualStyleBackColor = true;
            this.cbFlipYZ.CheckedChanged += new System.EventHandler(this.cbFlipYZ_CheckedChanged);
            // 
            // cbFlipXZ
            // 
            this.cbFlipXZ.AutoSize = true;
            this.cbFlipXZ.Location = new System.Drawing.Point(25, 469);
            this.cbFlipXZ.Name = "cbFlipXZ";
            this.cbFlipXZ.Size = new System.Drawing.Size(139, 24);
            this.cbFlipXZ.TabIndex = 22;
            this.cbFlipXZ.Text = "Отразить по XZ";
            this.cbFlipXZ.UseVisualStyleBackColor = true;
            this.cbFlipXZ.CheckedChanged += new System.EventHandler(this.cbFlipXZ_CheckedChanged);
            // 
            // cbFlipXY
            // 
            this.cbFlipXY.AutoSize = true;
            this.cbFlipXY.Location = new System.Drawing.Point(25, 439);
            this.cbFlipXY.Name = "cbFlipXY";
            this.cbFlipXY.Size = new System.Drawing.Size(138, 24);
            this.cbFlipXY.TabIndex = 21;
            this.cbFlipXY.Text = "Отразить по XY";
            this.cbFlipXY.UseVisualStyleBackColor = true;
            this.cbFlipXY.CheckedChanged += new System.EventHandler(this.cbFlipXY_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 387);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 20);
            this.label9.TabIndex = 20;
            this.label9.Text = "Z";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 354);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 20);
            this.label10.TabIndex = 19;
            this.label10.Text = "Y";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(25, 321);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(18, 20);
            this.label11.TabIndex = 18;
            this.label11.Text = "X";
            // 
            // txtScaleZ
            // 
            this.txtScaleZ.Location = new System.Drawing.Point(50, 384);
            this.txtScaleZ.Name = "txtScaleZ";
            this.txtScaleZ.Size = new System.Drawing.Size(125, 27);
            this.txtScaleZ.TabIndex = 17;
            this.txtScaleZ.Text = "1";
            // 
            // txtScaleY
            // 
            this.txtScaleY.Location = new System.Drawing.Point(50, 351);
            this.txtScaleY.Name = "txtScaleY";
            this.txtScaleY.Size = new System.Drawing.Size(125, 27);
            this.txtScaleY.TabIndex = 16;
            this.txtScaleY.Text = "1";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(14, 295);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 20);
            this.label12.TabIndex = 15;
            this.label12.Text = "Масштаб";
            // 
            // txtScaleX
            // 
            this.txtScaleX.Location = new System.Drawing.Point(50, 318);
            this.txtScaleX.Name = "txtScaleX";
            this.txtScaleX.Size = new System.Drawing.Size(125, 27);
            this.txtScaleX.TabIndex = 14;
            this.txtScaleX.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 248);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "Z";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 215);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 182);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(18, 20);
            this.label7.TabIndex = 11;
            this.label7.Text = "X";
            // 
            // txtRotationZ
            // 
            this.txtRotationZ.Location = new System.Drawing.Point(50, 245);
            this.txtRotationZ.Name = "txtRotationZ";
            this.txtRotationZ.Size = new System.Drawing.Size(125, 27);
            this.txtRotationZ.TabIndex = 10;
            this.txtRotationZ.Text = "0";
            // 
            // txtRotationY
            // 
            this.txtRotationY.Location = new System.Drawing.Point(50, 212);
            this.txtRotationY.Name = "txtRotationY";
            this.txtRotationY.Size = new System.Drawing.Size(125, 27);
            this.txtRotationY.TabIndex = 9;
            this.txtRotationY.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 156);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 20);
            this.label8.TabIndex = 8;
            this.label8.Text = "Поворот";
            // 
            // txtRotationX
            // 
            this.txtRotationX.Location = new System.Drawing.Point(50, 179);
            this.txtRotationX.Name = "txtRotationX";
            this.txtRotationX.Size = new System.Drawing.Size(125, 27);
            this.txtRotationX.TabIndex = 7;
            this.txtRotationX.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Z";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Y";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "X";
            // 
            // txtOffsetZ
            // 
            this.txtOffsetZ.Location = new System.Drawing.Point(50, 110);
            this.txtOffsetZ.Name = "txtOffsetZ";
            this.txtOffsetZ.Size = new System.Drawing.Size(125, 27);
            this.txtOffsetZ.TabIndex = 3;
            this.txtOffsetZ.Text = "0";
            // 
            // txtOffsetY
            // 
            this.txtOffsetY.Location = new System.Drawing.Point(50, 77);
            this.txtOffsetY.Name = "txtOffsetY";
            this.txtOffsetY.Size = new System.Drawing.Size(125, 27);
            this.txtOffsetY.TabIndex = 2;
            this.txtOffsetY.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Смещение";
            // 
            // txtOffsetX
            // 
            this.txtOffsetX.Location = new System.Drawing.Point(50, 44);
            this.txtOffsetX.Name = "txtOffsetX";
            this.txtOffsetX.Size = new System.Drawing.Size(125, 27);
            this.txtOffsetX.TabIndex = 0;
            this.txtOffsetX.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 648);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox pictureBox1;
        private Panel panel1;
        private TextBox txtOffsetZ;
        private TextBox txtOffsetY;
        private Label label1;
        private TextBox txtOffsetX;
        private Label label2;
        private Label label4;
        private Label label3;
        private Label label9;
        private Label label10;
        private Label label11;
        private TextBox txtScaleZ;
        private TextBox txtScaleY;
        private Label label12;
        private TextBox txtScaleX;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox txtRotationZ;
        private TextBox txtRotationY;
        private Label label8;
        private TextBox txtRotationX;
        private CheckBox cbFlipYZ;
        private CheckBox cbFlipXZ;
        private CheckBox cbFlipXY;
    }
}
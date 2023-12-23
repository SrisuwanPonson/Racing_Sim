
namespace Racing_Simulation
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
            this.components = new System.ComponentModel.Container();
            this.frame = new System.Windows.Forms.PictureBox();
            this.car = new System.Windows.Forms.PictureBox();
            this.labelVel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.labelTotalPoints = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelLocationX = new System.Windows.Forms.Label();
            this.labelLocationY = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.labelMaxDegreePoint = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.labelMaxDegree = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.labelAngle = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.labelStatus = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelMeterPerPoints = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelPoint = new System.Windows.Forms.Label();
            this.btInsertList = new System.Windows.Forms.Button();
            this.btnSearchCurve = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.frame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.car)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // frame
            // 
            this.frame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.frame.Location = new System.Drawing.Point(12, 34);
            this.frame.Name = "frame";
            this.frame.Size = new System.Drawing.Size(307, 387);
            this.frame.TabIndex = 1;
            this.frame.TabStop = false;
            this.frame.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // car
            // 
            this.car.BackColor = System.Drawing.Color.Red;
            this.car.Location = new System.Drawing.Point(69, 213);
            this.car.Name = "car";
            this.car.Size = new System.Drawing.Size(11, 10);
            this.car.TabIndex = 4;
            this.car.TabStop = false;
            // 
            // labelVel
            // 
            this.labelVel.AutoSize = true;
            this.labelVel.Location = new System.Drawing.Point(149, 65);
            this.labelVel.Name = "labelVel";
            this.labelVel.Size = new System.Drawing.Size(16, 13);
            this.labelVel.TabIndex = 13;
            this.labelVel.Text = "---";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(93, 65);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Velocity :";
            // 
            // labelTotalPoints
            // 
            this.labelTotalPoints.AutoSize = true;
            this.labelTotalPoints.Location = new System.Drawing.Point(267, 44);
            this.labelTotalPoints.Name = "labelTotalPoints";
            this.labelTotalPoints.Size = new System.Drawing.Size(16, 13);
            this.labelTotalPoints.TabIndex = 16;
            this.labelTotalPoints.Text = "---";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(196, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "Total Points";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnStart.Location = new System.Drawing.Point(276, 19);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(101, 35);
            this.btnStart.TabIndex = 17;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnStop.Location = new System.Drawing.Point(169, 19);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(101, 35);
            this.btnStop.TabIndex = 18;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Location X :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(218, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Location Y:";
            // 
            // labelLocationX
            // 
            this.labelLocationX.AutoSize = true;
            this.labelLocationX.Location = new System.Drawing.Point(149, 42);
            this.labelLocationX.Name = "labelLocationX";
            this.labelLocationX.Size = new System.Drawing.Size(16, 13);
            this.labelLocationX.TabIndex = 21;
            this.labelLocationX.Text = "---";
            // 
            // labelLocationY
            // 
            this.labelLocationY.AutoSize = true;
            this.labelLocationY.Location = new System.Drawing.Point(285, 42);
            this.labelLocationY.Name = "labelLocationY";
            this.labelLocationY.Size = new System.Drawing.Size(16, 13);
            this.labelLocationY.TabIndex = 22;
            this.labelLocationY.Text = "---";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelPoint);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.labelLocationY);
            this.groupBox1.Controls.Add(this.labelLocationX);
            this.groupBox1.Controls.Add(this.labelAngle);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.labelVel);
            this.groupBox1.Location = new System.Drawing.Point(337, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(401, 141);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controller";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(209, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "km/h";
            // 
            // labelMaxDegreePoint
            // 
            this.labelMaxDegreePoint.AutoSize = true;
            this.labelMaxDegreePoint.Location = new System.Drawing.Point(708, 490);
            this.labelMaxDegreePoint.Name = "labelMaxDegreePoint";
            this.labelMaxDegreePoint.Size = new System.Drawing.Size(16, 13);
            this.labelMaxDegreePoint.TabIndex = 32;
            this.labelMaxDegreePoint.Text = "---";
            this.labelMaxDegreePoint.Visible = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(665, 490);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(37, 13);
            this.label14.TabIndex = 32;
            this.label14.Text = "Point :";
            this.label14.Visible = false;
            // 
            // labelMaxDegree
            // 
            this.labelMaxDegree.AutoSize = true;
            this.labelMaxDegree.Location = new System.Drawing.Point(624, 490);
            this.labelMaxDegree.Name = "labelMaxDegree";
            this.labelMaxDegree.Size = new System.Drawing.Size(16, 13);
            this.labelMaxDegree.TabIndex = 31;
            this.labelMaxDegree.Text = "---";
            this.labelMaxDegree.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(555, 490);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 13);
            this.label13.TabIndex = 30;
            this.label13.Text = "Max Degree :";
            this.label13.Visible = false;
            // 
            // labelAngle
            // 
            this.labelAngle.AutoSize = true;
            this.labelAngle.Location = new System.Drawing.Point(141, 99);
            this.labelAngle.Name = "labelAngle";
            this.labelAngle.Size = new System.Drawing.Size(16, 13);
            this.labelAngle.TabIndex = 28;
            this.labelAngle.Text = "---";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Steering Angle(Degree) :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSearchCurve);
            this.groupBox2.Controls.Add(this.btInsertList);
            this.groupBox2.Controls.Add(this.btnStart);
            this.groupBox2.Controls.Add(this.btnStop);
            this.groupBox2.Location = new System.Drawing.Point(337, 187);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(401, 234);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Manual";
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Interval = 30;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.labelStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.labelStatus.Location = new System.Drawing.Point(226, 356);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(86, 20);
            this.labelStatus.TabIndex = 29;
            this.labelStatus.Text = "READY !!";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(187, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "meter / points";
            // 
            // labelMeterPerPoints
            // 
            this.labelMeterPerPoints.AutoSize = true;
            this.labelMeterPerPoints.Location = new System.Drawing.Point(267, 62);
            this.labelMeterPerPoints.Name = "labelMeterPerPoints";
            this.labelMeterPerPoints.Size = new System.Drawing.Size(16, 13);
            this.labelMeterPerPoints.TabIndex = 31;
            this.labelMeterPerPoints.Text = "---";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(106, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 36;
            this.label7.Text = "Point :";
            // 
            // labelPoint
            // 
            this.labelPoint.AutoSize = true;
            this.labelPoint.Location = new System.Drawing.Point(149, 23);
            this.labelPoint.Name = "labelPoint";
            this.labelPoint.Size = new System.Drawing.Size(16, 13);
            this.labelPoint.TabIndex = 32;
            this.labelPoint.Text = "---";
            // 
            // btInsertList
            // 
            this.btInsertList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btInsertList.Enabled = false;
            this.btInsertList.Location = new System.Drawing.Point(276, 60);
            this.btInsertList.Name = "btInsertList";
            this.btInsertList.Size = new System.Drawing.Size(101, 35);
            this.btInsertList.TabIndex = 35;
            this.btInsertList.Text = "Insert List";
            this.btInsertList.UseVisualStyleBackColor = false;
            this.btInsertList.Visible = false;
            this.btInsertList.Click += new System.EventHandler(this.btInsertList_Click);
            // 
            // btnSearchCurve
            // 
            this.btnSearchCurve.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSearchCurve.Enabled = false;
            this.btnSearchCurve.Location = new System.Drawing.Point(169, 60);
            this.btnSearchCurve.Name = "btnSearchCurve";
            this.btnSearchCurve.Size = new System.Drawing.Size(101, 35);
            this.btnSearchCurve.TabIndex = 32;
            this.btnSearchCurve.Text = "Search for Curve";
            this.btnSearchCurve.UseVisualStyleBackColor = false;
            this.btnSearchCurve.Visible = false;
            this.btnSearchCurve.Click += new System.EventHandler(this.btnSearchCurve_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(753, 603);
            this.Controls.Add(this.labelMeterPerPoints);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.labelMaxDegreePoint);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelMaxDegree);
            this.Controls.Add(this.labelTotalPoints);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.car);
            this.Controls.Add(this.frame);
            this.Name = "Form1";
            this.Text = "RACHING SIMULATION";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.frame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.car)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox frame;
        private System.Windows.Forms.PictureBox car;
        private System.Windows.Forms.Label labelVel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labelTotalPoints;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelLocationX;
        private System.Windows.Forms.Label labelLocationY;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelAngle;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelMaxDegree;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label labelMaxDegreePoint;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelMeterPerPoints;
        private System.Windows.Forms.Label labelPoint;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSearchCurve;
        private System.Windows.Forms.Button btInsertList;
    }
}


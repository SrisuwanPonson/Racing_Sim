using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Racing_Simulation
{
    public partial class Form1 : Form
    {
        #region Variable
        public string Revision = "23.12.22.00";
        Vector CarVector, PathVector,NewPathVector;
        List<LineTracking> points = null;
       
        Pen bluePen = new Pen(Color.Blue, 2);
        Pen redPen = new Pen(Color.Red, 2);
        Point P = new Point(0, 0);
        Point CarLocation = new Point(0, 0);
        int offsetX;
        int offsetY;
        int i = 1;
        int VEL = 0;
        int InternalDelay = 10;
        private bool _isStarted = false;
        private bool _enableSteering = false;
        private double MaxDegree = 0;
        private int _pointPerStep = 5;
        private double _meterPerPoint = 0.69;//1000/1449 
        #endregion

        #region Constructure
        public Form1()
        {
            InitializeComponent();
            CarVector = new Vector();
            PathVector = new Vector(CarVector);
            NewPathVector = new Vector(CarVector);
            this.Text = "Racing Simulator Rev." + this.Revision;
            try
            {
                points = File.ReadAllLines("coordinates_v2.csv")
                                                .Skip(0)
                    .Select(v => LineTracking.FromCsv(v))
                    .ToList();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            labelStatus.ForeColor = Color.Blue;
            

        }
        #endregion

        #region Event Handler

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {


            float x1 = 20.0F, y1 = 20.0F;
            float x2 = 200.0F, y2 = 20.0F;

            for (int i = 0; i < points.Count - 1; i++)
            {

                x1 = points[i].Xaxis;
                y1 = points[i].Yaxis;
                x2 = points[i + 1].Xaxis;
                y2 = points[i + 1].Yaxis;
                e.Graphics.DrawLine(bluePen, x1, y1, x2, y2);

            }
            //for (int i = 0; i < points_Old.Count - 1; i++)
            //{

            //    x1 = points_Old[i].Xaxis;
            //    y1 = points_Old[i].Yaxis;
            //    x2 = points_Old[i + 1].Xaxis;
            //    y2 = points_Old[i + 1].Yaxis;
            //    e.Graphics.DrawLine(redPen, x1+3, y1, x2, y2);

            //}

        }
        public void UpdatePath(List<LineTracking> path,string FileName)
        {
            string data = null;
            for(int i=0;i<path.Count;i++)
            {
                data += path[i].Xaxis.ToString() + "," + path[i].Yaxis.ToString() + "\n";
            }
            System.IO.File.WriteAllText("coordinates_v2.csv", data);
        }
        private void btnAcc_Click(object sender, EventArgs e)
        {
            
            labelStatus.Text = "VEL UP";
            labelStatus.ForeColor = Color.Red;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //double Velocity = (_pointPerStep * 1000 / timer1.Interval);
           
            //belVel.Text = (Velocity*_meterPerPoint*3.6).ToString("F2");

            if (_enableSteering)
            {

            }
            else
            {
                if (i < points.Count-_pointPerStep)
                {
                    P.X = offsetX + Convert.ToInt32(points[i+1].Xaxis);
                    P.Y = offsetY + Convert.ToInt32(points[i+ 1].Yaxis);
                    car.Location = P;

                    #region Update Current Locatio
                    labelLocationX.Text = points[i].Xaxis.ToString();
                    labelLocationY.Text = points[i].Yaxis.ToString();
                    try
                    {
                        if(i==0)
                        {
                            //CarVector.X = points[i].Xaxis - points[i + 1].Xaxis;
                            //CarVector.Y = points[i].Yaxis - points[i + 1].Yaxis;
                        }
                        else
                        {
                            CarVector.X = points[i].Xaxis - points[i - 1].Xaxis;
                            CarVector.Y = points[i].Yaxis - points[i - 1].Yaxis;
                        }
                        
                        CarVector.Uniteze();
                        labelCarVector.Text = CarVector.ToString();
                    }
                    catch (Exception ex)
                    {

                        //labelCarVector.Text = "Error";
                    }
                    try
                    {
                       
                        PathVector.X = points[i + 1].Xaxis - points[i].Xaxis;
                        PathVector.Y = points[i + 1].Yaxis - points[i].Yaxis;
                        PathVector.Uniteze();
                        labelPathVector.Text = PathVector.ToString();
                    }
                    catch (Exception)
                    {
                        //labelPathVector.Text = "Error";
                    }

                    try
                    {
                        double angle_rads = Vector.GetAngle(PathVector, CarVector);

                        double Deg = Vector.ConvertRadiansToDegrees(angle_rads);
                        //Deg = Deg ;
                        //if (Deg < 1)
                        //{
                        //    Deg = 0;
                        //    if (timer1.Interval > 10)
                        //    {
                        //        //VEL = VEL + 5;//modify later
                        //        timer1.Interval = timer1.Interval - 10;
                        //    }
                        //}
                        //else if(Deg>=1&&Deg<5)
                        //{
                        //    timer1.Interval = timer1.Interval + 5;
                        //}
                        // else
                        //{
                        //    timer1.Interval = timer1.Interval + 10;
                        //}
                        if (Deg > 6) //7*5
                        {
                            points[i].Xaxis = (points[i + 2].Xaxis + points[i + 1].Xaxis + points[i - 1].Xaxis + points[i - 2].Xaxis) / 4;
                            points[i].Yaxis = (points[i + 2].Yaxis + points[i + 1].Yaxis + points[i - 1].Yaxis + points[i - 2].Yaxis) / 4;
                        }
                        if (Deg > 0 && Deg <= 1) //7*5
                        {
                            points[i].Xaxis = (points[i + 2].Xaxis + points[i + 1].Xaxis + points[i - 1].Xaxis + points[i - 2].Xaxis) / 4;
                            points[i].Yaxis = (points[i + 2].Yaxis + points[i + 1].Yaxis + points[i - 1].Yaxis + points[i - 2].Yaxis) / 4;
                        }

                        labelAngle.Text = Deg.ToString("F2");
                        if(Deg>MaxDegree)
                        {
                            MaxDegree = Math.Round(Deg,5);
                            labelMaxDegreePoint.Text = i.ToString();
                        }
                        labelVel.Text= CarVector.GetVelocity(_meterPerPoint, InternalDelay,timer1.Interval).ToString("F2");
                        labelMaxDegree.Text = MaxDegree.ToString();
                    }
                    catch (Exception)
                    {
                        //labelAngle.Text = "Error";
                        //throw;
                    }

                    #endregion
                    i = i + 1;
                    Thread.Sleep(InternalDelay);
                    //i = i + 5;
                }
                else
                {
                    i = 1;
                    MaxDegree = 0;
                    UpdatePath(points, "coordinates_v2.csv");
                    points = File.ReadAllLines("coordinates_v2.csv")
                                              .Skip(0)
                  .Select(v => LineTracking.FromCsv(v))
                  .ToList();
                    this.Invalidate();
                    i = 0;
                }

            }
        }

        private void btnBrk_Click(object sender, EventArgs e)
        {
            if (VEL > 0)
            {
                VEL--;
                labelStatus.Text = "VEL DOWN";
                labelStatus.ForeColor = Color.Blue;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_isStarted)
            {
                return;
            }
            VEL = 1;
            timer1.Enabled = true;
            Point P = new Point(0, 0);
            P.X = 30;
            P.Y = 20;
            //P = frame.Location;
            offsetX = Convert.ToInt32(frame.Location.X - car.Width / 2);
            offsetY = Convert.ToInt32(frame.Location.Y - car.Height / 2);
            P.X = offsetX + Convert.ToInt32(points[0].Xaxis);
            P.Y = offsetY + Convert.ToInt32(points[0].Yaxis);
            car.Location = P;
            i = 0;
            labelStatus.Text = "START";
            labelStatus.ForeColor = Color.Green;
            MaxDegree = 0;
        }



        private void btnAcc_MouseDown(object sender, MouseEventArgs e)
        {
            timer2.Enabled = true;
        }

        private void btnAcc_MouseUp(object sender, MouseEventArgs e)
        {
            timer2.Enabled = false;
            labelStatus.Text = "----";
            labelStatus.ForeColor = Color.Black;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (VEL < 100)
            {
                VEL++;
                labelStatus.Text = "VEL UP";
                labelStatus.ForeColor = Color.Red;
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (VEL > 0)
            {
                labelStatus.Text = "VEL DOWN";
                labelStatus.ForeColor = Color.Blue;
                VEL--;
            }
        }

        private void btnBrk_MouseDown(object sender, MouseEventArgs e)
        {
            timer3.Enabled = true;
        }

        private void btnBrk_MouseUp(object sender, MouseEventArgs e)
        {
            timer3.Enabled = false;
            labelStatus.Text = "----";
            labelStatus.ForeColor = Color.Black;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _isStarted = false;
            VEL = 0;
            labelVel.Text = 0.ToString();
            timer1.Enabled = false;
            labelStatus.Text = "STOP";
            labelStatus.ForeColor = Color.Red;
        } 
        #endregion
    }
}

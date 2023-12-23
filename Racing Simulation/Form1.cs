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
        List<Curve> curves = null;
       
        Pen bluePen = new Pen(Color.Blue, 2);
        Pen redPen = new Pen(Color.Red, 2);
        Point P = new Point(0, 0);
        Point CarLocation = new Point(0, 0);
        int offsetX;
        int offsetY;
        int i = 48;//48 point = 2 meter
        int VEL = 0;
        int InternalDelay = 100;
        private bool _isStarted = false;
        private bool _enableSteering = false;
        private double MaxDegree = 0;
        private int _pointPerStep = 5;
        private double _meterPerPoint = 0.0416649306278905;//1000/24,001 
        int StepPerPoint = 1;
        string anglePoint = "";
        private bool EnableUpdatPath = false;
        Thread ThreadMonitorCurve = null;
        bool EnableUpdateMarkRed = false;
        #endregion

        #region Constructure
        public Form1()
        {
            InitializeComponent();
            ThreadMonitorCurve= new Thread(new ThreadStart(MonitorCurve));
            CarVector = new Vector();
            PathVector = new Vector(CarVector);
            NewPathVector = new Vector(CarVector);
            this.Text = "Racing Simulator Rev." + this.Revision;
            try
            {
                points = File.ReadAllLines("coordinates_v2_2.csv")
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
            //_meterPerPoint = 1000 / points.Count;
            labelTotalPoints.Text = points.Count.ToString();
            labelMeterPerPoints.Text =(_meterPerPoint).ToString("F5");


        }

        private void MonitorCurve()
        {
            //curves = CurveSeacher(points);
            if (curves.Count>0)
            {
                while (CarVector.IsRun)
                {
                    foreach (Curve curve in curves)
                    {

                    }
                } 
            }
        }

        public List<Curve> CurveSeacher(List<LineTracking> loop)
        {
            bool start,stop;
            start = false;
            stop = true;
            List<Curve> curves = new List<Curve>();
            Curve curve = new Curve();
            List<LineTracking> Loop = loop;
            Coords startPoint,endPoint;
            startPoint = new Coords();
            endPoint = new Coords();
            double count=0;
            int startPathPoint = 0;
            int endPathPoint = 0;
            for (int i=0;i<Loop.Count;i+=StepPerPoint)
            {
                #region Car
                try
                {
                    if (i == 0)
                    {
                        CarVector.X = Loop[i].Xaxis - Loop[i + 48].Xaxis;//48 points =2 meter
                        CarVector.Y = Loop[i].Yaxis - Loop[i + 48].Yaxis;//48 points =2 meter
                    }
                    else
                    {
                        CarVector.X = Loop[i].Xaxis - Loop[i - 48].Xaxis;//48 points =2 meter
                        CarVector.Y = Loop[i].Yaxis - Loop[i - 48].Yaxis;//48 points =2 meter
                    }

                    CarVector.Uniteze();

                }
                catch (Exception ex)
                {

                    //labelCarVector.Text = "Error";
                }
                #endregion

                #region Path
                try
                {

                    PathVector.X = Loop[i + 12].Xaxis - Loop[i].Xaxis;//12 points = 0.25 meter
                    PathVector.Y = Loop[i + 12].Yaxis - Loop[i].Yaxis;//12 points = 0.25 meter
                    PathVector.Uniteze();
                    //labelPathVector.Text = PathVector.ToString();
                }
                catch (Exception)
                {
                    //labelPathVector.Text = "Error";
                }
                #endregion

                #region Angle
                double angle_rads = Vector.GetAngle(PathVector, CarVector);

                double Deg = Vector.ConvertRadiansToDegrees(angle_rads);
                if (Deg > 90)
                {
                    Deg = Deg - 180;
                } 
                #endregion

                if(Deg>3)
                {
                    if (!start)
                    {
                        //startPoint = new Coords(Loop[i].Xaxis, Loop[i].Yaxis);
                        startPoint.X = Loop[i].Xaxis;
                        startPoint.Y = Loop[i].Yaxis;
                        startPathPoint = i;
                        start = true;
                        stop = false;
                    }
                }
                else
                {
                    if(start)
                    {
                        //endPoint = new Coords(Loop[i].Xaxis, Loop[i].Yaxis);
                        endPoint.X = Loop[i].Xaxis;
                        endPoint.Y = Loop[i].Yaxis;
                        endPathPoint = i;
                        start = false;
                        stop = false;
                        count = endPathPoint - startPathPoint;
                        if (true)//50 point= 2meter/0.04
                        {
                            curve = new Curve(startPathPoint, endPathPoint, startPoint, endPoint, CalculateRadius(Loop, startPathPoint, endPathPoint));
                            if (curve.Radius<200&&count>300)
                            {
                                curves.Add(curve); 
                            }
                            //MessageBox.Show("Radius : " + curve.Radius.ToString()); 
                        }
                    }
                }
            }


            string str = "";
            str = "Start(Point_Number),End(Point_Number),Curve_Length(Meter),Radius(Meter)" + "\n";
            foreach(Curve item in curves)
            {
                str += item.StartPathPoint.ToString() + "," + item.EndPathPoint.ToString() + ","+((item.EndPathPoint - item.StartPathPoint)*_meterPerPoint).ToString()+"," + item.Radius.ToString()+"\n";
            }
            System.IO.File.WriteAllText("Curves.csv", str);
            return curves;
        }
        public double CalculateRadius(List<LineTracking> loop,int start,int end)
        {
            int totalPoint = end - start + 1;
            double radius;
            PointF center;
            LineTracking StartPoint = new LineTracking();
            LineTracking EndPoint = new LineTracking();
            LineTracking MidPoint = new LineTracking();
            StartPoint = loop[start];
            EndPoint = loop[end];
            MidPoint = loop[Convert.ToInt32(end - start)];
            Vector temp = new Vector();


            #region calculation
            // Get the perpendicular bisector of (x1, y1) and (x2, y2).
            float x1 = (MidPoint.Xaxis + StartPoint.Xaxis) / 2;
            float y1 = (MidPoint.Yaxis + StartPoint.Yaxis) / 2;
            float dy1 = MidPoint.Xaxis - StartPoint.Xaxis;
            float dx1 = -(MidPoint.Yaxis - StartPoint.Yaxis);

            // Get the perpendicular bisector of (x2, y2) and (x3, y3).
            float x2 = (EndPoint.Xaxis + MidPoint.Xaxis) / 2;
            float y2 = (EndPoint.Yaxis + MidPoint.Yaxis) / 2;
            float dy2 = EndPoint.Xaxis - MidPoint.Xaxis;
            float dx2 = -(EndPoint.Yaxis - MidPoint.Yaxis);

            // See where the lines intersect.
            bool lines_intersect, segments_intersect;
            PointF intersection, close1, close2;
            FindIntersection(
                new PointF(x1, y1), new PointF(x1 + dx1, y1 + dy1),
                new PointF(x2, y2), new PointF(x2 + dx2, y2 + dy2),
                out lines_intersect, out segments_intersect,
                out intersection, out close1, out close2);
            if (!lines_intersect)
            {
                MessageBox.Show("The points are colinear");
                center = new PointF(0, 0);
                radius = 0;
            }
            else
            {
                center = intersection;
                float dx = center.X - StartPoint.Xaxis;
                float dy = center.Y - StartPoint.Yaxis;
                radius = (float)Math.Sqrt(dx * dx + dy * dy);
            } 
            #endregion
            

            return ConvertPointLength2DistanceMeter(radius,loop);
        }
        private void FindIntersection(PointF p1, PointF p2, PointF p3, PointF p4, out bool lines_intersect, out bool segments_intersect, out PointF intersection, out PointF close_p1, out PointF close_p2)
        {
            // Get the segments' parameters.
            float dx12 = p2.X - p1.X;
            float dy12 = p2.Y - p1.Y;
            float dx34 = p4.X - p3.X;
            float dy34 = p4.Y - p3.Y;

            // Solve for t1 and t2
            float denominator = (dy12 * dx34 - dx12 * dy34);

            float t1 =
                ((p1.X - p3.X) * dy34 + (p3.Y - p1.Y) * dx34)
                    / denominator;
            if (float.IsInfinity(t1))
            {
                // The lines are parallel (or close enough to it).
                lines_intersect = false;
                segments_intersect = false;
                intersection = new PointF(float.NaN, float.NaN);
                close_p1 = new PointF(float.NaN, float.NaN);
                close_p2 = new PointF(float.NaN, float.NaN);
                return;
            }
            lines_intersect = true;

            float t2 =
                ((p3.X - p1.X) * dy12 + (p1.Y - p3.Y) * dx12)
                    / -denominator;

            // Find the point of intersection.
            intersection = new PointF(p1.X + dx12 * t1, p1.Y + dy12 * t1);

            // The segments intersect if t1 and t2 are between 0 and 1.
            segments_intersect =
                ((t1 >= 0) && (t1 <= 1) &&
                 (t2 >= 0) && (t2 <= 1));

            // Find the closest points on the segments.
            if (t1 < 0)
            {
                t1 = 0;
            }
            else if (t1 > 1)
            {
                t1 = 1;
            }

            if (t2 < 0)
            {
                t2 = 0;
            }
            else if (t2 > 1)
            {
                t2 = 1;
            }

            close_p1 = new PointF(p1.X + dx12 * t1, p1.Y + dy12 * t1);
            close_p2 = new PointF(p3.X + dx34 * t2, p3.Y + dy34 * t2);
        }
        private double ConvertPointLength2DistanceMeter(double rawlength,List<LineTracking>loop)
        {
            double pointDistance;
            Vector temp = new Vector();
            temp.X = loop[100].Xaxis- loop[0].Xaxis;
            temp.Y = loop[100].Yaxis- loop[0].Yaxis;
            pointDistance = temp.Length;
            pointDistance /= 100;
            double MeterPerPointLength = _meterPerPoint / pointDistance;
            return (rawlength * MeterPerPointLength);

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
            if(/*EnableUpdateMarkRed*/true)
            {
                try
                {
                    curves = CurveSeacher(points);
                    foreach (Curve curve in curves)
                    {
                        for (int i = curve.StartPathPoint; i < curve.EndPathPoint; i++)
                        {
                            x1 = points[i].Xaxis;
                            y1 = points[i].Yaxis;
                            x2 = points[i+1].Xaxis;
                            y2 = points[i+1].Yaxis;
                            e.Graphics.DrawLine(redPen, x1, y1, x2, y2); 
                        }
                        //x1 = points[curve.StartPathPoint].Xaxis;
                        //y1 = points[curve.StartPathPoint].Yaxis;
                        //x2 = points[curve.EndPathPoint].Xaxis;
                        //y2 = points[curve.EndPathPoint].Yaxis;
                        //e.Graphics.DrawLine(redPen, x1, y1, x2, y2);
                    }
                    EnableUpdateMarkRed = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    throw;
                }
            }

        }
        public void UpdatePath(List<LineTracking> path,string FileName)
        {
            string data = null;
            for(int i=0;i<path.Count;i++)
            {
                data += path[i].Xaxis.ToString() + "," + path[i].Yaxis.ToString() + "\n";
            }
            System.IO.File.WriteAllText(FileName, data);
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
                //i = 20000;
                if (i < points.Count - _pointPerStep/*22000*/)
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
                            CarVector.X = points[i].Xaxis - points[i + 24].Xaxis;//48 points =2 meter
                            CarVector.Y = points[i].Yaxis - points[i + 24].Yaxis;//48 points =2 meter
                        }
                        else
                        {
                            CarVector.X = points[i].Xaxis - points[i - 24].Xaxis;//24 points =1 meter
                            CarVector.Y = points[i].Yaxis - points[i - 24].Yaxis;//24 points =1 meter
                        }
                        
                        CarVector.Uniteze();
                        //labelCarVector.Text = CarVector.ToString();
                    }
                    catch (Exception ex)
                    {

                        //labelCarVector.Text = "Error";
                    }
                    try
                    {
                       
                        PathVector.X = points[i + 12].Xaxis - points[i].Xaxis;//12 points = 0.25 meter
                        PathVector.Y = points[i + 12].Yaxis - points[i].Yaxis;//12 points = 0.25 meter
                        PathVector.Uniteze();
                        //labelPathVector.Text = PathVector.ToString();
                    }
                    catch (Exception)
                    {
                        //labelPathVector.Text = "Error";
                    }

                    try
                    {
                        double angle_rads = Vector.GetAngle(PathVector, CarVector);

                        double Deg = Vector.ConvertRadiansToDegrees(angle_rads);
                        if(Deg>90)
                        {
                            Deg = Deg - 180;
                        }

                        if (EnableUpdatPath)
                        {
                            if (Deg > 7) //7*5
                            {
                                points[i].Xaxis = (points[i + 15].Xaxis + points[i + 14].Xaxis + points[i + 13].Xaxis + points[i + 12].Xaxis + points[i + 11].Xaxis +
                                                points[i + 10].Xaxis + points[i + 9].Xaxis + points[i + 8].Xaxis + points[i + 7].Xaxis + points[i + 6].Xaxis+
                                                   points[i + 5].Xaxis + points[i + 4].Xaxis + points[i + 3].Xaxis + points[i + 2].Xaxis + points[i + 1].Xaxis
                                                 + points[i - 1].Xaxis + points[i - 2].Xaxis + points[i - 3].Xaxis + points[i - 4].Xaxis + points[i - 5].Xaxis
                                                 + points[i - 6].Xaxis + points[i - 7].Xaxis + points[i - 8].Xaxis + points[i - 9].Xaxis + points[i - 10].Xaxis
                                                 + points[i - 11].Xaxis + points[i - 12].Xaxis + points[i - 13].Xaxis + points[i - 14].Xaxis + points[i - 15].Xaxis) / 30;

                                points[i].Yaxis = (points[i + 15].Yaxis + points[i + 14].Yaxis + points[i + 13].Yaxis + points[i + 12].Yaxis + points[i + 11].Yaxis +
                                                points[i + 10].Yaxis + points[i + 9].Yaxis + points[i + 8].Yaxis + points[i + 7].Yaxis + points[i + 6].Yaxis+
                                                   points[i + 5].Yaxis + points[i + 4].Yaxis + points[i + 3].Yaxis + points[i + 2].Yaxis + points[i + 1].Yaxis
                                                  + points[i - 1].Yaxis + points[i - 2].Yaxis + points[i - 3].Yaxis + points[i - 4].Yaxis + points[i - 5].Yaxis
                                                  + points[i - 6].Yaxis + points[i - 7].Yaxis + points[i - 8].Yaxis + points[i - 9].Yaxis + points[i - 10].Yaxis
                                                  + points[i - 11].Yaxis + points[i - 12].Yaxis + points[i - 13].Yaxis + points[i - 14].Yaxis + points[i - 15].Yaxis) / 30;
                             
                            } 
                            else
                            {
                                
                            }
                        }
                       
                        if (Deg>90)
                        {
                            anglePoint += 0.ToString() + "\n"; 
                        }
                        else
                        {
                            anglePoint += Deg.ToString() + "\n";
                        }

                    


                        labelAngle.Text = Deg.ToString("F2");
                        if(Deg>MaxDegree)
                        {
                            MaxDegree = Math.Round(Deg,5);
                            labelMaxDegreePoint.Text = i.ToString();
                        }
                        labelVel.Text = CarVector.GetVelocity(StepPerPoint, _meterPerPoint, InternalDelay, timer1.Interval).ToString("F2");
                        labelMaxDegree.Text = MaxDegree.ToString();
                    }
                    catch (Exception)
                    {
                        //labelAngle.Text = "Error";
                        //throw;
                    }

                    #endregion
                    //if (i > 1500 && i < 2000)
                    //{

                    //}
                    //else if(i>15600&& i<22000)
                    //{
                    //    StepPerPoint = 1;
                    //}
                    //else
                    //{
                    //    StepPerPoint = 50;
                    //}
                    //StepPerPoint = 1;
                    labelPoint.Text = i.ToString();
                    InternalDelay = 10;
                    StepPerPoint = 100;
                    i = i + StepPerPoint;
                    Thread.Sleep(InternalDelay);
                    //i = i + 5;
                }
                else
                {
                    i = 48;
                    //i = 20000;
                    MaxDegree = 0;
                    if (EnableUpdatPath)
                    {
                        UpdatePath(points, "coordinates_v2_2.csv");
                        points = File.ReadAllLines("coordinates_v2_2.csv")
                                                  .Skip(0)
                      .Select(v => LineTracking.FromCsv(v))
                      .ToList();
                        this.Invalidate();
                        System.IO.File.WriteAllText("angle.csv", anglePoint);
                        anglePoint = ""; 
                    }
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
            
            EnableUpdateMarkRed = true;
            this.Invalidate();
            if (ThreadMonitorCurve != null)
            {
                ThreadMonitorCurve.Start();
            }
            //else
            //{
            //    ThreadMonitorCurve = new Thread(new ThreadStart(MonitorCurve));
            //    ThreadMonitorCurve.Start();
            //}
            VEL = 1;
            //i = 20000;
            timer1.Enabled = true;
            Point P = new Point(0, 0);
            P.X = 30;
            P.Y = 20;
            //P = frame.Location;
            CarVector.IsRun = true;
            offsetX = Convert.ToInt32(frame.Location.X - car.Width / 2);
            offsetY = Convert.ToInt32(frame.Location.Y - car.Height / 2);
            P.X = offsetX + Convert.ToInt32(points[0].Xaxis);
            P.Y = offsetY + Convert.ToInt32(points[0].Yaxis);
            car.Location = P;
            i = 0;
            labelStatus.Text = "START";
            labelStatus.ForeColor = Color.Green;
            MaxDegree = 0;
            CurveSeacher(points);
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

        private void btMarkRedPoint_Click(object sender, EventArgs e)
        {
            float x1 = 20.0F, y1 = 20.0F;
            float x2 = 200.0F, y2 = 20.0F;

           
            this.Invalidate();
        }

        private void btInsertList_Click(object sender, EventArgs e)
        {
            LineTracking dot = new LineTracking();

            #region Read CSV
            try
            {
                points = File.ReadAllLines("coordinates_v2_2.csv")
                                                .Skip(0)
                    .Select(v => LineTracking.FromCsv(v))
                    .ToList();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            #endregion

            #region Add Zero between point
            for (int i = 1; i < points.Count; i += 2)
            {
                //points_new.Add(points[i]);
                dot.Xaxis = 0;
                dot.Yaxis = 0;
                points.Insert(i, dot);

                //points_new.Add(dot)

            }
            #endregion

            #region Save to CSV
            UpdatePath(points, "coordinates_v2_2.csv"); 
            #endregion

            Thread.Sleep(1000);
            

        

        }

        private void btnSearchCurve_Click(object sender, EventArgs e)
        {
            CurveSeacher(points);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CarVector.IsRun = false;
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
            CarVector.IsRun = false;
            VEL = 0;
            labelVel.Text = 0.ToString();
            timer1.Enabled = false;
            labelStatus.Text = "STOP";
            labelStatus.ForeColor = Color.Red;
        } 
        #endregion

    }
}

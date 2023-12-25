using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Racing_Simulation
{
    public class Vector
    {
        #region Variable
        public static List<LineTracking> points = null;
        public static List<Curve> curves = null;
        public static string FileName = "coordinates_v2.csv";
        #endregion

        #region Property

        #region Non-static
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public bool IsRun { get; set; }
        public int position { get; set; }

        private int _movingStep = 10;
        public int MovingStep
        {
            get
            {
                return _movingStep;
            }
            set
            {
                _movingStep = value;
            }
        }

        private int _stepInterval = 100;
        public int StepInterval
        {
            get
            {
                return _stepInterval;
            }
            set
            {
                _stepInterval = value;
            }
        }
        public double Length
        {
            get
            {
                return GetLenth();
            }
        }

        private double _actualFrontToBackWheel_Meter = 2;//meter
        public double ActualFrontToBackWheel_Meter
        {
            get
            {
                return _actualFrontToBackWheel_Meter;
            }
            set
            {
                _actualFrontToBackWheel_Meter = value;
            }
        }

        #endregion

        #region Static Property

        private static double _actualFrontPath_Meter = 0.25;
        public static double ActualFrontPath_Meter
        {
            get
            {
                return _actualFrontPath_Meter;
            }
            set
            {
                _actualFrontPath_Meter = value;
            }
        }
        private static double _actual_Loop_Lenght = 1000;
        public static double Actual_Loop_Lenght
        {
            get
            {
                return _actual_Loop_Lenght;
            }
            set
            {
                _actual_Loop_Lenght = value; ;
            }
        }


        public static int PointsCount
        {
            get
            {
                return points.Count;
            }

        }


        public static double MeterPerPoint
        {
            get
            {
                return _actual_Loop_Lenght / PointsCount;
            }

        }

        private static int _surroundCouple = 5;
        public static int SurroundCouple
        {
            get
            {
                return _surroundCouple;
            }
            set
            {
                _surroundCouple = value;
            }
        }

        private static double _minimumExceptedAngle = 7;
        public static double MinimumExceptedAngle
        {
            get
            {
                return _minimumExceptedAngle;
            }
            set
            {
                _minimumExceptedAngle = value;
            }
        }

        private static double _minimumCurveAngle = 3;
        public static double MinimumCurveAngle
        {
            get
            {
                return _minimumCurveAngle;
            }
            set
            {
                _minimumCurveAngle = value;
            }
        }

        private static double _maximumActualRedius_Meter = 200;
        public static double MaximumActualRedius_Meter
        {
            get
            {
                return _maximumActualRedius_Meter;
            }
            set
            {
                _maximumActualRedius_Meter = value;
            }
        }
        private static int _minLengthPointCurve = 300;
        public static int MinLengthPointCurve
        {
            get
            {
                return _minLengthPointCurve;
            }
            set
            {
                _minLengthPointCurve = value;
            }
        }
        #endregion

        #endregion

        #region Constructors
        public Vector()
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
            points=GetLoopFromCSV(FileName);
        }
        public Vector(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            points = GetLoopFromCSV(FileName);
        }

        public Vector(Vector other)
        {
            this.X = other.X;
            this.Y = other.Y;
            this.Z = other.Z;
            points = GetLoopFromCSV(FileName);
        }
        #endregion

        #region Override
        public override string ToString()
        {
            return $"{Math.Round(this.X, 2)},{Math.Round(this.Y, 2)}";

        }
        #endregion

        #region Method

        #region Non-static
        private double GetLenth()
        {
            double sql = this.X * this.X + this.Y * this.Y + this.Z * this.Z;
            double len = Math.Sqrt(sql);
            return len;
        }

        public void Revers()
        {
            this.X = -this.X;
            this.Y = -this.Y;
            this.Z = -this.Z;
        }
        public void Scale(double factor)
        {
            this.X *= factor;
            this.Y *= factor;
            this.Z *= factor;
        }
        public bool Uniteze()
        {
            double len = this.GetLenth();
            if (len <= 0)
            {
                return false;
            }
            this.X /= len;
            this.Y /= len;
            this.Z /= len;
            return true;
        }

        public void Add(Vector other)
        {
            this.X += other.X;
            this.Y += other.Y;
            this.Z += other.Z;
        }
        public void Subtract(Vector other)
        {
            this.X -= other.X;
            this.Y -= other.Y;
            this.Z -= other.Z;
        }
        public void SetVelocity(double TagetVel, double InternalDelay, double TimerInterval, out int step)
        {
            step = Convert.ToInt32(TagetVel * (InternalDelay + TimerInterval));
        }

        public double GetVelocity(int stpe, double MeterPerPoint, double InternalDelay, double TimerInterval)
        {
            double Velocity;// Km/h
            Velocity = stpe * MeterPerPoint / (InternalDelay + TimerInterval); //Meter/mSec
            Velocity = Velocity * 1000;//Meter/Sec
            Velocity = Velocity / 1000;//Km/Sec
            Velocity = Velocity * 3600;//Km/h
            return Velocity;
        }
        #endregion

        #region Static
        public static Vector Addition(Vector a, Vector b)
        {
            double newX = a.X + b.X;
            double newY = a.Y + b.Y;
            double newZ = a.Z + b.Z;
            Vector v = new Vector(newX, newY, newZ);
            return v;
        }
        public static double DotProduct(Vector a, Vector b)
        {
            return ((a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z));
        }
        public static double GetAngle(Vector a, Vector b)
        {
            if (double.IsNaN(Math.Acos(DotProduct(a, b) / (a.Length * b.Length))))
            {
                return 0;
            }
            return Math.Acos(DotProduct(a, b) / (a.Length * b.Length));
        }
        public static double ConvertRadiansToDegrees(double radians)
        {
            double degrees = (180 / Math.PI) * radians;
            return (degrees);
        }
        public static List<LineTracking> GetLoopFromCSV(string FileName)
        {
            try
            {
                return File.ReadAllLines(FileName)
                                                .Skip(0)
                    .Select(v => LineTracking.FromCsv(v))
                    .ToList();

            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        }


        public static List<Curve> CurveSeacher(List<LineTracking> loop)
        {
            bool start, stop;
            start = false;
            stop = true;
            List<Curve> curves = new List<Curve>();
            Curve curve = new Curve();
            List<LineTracking> Loop = loop;
            Coords startPoint, endPoint;
            startPoint = new Coords();
            endPoint = new Coords();
            double count = 0;
            int startPathPoint = 0;
            int endPathPoint = 0;

            Vector CarVector = new Vector();
            Vector PathVector = new Vector();
            for (int i = 0; i < Loop.Count; i += CarVector.MovingStep)
            {
                #region Car
                try
                {
                    if (i == 0)
                    {
                        CarVector.X = Loop[i].Xaxis - Loop[i + Convert.ToInt32(CarVector.ActualFrontToBackWheel_Meter / MeterPerPoint)].Xaxis;
                        CarVector.Y = Loop[i].Yaxis - Loop[i + Convert.ToInt32(CarVector.ActualFrontToBackWheel_Meter / MeterPerPoint)].Yaxis;
                    }
                    else
                    {
                        CarVector.X = Loop[i].Xaxis - Loop[i - Convert.ToInt32(CarVector.ActualFrontToBackWheel_Meter / MeterPerPoint)].Xaxis;
                        CarVector.Y = Loop[i].Yaxis - Loop[i - Convert.ToInt32(CarVector.ActualFrontToBackWheel_Meter / MeterPerPoint)].Yaxis;
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

                    PathVector.X = Loop[i + Convert.ToInt32(Vector.ActualFrontPath_Meter / MeterPerPoint)].Xaxis - Loop[i].Xaxis;//12 points = 0.25 meter
                    PathVector.Y = Loop[i + Convert.ToInt32(Vector.ActualFrontPath_Meter / MeterPerPoint)].Yaxis - Loop[i].Yaxis;//12 points = 0.25 meter
                    PathVector.Uniteze();
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

                if (Deg > Vector.MinimumCurveAngle)//Minimum Curve angle
                {
                    if (!start)
                    {
                        startPoint.X = Loop[i].Xaxis;
                        startPoint.Y = Loop[i].Yaxis;
                        startPathPoint = i;
                        start = true;
                        stop = false;
                    }
                }
                else
                {
                    if (start)
                    {
                        endPoint.X = Loop[i].Xaxis;
                        endPoint.Y = Loop[i].Yaxis;
                        endPathPoint = i;
                        start = false;
                        stop = false;
                        count = endPathPoint - startPathPoint;

                        curve = new Curve(startPathPoint, endPathPoint, startPoint, endPoint, CalculateRadius(Loop, startPathPoint, endPathPoint));
                        if (curve.Radius < Vector.MaximumActualRedius_Meter && count > Vector.MinLengthPointCurve)
                        {
                            curves.Add(curve);
                        }
                    }
                }
            }


            string str = "";
            str = "Start(Point_Number),End(Point_Number),Curve_Length(Meter),Radius(Meter)" + "\n";
            foreach (Curve item in curves)
            {
                str += item.StartPathPoint.ToString() + "," + item.EndPathPoint.ToString() + "," + ((item.EndPathPoint - item.StartPathPoint) * MeterPerPoint).ToString() + "," + item.Radius.ToString() + "\n";
            }
            System.IO.File.WriteAllText("Curves.csv", str);
            return curves;
        }
        public static double CalculateRadius(List<LineTracking> loop, int start, int end)
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


            return ConvertPointLength2DistanceMeter(radius, loop);
        }
        private static void FindIntersection(PointF p1, PointF p2, PointF p3, PointF p4, out bool lines_intersect, out bool segments_intersect, out PointF intersection, out PointF close_p1, out PointF close_p2)
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
        private static double ConvertPointLength2DistanceMeter(double rawlength, List<LineTracking> loop)
        {
            double pointDistance;
            Vector temp = new Vector();
            temp.X = loop[100].Xaxis - loop[0].Xaxis;
            temp.Y = loop[100].Yaxis - loop[0].Yaxis;
            pointDistance = temp.Length;
            pointDistance /= 100;
            double MeterPerPointLength = MeterPerPoint / pointDistance;
            return (rawlength * MeterPerPointLength);

        }

        public static void UpdatePath(List<LineTracking> path, string FileName)//to save smooth path to csv file
        {
            string data = null;
            for (int i = 0; i < path.Count; i++)
            {
                data += path[i].Xaxis.ToString() + "," + path[i].Yaxis.ToString() + "\n";
            }
            System.IO.File.WriteAllText(FileName, data);
        }

        public static void InserList()
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
            Vector.UpdatePath(points, "coordinates_v2_2.csv");
            #endregion

            Thread.Sleep(1000);
        }
        #endregion

        #endregion
    }
}

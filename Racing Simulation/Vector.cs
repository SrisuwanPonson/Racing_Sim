using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing_Simulation
{
    public class Vector
    {
        #region Property
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Length
        {
            get
            {
                return GetLenth();
            }
        }
        #endregion

        #region Constructors
        public Vector()
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
        }
        public Vector(double x,double y,double z )
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vector(Vector other)
        {
            this.X = other.X;
            this.Y = other.Y;
            this.Z = other.Z;
        }
        #endregion

        #region Override
        public override string ToString()
        {
            return $"{Math.Round(this.X,2)},{Math.Round(this.Y,2)}";

        }
        #endregion

        #region Method
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
            if(len<=0)
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
        public static Vector Addition(Vector a,Vector b)
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
        public static double GetAngle(Vector a,Vector b)
        {
            //double dotProduct = DotProduct(a, b);
            //double CosTheta = dotProduct / (a.Length * b.Length);
            return  Math.Acos(DotProduct(a, b) / (a.Length * b.Length));
        }
        public static double ConvertRadiansToDegrees(double radians)
        {
            double degrees = (180 / Math.PI) * radians;
            return (degrees);
        }

        public void SetVelocity(double CurrentVel,double TagetVel,double Distance)
        {
            //v   = u + at
            //v^2 = u^2 + 2*a*s
        }
        public double GetVelocity(double MeterPerPoint,double InternalDelay,double TimerInterval)
        {
            double Velocity;// Km/h
            Velocity = MeterPerPoint / (InternalDelay + TimerInterval); //Meter/mSec
            Velocity = Velocity * 1000;//Meter/Sec
            Velocity = Velocity / 1000;//Km/Sec
            Velocity = Velocity * 3600;//Km/h
            return Velocity;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Racing_Simulation
{
    public class LineTracking
    {
        public float Xaxis;
        public float Yaxis;
        public static LineTracking FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            LineTracking lineTracking = new LineTracking();
            lineTracking.Xaxis = float.Parse(values[0]);
            lineTracking.Yaxis = float.Parse(values[1]);
            return lineTracking;
        }

    }
    public struct Coords
    {
        public Coords(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public override string ToString() => $"({X}, {Y})";
    }
    public struct Curve
    {
        public Curve(int startPathPoint,int endPathPoint,Coords start, Coords end,double radius)
        {
            Start = start;
            End = end;
            Radius = radius;
            StartPathPoint = startPathPoint;
            EndPathPoint = endPathPoint;
        }

        public Coords Start { get; set; }
        public Coords End { get; set; }
        public double Radius { get; set; }
        public int StartPathPoint { get; set; }
        public int EndPathPoint { get; set; }
        public override string ToString() => $"({Start}, {End})";
    }
    
}

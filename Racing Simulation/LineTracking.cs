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
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cbLibrary
{
    public class clsDoublePoint
    {
        double xAxis;
        double yAxis;

        public clsDoublePoint(double x, double y)
        {
            XAxis = x;
            YAxis = y;
        }

        public double XAxis { get => xAxis; set => xAxis = value; }
        public double YAxis { get => yAxis; set => yAxis = value; }
    }
}


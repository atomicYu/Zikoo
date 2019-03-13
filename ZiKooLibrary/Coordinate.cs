using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZiKooLibrary
{
    public class Coordinate : ICoordinate
    {
        public string PointNumber { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Coordinate()
        {
        }

        public Coordinate(string number, double y, double x, double z)
        {
            PointNumber = number;
            Y = y;
            X = x;
            Z = z;
        }
    }
}

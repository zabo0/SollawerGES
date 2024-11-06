using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SollawerGES.Utils
{
    public class Vector2
    {
        private double x;
        private double y;
        //private double z;

        public double X
        {
            get { return x; }
            set { x = value; }
        }


        public double Y
        {
            get { return y; }
            set { y = value; }
        }


        //public double ZS
        //{
        //    get { return z; }
        //    set { z = value; }
        //}



        public Vector2(double x, double y)
        {
            this.X = x;
            this.Y = y;
            //this.Z = 0.0;
        }

        //public Vector2(double x, double y, double z) : this(x, y)
        //{
        //    this.Z = z;
        //}

        public Vector2(PointF point)
        {
            this.X = point.X;
            this.Y = point.Y;
        }
    }
}

using SollawerGES.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SollawerGES.Entities
{
    public class Point
    {
        private int id;
        private Vector2 position;
        private double thickness;


        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }


        public double Thickness
        {
            get { return thickness; }
            set { thickness = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Point(int id, Vector2 position)
        {
            this.Id = id;
            this.Position = position;
            this.Thickness = thickness;
        }
    }
}

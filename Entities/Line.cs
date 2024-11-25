using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SollawerGES.Utils;

namespace SollawerGES.Entities
{
    public class Line
    {
		private int id;

		private Vector2 startPosition;

		private Vector2 endPosition;

		private Vector2 centerPosition;

		private double lenght;

		private double thickness;

		private bool isSelected;


		public Line(int id, Vector2 startPosition, Vector2 endPosition)
		{
			this.ID = id;
			this.StartPosition = startPosition;
			this.EndPosition = endPosition;
			this.Thickness = 0.0;
		}


		public bool IsSelected
		{
			get { return isSelected; }
			set { isSelected = value; }
		}


		public double Thickness
		{
			get { return thickness; }
			set { thickness = value; }
		}


		public Vector2 CenterPosition	
		{
			get { return new Vector2((startPosition.X + endPosition.X) / 2, (startPosition.Y + endPosition.Y) / 2); }
		}

        public double Length
        {
			get
			{
                double dx = endPosition.X - startPosition.X;
                double dy = endPosition.Y - startPosition.Y;
                return (double)Math.Sqrt(dx * dx + dy * dy);
            }
        }

        public Vector2 EndPosition
		{
			get { return endPosition; }
			set { endPosition = value; }
		}


		public Vector2 StartPosition
		{
			get { return startPosition; }
			set { startPosition = value; }
		}



		public int ID
		{
			get { return id; }
			set { id = value; }
		}

	}
}

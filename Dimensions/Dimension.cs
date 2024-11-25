using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SollawerGES.Components;
using SollawerGES.Dimensions;
using SollawerGES.Utils;

namespace SollawerGES.Dimensions
{
    public class Dimension
    {

        private Vector2 firstPoint;

        private Vector2 secondPoint;

        private Vector2 dimPosition;

        private Vector2 centerPosition;

        private double lenght;

        private List<Entities.Line> shapeLinesList;


        public Dimension(Vector2 firstPoint, Vector2 secondPoint, Vector2 dimPosition)
        {
            this.FirstPoint = firstPoint;
            this.SecondPoint = secondPoint;
            this.DimPosition = dimPosition;


            this.Lenght = DimensionManager.getDistance(firstPoint, secondPoint);
            this.ShapeLinesList = DimensionManager.createShape(firstPoint, secondPoint, dimPosition);
        }


        public double Lenght
        {
            get { return lenght; }
            set { lenght = value; }
        }

        public List<Entities.Line> ShapeLinesList
        {
            get { return shapeLinesList; }
            set { shapeLinesList = value; }
        }

        public Vector2 DimPosition
        {
            get { return dimPosition; }
            set { dimPosition = value; }
        }

        public Vector2 FirstPoint
        {
            get { return firstPoint; }
            set { firstPoint = value; }
        }

        public Vector2 SecondPoint
        {
            get { return secondPoint; }
            set { secondPoint = value; }
        }

        public Vector2 CenterPosition
        {
            get
            {
                return new Vector2((firstPoint.X + secondPoint.X) / 2, (firstPoint.Y + secondPoint.Y) / 2);
            }
        }
    }
}

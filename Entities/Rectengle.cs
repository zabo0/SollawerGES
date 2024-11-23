using SollawerGES.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SollawerGES.Entities
{
    public class Rectengle
    {
        private int id;

        private double width;
        private double height;

        private Vector2 centerPosition;
        private Vector2 startPos;
        private Vector2 endPos;
        private Vector2 topPos;
        private Vector2 bottomPos;

        private double thickness;

        private bool isSelected = false;

        public Rectengle(int id, double width, double height, Vector2 cPos)
        {
            this.ID = id;
            this.Width = width;
            this.Height = height;
            this.CenterPosition = cPos;
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
            get { return centerPosition; }
            set { centerPosition = value; }
        }

        public Vector2 StartPos
        {
            get
            {
                return new Vector2(centerPosition.X - width / 2, centerPosition.Y);
            }
        }
        public Vector2 EndPos
        {
            get
            {
                return new Vector2(centerPosition.X + width / 2, centerPosition.Y);
            }
        }

        public double Height
        {
            get { return height; }
            set { height = value; }
        }


        public double Width
        {
            get { return width; }
            set { width = value; }
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public Vector2 BottomPos
        {
            get { return new Vector2(centerPosition.X, centerPosition.Y - height/2); }
        }


        public Vector2 TopPos
        {
            get { return new Vector2(centerPosition.X, centerPosition.Y + height / 2); }
        }
    }
}

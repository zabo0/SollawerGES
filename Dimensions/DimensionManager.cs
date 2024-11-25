using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SollawerGES.Utils;

namespace SollawerGES.Dimensions
{
    public static class DimensionManager
    {

        public static void addDimension(Vector2 firstPoint, Vector2 secondPoint, Vector2 dimPosition = null)
        {
            if (dimPosition == null)
                dimPosition = calculateDefaultDimPosition(firstPoint, secondPoint, 1500);

            Dimension dimension = new Dimension(firstPoint, secondPoint, dimPosition);
            Components.Lists.Dimensions.Add(dimension);
        }

        public static void addDimension(Entities.Line line)
        {

        }

        public static List<Entities.Line> createShape(Vector2 firstPoint, Vector2 secondPoint, Vector2 dimPosition)
        {
            Entities.Line lineFirst = new Entities.Line(0, new Vector2(firstPoint.X, firstPoint.Y + 200), new Vector2(firstPoint.X, dimPosition.Y + 200));
            Entities.Line lineSecond = new Entities.Line(0, new Vector2(secondPoint.X, secondPoint.Y + 200), new Vector2(secondPoint.X, dimPosition.Y + 200));
            Entities.Line line3 = new Entities.Line(1, new Vector2(firstPoint.X + 200, dimPosition.Y), dimPosition);
            Entities.Line line4 = new Entities.Line(0, dimPosition, new Vector2(secondPoint.X - 200, dimPosition.Y));

            /*
			 * 
			 *									
			 *					                           dimPosition  
			 *					  |	     line3	                	               line4      |					
			 * 					  |-----------------------------.-----------------------------|
			 * 					  |				                			                  |
			 * 					  |				                			                  |
			 * 			lineFirst |				                			                  | lineSecond
			 *					  |				                			                  |
			 *					  |				                			                  |
			 * 					  |				                			                  |
			 * 					  |				                			                  |
			 * 	  	   firstPoint .                                                           . SecondPoint										                  
			 * 
			 * 
			 * 
			 * 
			 * 
			 */

            List<Entities.Line> lines = new List<Entities.Line>();
            lines.Add(lineFirst);
            lines.Add(lineSecond);
            lines.Add(line3);
            lines.Add(line4);

            return lines;
        }

        public static void clearAllDimensions()
        {
            Components.Lists.Dimensions.Clear();
        }

        public static Vector2 calculateDefaultDimPosition(Vector2 firstPoint, Vector2 secondPoint, double distance)
        {
            Vector2 centerPosition = new Vector2((firstPoint.X + secondPoint.X) / 2, (firstPoint.Y + secondPoint.Y) / 2);
            return new Vector2(centerPosition.X, centerPosition.Y + distance);
        }

        public static double getDistance(Vector2 firstPoint, Vector2 secondPoint)
        {
            double dx = secondPoint.X - firstPoint.X;
            double dy = secondPoint.Y - firstPoint.Y;
            return (double)Math.Sqrt(dx * dx + dy * dy);
        }

    }
}

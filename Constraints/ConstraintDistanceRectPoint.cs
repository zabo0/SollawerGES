using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SollawerGES.Constraints
{
    internal class ConstraintDistanceRectPoint : Constraint
    {

        private readonly Entities.Rectengle component1;
        private readonly Entities.Point component2;
        private readonly double distance;


        public ConstraintDistanceRectPoint(Entities.Rectengle component1,  Entities.Point component2, double distance)
        {
            this.component1 = component1;
            this.component2 = component2;
            this.distance = distance;
        }

        public override void Apply()
        {
            double currentDistance = component1.CenterPosition.X - component2.Position.X;

            double diff = currentDistance - distance;

            if (Math.Abs(diff) > 0.1f)
            {
                double rateCorrection = diff / currentDistance / 2;
                currentDistance *= rateCorrection;

                // Birinci nesneyi kaydır
                if (component1 is Entities.Rectengle n1)
                {
                    n1.CenterPosition.X -= currentDistance;
                }

                // İkinci nesneyi kaydır
                if (component2 is Entities.Point n2)
                {
                    n2.Position.X += currentDistance;
                }
            }
        }
    }
}

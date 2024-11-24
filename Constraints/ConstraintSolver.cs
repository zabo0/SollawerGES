using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SollawerGES.Constraints
{
    public class ConstraintSolver
    {
        private readonly List<Constraint> constraints = new List<Constraint>();

        public void addConstraint(Constraint constraint)
        {
            constraints.Add(constraint);
        }

        public void removeConstraint(Constraint constraint)
        {
            constraints.Remove(constraint);
        }

        public void solve()
        {
            foreach (var constraint in constraints)
            {
                constraint.Apply();
            }
        }
    }
}

using SollawerGES.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SollawerGES.Components
{
    public static class Origins
    {
        private static Entities.Point baseOrigin_primary;
        private static Entities.Point baseOrigin_secondary;

        private static Entities.Point mainOrigin;
        private static Entities.Point smallOrigin;



        public static Entities.Point scrollOrigin(this Entities.Point o, double value)
        {
            o.Position.X += value;
            return o;
        }


        public static Entities.Point SmallOrigin
        {
            get { return smallOrigin; }
            set { smallOrigin = value; }
        }


        public static Entities.Point MainOrigin
        {
            get { return mainOrigin; }
            set { mainOrigin = value; }
        }


        public static Entities.Point BaseOrigin_Primary
        {
            get { return baseOrigin_primary; }
            set { baseOrigin_primary = value; }
        }

        public static Entities.Point BaseOrigin_Secondary
        {
            get { return baseOrigin_secondary; }
            set { baseOrigin_secondary = value; }
        }
    }
}

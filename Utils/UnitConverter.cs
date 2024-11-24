using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SollawerGES.Utils
{
    public static class UnitConverter
    {
        private static double deviceDPI = 96;
        private static double realDPI = 158;
        private static double pdfDPI = 72;


        private static double primaryScale = 1;
        private static double secondaryScale = 1;

        public static double toMM(this double px, double scale) //pixel to millimeter
        {
            double mm = px * 25.4f / realDPI;
            double a = px == 0 ? 0 : mm * scale; 
            return a;
        }

        public static int toPX(this double mm, double scale) //millimeter to pixel
        {
            double px = (mm * realDPI) / 25.4f;
            int a = mm == 0 ? 0 : (int)(px / scale);
            return a;
        }

        public static double toPT(this double mm, double scale) //millimeter to point
        {
            double pt = pdfDPI / 25.4f * mm;
            double a = pt == 0 ? 0 : mm / scale;
            return a;
        }

        public static double round(this double mm, double step)
        {
            double remainder = mm % step;

            if(mm > 0)
            {
                if (remainder >= step / 2)
                {
                    return mm - remainder + step;
                }

                return mm - remainder;
            }

            if (mm < 0)
            {
                if (remainder <= -step / 2)
                {
                    return mm - remainder - step;
                }

                return mm - remainder;
            }

            return 0;
        }
        


        public static double PrimaryScale
        {
            get { return primaryScale; }
            set { primaryScale = value >= 0.1 ? value : 0.1; }
        }

        public static double SecondaryScale
        {
            get { return secondaryScale; }
            set { secondaryScale = value >= 0.1 ? value : 0.1; }
        }


        public static double RealDPI
        {
            get { return realDPI; }
            set { realDPI = value; }
        }

        public static double DeviceDPI
        {
            get { return deviceDPI; }
            set { deviceDPI = value; }
        }

        public static double PdfDPI
        {
            get { return pdfDPI = 72; }
            set { pdfDPI = value; }
        }
    }
}

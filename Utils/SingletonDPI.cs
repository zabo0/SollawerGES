using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SollawerGES.Utils
{
    public class SingletonDPI
    {
        private SingletonDPI()
        {
            realDPI = 158;
            deviceDPI = 0;
        }

        public static SingletonDPI _instance;

        private double realDPI;
        private double deviceDPI;


        public static SingletonDPI getInstance()
        {
            if (_instance == null)
            {
                _instance = new SingletonDPI();
            }
            return _instance;
        }


        public double DeviceDPI
        {
            get { return deviceDPI; }
            set { deviceDPI = value; }
        }


        public double RealDPI
        {
            get { return realDPI; }
            set { realDPI = value; }
        }

    }
}

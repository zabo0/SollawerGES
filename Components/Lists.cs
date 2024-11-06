using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SollawerGES.Components
{
    public static class Lists
    {
        private static List<Entities.Point> points;
        private static List<Entities.Rectengle> positionBoxes;
        private static List<Entities.Rectengle> panels;
        private static List<Entities.Rectengle> asiksZ;
        private static List<Entities.Rectengle> asiksW;
        private static List<Entities.Rectengle> profiles;
        private static List<Entities.Rectengle> aksBirls;
        private static List<Entities.Rectengle> mafsals;
        private static List<Entities.Rectengle> centerDireks;
        private static List<Entities.Rectengle> sideDireks;


        public static void initializeComponents()
        {
            points = new List<Entities.Point>();
            positionBoxes = new List<Entities.Rectengle>();
            panels = new List<Entities.Rectengle>();
            asiksZ = new List<Entities.Rectengle>();
            asiksW = new List<Entities.Rectengle>();
            profiles = new List<Entities.Rectengle>();
            aksBirls = new List<Entities.Rectengle>();
            mafsals = new List<Entities.Rectengle>();
            centerDireks = new List<Entities.Rectengle>();
            sideDireks = new List<Entities.Rectengle>();
        }

        public static List<List<Entities.Rectengle>> getComponentList()
        {
            List<List<Entities.Rectengle>> componentList = new List<List<Entities.Rectengle>>();
            componentList.Add(panels);
            componentList.Add(asiksZ);
            componentList.Add(asiksW);
            componentList.Add(profiles);
            componentList.Add(aksBirls);
            componentList.Add(mafsals);
            componentList.Add(centerDireks);
            componentList.Add(sideDireks);

            return componentList;
        }

        public static List<List<Entities.Rectengle>> getComponentList(List<bool> checkList)
        {
            List<List<Entities.Rectengle>> componentListWithChecked = new List<List<Entities.Rectengle>>();

            if (checkList[0])
                componentListWithChecked.Add(panels);
            if (checkList[1])
                componentListWithChecked.Add(asiksZ);
            if (checkList[2])
                componentListWithChecked.Add(asiksW);
            if (checkList[3])
                componentListWithChecked.Add(profiles);
            if (checkList[4])
                componentListWithChecked.Add(aksBirls);
            if (checkList[5])
                componentListWithChecked.Add(mafsals);
            if (checkList[6])
                componentListWithChecked.Add(centerDireks);
            if (checkList[7])
                componentListWithChecked.Add(sideDireks);

            return componentListWithChecked;
        }




        public static List<Entities.Point> Points
        {
            get { return points; }
            set { points = value; }
        }

        public static List<Entities.Rectengle> PositionBoxes
        {
            get { return positionBoxes; }
            set { positionBoxes = value; }
        }

        public static List<Entities.Rectengle> Panels
        {
            get { return panels; }
            set { panels = value; }
        }

        public static List<Entities.Rectengle> AsiksZ
        {
            get { return asiksZ; }
            set { asiksZ = value; }
        }

        public static List<Entities.Rectengle> AsiksW
        {
            get { return asiksW; }
            set { asiksW = value; }
        }

        public static List<Entities.Rectengle> Profiles
        {
            get { return profiles; }
            set { profiles = value; }
        }

        public static List<Entities.Rectengle> AksBirls
        {
            get { return aksBirls; }
            set { aksBirls = value; }
        }

        public static List<Entities.Rectengle> Mafsals
        {
            get { return mafsals; }
            set { mafsals = value; }
        }

        public static List<Entities.Rectengle> CenterDireks
        {
            get { return centerDireks; }
            set { centerDireks = value; }
        }

        public static List<Entities.Rectengle> SideDireks
        {
            get { return sideDireks; }
            set { sideDireks = value; }
        }
    }
}

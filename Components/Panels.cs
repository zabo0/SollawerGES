using SollawerGES.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SollawerGES.Components
{
    public static class Panels
    {
        private static double width = 0;
        private static double height = 0;

        private static int count = 0;

        private static double space = 0;


        public static Entities.Rectengle createCenterPanel()
        {
            int id = 0;
            Vector2 cPos = new Vector2(0, 0);
            return new Entities.Rectengle(id, width, height, cPos);
        }

        public static Entities.Rectengle addPanelNextTo(Entities.Rectengle panel, string where = "right")
        {
            if(panel.ID == 0)
            {
                if(where.Equals("right"))
                {
                    int id = panel.ID + 1;
                    Vector2 cPos = new Vector2(panel.CenterPosition.X + width + space, panel.CenterPosition.Y);
                    return new Entities.Rectengle(id, width, height, cPos);
                }
                else if (where.Equals("left"))
                {
                    int id = panel.ID - 1;
                    Vector2 cPos = new Vector2(panel.CenterPosition.X - width - space, panel.CenterPosition.Y);
                    return new Entities.Rectengle(id, width, height, cPos);
                }
            }
            else if (panel.ID > 0)
            {
                int id = panel.ID + 1;
                Vector2 cPos = new Vector2(panel.CenterPosition.X + width + space, panel.CenterPosition.Y);
                return new Entities.Rectengle(id, width, height, cPos);
            }
            else if (panel.ID < 0)
            {
                int id = panel.ID - 1;
                Vector2 cPos = new Vector2(panel.CenterPosition.X - width - space, panel.CenterPosition.Y);
                return new Entities.Rectengle(id, width, height, cPos);
            }
            return null;
        }


        public static List<Entities.Rectengle> createAllPanel()
        {
            List<Entities.Rectengle> list = new List<Entities.Rectengle>();

            list.Add(createCenterPanel());
            list.Add(addPanelNextTo(list[0], "right"));
            list.Add(addPanelNextTo(list[0], "left"));

            for(int i = 0; i < count - 3; i++)
            {
                if(i%2 == 0)
                {
                    list.Add(addPanelNextTo(list.OrderBy(d => d.ID).Last()));
                }
                else
                {
                    list.Add(addPanelNextTo(list.OrderBy(d => d.ID).First()));
                }
            }
            return list;
        }

        public static double Height
        {
            get { return height; }
            set { height = value; }
        }

        public static double Width
        {
            get { return width; }
            set { width = value; }
        }

        public static double Space
        {
            get { return space; }
            set { space = value; }
        }
        public static int Count
        {
            get { return count; }
            set { count = value; }
        }
    }
}

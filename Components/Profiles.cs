using SollawerGES.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SollawerGES.Components
{
    public static class Profiles
    {
		private static double height;

		private static double space;


        public static List<Entities.Rectengle> createAllProfiles(List<Entities.Rectengle> asikList_W, List<Entities.Rectengle> asikList_Z)
        {
            List<Entities.Rectengle> list = new List<Entities.Rectengle>();

            Entities.Rectengle centerProfile = createCenterProfile(6000);
            centerProfile = conflictControl(centerProfile, asikList_W);
            list.Add(centerProfile);

            for (int i = 0; i < 50; i++)
            {
                Entities.Rectengle newProfile = addProfileNextTo(list.First(d => d.ID == i), 6000, "right");
                //newProfile = conflictControl(newProfile, asikList_W);
                list.Add(newProfile);
                if (newProfile.EndPos.X > asikList_Z.First(d => d.ID == 1).EndPos.X + 300)
                {
                    double width = newProfile.EndPos.X - (asikList_Z.First(d => d.ID == 1).EndPos.X + 300);
                    newProfile.Width -= width;
                    newProfile.CenterPosition.X -= width/2;
                    break;
                }
            }

            //for (int i = 0; i<50; i++)
            //{
            //    Entities.Rectengle newProfile = addProfileNextTo(list.First(d => d.ID == i), 6000, "right");
            //    newProfile = conflictControl(newProfile, asikList_W);
            //    list.Add(newProfile);
            //    if (newProfile.EndPos.X > asikList_Z.First(d=>d.ID == 1).EndPos.X + 100)
            //    {
            //        break;
            //    }
            //}
            //for (int i = 0; i > -50; i--)
            //{
            //    Entities.Rectengle newProfile = addProfileNextTo(list.First(d => d.ID == i), 6000, "left");
            //    newProfile = conflictControl(newProfile, asikList_W);
            //    list.Add(newProfile);
            //    if (newProfile.StartPos.X < asikList_Z.First(d => d.ID == -1).StartPos.X - 100)
            //    {
            //        break;
            //    }
            //}

            return list;
        }

        public static Entities.Rectengle createCenterProfile(double length)
        {
            int id = 0;
            return new Entities.Rectengle(id, length, height, new Vector2(0,0));
        }


        public static Entities.Rectengle conflictControl(Entities.Rectengle profile, List<Entities.Rectengle> asikList)
        {
            int tol = 320;

            if(profile.ID == 0)
            {
                foreach (Entities.Rectengle asikW in asikList)
                {
                    while (Math.Abs(profile.EndPos.X - asikW.CenterPosition.X) < tol)
                    {
                        profile.CenterPosition.X = profile.CenterPosition.X + 50;
                    }

                    while (Math.Abs(profile.StartPos.X - asikW.CenterPosition.X) < tol)
                    {
                        profile.CenterPosition.X = profile.CenterPosition.X + 50;
                    }
                }
            }

            if (profile.ID > 0)
            {
                for(int i = 1; i < asikList.Count/2+1; i++)
                {
                    while (Math.Abs(profile.EndPos.X - asikList.First(d => d.ID == i).CenterPosition.X) < tol)
                    {
                        profile.Width = profile.Width -= 10;
                        profile.CenterPosition.X -= 5;
                    }
                }
            }

            else if (profile.ID < 0)
            {
                for (int i = -1; i > -asikList.Count/2-1; i--)
                {
                    while (Math.Abs(profile.StartPos.X - asikList.First(d => d.ID == i).CenterPosition.X) < tol)
                    {
                        profile.Width = profile.Width -= 10;
                        profile.CenterPosition.X += 5;
                    }
                }
            }
            return profile;
        }


        public static Entities.Rectengle addProfileNextTo(Entities.Rectengle profile, double lenght, string where)
        {
            if (where.Equals("right"))
            {
                Vector2 cPos = new Vector2(profile.EndPos.X + lenght/2 + space, profile.CenterPosition.Y);
                return new Entities.Rectengle(profile.ID + 1, lenght, height, cPos);
            }
            else if (where.Equals("left"))
            {
                Vector2 cPos = new Vector2(profile.StartPos.X - lenght/2 - space, profile.CenterPosition.Y);
                return new Entities.Rectengle(profile.ID - 1, lenght, height, cPos);
            }
            return null;
        }

        public static double Space
		{
			get { return space; }
			set { space = value; }
		}


		public static double Height
		{
			get { return height; }
			set { height = value; }
		}

	}
}

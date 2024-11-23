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

            double lenght = 6000;

            devam:

            for (int i = 0; i < 50; i++)
            {
                Entities.Rectengle newProfile = addProfileNextTo(list.First(d => d.ID == i), lenght, "right");
                list.Add(newProfile);
                if (newProfile.EndPos.X > asikList_Z.First(d => d.ID == 1).EndPos.X + 300)
                {
                    double width = newProfile.EndPos.X - (asikList_Z.First(d => d.ID == 1).EndPos.X + 300);
                    newProfile.Width -= width;
                    newProfile.CenterPosition.X -= width / 2;
                    break;
                }
            }
            lenght -= 10;

            if (conflictControl())
            {
                goto devam;
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
            //{x
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

        public static List<Entities.Rectengle> createCenterProfile()
        {
            List<Entities.Rectengle> list = new List<Entities.Rectengle>();
            int id = 0;
            Entities.Rectengle cProfile = new Entities.Rectengle(id, 6000, height, new Vector2(0,0));
            list.Add(cProfile);
            return list;
        }

        public static bool conflictControl(double posX)
        {
            int tol = 320;

            foreach (Entities.Rectengle asikW in Lists.AsiksW)
            {
                double a = Math.Abs(posX - asikW.CenterPosition.X);
                if (a < tol)
                {
                    return true;
                }
            }
            return false;
        }


        public static bool conflictControl()
        {
            int tol = 320;

            foreach (Entities.Rectengle asikW in Lists.AsiksW)
            {
                if(asikW.ID > 0)
                {
                    foreach(Entities.Rectengle profile in Lists.Profiles)
                    {
                        if(profile.ID > 0)
                        {
                            double a = Math.Abs(profile.EndPos.X - asikW.CenterPosition.X);
                            if (a < tol)
                            {
                                return true;
                            }
                        }
                    }
                }
                if (asikW.ID < 0)
                {
                    foreach (Entities.Rectengle profile in Lists.Profiles)
                    {
                        if (profile.ID < 0)
                        {
                            double a = Math.Abs(profile.EndPos.X - asikW.CenterPosition.X);
                            if (a < tol)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public static Entities.Rectengle conflictControl(Entities.Rectengle profile)
        {
            int tol = 320;

            if(profile.ID == 0)
            {
                foreach (Entities.Rectengle asikW in Components.Lists.AsiksW)
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
                for(int i = 1; i < Components.Lists.AsiksW.Count/2+1; i++)
                {
                    while (Math.Abs(profile.EndPos.X - Components.Lists.AsiksW.First(d => d.ID == i).CenterPosition.X) < tol)
                    {
                        profile.Width = profile.Width -= 10;
                        profile.CenterPosition.X -= 5;
                    }
                }
            }

            else if (profile.ID < 0)
            {
                for (int i = -1; i > -Components.Lists.AsiksW.Count/2-1; i--)
                {
                    while (Math.Abs(profile.StartPos.X - Components.Lists.AsiksW.First(d => d.ID == i).CenterPosition.X) < tol)
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
            if(profile.EndPos.X > (Components.Lists.AsiksZ.First(d => d.ID == 1).EndPos.X))
            {
                return null;
            }
            if(profile.StartPos.X < (Components.Lists.AsiksZ.First(d => d.ID == -1).StartPos.X))
            {
                return null;
            }

            if (where.Equals("right"))
            {
                Vector2 cPos = new Vector2(profile.EndPos.X + lenght/2 + space, profile.CenterPosition.Y);
                Entities.Rectengle nProfile = new Entities.Rectengle(profile.ID + 1, lenght, height, cPos);
                if (nProfile.EndPos.X > Components.Lists.AsiksZ.First(d => d.ID == 1).EndPos.X + 300)
                {
                    double width = nProfile.EndPos.X - (Components.Lists.AsiksZ.First(d => d.ID == 1).EndPos.X + 300);
                    nProfile.Width -= width;
                    nProfile.CenterPosition.X -= width / 2;
                }
                return nProfile;
            }
            else if (where.Equals("left"))
            {
                Vector2 cPos = new Vector2(profile.StartPos.X - lenght/2 - space, profile.CenterPosition.Y);
                Entities.Rectengle nProfile = new Entities.Rectengle(profile.ID - 1, lenght, height, cPos);
                if (nProfile.EndPos.X > Components.Lists.AsiksZ.First(d => d.ID == 1).StartPos.X - 300)
                {
                    double width = nProfile.StartPos.X - (Components.Lists.AsiksZ.First(d => d.ID == -1).StartPos.X - 300);
                    nProfile.Width -= width;
                    nProfile.CenterPosition.X -= width / 2;
                }
                return nProfile;
            }
            return null;
        }


        public static void updateProfiles(Entities.Rectengle editedProfile)
        {
            if(Components.Lists.Profiles == null)
            {

            }
        }


        public static void moveProfiles(int editedComponent, double newLenght, double newCenterPosition)
        {

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

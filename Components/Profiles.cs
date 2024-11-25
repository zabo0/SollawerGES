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


        public static Entities.Rectengle addProfileNextTo(Entities.Rectengle profile, double lenght, string where)
        {
            if (where.Equals("right"))
            {
                Vector2 cPos = new Vector2(profile.EndPos.X + lenght/2 + space, profile.CenterPosition.Y);
                Entities.Rectengle nProfile = new Entities.Rectengle(profile.ID + 1, lenght, height, cPos);
                return nProfile;
            }
            else if (where.Equals("left"))
            {
                Vector2 cPos = new Vector2(profile.StartPos.X - lenght/2 - space, profile.CenterPosition.Y);
                Entities.Rectengle nProfile = new Entities.Rectengle(profile.ID - 1, lenght, height, cPos);
                return nProfile;
            }
            return null;
        }


        public static void updateProfiles()
        {
            foreach(Entities.Rectengle currentProfile in Components.Lists.Profiles.OrderBy(d => d.ID).ToList().FindAll(d=>d.ID > 0))
            {
                Entities.Rectengle previousProfile = Components.Lists.Profiles.Find(d=>d.ID == currentProfile.ID - 1);

                double distance = currentProfile.StartPos.X - previousProfile.EndPos.X;

                if(distance > Configurations.SpaceProfil)
                {
                    currentProfile.CenterPosition.X -= distance - Configurations.SpaceProfil;
                }
                else if(distance < Configurations.SpaceProfil)
                {
                    currentProfile.CenterPosition.X += -distance + Configurations.SpaceProfil;
                }
            }

            foreach (Entities.Rectengle currentProfile in Components.Lists.Profiles.OrderByDescending(d => d.ID).ToList().FindAll(d => d.ID < 0))
            {
                Entities.Rectengle previousProfile = Components.Lists.Profiles.Find(d => d.ID == currentProfile.ID + 1);

                double distance = currentProfile.EndPos.X - previousProfile.StartPos.X;

                if (distance > Configurations.SpaceProfil)
                {
                    currentProfile.CenterPosition.X -= distance + Configurations.SpaceProfil;
                }
                else if (distance < Configurations.SpaceProfil)
                {
                    currentProfile.CenterPosition.X += -distance - Configurations.SpaceProfil;
                }
            }

            while (Components.Lists.Profiles.OrderBy(d => d.EndPos.X).Last().EndPos.X < Components.Lists.AsiksZ.First(d => d.ID == 1).EndPos.X)
            {
                Components.Lists.Profiles.Add(addProfileNextTo(Components.Lists.Profiles.OrderBy(d => d.EndPos.X).Last(), 6000, "right"));
            }

            while (Components.Lists.Profiles.OrderBy(d => d.StartPos.X).Last().StartPos.X > Components.Lists.AsiksZ.First(d => d.ID == 1).EndPos.X)
            {
                Components.Lists.Profiles.Remove(Components.Lists.Profiles.OrderBy(d => d.StartPos.X).Last());
            }

            while (Components.Lists.Profiles.OrderBy(d => d.StartPos.X).First().StartPos.X > Components.Lists.AsiksZ.First(d => d.ID == -1).StartPos.X)
            {
                Components.Lists.Profiles.Add(addProfileNextTo(Components.Lists.Profiles.OrderBy(d => d.StartPos.X).First(), 6000, "left"));
            }

            while (Components.Lists.Profiles.OrderBy(d => d.EndPos.X).First().EndPos.X < Components.Lists.AsiksZ.First(d => d.ID == -1).StartPos.X)
            {
                Components.Lists.Profiles.Remove(Components.Lists.Profiles.OrderBy(d => d.EndPos.X).First());
            }

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

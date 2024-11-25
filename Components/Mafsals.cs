using SollawerGES.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SollawerGES.Components
{
    public static class Mafsals
    {
		private static double width;
		private static double height;

		private static double minSpace;
		private static double maxSpace;

		private static double actuatorDistance;
		

		public static List<Entities.Rectengle> createCenterMafsals()
		{
			List<Entities.Rectengle> list = new List<Entities.Rectengle>();

			int id1 = -1;
			Vector2 cPos1 = new Vector2 (-actuatorDistance/2, 0);
			Entities.Rectengle mafsal1 = new Entities.Rectengle(id1, width, height, cPos1);
			list.Add(mafsal1);
			
			int id2 = 1;
            Vector2 cPos2 = new Vector2(actuatorDistance / 2, 0);
            Entities.Rectengle mafsal2 = new Entities.Rectengle(id2, width, height, cPos2);
            list.Add(mafsal2);

			return list;
        }

		public static void updateMafsals()
		{
            foreach (Entities.Rectengle currentMafsal in Components.Lists.Mafsals.OrderBy(d => d.ID).ToList().FindAll(d => d.ID > 1))
            {
                Entities.Rectengle previousMafsal = Components.Lists.Mafsals.Find(d => d.ID == currentMafsal.ID - 1);

                double space = currentMafsal.CenterPosition.X - previousMafsal.CenterPosition.X;

                if (space > Configurations.MaxMafsalSpace)
                {
                    currentMafsal.CenterPosition.X -= space - Configurations.MaxMafsalSpace;
                }
                else if (space < Configurations.MinMafsalSpace)
                {
                    currentMafsal.CenterPosition.X += -space + Configurations.MinMafsalSpace;
                }
            }

            foreach (Entities.Rectengle currentMafsal in Components.Lists.Mafsals.OrderByDescending(d => d.ID).ToList().FindAll(d => d.ID < -1))
            {
                Entities.Rectengle previousMafsal = Components.Lists.Mafsals.Find(d => d.ID == currentMafsal.ID + 1);

                double space = previousMafsal.CenterPosition.X - currentMafsal.CenterPosition.X;

                if (space > Configurations.MaxMafsalSpace)
                {
                    currentMafsal.CenterPosition.X += space - Configurations.MaxMafsalSpace;
                }
                else if (space < Configurations.MinMafsalSpace)
                {
                    currentMafsal.CenterPosition.X -= -space + Configurations.MinMafsalSpace;
                }
            }

            while (Components.Lists.Mafsals.OrderBy(d => d.EndPos.X).Last().EndPos.X < Components.Lists.AsiksZ.First(d => d.ID == 1).EndPos.X)
            {
                Components.Lists.Mafsals.Add(addMafsalNextTo(Components.Lists.Mafsals.OrderBy(d => d.EndPos.X).Last(), (Configurations.MaxMafsalSpace + Configurations.MinMafsalSpace) / 2 , "right"));
            }

            while (Components.Lists.Mafsals.OrderBy(d => d.StartPos.X).Last().StartPos.X > Components.Lists.AsiksZ.First(d => d.ID == 1).EndPos.X)
            {
                Components.Lists.Mafsals.Remove(Components.Lists.Mafsals.OrderBy(d => d.StartPos.X).Last());
            }

            while (Components.Lists.Mafsals.OrderBy(d => d.StartPos.X).First().StartPos.X > Components.Lists.AsiksZ.First(d => d.ID == -1).StartPos.X)
            {
                Components.Lists.Mafsals.Add(addMafsalNextTo(Components.Lists.Mafsals.OrderBy(d => d.StartPos.X).First(), (Configurations.MaxMafsalSpace + Configurations.MinMafsalSpace) / 2, "left"));
            }

            while (Components.Lists.Mafsals.OrderBy(d => d.EndPos.X).First().EndPos.X < Components.Lists.AsiksZ.First(d => d.ID == -1).StartPos.X)
            {
                Components.Lists.Mafsals.Remove(Components.Lists.Mafsals.OrderBy(d => d.EndPos.X).First());
            }
        }

		public static List<Entities.Rectengle> createAllMafsals()
		{
			List<Entities.Rectengle> list = new List<Entities.Rectengle> ();

			list = createCenterMafsals();

			for (int i = 1; i < 7; i++)
			{
				Entities.Rectengle newMafsal = addMafsalNextTo(list.First(d => d.ID == i), 4500, "right");
				list.Add(newMafsal);
			}

			for(int i = -1; i > -7; i--)
			{
                Entities.Rectengle newMafsal = addMafsalNextTo(list.First(d => d.ID == i), 4500, "left");
                list.Add(newMafsal);
            }

			return list;
		}

		public static Entities.Rectengle addMafsalNextTo(Entities.Rectengle mafsal, double space, string where)
		{
            if (where.Equals("right"))
            {
                Vector2 cPos = new Vector2(mafsal.CenterPosition.X + space, mafsal.CenterPosition.Y);
                return new Entities.Rectengle(mafsal.ID + 1, width, height, cPos);
            }
            else if (where.Equals("left"))
            {
                Vector2 cPos = new Vector2(mafsal.CenterPosition.X - space, mafsal.CenterPosition.Y);
                return new Entities.Rectengle(mafsal.ID - 1, width, height, cPos);
            }
            return null;
        }

        public static double ActuatorDistance
        {
            get { return actuatorDistance; }
            set { actuatorDistance = value; }
        }

        public static double MaxSpace
		{
			get { return maxSpace; }
			set { maxSpace = value; }
		}


		public static double MinSpace
		{
			get { return minSpace; }
			set { minSpace = value; }
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

	}
}

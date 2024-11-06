using SollawerGES.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SollawerGES.Components
{
    public static class Direks
    {
		private static double width;
		private static double height;



		public static List<Entities.Rectengle> createCenterDireks(List<Entities.Rectengle> mafsalList)
		{
            List<Entities.Rectengle> list = new List<Entities.Rectengle>();

			Entities.Rectengle mafsal1 = mafsalList.First(d => d.ID == -1);
            Vector2 cPos1 = new Vector2(mafsal1.CenterPosition.X + (mafsal1.Width - width) / 2, mafsal1.BottomPos.Y - height/2);
            Entities.Rectengle newMafsal1 = new Entities.Rectengle(mafsal1.ID, width, height, cPos1);
            list.Add(newMafsal1);

            Entities.Rectengle mafsal2 = mafsalList.First(d => d.ID == 1);
            Vector2 cPos2 = new Vector2(mafsal2.CenterPosition.X - (mafsal2.Width - width) / 2, mafsal2.BottomPos.Y - height/2);
            Entities.Rectengle newMafsal2 = new Entities.Rectengle(mafsal2.ID, width, height, cPos2);
            list.Add(newMafsal2);

			return list;
        }

		public static List<Entities.Rectengle> createSideDireks(List<Entities.Rectengle> mafsalList)
		{
			List<Entities.Rectengle> list = new List<Entities.Rectengle> ();

			foreach (Entities.Rectengle mafsal in mafsalList)
			{
				if(mafsal.ID > 1)
				{
					Vector2 cPos = new Vector2(mafsal.CenterPosition.X - (mafsal.Width - width) / 2, mafsal.BottomPos.Y - height / 2);
					Entities.Rectengle newMafsal = new Entities.Rectengle(mafsal.ID -1, width, height, cPos);
					list.Add(newMafsal);
				}
                if (mafsal.ID < -1)
                {
                    Vector2 cPos = new Vector2(mafsal.CenterPosition.X + (mafsal.Width - width) / 2, mafsal.BottomPos.Y - height / 2);
                    Entities.Rectengle newMafsal = new Entities.Rectengle(mafsal.ID + 1, width, height, cPos);
                    list.Add(newMafsal);
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

	}
}

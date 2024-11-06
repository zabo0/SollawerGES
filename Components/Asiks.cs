using SollawerGES.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SollawerGES.Components
{
    public static class Asiks
    {
		private static double width_w;
		private static double height_w;

		private static double width_z;
		private static double height_z;

		public static List<Entities.Rectengle> createAsiks_Z(Vector2 startPos, Vector2 endPos)
		{
			List<Entities.Rectengle> list = new List<Entities.Rectengle>();

			int id = -1;
			Vector2 cPos = new Vector2(startPos.X + width_z/2 + 3, startPos.Y);
			Entities.Rectengle asik = new Entities.Rectengle(id, width_z, height_z, cPos);
			list.Add(asik);

			int id1 = 1;
            Vector2 cPos1 = new Vector2(endPos.X - width_z / 2 - 3, endPos.Y);
            Entities.Rectengle asik1 = new Entities.Rectengle(id1, width_z, height_z, cPos1);
            list.Add(asik1);

            return list;
		}


		public static List<Entities.Rectengle> createAsiks_W(List<Entities.Rectengle> panelList)
		{
			List<Entities.Rectengle> list = new List<Entities.Rectengle>();

			foreach (Entities.Rectengle panel in panelList.OrderBy(d=>d.ID))
			{
				if(panel.ID < 0)
				{
					int id = panel.ID;
					Vector2 cPos = new Vector2(panel.EndPos.X + Panels.Space / 2, panel.EndPos.Y);
					Entities.Rectengle asik_w = new Entities.Rectengle(id, width_w, height_w, cPos);
					list.Add(asik_w);
				}
				else if(panel.ID >= 0)
				{
                    int id = panel.ID + 1;
                    Vector2 cPos = new Vector2(panel.EndPos.X + Panels.Space / 2, panel.EndPos.Y);
                    Entities.Rectengle asik_w = new Entities.Rectengle(id, width_w, height_w, cPos);
                    list.Add(asik_w);
                }
			}
			list.RemoveAt(list.Count - 1);

			return list;
		}




		public static double Height_Z
		{
			get { return height_z; }
			set { height_z = value; }
		}


		public static double Width_Z
		{
			get { return width_z; }
			set { width_z = value; }
		}


		public static double Height_W
		{
			get { return height_w; }
			set { height_w = value; }
		}


		public static double Width_W
		{
			get { return width_w; }
			set { width_w = value; }
		}


	}
}

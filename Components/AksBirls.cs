using SollawerGES.Utils;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SollawerGES.Components
{
    public static class AksBirls
    {
		private static double width;
		private static double height;


		public static List<Entities.Rectengle> createAksBirls(List<Entities.Rectengle> profileList)
		{
			List<Entities.Rectengle> list = new List<Entities.Rectengle>();

			foreach (Entities.Rectengle profile in profileList.OrderBy(d => d.ID))
			{
				int id = profile.ID < 0 ? profile.ID : profile.ID + 1;

				Vector2 cPos = new Vector2(profile.EndPos.X + Configurations.SpaceProfil/2, profile.EndPos.Y);
				Entities.Rectengle aksBirl = new Entities.Rectengle(id, Width, Height, cPos);
				list.Add(aksBirl);
			}
			list.Remove(list.Last());
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

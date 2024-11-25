using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SollawerGES.Utils;

namespace SollawerGES.Components
{
    public static class SelectionIndicators
    {

        private static double height = 200;
        private static double width = 200;


        public static void updateProfileIndicators(Entities.Rectengle profile)
        { 
            if(Components.Lists.SelectionIndicators.Count > 0)
            {
                foreach(Entities.Rectengle indicator in Components.Lists.SelectionIndicators)
                {
                    switch (indicator.ID)
                    {
                        case 0:
                            {
                                indicator.CenterPosition = profile.CenterPosition;
                                break;
                            }
                        case -1:
                            {
                                indicator.CenterPosition = profile.StartPos;
                                indicator.CenterPosition.X += width / 4;
                                break;
                            }
                        case 1:
                            {
                                indicator.CenterPosition = profile.EndPos;
                                indicator.CenterPosition.X -= width / 4;
                                break;
                            }
                    }
                }
            }
            else
            {
                int id = 0;
                Vector2 cPos = new Vector2(profile.CenterPosition.X, profile.CenterPosition.Y);
                Entities.Rectengle centerIndicator = new Entities.Rectengle(id, width, height, cPos);
                Components.Lists.SelectionIndicators.Add(centerIndicator);

                int id1 = -1;
                Vector2 sPos = new Vector2(profile.StartPos.X, profile.StartPos.Y);
                Entities.Rectengle startIndicator = new Entities.Rectengle(id1, width, height, sPos);
                startIndicator.CenterPosition.X += width / 4;
                Components.Lists.SelectionIndicators.Add(startIndicator);

                int id2 = 1;
                Vector2 ePos = new Vector2(profile.EndPos.X, profile.EndPos.Y);
                Entities.Rectengle endIndicator = new Entities.Rectengle(id2, width, height, ePos);
                endIndicator.CenterPosition.X -= width / 4;
                Components.Lists.SelectionIndicators.Add(endIndicator);
            }
        }

        public static void updateMafsalIndicators(Entities.Rectengle mafsal)
        {
            if (Components.Lists.SelectionIndicators.Count > 0)
            {
                foreach (Entities.Rectengle indicator in Components.Lists.SelectionIndicators)
                {
                    switch (indicator.ID)
                    {
                        case 0:
                            {
                                indicator.CenterPosition = mafsal.CenterPosition;
                                break;
                            }
                    }
                }
            }
            else
            {
                int id = 0;
                Vector2 cPos = new Vector2(mafsal.CenterPosition.X, mafsal.CenterPosition.Y);
                Entities.Rectengle centerIndicator = new Entities.Rectengle(id, width, height, cPos);
                Components.Lists.SelectionIndicators.Add(centerIndicator);
            }
        }

    }
}

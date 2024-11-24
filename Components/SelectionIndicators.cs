using System;
using System.Collections.Generic;
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


        public static void updateIndicators(Entities.Rectengle component)
        { 
            if(Components.Lists.SelectionIndicators.Count > 0)
            {
                foreach(Entities.Rectengle indicator in Components.Lists.SelectionIndicators)
                {
                    switch (indicator.ID)
                    {
                        case 0:
                            {
                                indicator.CenterPosition = component.CenterPosition;
                                break;
                            }
                        case -1:
                            {
                                indicator.CenterPosition = component.StartPos;
                                indicator.CenterPosition.X += width / 4;
                                break;
                            }
                        case 1:
                            {
                                indicator.CenterPosition = component.EndPos;
                                indicator.CenterPosition.X -= width / 4;
                                break;
                            }
                    }
                }
            }
            else
            {
                int id = 0;
                Vector2 cPos = new Vector2(component.CenterPosition.X, component.CenterPosition.Y);
                Entities.Rectengle centerIndicator = new Entities.Rectengle(id, width, height, cPos);
                Components.Lists.SelectionIndicators.Add(centerIndicator);

                int id1 = -1;
                Vector2 sPos = new Vector2(component.StartPos.X, component.StartPos.Y);
                Entities.Rectengle startIndicator = new Entities.Rectengle(id1, width, height, sPos);
                startIndicator.CenterPosition.X += width / 4;
                Components.Lists.SelectionIndicators.Add(startIndicator);

                int id2 = 1;
                Vector2 ePos = new Vector2(component.EndPos.X, component.EndPos.Y);
                Entities.Rectengle endIndicator = new Entities.Rectengle(id2, width, height, ePos);
                endIndicator.CenterPosition.X -= width / 4;
                Components.Lists.SelectionIndicators.Add(endIndicator);
            }
        }

    }
}

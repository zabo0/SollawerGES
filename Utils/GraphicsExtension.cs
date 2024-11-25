using PdfSharp.Charting;
using SollawerGES.Components;
using SollawerGES.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace SollawerGES.Utils
{
    public static class GraphicsExtension
    {
        private static float Height;
        private static float Width;
        private static Pen extPen = new Pen(Color.Gray, 1);


        public static void SetParameters(this System.Drawing.Graphics g, float height, float width)
        {
            Height = height;
            Width = width;
        }


        public static void SetTransform(this System.Drawing.Graphics g)
        {
            g.PageUnit = System.Drawing.GraphicsUnit.Pixel;
        }

        public static void GES_DrawPoint(this System.Drawing.Graphics g, System.Drawing.Pen pen, Entities.Point point, double scale)
        {
            g.SetTransform();
            int thickness = 2;
            g.DrawEllipse(pen, (float)(Origins.BaseOrigin_Primary.Position.X-thickness/2 + point.Position.X.toPX(scale)), (float)(Origins.BaseOrigin_Primary.Position.Y-thickness/2 + point.Position.Y.toPX(scale)), thickness, thickness);
            g.ResetTransform();
        }

        public static void GES_DrawLine(this System.Drawing.Graphics g, System.Drawing.Pen pen, Entities.Line line, double scale, string text = "")
        {
            g.SetTransform();
            double startPosX = Origins.BaseOrigin_Primary.Position.X + Origins.MainOrigin.Position.X.toPX(scale) + line.StartPosition.X.toPX(scale);
            double startPosY = Origins.BaseOrigin_Primary.Position.Y + Origins.MainOrigin.Position.Y.toPX(scale) - line.StartPosition.Y.toPX(scale);

            double endPosX = Origins.BaseOrigin_Primary.Position.X + Origins.MainOrigin.Position.X.toPX(scale) + line.EndPosition.X.toPX(scale);
            double endPosY = Origins.BaseOrigin_Primary.Position.Y + Origins.MainOrigin.Position.Y.toPX(scale) - line.EndPosition.Y.toPX(scale);

            if (text != "")
            {
                System.Drawing.Font font = new System.Drawing.Font("Arial", 12);
                SolidBrush solidBrush = new SolidBrush(Color.Black);
                Vector2 centerPos = new Vector2((startPosX + endPosX) / 2, (startPosY + endPosY) / 2);
                g.DrawString(text, font, solidBrush, (float)centerPos.X, (float)centerPos.Y - 30);
            }

            g.DrawLine(pen, (float)startPosX, (float)startPosY, (float)endPosX, (float)endPosY);
            g.ResetTransform();
        }

        public static void GES_DrawRect(this System.Drawing.Graphics g, System.Drawing.Pen pen, Entities.Rectengle rectengle, double scale, string text = "")
        {
            g.SetTransform();
            double posX;
            double posY;

            if (scale == UnitConverter.PrimaryScale)
            {
                posX = Origins.BaseOrigin_Primary.Position.X + Origins.MainOrigin.Position.X.toPX(scale) + rectengle.CenterPosition.X.toPX(scale) - (rectengle.Width / 2).toPX(scale); //dikdortgenin baslangic noktalari
                posY = Origins.BaseOrigin_Primary.Position.Y + Origins.MainOrigin.Position.Y.toPX(scale) - rectengle.CenterPosition.Y.toPX(scale) - (rectengle.Height / 2).toPX(scale);
            }
            else
            {
                posX = Origins.BaseOrigin_Secondary.Position.X  + rectengle.CenterPosition.X.toPX(scale) - (rectengle.Width / 2).toPX(scale); //dikdortgenin baslangic noktalari
                posY = Origins.BaseOrigin_Secondary.Position.Y  - rectengle.CenterPosition.Y.toPX(scale) - (rectengle.Height / 2).toPX(scale);
            }
            

            double rWidth = rectengle.Width.toPX(scale); //dikdortgenin genislik ve yuksekligi
            double rHeight = rectengle.Height.toPX(scale);

            Rectangle rect = new Rectangle((int)posX, (int)posY, (int)rWidth, (int)rHeight); //dikdortgen olusturulur

            if (text != "")
            {
                System.Drawing.Font font = new System.Drawing.Font("Arial", 12);
                SolidBrush solidBrush = new SolidBrush(Color.Red);
                g.DrawString(text, font, solidBrush, (float)posX + 10, (float)posY - 50);
            }

            if (rectengle.IsSelected)
            {
                g.DrawRectangle(new Pen(Color.Gray, 1), rect);
            }
            else
            {
                g.DrawRectangle(pen, rect);
            }
            g.ResetTransform();
        }
    }
}

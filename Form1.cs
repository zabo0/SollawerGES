using SollawerGES.Components;
using SollawerGES.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace SollawerGES
{
    public partial class Form1 : Form
    {
        //private List<Entities.Point> points = new List<Entities.Point>();
        //private List<Entities.Rectengle> positionBoxes = new List<Entities.Rectengle>();
        //private List<Entities.Rectengle> panels = new List<Entities.Rectengle>();
        //private List<Entities.Rectengle> asiksZ = new List<Entities.Rectengle>();
        //private List<Entities.Rectengle> asiksW = new List<Entities.Rectengle>();
        //private List<Entities.Rectengle> profiles = new List<Entities.Rectengle>();
        //private List<Entities.Rectengle> aksBirls = new List<Entities.Rectengle>();
        //private List<Entities.Rectengle> mafsals = new List<Entities.Rectengle>();
        //private List<Entities.Rectengle> centerDireks = new List<Entities.Rectengle>();
        //private List<Entities.Rectengle> sideDireks = new List<Entities.Rectengle>();

        private Vector2 currentPosition;

        private bool changed = false;
        private bool animation = false;
        private int animationDelayTime;

        public Form1()
        {
            InitializeComponent();
            Components.Components.initializeComponents();

            // Panelin mouse wheel eventini manuel olarak ekle
            panel_drawingBig.MouseWheel += Panel_DrawingBig_MouseWheel;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Configurations.readConfiguration();

            UnitConverter.PrimaryScale = 80;
            UnitConverter.SecondaryScale = 290;

            Origins.BaseOrigin_Primary = new Entities.Point(0, new Vector2(panel_drawingBig.Width / 2, panel_drawingBig.Height / 2));
            Origins.BaseOrigin_Secondary = new Entities.Point(0, new Vector2(panel_drawingSmall.Width / 2, panel_drawingSmall.Height / 2));

            Origins.MainOrigin = new Entities.Point(1, new Vector2(0, 0));

            initializeConfigurations();

            updateFrame();
            label_deviceDPI.Text = UnitConverter.DeviceDPI.ToString();
            label_realDPI.Text = UnitConverter.RealDPI.ToString();

        }

        private void updateFrame()
        {
            showDatas();
            drawPositionBox();
            panel_drawingSmall.Refresh();
            panel_drawingBig.Refresh();

        }

        private void initializeConfigurations()
        {
            animation = Configurations.Animation;
            animationDelayTime = Configurations.AnimationDelayTime;

            Panels.Width = Configurations.WidthPanel;
            Panels.Height = Configurations.HeightPanel;
            Panels.Space = Configurations.SpacePanel;
            Panels.Count = Configurations.CountPanel;

            Asiks.Width_Z = Configurations.WidthZAsik;
            Asiks.Height_Z = Configurations.HeightZAsik;

            Asiks.Width_W = Configurations.WidthWAsik;
            Asiks.Height_W = Configurations.HeightWAsik;

            Profiles.Height = Configurations.HeightProfil;
            Profiles.Space = Configurations.SpaceProfil;

            AksBirls.Width = Configurations.WidthAksBirl;
            AksBirls.Height = Configurations.HeightAksBirl;

            Mafsals.Width = Configurations.WidthMafsal;
            Mafsals.Height = Configurations.HeightMafsal;
            Mafsals.ActuatorDistance = Configurations.ActuatorDistanceMafsal;

            Direks.Width = Configurations.WidthDirek;
            Direks.Height = Configurations.HeightDirek;
        }

        private void createPanels()
        {
            Components.Components.Panels = Panels.createAllPanel();
        }
        private void createAsiks()
        {
            Components.Components.AsiksZ = Asiks.createAsiks_Z(Components.Components.Panels.OrderBy(d => d.ID).First().StartPos, Components.Components.Panels.OrderBy(d => d.ID).Last().EndPos);
            Components.Components.AsiksW = Asiks.createAsiks_W(Components.Components.Panels);
        }

        private void createProfiles()
        {
            Components.Components.Profiles = Profiles.createAllProfiles(Components.Components.AsiksW, Components.Components.AsiksZ);
        }

        private void createAksBirls()
        {
            Components.Components.AksBirls = AksBirls.createAksBirls(Components.Components.Profiles);
        }

        private void createMafsals()
        {
            Components.Components.Mafsals = Mafsals.createAllMafsals();
        }

        private void createDireks()
        {
            Components.Components.CenterDireks = Direks.createCenterDireks(Components.Components.Mafsals);
            Components.Components.SideDireks = Direks.createSideDireks(Components.Components.Mafsals);
        }

        private void drawPositionBox()
        {
            Components.Components.PositionBoxes.Clear();

            int positionBoxID = 0;
            Vector2 cPos = new Vector2(-Origins.MainOrigin.Position.X,0);
            double positionBoxWidht = (double)panel_drawingBig.Width;
            double positionBoxHeight = (double)panel_drawingBig.Height;
            Entities.Rectengle positionBox = new Entities.Rectengle(positionBoxID, positionBoxWidht.toMM(UnitConverter.PrimaryScale), positionBoxHeight.toMM(UnitConverter.PrimaryScale), cPos);

            Components.Components.PositionBoxes.Add(positionBox);
        }
        private void showDatas()
        {
            string dataToShow = "";
            string dataToShow2 = "";

            dataToShow += "Panel: " + Components.Components.Panels.Count.ToString() + Environment.NewLine;
            dataToShow += "Z Asiks: " + Components.Components.AsiksZ.Count.ToString() + Environment.NewLine;
            dataToShow += "W Asiks: " + Components.Components.AsiksW.Count.ToString() + Environment.NewLine;
            dataToShow += "AksBirls: " + Components.Components.AksBirls.Count.ToString() + Environment.NewLine;
            dataToShow += "Mafsals: " + Components.Components.Mafsals.Count.ToString() + Environment.NewLine;
            dataToShow += "Center Direks: " + Components.Components.CenterDireks.Count.ToString() + Environment.NewLine;
            dataToShow += "Side Direks: " + Components.Components.SideDireks.Count.ToString() + Environment.NewLine;
            dataToShow += "Profile: " + Components.Components.Profiles.Count.ToString() + Environment.NewLine;
            foreach (Entities.Rectengle profile in Components.Components.Profiles.OrderBy(d => d.ID))
            {
                dataToShow2 += "   " + profile.ID + ": " + profile.Width + " / ";
            }

            label_info2.Text = dataToShow;
            label_info3.Text = dataToShow2;
        }

        #region EventHendlers

        private void panel_drawingBig_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SetParameters(panel_drawingBig.Height, panel_drawingBig.Width);

            e.Graphics.DrawPoint(new Pen(Color.Red, 2), Origins.MainOrigin, UnitConverter.PrimaryScale);

            if (Components.Components.Panels.Count > 0 && checkBox_showPanel.Checked)
            {
                foreach (Entities.Rectengle panel in Components.Components.Panels)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), panel, UnitConverter.PrimaryScale, false);
                }
            }
            if(Components.Components.AsiksZ.Count > 0 && checkBox_showAsikZ.Checked)
            {
                foreach(Entities.Rectengle asikZ in Components.Components.AsiksZ)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), asikZ, UnitConverter.PrimaryScale, false);
                }
            }
            if(Components.Components.AsiksW.Count > 0 && checkBox_showAsikW.Checked)
            {
                foreach(Entities.Rectengle asikW in Components.Components.AsiksW)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), asikW, UnitConverter.PrimaryScale, false);
                }
            }
            if(Components.Components.Profiles.Count > 0 && checkBox_showProfile.Checked)
            {
                foreach(Entities.Rectengle profile in Components.Components.Profiles)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), profile, UnitConverter.PrimaryScale, false);
                }
            }
            if(Components.Components.AksBirls.Count > 0 && checkBox_showAksBirl.Checked)
            {
                foreach(Entities.Rectengle aksBirl in Components.Components.AksBirls)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), aksBirl, UnitConverter.PrimaryScale, false);
                }
            }
            if(Components.Components.Mafsals.Count > 0 && checkBox_showMafsal.Checked)
            {
                foreach(Entities.Rectengle mafsal in Components.Components.Mafsals)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), mafsal, UnitConverter.PrimaryScale, false);
                }
            }
            if(Components.Components.CenterDireks.Count  > 0 && checkBox_showCenterDirek.Checked)
            {
                foreach(Entities.Rectengle centerDirek in Components.Components.CenterDireks)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), centerDirek, UnitConverter.PrimaryScale, false);
                }
            }
            if (Components.Components.SideDireks.Count > 0 && checkBox_showSideDirek.Checked)
            {
                foreach (Entities.Rectengle sideDirek in Components.Components.SideDireks)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), sideDirek, UnitConverter.PrimaryScale, false);
                }
            }
            changed = false;
        }

        private void panel_drawingSmall_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SetParameters(panel_drawingSmall.Height, panel_drawingSmall.Width);

            if (Components.Components.Panels.Count > 0 && checkBox_showPanel.Checked)
            {
                foreach (Entities.Rectengle panel in Components.Components.Panels)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), panel, UnitConverter.SecondaryScale, false);
                    if(changed && animation)
                        Thread.Sleep(animationDelayTime);
                }
            }
            if (Components.Components.AsiksZ.Count > 0 && checkBox_showAsikZ.Checked)
            {
                foreach (Entities.Rectengle asikZ in Components.Components.AsiksZ)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), asikZ, UnitConverter.SecondaryScale, false);
                    if (changed && animation)
                        Thread.Sleep(animationDelayTime);
                }
            }
            if (Components.Components.AsiksW.Count > 0 && checkBox_showAsikW.Checked)
            {
                foreach (Entities.Rectengle asikW in Components.Components.AsiksW)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), asikW, UnitConverter.SecondaryScale, false);
                    if (changed && animation)
                        Thread.Sleep(animationDelayTime);
                }
            }
            if (Components.Components.Profiles.Count > 0 && checkBox_showProfile.Checked)
            {
                foreach (Entities.Rectengle profile in Components.Components.Profiles)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), profile, UnitConverter.SecondaryScale, false);
                    if (changed && animation)
                        Thread.Sleep(animationDelayTime);
                }
            }
            if(Components.Components.AksBirls.Count > 0 && checkBox_showAksBirl.Checked)
            {
                foreach(Entities.Rectengle aksBirl in Components.Components.AksBirls)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), aksBirl, UnitConverter.SecondaryScale, false);
                    if (changed && animation)
                        Thread.Sleep(animationDelayTime);
                }
            }
            if (Components.Components.Mafsals.Count > 0 && checkBox_showMafsal.Checked)
            {
                foreach (Entities.Rectengle mafsal in Components.Components.Mafsals)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), mafsal, UnitConverter.SecondaryScale, false);
                    if (changed && animation)
                        Thread.Sleep(animationDelayTime);
                }
            }
            if (Components.Components.CenterDireks.Count > 0 && checkBox_showCenterDirek.Checked)
            {
                foreach (Entities.Rectengle centerDirek in Components.Components.CenterDireks)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), centerDirek, UnitConverter.SecondaryScale, false);
                    if (changed && animation)
                        Thread.Sleep(animationDelayTime);
                }
            }
            if (Components.Components.SideDireks.Count > 0 && checkBox_showSideDirek.Checked)
            {
                foreach (Entities.Rectengle sideDirek in Components.Components.SideDireks)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), sideDirek, UnitConverter.SecondaryScale, false);
                    if (changed && animation)
                        Thread.Sleep(animationDelayTime);
                }
            }
            if(Components.Components.PositionBoxes.Count > 0)
            {
                foreach(Entities.Rectengle positionBox in Components.Components.PositionBoxes)
                {
                    e.Graphics.DrawRect(new Pen(Color.Black, 2), positionBox, UnitConverter.SecondaryScale, false);
                }
            }
            changed = false;
        }

        private void panel_drawingBig_MouseMove(object sender, MouseEventArgs e)
        {
            currentPosition = new Vector2(e.Location);
            label_locationBigMM.Text = string.Format("{0,0:F0} / {1,0:F0} mm", currentPosition.X.toMM(UnitConverter.PrimaryScale) - (Origins.BaseOrigin_Primary.Position.X.toMM(UnitConverter.PrimaryScale) + Origins.MainOrigin.Position.X), Origins.BaseOrigin_Primary.Position.Y.toMM(UnitConverter.PrimaryScale) + Origins.MainOrigin.Position.Y - currentPosition.Y.toMM(UnitConverter.PrimaryScale)); //mm cinsinde yazdirir
            label_locationBigPX.Text = string.Format("{0} / {1} px", currentPosition.X - (Origins.BaseOrigin_Primary.Position.X + Origins.MainOrigin.Position.X.toPX(UnitConverter.PrimaryScale)), Origins.BaseOrigin_Primary.Position.Y + Origins.MainOrigin.Position.Y.toPX(UnitConverter.PrimaryScale) - currentPosition.Y); //pixel cinsinde yazdirir
            label_locationGlobalBigPX.Text = string.Format("{0} / {1} px", currentPosition.X, currentPosition.Y); //pixel cinsinde yazdirir
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            Origins.BaseOrigin_Primary = new Entities.Point(0, new Vector2(panel_drawingBig.Width / 2, panel_drawingBig.Height / 2));
            Origins.BaseOrigin_Secondary = new Entities.Point(0, new Vector2(panel_drawingSmall.Width / 2, panel_drawingSmall.Height / 2));
            updateFrame();
        }

        private void Panel_DrawingBig_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                Origins.MainOrigin.scrollOrigin(20 * UnitConverter.PrimaryScale);
                updateFrame();
            }
            else
            {
                Origins.MainOrigin.scrollOrigin(-20 * UnitConverter.PrimaryScale);
                updateFrame();
            }
        }
        

        private void button_zoomIn_Click(object sender, EventArgs e)
        {
            double zoomParameter = 3;
            UnitConverter.PrimaryScale -= zoomParameter - Math.Pow(zoomParameter, -2);
            label_scaleInfo.Text = "" + UnitConverter.PrimaryScale;
            updateFrame();
        }

        private void button_zoomOut_Click(object sender, EventArgs e)
        {
            double zoomParameter = 3;
            UnitConverter.PrimaryScale += zoomParameter + Math.Pow(zoomParameter, 2);
            label_scaleInfo.Text = "" + UnitConverter.PrimaryScale;
            updateFrame();
        }

        private void panel_drawingSmall_MouseMove(object sender, MouseEventArgs e)
        {
            currentPosition = new Vector2(e.Location);
            label_locationSmallMM.Text = string.Format("{0,0:F0} / {1,0:F0} mm", currentPosition.X.toMM(UnitConverter.PrimaryScale) - (Origins.BaseOrigin_Primary.Position.X.toMM(UnitConverter.PrimaryScale) + Origins.MainOrigin.Position.X), Origins.BaseOrigin_Primary.Position.Y.toMM(UnitConverter.PrimaryScale) + Origins.MainOrigin.Position.Y - currentPosition.Y.toMM(UnitConverter.PrimaryScale)); //mm cinsinde yazdirir
            label_locationSmallPX.Text = string.Format("{0} / {1} px", currentPosition.X - (Origins.BaseOrigin_Primary.Position.X + Origins.MainOrigin.Position.X.toPX(UnitConverter.PrimaryScale)), Origins.BaseOrigin_Primary.Position.Y + Origins.MainOrigin.Position.Y.toPX(UnitConverter.PrimaryScale) - currentPosition.Y); //pixel cinsinde yazdirir
            label_locationGlobalBigPX.Text = string.Format("{0} / {1} px", currentPosition.X, currentPosition.Y); //pixel cinsinde yazdirir
        }

        private void panel_drawingSmall_MouseClick(object sender, MouseEventArgs e)
        {
            double distanceFromBasePX = e.Location.X - Origins.BaseOrigin_Secondary.Position.X;

            Origins.MainOrigin.Position.X = -distanceFromBasePX.toMM(UnitConverter.SecondaryScale);
            updateFrame();
        }

        private void button_applyPanelCount_Click(object sender, EventArgs e)
        {
            changed = true;

            Origins.MainOrigin = new Entities.Point(1, new Vector2(0, 0));

            initializeConfigurations();

            createPanels();
            createAsiks();
            createProfiles();
            createAksBirls();
            createMafsals();
            createDireks();

            updateFrame();
        }
        private void button_debug_Click(object sender, EventArgs e)
        {
            
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            Origins.MainOrigin = new Entities.Point(1, new Vector2(0, 0));

            Components.Components.Panels.Clear();
            Components.Components.AsiksZ.Clear();
            Components.Components.AsiksW.Clear();
            Components.Components.Profiles.Clear();
            Components.Components.AksBirls.Clear();
            Components.Components.Mafsals.Clear();
            Components.Components.CenterDireks.Clear();
            Components.Components.SideDireks.Clear();
            Components.Components.PositionBoxes.Clear();

            changed = true;
            updateFrame();
        }

        private void button_configurations_Click(object sender, EventArgs e)
        {
            FormConfigurations formConfigurations = new FormConfigurations();
            formConfigurations.ShowDialog();
        }


        private void showComponentEventHandler(object sender, EventArgs e)
        {
            updateFrame();
        }

        #endregion
    }
}

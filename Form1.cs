﻿using SollawerGES.Components;
using SollawerGES.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.AxHost;

namespace SollawerGES
{
    public partial class Form1 : Form
    {
        private Vector2 currentGlobalMousePosition; // mouseun globaldeki konumu
        private Vector2 currentMousePosition_MM = new Vector2(0, 0);

        private bool isMousePressed = false; // mouse basili mi degil mi
        private Vector2 mousePressedPosition = new Vector2(0, 0);
        private Vector2 mouseReleasedPosition = new Vector2(0, 0);

        private bool isEditModeEnabled = false;

        private bool changed = false;
        private bool animation = false;
        private int animationDelayTime;

        public Form1()
        {
            InitializeComponent();
            Components.Lists.initializeComponents();

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
            Components.Lists.Panels = Panels.createAllPanel();
        }
        private void createAsiks()
        {
            Components.Lists.AsiksZ = Asiks.createAsiks_Z(Components.Lists.Panels.OrderBy(d => d.ID).First().StartPos, Components.Lists.Panels.OrderBy(d => d.ID).Last().EndPos);
            Components.Lists.AsiksW = Asiks.createAsiks_W(Components.Lists.Panels);
        }

        private void createCenterProfile()
        {
            Components.Lists.Profiles = Profiles.createCenterProfile();
        }

        private void createProfiles1()
        {
            Entities.Rectengle centerProfile = Components.Lists.Profiles.Find(d => d.ID == 0);

            Components.Lists.Profiles.Clear();

            Components.Lists.Profiles.Add(centerProfile);


            double length = 6000;
            int i = 0;

            while (true)
            {
                Entities.Rectengle newProfile = Components.Profiles.addProfileNextTo(Components.Lists.Profiles.First(d => d.ID == i), length, "right");
                if (newProfile == null)
                {
                    break;
                }
                Components.Lists.Profiles.Add(newProfile);
                i++;
            }

            length = 6000;
            i = 0;

            while (true)
            {
                Entities.Rectengle newProfile = Components.Profiles.addProfileNextTo(Components.Lists.Profiles.First(d => d.ID == i), length, "left");
                if (newProfile == null)
                {
                    break;
                }
                Components.Lists.Profiles.Add(newProfile);
                i--;
            }
        }

        private void createProfiles()
        {
            double length = 6000;
            int i = 0;

            while (true)
            {
                Entities.Rectengle newProfile = Components.Profiles.addProfileNextTo(Components.Lists.Profiles.First(d => d.ID == i), length, "right");
                if(newProfile == null)
                {
                    break;
                }

                if (Components.Profiles.conflictControl(newProfile.EndPos.X))
                {
                    foreach(Entities.Rectengle profile in Components.Lists.Profiles.ToList())
                    {
                        if(profile.ID > 0)
                        {
                            Components.Lists.Profiles.Remove(profile);
                        }
                    }
                    length -= 10;
                    i = 0;
                    continue;
                }
                Components.Lists.Profiles.Add(newProfile);
                i++;
            }

            length = 6000;
            i = 0;

            while (true)
            {
                Entities.Rectengle newProfile = Components.Profiles.addProfileNextTo(Components.Lists.Profiles.First(d => d.ID == i), length, "left");
                if (newProfile == null)
                {
                    break;
                }

                if (Components.Profiles.conflictControl(newProfile.StartPos.X))
                {
                    foreach (Entities.Rectengle profile in Components.Lists.Profiles.ToList())
                    {
                        if (profile.ID < 0)
                        {
                            Components.Lists.Profiles.Remove(profile);
                        }
                    }
                    length -= 10;
                    i = 0;
                    continue;
                }
                Components.Lists.Profiles.Add(newProfile);
                i--;
            }

        }

        private void createAksBirls()
        {
            Components.Lists.AksBirls = AksBirls.createAksBirls(Components.Lists.Profiles);
        }

        private void createMafsals()
        {
            Components.Lists.Mafsals = Mafsals.createAllMafsals();
        }

        private void createDireks()
        {
            Components.Lists.CenterDireks = Direks.createCenterDireks(Components.Lists.Mafsals);
            Components.Lists.SideDireks = Direks.createSideDireks(Components.Lists.Mafsals);
        }

        private void drawPositionBox()
        {
            Components.Lists.PositionBoxes.Clear();

            int positionBoxID = 0;
            Vector2 cPos = new Vector2(-Origins.MainOrigin.Position.X,0);
            double positionBoxWidht = (double)panel_drawingBig.Width;
            double positionBoxHeight = (double)panel_drawingBig.Height;
            Entities.Rectengle positionBox = new Entities.Rectengle(positionBoxID, positionBoxWidht.toMM(UnitConverter.PrimaryScale), positionBoxHeight.toMM(UnitConverter.PrimaryScale), cPos);

            Components.Lists.PositionBoxes.Add(positionBox);
        }
        private void showDatas()
        {
            string dataToShow = "";
            string dataToShow2 = "";

            dataToShow += "Panel: " + Components.Lists.Panels.Count.ToString() + Environment.NewLine;
            dataToShow += "Z Asiks: " + Components.Lists.AsiksZ.Count.ToString() + Environment.NewLine;
            dataToShow += "W Asiks: " + Components.Lists.AsiksW.Count.ToString() + Environment.NewLine;
            dataToShow += "AksBirls: " + Components.Lists.AksBirls.Count.ToString() + Environment.NewLine;
            dataToShow += "Mafsals: " + Components.Lists.Mafsals.Count.ToString() + Environment.NewLine;
            dataToShow += "Center Direks: " + Components.Lists.CenterDireks.Count.ToString() + Environment.NewLine;
            dataToShow += "Side Direks: " + Components.Lists.SideDireks.Count.ToString() + Environment.NewLine;
            dataToShow += "Profile: " + Components.Lists.Profiles.Count.ToString() + Environment.NewLine;
            foreach (Entities.Rectengle profile in Components.Lists.Profiles.OrderBy(d => d.ID))
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

            if (Components.Lists.Panels.Count > 0 && checkBox_showPanel.Checked)
            {
                foreach (Entities.Rectengle panel in Components.Lists.Panels)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), panel, UnitConverter.PrimaryScale, false);
                }
            }
            if(Components.Lists.AsiksZ.Count > 0 && checkBox_showAsikZ.Checked)
            {
                foreach(Entities.Rectengle asikZ in Components.Lists.AsiksZ)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), asikZ, UnitConverter.PrimaryScale, false);
                }
            }
            if(Components.Lists.AsiksW.Count > 0 && checkBox_showAsikW.Checked)
            {
                foreach(Entities.Rectengle asikW in Components.Lists.AsiksW)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), asikW, UnitConverter.PrimaryScale, false);
                }
            }
            if(Components.Lists.Profiles.Count > 0 && checkBox_showProfile.Checked)
            {
                foreach(Entities.Rectengle profile in Components.Lists.Profiles)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), profile, UnitConverter.PrimaryScale, true);
                }
            }
            if(Components.Lists.AksBirls.Count > 0 && checkBox_showAksBirl.Checked)
            {
                foreach(Entities.Rectengle aksBirl in Components.Lists.AksBirls)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), aksBirl, UnitConverter.PrimaryScale, false);
                }
            }
            if(Components.Lists.Mafsals.Count > 0 && checkBox_showMafsal.Checked)
            {
                foreach(Entities.Rectengle mafsal in Components.Lists.Mafsals)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), mafsal, UnitConverter.PrimaryScale, false);
                }
            }
            if(Components.Lists.CenterDireks.Count  > 0 && checkBox_showCenterDirek.Checked)
            {
                foreach(Entities.Rectengle centerDirek in Components.Lists.CenterDireks)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), centerDirek, UnitConverter.PrimaryScale, false);
                }
            }
            if (Components.Lists.SideDireks.Count > 0 && checkBox_showSideDirek.Checked)
            {
                foreach (Entities.Rectengle sideDirek in Components.Lists.SideDireks)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), sideDirek, UnitConverter.PrimaryScale, false);
                }
            }
            changed = false;
        }

        private void panel_drawingSmall_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SetParameters(panel_drawingSmall.Height, panel_drawingSmall.Width);

            if (Components.Lists.Panels.Count > 0 && checkBox_showPanel.Checked)
            {
                foreach (Entities.Rectengle panel in Components.Lists.Panels)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), panel, UnitConverter.SecondaryScale, false);
                    if(changed && animation)
                        Thread.Sleep(animationDelayTime);
                }
            }
            if (Components.Lists.AsiksZ.Count > 0 && checkBox_showAsikZ.Checked)
            {
                foreach (Entities.Rectengle asikZ in Components.Lists.AsiksZ)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), asikZ, UnitConverter.SecondaryScale, false);
                    if (changed && animation)
                        Thread.Sleep(animationDelayTime);
                }
            }
            if (Components.Lists.AsiksW.Count > 0 && checkBox_showAsikW.Checked)
            {
                foreach (Entities.Rectengle asikW in Components.Lists.AsiksW)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), asikW, UnitConverter.SecondaryScale, false);
                    if (changed && animation)
                        Thread.Sleep(animationDelayTime);
                }
            }
            if (Components.Lists.Profiles.Count > 0 && checkBox_showProfile.Checked)
            {
                foreach (Entities.Rectengle profile in Components.Lists.Profiles)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), profile, UnitConverter.SecondaryScale, false);
                    if (changed && animation)
                        Thread.Sleep(animationDelayTime);
                }
            }
            if(Components.Lists.AksBirls.Count > 0 && checkBox_showAksBirl.Checked)
            {
                foreach(Entities.Rectengle aksBirl in Components.Lists.AksBirls)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), aksBirl, UnitConverter.SecondaryScale, false);
                    if (changed && animation)
                        Thread.Sleep(animationDelayTime);
                }
            }
            if (Components.Lists.Mafsals.Count > 0 && checkBox_showMafsal.Checked)
            {
                foreach (Entities.Rectengle mafsal in Components.Lists.Mafsals)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), mafsal, UnitConverter.SecondaryScale, false);
                    if (changed && animation)
                        Thread.Sleep(animationDelayTime);
                }
            }
            if (Components.Lists.CenterDireks.Count > 0 && checkBox_showCenterDirek.Checked)
            {
                foreach (Entities.Rectengle centerDirek in Components.Lists.CenterDireks)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), centerDirek, UnitConverter.SecondaryScale, false);
                    if (changed && animation)
                        Thread.Sleep(animationDelayTime);
                }
            }
            if (Components.Lists.SideDireks.Count > 0 && checkBox_showSideDirek.Checked)
            {
                foreach (Entities.Rectengle sideDirek in Components.Lists.SideDireks)
                {
                    e.Graphics.DrawRect(new Pen(Color.Red, 1), sideDirek, UnitConverter.SecondaryScale, false);
                    if (changed && animation)
                        Thread.Sleep(animationDelayTime);
                }
            }
            if(Components.Lists.PositionBoxes.Count > 0)
            {
                foreach(Entities.Rectengle positionBox in Components.Lists.PositionBoxes)
                {
                    e.Graphics.DrawRect(new Pen(Color.Black, 2), positionBox, UnitConverter.SecondaryScale, false);
                }
            }
            changed = false;
        }

        private void panel_drawingBig_MouseMove(object sender, MouseEventArgs e)
        {
            currentGlobalMousePosition = new Vector2(e.Location);
            currentMousePosition_MM.X = currentGlobalMousePosition.X.toMM(UnitConverter.PrimaryScale) - (Origins.BaseOrigin_Primary.Position.X.toMM(UnitConverter.PrimaryScale) + Origins.MainOrigin.Position.X);
            currentMousePosition_MM.Y = Origins.BaseOrigin_Primary.Position.Y.toMM(UnitConverter.PrimaryScale) + Origins.MainOrigin.Position.Y - currentGlobalMousePosition.Y.toMM(UnitConverter.PrimaryScale);

            label_locationBigMM.Text = string.Format("{0,0:F0} / {1,0:F0} mm", currentMousePosition_MM.X, currentMousePosition_MM.Y); //mm cinsinde yazdirir
            label_locationBigPX.Text = string.Format("{0} / {1} px", currentGlobalMousePosition.X - (Origins.BaseOrigin_Primary.Position.X + Origins.MainOrigin.Position.X.toPX(UnitConverter.PrimaryScale)), Origins.BaseOrigin_Primary.Position.Y + Origins.MainOrigin.Position.Y.toPX(UnitConverter.PrimaryScale) - currentGlobalMousePosition.Y); //pixel cinsinde yazdirir
            label_locationGlobalBigPX.Text = string.Format("{0} / {1} px", currentGlobalMousePosition.X, currentGlobalMousePosition.Y); //pixel cinsinde yazdirir

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
            currentGlobalMousePosition = new Vector2(e.Location);
            label_locationSmallMM.Text = string.Format("{0,0:F0} / {1,0:F0} mm", currentGlobalMousePosition.X.toMM(UnitConverter.PrimaryScale) - (Origins.BaseOrigin_Primary.Position.X.toMM(UnitConverter.PrimaryScale) + Origins.MainOrigin.Position.X), Origins.BaseOrigin_Primary.Position.Y.toMM(UnitConverter.PrimaryScale) + Origins.MainOrigin.Position.Y - currentGlobalMousePosition.Y.toMM(UnitConverter.PrimaryScale)); //mm cinsinde yazdirir
            label_locationSmallPX.Text = string.Format("{0} / {1} px", currentGlobalMousePosition.X - (Origins.BaseOrigin_Primary.Position.X + Origins.MainOrigin.Position.X.toPX(UnitConverter.PrimaryScale)), Origins.BaseOrigin_Primary.Position.Y + Origins.MainOrigin.Position.Y.toPX(UnitConverter.PrimaryScale) - currentGlobalMousePosition.Y); //pixel cinsinde yazdirir
            label_locationGlobalBigPX.Text = string.Format("{0} / {1} px", currentGlobalMousePosition.X, currentGlobalMousePosition.Y); //pixel cinsinde yazdirir

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
            createCenterProfile();
            createProfiles1();
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

            Components.Lists.Panels.Clear();
            Components.Lists.AsiksZ.Clear();
            Components.Lists.AsiksW.Clear();
            Components.Lists.Profiles.Clear();
            Components.Lists.AksBirls.Clear();
            Components.Lists.Mafsals.Clear();
            Components.Lists.CenterDireks.Clear();
            Components.Lists.SideDireks.Clear();
            Components.Lists.PositionBoxes.Clear();

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



        private void panel_drawingBig_MouseDown(object sender, MouseEventArgs e)
        {
            isMousePressed = true;
            mousePressedPosition.X = currentGlobalMousePosition.X.toMM(UnitConverter.PrimaryScale) - (Origins.BaseOrigin_Primary.Position.X.toMM(UnitConverter.PrimaryScale) + Origins.MainOrigin.Position.X);
            mousePressedPosition.Y = Origins.BaseOrigin_Primary.Position.Y.toMM(UnitConverter.PrimaryScale) + Origins.MainOrigin.Position.Y - currentGlobalMousePosition.Y.toMM(UnitConverter.PrimaryScale);

        }

        private void panel_drawingBig_MouseUp(object sender, MouseEventArgs e)
        {
            isMousePressed = false;
            mouseReleasedPosition.X = currentGlobalMousePosition.X.toMM(UnitConverter.PrimaryScale) - (Origins.BaseOrigin_Primary.Position.X.toMM(UnitConverter.PrimaryScale) + Origins.MainOrigin.Position.X);
            mouseReleasedPosition.Y = Origins.BaseOrigin_Primary.Position.Y.toMM(UnitConverter.PrimaryScale) + Origins.MainOrigin.Position.Y - currentGlobalMousePosition.Y.toMM(UnitConverter.PrimaryScale);

        }

        private void button_editMode_Click(object sender, EventArgs e)
        {
            if (isEditModeEnabled)
            {
                isEditModeEnabled = false;
            }
            else
            {
                isEditModeEnabled = true;
            }
        }


        

        private void panel_drawingBig_MouseClick(object sender, MouseEventArgs e)
        {
            mousePressedPosition.X = currentGlobalMousePosition.X.toMM(UnitConverter.PrimaryScale) - (Origins.BaseOrigin_Primary.Position.X.toMM(UnitConverter.PrimaryScale) + Origins.MainOrigin.Position.X);
            mousePressedPosition.Y = Origins.BaseOrigin_Primary.Position.Y.toMM(UnitConverter.PrimaryScale) + Origins.MainOrigin.Position.Y - currentGlobalMousePosition.Y.toMM(UnitConverter.PrimaryScale);

            label_infoEdit.Text = $"pressed location: {mousePressedPosition.X} - {mousePressedPosition.Y}";


            if (Components.Lists.Profiles.Count > 0 && isEditModeEnabled)
            {
                foreach (Entities.Rectengle profile in Components.Lists.Profiles)
                {
                    double centerProfilStartX = profile.StartPos.X;
                    double centerProfilEndX = profile.EndPos.X;
                    double centerProfilTopX = profile.TopPos.Y;
                    double centerProfilBottomX = profile.BottomPos.Y;

                    if (centerProfilStartX <= mousePressedPosition.X && mousePressedPosition.X <= centerProfilEndX && centerProfilBottomX <= mousePressedPosition.Y && mousePressedPosition.Y <= centerProfilTopX)
                    {
                        profile.IsSelected = true;
                    }
                    else
                    {
                        profile.IsSelected = false;
                    }
                }


                if (Components.Lists.Profiles.Find(d => d.IsSelected == true) != null)
                {
                    label_info.Text = $"selected component id: {Components.Lists.Profiles.Find(d => d.IsSelected == true).ID}";
                }
                else
                {
                    label_info.Text = $"selected component id: null";
                }
                updateFrame();
            }
        }

        #endregion
    }
}

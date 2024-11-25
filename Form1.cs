using SollawerGES.Components;
using SollawerGES.Constraints;
using SollawerGES.Dimensions;
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

        private Vector2 selectedProfileCenterPoint;
        private Vector2 selectedProfileStartPoint;
        private Vector2 selectedProfileEndPoint;

        private Vector2 selectedMafsalCenterPoint;
        private Vector2 selectedMafsalStartPoint;
        private Vector2 selectedMafsalEndPoint;

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

        private void updateProfile()
        {
            Components.Profiles.updateProfiles();
        }


        private void createAksBirls()
        {
            Components.Lists.AksBirls = AksBirls.createAksBirls(Components.Lists.Profiles);
        }

        private void createCenterMafsals()
        {
            Components.Lists.Mafsals = Components.Mafsals.createCenterMafsals();
        }

        private void updateMafsals()
        {
            Components.Mafsals.updateMafsals();
        }

        private void createDireks()
        {
            Components.Lists.CenterDireks = Direks.createCenterDireks(Components.Lists.Mafsals);
            Components.Lists.SideDireks = Direks.createSideDireks(Components.Lists.Mafsals);
        }

        private void updateDimensions()
        {
            Components.Lists.Dimensions.Clear();

            foreach(Entities.Rectengle profile in Components.Lists.Profiles)
            {
                if(profile.ID == 0)
                {
                    DimensionManager.addDimension(0, profile.StartPos, Components.Lists.Mafsals.Find(d => d.ID == -1).StartPos, DimensionManager.calculateDimPosition(profile.StartPos, Components.Lists.Mafsals.Find(d => d.ID == -1).StartPos, 500));
                }
                DimensionManager.addDimension(0, profile.StartPos, profile.EndPos);
            }

            foreach (Entities.Rectengle currentMafsal in Components.Lists.Mafsals.OrderBy(d => d.ID).ToList().FindAll(d => d.ID > 1))
            {
                Entities.Rectengle previousMafsal = Components.Lists.Mafsals.Find(d => d.ID == currentMafsal.ID - 1);
                DimensionManager.addDimension(1, previousMafsal.StartPos, currentMafsal.StartPos);
            }

            foreach (Entities.Rectengle currentMafsal in Components.Lists.Mafsals.OrderByDescending(d => d.ID).ToList().FindAll(d => d.ID < -1))
            {
                Entities.Rectengle previousMafsal = Components.Lists.Mafsals.Find(d => d.ID == currentMafsal.ID + 1);
                DimensionManager.addDimension(1, currentMafsal.EndPos, previousMafsal.EndPos);
            }


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

        private void editMode()
        {
            if (isEditModeEnabled)
            {
                Entities.Rectengle selectedProfile = Components.Lists.Profiles.Find(d => d.IsSelected == true);
                Entities.Rectengle selectedMafsal = Components.Lists.Mafsals.Find(d => d.IsSelected == true);
                Entities.Rectengle selectedIndicator = Components.Lists.SelectionIndicators.Find(d => d.IsSelected == true);

                double roundedCurrentMousePosX = currentMousePosition_MM.X.round(Configurations.SnapStep);

                if (selectedProfile != null)
                {
                    if (selectedIndicator != null && isMousePressed)
                    {
                        if (selectedProfile.ID == 0)
                        {
                            switch (selectedIndicator.ID)
                            {
                                case 0:
                                    {
                                        selectedProfile.CenterPosition.X = currentMousePosition_MM.X.round(1);
                                        SelectionIndicators.updateProfileIndicators(selectedProfile);
                                        break;
                                    }
                                case -1:
                                    {
                                        double lenght = selectedProfile.EndPos.X.round(Configurations.SnapStep) - roundedCurrentMousePosX;

                                        if (lenght < 1000)
                                            lenght = 1000;
                                        if (lenght > 6000)
                                            lenght = 6000;
                                            
                                        selectedProfile.Width = lenght;
                                        selectedProfile.CenterPosition.X = selectedProfileEndPoint.X - lenght / 2;
                                        SelectionIndicators.updateProfileIndicators(selectedProfile);
                                        break;
                                    }
                                case 1:
                                    {
                                        double lenght = roundedCurrentMousePosX - selectedProfile.StartPos.X.round(Configurations.SnapStep);

                                        if (lenght < 1000)
                                            lenght = 1000;
                                        if (lenght > 6000)
                                            lenght = 6000;

                                        selectedProfile.Width = lenght;
                                        selectedProfile.CenterPosition.X = selectedProfileStartPoint.X + lenght / 2;
                                        SelectionIndicators.updateProfileIndicators(selectedProfile);

                                        break;
                                    }
                            }
                        }
                        else if (selectedProfile.ID > 0)
                        {
                            switch (selectedIndicator.ID)
                            {
                                case 1:
                                    {
                                        double lenght = roundedCurrentMousePosX - selectedProfile.StartPos.X.round(Configurations.SnapStep);

                                        if (lenght < 1000)
                                            lenght = 1000;
                                        if (lenght > 6000)
                                            lenght = 6000;

                                        selectedProfile.Width = lenght;
                                        selectedProfile.CenterPosition.X = selectedProfileStartPoint.X + lenght / 2;
                                        SelectionIndicators.updateProfileIndicators(selectedProfile);
                                        break;
                                    }
                            }
                        }
                        else if (selectedProfile.ID < 0)
                        {
                            switch (selectedIndicator.ID)
                            {
                                case -1:
                                    {
                                        double lenght = selectedProfile.EndPos.X.round(Configurations.SnapStep) - roundedCurrentMousePosX;

                                        if (lenght < 1000)
                                            lenght = 1000;
                                        if (lenght > 6000)
                                            lenght = 6000;

                                        selectedProfile.Width = lenght;
                                        selectedProfile.CenterPosition.X = selectedProfileEndPoint.X - lenght / 2;
                                        SelectionIndicators.updateProfileIndicators(selectedProfile);
                                        break;
                                    }
                            }
                        }
                        updateProfile();
                        createAksBirls();
                        updateFrame();
                    }
                }
                if (selectedMafsal != null)
                {
                    if (selectedIndicator != null && isMousePressed)
                    {
                        if (selectedMafsal.ID > 1)
                        {
                            switch (selectedIndicator.ID)
                            {
                                case 0:
                                    {
                                        double space = roundedCurrentMousePosX - Components.Lists.Mafsals.Find(d => d.ID == selectedMafsal.ID - 1).CenterPosition.X;

                                        if(space < Configurations.MinMafsalSpace)
                                            space = Configurations.MinMafsalSpace;
                                        if (space > Configurations.MaxMafsalSpace)
                                            space = Configurations.MaxMafsalSpace;

                                        selectedMafsal.CenterPosition.X = Components.Lists.Mafsals.Find(d => d.ID == selectedMafsal.ID - 1).CenterPosition.X + space;
                                        SelectionIndicators.updateMafsalIndicators(selectedMafsal);
                                        break;
                                    }
                            }
                        }
                        else if (selectedMafsal.ID < -1)
                        {
                            switch (selectedIndicator.ID)
                            {
                                case 0:
                                    {
                                        double space = Components.Lists.Mafsals.Find(d => d.ID == selectedMafsal.ID + 1).CenterPosition.X - roundedCurrentMousePosX;

                                        if (space < Configurations.MinMafsalSpace)
                                            space = Configurations.MinMafsalSpace;
                                        if (space > Configurations.MaxMafsalSpace)
                                            space = Configurations.MaxMafsalSpace;

                                        selectedMafsal.CenterPosition.X = Components.Lists.Mafsals.Find(d => d.ID == selectedMafsal.ID + 1).CenterPosition.X - space;
                                        SelectionIndicators.updateMafsalIndicators(selectedMafsal);
                                        break;
                                    }
                            }
                        }
                        updateMafsals();
                        createDireks();
                        updateFrame();
                    }
                }
                else
                {
                    //label_infoEdit.Text = $"selected profile: null";
                }
            }
            updateDimensions();
        }

        #region EventHendlers

        private void panel_drawingBig_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SetParameters(panel_drawingBig.Height, panel_drawingBig.Width);

            e.Graphics.GES_DrawPoint(new Pen(Color.Red, 2), Origins.MainOrigin, UnitConverter.PrimaryScale);

            if (Components.Lists.Panels.Count > 0 && checkBox_showPanel.Checked)
            {
                foreach (Entities.Rectengle panel in Components.Lists.Panels)
                {
                    e.Graphics.GES_DrawRect(new Pen(Color.Red, 1), panel, UnitConverter.PrimaryScale);
                }
            }
            if(Components.Lists.AsiksZ.Count > 0 && checkBox_showAsikZ.Checked)
            {
                foreach(Entities.Rectengle asikZ in Components.Lists.AsiksZ)
                {
                    e.Graphics.GES_DrawRect(new Pen(Color.Red, 1), asikZ, UnitConverter.PrimaryScale);
                }
            }
            if(Components.Lists.AsiksW.Count > 0 && checkBox_showAsikW.Checked)
            {
                foreach(Entities.Rectengle asikW in Components.Lists.AsiksW)
                {
                    e.Graphics.GES_DrawRect(new Pen(Color.Red, 1), asikW, UnitConverter.PrimaryScale);
                }
            }
            if(Components.Lists.Profiles.Count > 0 && checkBox_showProfile.Checked)
            {
                foreach(Entities.Rectengle profile in Components.Lists.Profiles)
                {
                    e.Graphics.GES_DrawRect(new Pen(Color.Red, 1), profile, UnitConverter.PrimaryScale, profile.ID.ToString());
                }
            }
            if(Components.Lists.AksBirls.Count > 0 && checkBox_showAksBirl.Checked)
            {
                foreach(Entities.Rectengle aksBirl in Components.Lists.AksBirls)
                {
                    e.Graphics.GES_DrawRect(new Pen(Color.Red, 1), aksBirl, UnitConverter.PrimaryScale);
                }
            }
            if(Components.Lists.Mafsals.Count > 0 && checkBox_showMafsal.Checked)
            {
                foreach(Entities.Rectengle mafsal in Components.Lists.Mafsals)
                {
                    e.Graphics.GES_DrawRect(new Pen(Color.Red, 1), mafsal, UnitConverter.PrimaryScale);
                }
            }
            if(Components.Lists.CenterDireks.Count  > 0 && checkBox_showCenterDirek.Checked)
            {
                foreach(Entities.Rectengle centerDirek in Components.Lists.CenterDireks)
                {
                    e.Graphics.GES_DrawRect(new Pen(Color.Red, 1), centerDirek, UnitConverter.PrimaryScale);
                }
            }
            if (Components.Lists.SideDireks.Count > 0 && checkBox_showSideDirek.Checked)
            {
                foreach (Entities.Rectengle sideDirek in Components.Lists.SideDireks)
                {
                    e.Graphics.GES_DrawRect(new Pen(Color.Red, 1), sideDirek, UnitConverter.PrimaryScale);
                }
            }
            if(Components.Lists.SelectionIndicators.Count > 0)
            {
                foreach(Entities.Rectengle selectionIndicator in Components.Lists.SelectionIndicators)
                {
                    e.Graphics.GES_DrawRect(new Pen(Color.Blue, 1), selectionIndicator, UnitConverter.PrimaryScale);
                }
            }
            if(Components.Lists.Dimensions.Count > 0)
            {
                foreach(Dimension dimension in Components.Lists.Dimensions)
                {
                    if(dimension.ID == 0 && checkBox_showProfileDimensions.Checked)
                    {
                        foreach (Entities.Line line in dimension.ShapeLinesList)
                        {
                            string text = "";
                            if (line.ID == 1)
                            {
                                text = dimension.Lenght.ToString();
                            }
                            e.Graphics.GES_DrawLine(new Pen(Color.Black, 0.5f), line, UnitConverter.PrimaryScale, text);
                        }
                    }

                    if (dimension.ID == 1 && checkBox_showMafsalDimensions.Checked)
                    {
                        foreach (Entities.Line line in dimension.ShapeLinesList)
                        {
                            string text = "";
                            if (line.ID == 1)
                            {
                                text = dimension.Lenght.ToString();
                            }
                            e.Graphics.GES_DrawLine(new Pen(Color.Black, 0.5f), line, UnitConverter.PrimaryScale, text);
                        }
                    }

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
                    e.Graphics.GES_DrawRect(new Pen(Color.Red, 1), panel, UnitConverter.SecondaryScale);
                    if(changed && animation)
                        Thread.Sleep(animationDelayTime);
                }
            }
            if (Components.Lists.AsiksZ.Count > 0 && checkBox_showAsikZ.Checked)
            {
                foreach (Entities.Rectengle asikZ in Components.Lists.AsiksZ)
                {
                    e.Graphics.GES_DrawRect(new Pen(Color.Red, 1), asikZ, UnitConverter.SecondaryScale);
                    if (changed && animation)
                        Thread.Sleep(animationDelayTime);
                }
            }
            if (Components.Lists.AsiksW.Count > 0 && checkBox_showAsikW.Checked)
            {
                foreach (Entities.Rectengle asikW in Components.Lists.AsiksW)
                {
                    e.Graphics.GES_DrawRect(new Pen(Color.Red, 1), asikW, UnitConverter.SecondaryScale);
                    if (changed && animation)
                        Thread.Sleep(animationDelayTime);
                }
            }
            if (Components.Lists.Profiles.Count > 0 && checkBox_showProfile.Checked)
            {
                foreach (Entities.Rectengle profile in Components.Lists.Profiles)
                {
                    e.Graphics.GES_DrawRect(new Pen(Color.Red, 1), profile, UnitConverter.SecondaryScale);
                    if (changed && animation)
                        Thread.Sleep(animationDelayTime);
                }
            }
            if(Components.Lists.AksBirls.Count > 0 && checkBox_showAksBirl.Checked)
            {
                foreach(Entities.Rectengle aksBirl in Components.Lists.AksBirls)
                {
                    e.Graphics.GES_DrawRect(new Pen(Color.Red, 1), aksBirl, UnitConverter.SecondaryScale);
                    if (changed && animation)
                        Thread.Sleep(animationDelayTime);
                }
            }
            if (Components.Lists.Mafsals.Count > 0 && checkBox_showMafsal.Checked)
            {
                foreach (Entities.Rectengle mafsal in Components.Lists.Mafsals)
                {
                    e.Graphics.GES_DrawRect(new Pen(Color.Red, 1), mafsal, UnitConverter.SecondaryScale);
                    if (changed && animation)
                        Thread.Sleep(animationDelayTime);
                }
            }
            if (Components.Lists.CenterDireks.Count > 0 && checkBox_showCenterDirek.Checked)
            {
                foreach (Entities.Rectengle centerDirek in Components.Lists.CenterDireks)
                {
                    e.Graphics.GES_DrawRect(new Pen(Color.Red, 1), centerDirek, UnitConverter.SecondaryScale);
                    if (changed && animation)
                        Thread.Sleep(animationDelayTime);
                }
            }
            if (Components.Lists.SideDireks.Count > 0 && checkBox_showSideDirek.Checked)
            {
                foreach (Entities.Rectengle sideDirek in Components.Lists.SideDireks)
                {
                    e.Graphics.GES_DrawRect(new Pen(Color.Red, 1), sideDirek, UnitConverter.SecondaryScale);
                    if (changed && animation)
                        Thread.Sleep(animationDelayTime);
                }
            }
            if(Components.Lists.PositionBoxes.Count > 0)
            {
                foreach(Entities.Rectengle positionBox in Components.Lists.PositionBoxes)
                {
                    e.Graphics.GES_DrawRect(new Pen(Color.Black, 2), positionBox, UnitConverter.SecondaryScale);
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

            editMode();
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
            updateProfile();
            createAksBirls();
            createCenterMafsals();
            updateMafsals();
            createDireks();

            updateDimensions();

            updateFrame();
        }
        private void button_debug_Click(object sender, EventArgs e)
        {
            if (isEditModeEnabled)
            {
                Entities.Rectengle selectedProfile = Components.Lists.Profiles.First(d => d.IsSelected == true);
                if (selectedProfile.ID > 0)
                {
                    Components.Lists.Profiles.Add(Components.Profiles.addProfileNextTo(selectedProfile, 6000, "right"));
                }
                if (selectedProfile.ID < 0)
                {
                    Components.Lists.Profiles.Add(Components.Profiles.addProfileNextTo(selectedProfile, 6000, "left"));
                }
                if(selectedProfile.ID == 0)
                {
                    Components.Lists.Profiles.Add(Components.Profiles.addProfileNextTo(selectedProfile, 6000, "left"));
                }
                updateFrame();
            }
            
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

            if (isEditModeEnabled)
            {
                if (Components.Lists.Profiles.Count > 0)
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

                            selectedProfileCenterPoint = profile.CenterPosition;
                            selectedProfileStartPoint = profile.StartPos;
                            selectedProfileEndPoint = profile.EndPos;

                            Components.Lists.SelectionIndicators = new List<Entities.Rectengle>();
                            SelectionIndicators.updateProfileIndicators(profile);

                            foreach (Entities.Rectengle indicator in Components.Lists.SelectionIndicators)
                            {
                                double indicatorStartX = indicator.StartPos.X;
                                double indicatorEndX = indicator.EndPos.X;
                                double indicatorTopY = indicator.TopPos.Y;
                                double indicatorBottomY = indicator.BottomPos.Y;

                                if (indicatorStartX <= currentMousePosition_MM.X && currentMousePosition_MM.X <= indicatorEndX && indicatorBottomY <= currentMousePosition_MM.Y && currentMousePosition_MM.Y <= indicatorTopY)
                                {
                                    indicator.IsSelected = true;
                                }
                                else
                                {
                                    indicator.IsSelected = false;
                                }
                            }
                        }
                        else
                        {
                            profile.IsSelected = false;
                        }
                    }
                }

                if(Components.Lists.Mafsals.Count > 0)
                {
                    foreach(Entities.Rectengle mafsal in Components.Lists.Mafsals)
                    {
                        double centerMafsalStartX = mafsal.StartPos.X;
                        double centerMafsalEndX = mafsal.EndPos.X;
                        double centerMafsalTopY = mafsal.TopPos.Y;
                        double centerMafsalBottomY = mafsal.BottomPos.Y;

                        if (centerMafsalStartX <= mousePressedPosition.X && mousePressedPosition.X <= centerMafsalEndX && centerMafsalBottomY <= mousePressedPosition.Y && mousePressedPosition.Y <= centerMafsalTopY)
                        {
                            mafsal.IsSelected = true;

                            selectedMafsalCenterPoint = mafsal.CenterPosition;
                            selectedMafsalStartPoint = mafsal.StartPos;
                            selectedMafsalEndPoint = mafsal.EndPos;

                            Components.Lists.SelectionIndicators = new List<Entities.Rectengle>();
                            SelectionIndicators.updateMafsalIndicators(mafsal);

                            foreach (Entities.Rectengle indicator in Components.Lists.SelectionIndicators)
                            {
                                double indicatorStartX = indicator.StartPos.X;
                                double indicatorEndX = indicator.EndPos.X;
                                double indicatorTopY = indicator.TopPos.Y;
                                double indicatorBottomY = indicator.BottomPos.Y;

                                if (indicatorStartX <= currentMousePosition_MM.X && currentMousePosition_MM.X <= indicatorEndX && indicatorBottomY <= currentMousePosition_MM.Y && currentMousePosition_MM.Y <= indicatorTopY)
                                {
                                    indicator.IsSelected = true;
                                }
                                else
                                {
                                    indicator.IsSelected = false;
                                }
                            }

                            if(Components.Lists.Profiles.Find(d => d.IsSelected == true) != null)
                                Components.Lists.Profiles.Find(d => d.IsSelected == true).IsSelected = false;
                        }
                        else
                        {
                            mafsal.IsSelected = false;
                        }
                    }
                }
                if (Components.Lists.Profiles.Find(d => d.IsSelected == true) == null && Components.Lists.Mafsals.Find(d => d.IsSelected == true) == null)
                {
                    Components.Lists.SelectionIndicators.Clear();
                }
                updateFrame();
            }
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

        #endregion
    }
}

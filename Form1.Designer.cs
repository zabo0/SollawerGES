namespace SollawerGES
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_header = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button_configurations = new System.Windows.Forms.Button();
            this.button_debug = new System.Windows.Forms.Button();
            this.panel_drawingSmall = new System.Windows.Forms.Panel();
            this.label_locationSmallMM = new System.Windows.Forms.Label();
            this.label_locationSmallPX = new System.Windows.Forms.Label();
            this.panel_drawingBig = new System.Windows.Forms.Panel();
            this.label_scaleInfo = new System.Windows.Forms.Label();
            this.button_zoomIn = new System.Windows.Forms.Button();
            this.button_zoomOut = new System.Windows.Forms.Button();
            this.label_locationBigMM = new System.Windows.Forms.Label();
            this.label_locationBigPX = new System.Windows.Forms.Label();
            this.panel_content = new System.Windows.Forms.Panel();
            this.checkBox_showSideDirek = new System.Windows.Forms.CheckBox();
            this.checkBox_showCenterDirek = new System.Windows.Forms.CheckBox();
            this.checkBox_showMafsal = new System.Windows.Forms.CheckBox();
            this.checkBox_showAksBirl = new System.Windows.Forms.CheckBox();
            this.checkBox_showProfile = new System.Windows.Forms.CheckBox();
            this.checkBox_showAsikW = new System.Windows.Forms.CheckBox();
            this.checkBox_showAsikZ = new System.Windows.Forms.CheckBox();
            this.checkBox_showPanel = new System.Windows.Forms.CheckBox();
            this.button_clear = new System.Windows.Forms.Button();
            this.label_info3 = new System.Windows.Forms.Label();
            this.label_info2 = new System.Windows.Forms.Label();
            this.button_applyPanelCount = new System.Windows.Forms.Button();
            this.label_deviceDPI = new System.Windows.Forms.Label();
            this.label_labelDeviceDPI = new System.Windows.Forms.Label();
            this.label_realDPI = new System.Windows.Forms.Label();
            this.label_labelRealDPI = new System.Windows.Forms.Label();
            this.button_configureRealDPI = new System.Windows.Forms.Button();
            this.label_info = new System.Windows.Forms.Label();
            this.label_locationGlobalBigPX = new System.Windows.Forms.Label();
            this.panel_header.SuspendLayout();
            this.panel_drawingSmall.SuspendLayout();
            this.panel_drawingBig.SuspendLayout();
            this.panel_content.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_header
            // 
            this.panel_header.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_header.Controls.Add(this.label1);
            this.panel_header.Controls.Add(this.button_configurations);
            this.panel_header.Location = new System.Drawing.Point(12, 12);
            this.panel_header.Name = "panel_header";
            this.panel_header.Size = new System.Drawing.Size(1260, 50);
            this.panel_header.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(270, 46);
            this.label1.TabIndex = 21;
            this.label1.Text = "Sollawer GES";
            // 
            // button_configurations
            // 
            this.button_configurations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_configurations.Location = new System.Drawing.Point(1171, 9);
            this.button_configurations.Name = "button_configurations";
            this.button_configurations.Size = new System.Drawing.Size(84, 31);
            this.button_configurations.TabIndex = 20;
            this.button_configurations.Text = "Configurations";
            this.button_configurations.UseVisualStyleBackColor = true;
            this.button_configurations.Click += new System.EventHandler(this.button_configurations_Click);
            // 
            // button_debug
            // 
            this.button_debug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_debug.Location = new System.Drawing.Point(1172, 754);
            this.button_debug.Name = "button_debug";
            this.button_debug.Size = new System.Drawing.Size(100, 23);
            this.button_debug.TabIndex = 19;
            this.button_debug.Text = "Debug";
            this.button_debug.UseVisualStyleBackColor = true;
            this.button_debug.Click += new System.EventHandler(this.button_debug_Click);
            // 
            // panel_drawingSmall
            // 
            this.panel_drawingSmall.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_drawingSmall.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel_drawingSmall.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_drawingSmall.Controls.Add(this.label_locationSmallMM);
            this.panel_drawingSmall.Controls.Add(this.label_locationSmallPX);
            this.panel_drawingSmall.Location = new System.Drawing.Point(12, 598);
            this.panel_drawingSmall.Name = "panel_drawingSmall";
            this.panel_drawingSmall.Size = new System.Drawing.Size(1260, 150);
            this.panel_drawingSmall.TabIndex = 2;
            this.panel_drawingSmall.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_drawingSmall_Paint);
            this.panel_drawingSmall.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel_drawingSmall_MouseClick);
            this.panel_drawingSmall.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_drawingSmall_MouseMove);
            // 
            // label_locationSmallMM
            // 
            this.label_locationSmallMM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_locationSmallMM.AutoSize = true;
            this.label_locationSmallMM.Location = new System.Drawing.Point(3, 126);
            this.label_locationSmallMM.Name = "label_locationSmallMM";
            this.label_locationSmallMM.Size = new System.Drawing.Size(49, 13);
            this.label_locationSmallMM.TabIndex = 13;
            this.label_locationSmallMM.Text = "0 / 0 mm";
            this.label_locationSmallMM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_locationSmallPX
            // 
            this.label_locationSmallPX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_locationSmallPX.AutoSize = true;
            this.label_locationSmallPX.Location = new System.Drawing.Point(178, 126);
            this.label_locationSmallPX.Name = "label_locationSmallPX";
            this.label_locationSmallPX.Size = new System.Drawing.Size(44, 13);
            this.label_locationSmallPX.TabIndex = 14;
            this.label_locationSmallPX.Text = "0 / 0 px";
            this.label_locationSmallPX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel_drawingBig
            // 
            this.panel_drawingBig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_drawingBig.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel_drawingBig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_drawingBig.Controls.Add(this.label_scaleInfo);
            this.panel_drawingBig.Controls.Add(this.button_zoomIn);
            this.panel_drawingBig.Controls.Add(this.button_zoomOut);
            this.panel_drawingBig.Controls.Add(this.label_locationBigMM);
            this.panel_drawingBig.Controls.Add(this.label_locationBigPX);
            this.panel_drawingBig.Location = new System.Drawing.Point(12, 211);
            this.panel_drawingBig.Margin = new System.Windows.Forms.Padding(0);
            this.panel_drawingBig.Name = "panel_drawingBig";
            this.panel_drawingBig.Size = new System.Drawing.Size(1260, 381);
            this.panel_drawingBig.TabIndex = 3;
            this.panel_drawingBig.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_drawingBig_Paint);
            this.panel_drawingBig.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_drawingBig_MouseMove);
            // 
            // label_scaleInfo
            // 
            this.label_scaleInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_scaleInfo.AutoSize = true;
            this.label_scaleInfo.Location = new System.Drawing.Point(1142, 358);
            this.label_scaleInfo.Name = "label_scaleInfo";
            this.label_scaleInfo.Size = new System.Drawing.Size(0, 13);
            this.label_scaleInfo.TabIndex = 7;
            this.label_scaleInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_zoomIn
            // 
            this.button_zoomIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_zoomIn.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.button_zoomIn.Location = new System.Drawing.Point(1061, 353);
            this.button_zoomIn.Name = "button_zoomIn";
            this.button_zoomIn.Size = new System.Drawing.Size(75, 23);
            this.button_zoomIn.TabIndex = 1;
            this.button_zoomIn.Text = "Zoom In";
            this.button_zoomIn.UseVisualStyleBackColor = false;
            this.button_zoomIn.Click += new System.EventHandler(this.button_zoomIn_Click);
            // 
            // button_zoomOut
            // 
            this.button_zoomOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_zoomOut.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.button_zoomOut.Location = new System.Drawing.Point(1180, 353);
            this.button_zoomOut.Name = "button_zoomOut";
            this.button_zoomOut.Size = new System.Drawing.Size(75, 23);
            this.button_zoomOut.TabIndex = 0;
            this.button_zoomOut.Text = "Zoom Out";
            this.button_zoomOut.UseVisualStyleBackColor = false;
            this.button_zoomOut.Click += new System.EventHandler(this.button_zoomOut_Click);
            // 
            // label_locationBigMM
            // 
            this.label_locationBigMM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_locationBigMM.AutoSize = true;
            this.label_locationBigMM.Location = new System.Drawing.Point(3, 358);
            this.label_locationBigMM.Name = "label_locationBigMM";
            this.label_locationBigMM.Size = new System.Drawing.Size(49, 13);
            this.label_locationBigMM.TabIndex = 5;
            this.label_locationBigMM.Text = "0 / 0 mm";
            this.label_locationBigMM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_locationBigPX
            // 
            this.label_locationBigPX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_locationBigPX.AutoSize = true;
            this.label_locationBigPX.Location = new System.Drawing.Point(178, 358);
            this.label_locationBigPX.Name = "label_locationBigPX";
            this.label_locationBigPX.Size = new System.Drawing.Size(44, 13);
            this.label_locationBigPX.TabIndex = 6;
            this.label_locationBigPX.Text = "0 / 0 px";
            this.label_locationBigPX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel_content
            // 
            this.panel_content.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_content.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_content.Controls.Add(this.checkBox_showSideDirek);
            this.panel_content.Controls.Add(this.checkBox_showCenterDirek);
            this.panel_content.Controls.Add(this.checkBox_showMafsal);
            this.panel_content.Controls.Add(this.checkBox_showAksBirl);
            this.panel_content.Controls.Add(this.checkBox_showProfile);
            this.panel_content.Controls.Add(this.checkBox_showAsikW);
            this.panel_content.Controls.Add(this.checkBox_showAsikZ);
            this.panel_content.Controls.Add(this.checkBox_showPanel);
            this.panel_content.Controls.Add(this.button_clear);
            this.panel_content.Controls.Add(this.label_info3);
            this.panel_content.Controls.Add(this.label_info2);
            this.panel_content.Controls.Add(this.button_applyPanelCount);
            this.panel_content.Location = new System.Drawing.Point(12, 68);
            this.panel_content.Name = "panel_content";
            this.panel_content.Size = new System.Drawing.Size(1260, 140);
            this.panel_content.TabIndex = 4;
            // 
            // checkBox_showSideDirek
            // 
            this.checkBox_showSideDirek.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_showSideDirek.AutoSize = true;
            this.checkBox_showSideDirek.Checked = true;
            this.checkBox_showSideDirek.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_showSideDirek.Location = new System.Drawing.Point(1165, 72);
            this.checkBox_showSideDirek.Name = "checkBox_showSideDirek";
            this.checkBox_showSideDirek.Size = new System.Drawing.Size(75, 17);
            this.checkBox_showSideDirek.TabIndex = 21;
            this.checkBox_showSideDirek.Text = "Side Direk";
            this.checkBox_showSideDirek.UseVisualStyleBackColor = true;
            this.checkBox_showSideDirek.CheckedChanged += new System.EventHandler(this.showComponentEventHandler);
            // 
            // checkBox_showCenterDirek
            // 
            this.checkBox_showCenterDirek.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_showCenterDirek.AutoSize = true;
            this.checkBox_showCenterDirek.Checked = true;
            this.checkBox_showCenterDirek.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_showCenterDirek.Location = new System.Drawing.Point(1165, 49);
            this.checkBox_showCenterDirek.Name = "checkBox_showCenterDirek";
            this.checkBox_showCenterDirek.Size = new System.Drawing.Size(85, 17);
            this.checkBox_showCenterDirek.TabIndex = 21;
            this.checkBox_showCenterDirek.Text = "Center Direk";
            this.checkBox_showCenterDirek.UseVisualStyleBackColor = true;
            this.checkBox_showCenterDirek.CheckedChanged += new System.EventHandler(this.showComponentEventHandler);
            // 
            // checkBox_showMafsal
            // 
            this.checkBox_showMafsal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_showMafsal.AutoSize = true;
            this.checkBox_showMafsal.Checked = true;
            this.checkBox_showMafsal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_showMafsal.Location = new System.Drawing.Point(1165, 26);
            this.checkBox_showMafsal.Name = "checkBox_showMafsal";
            this.checkBox_showMafsal.Size = new System.Drawing.Size(57, 17);
            this.checkBox_showMafsal.TabIndex = 21;
            this.checkBox_showMafsal.Text = "Mafsal";
            this.checkBox_showMafsal.UseVisualStyleBackColor = true;
            this.checkBox_showMafsal.CheckedChanged += new System.EventHandler(this.showComponentEventHandler);
            // 
            // checkBox_showAksBirl
            // 
            this.checkBox_showAksBirl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_showAksBirl.AutoSize = true;
            this.checkBox_showAksBirl.Checked = true;
            this.checkBox_showAksBirl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_showAksBirl.Location = new System.Drawing.Point(1165, 3);
            this.checkBox_showAksBirl.Name = "checkBox_showAksBirl";
            this.checkBox_showAksBirl.Size = new System.Drawing.Size(90, 17);
            this.checkBox_showAksBirl.TabIndex = 21;
            this.checkBox_showAksBirl.Text = "Aks Birlestirici";
            this.checkBox_showAksBirl.UseVisualStyleBackColor = true;
            this.checkBox_showAksBirl.CheckedChanged += new System.EventHandler(this.showComponentEventHandler);
            // 
            // checkBox_showProfile
            // 
            this.checkBox_showProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_showProfile.AutoSize = true;
            this.checkBox_showProfile.Checked = true;
            this.checkBox_showProfile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_showProfile.Location = new System.Drawing.Point(1051, 72);
            this.checkBox_showProfile.Name = "checkBox_showProfile";
            this.checkBox_showProfile.Size = new System.Drawing.Size(55, 17);
            this.checkBox_showProfile.TabIndex = 21;
            this.checkBox_showProfile.Text = "Profile";
            this.checkBox_showProfile.UseVisualStyleBackColor = true;
            this.checkBox_showProfile.CheckedChanged += new System.EventHandler(this.showComponentEventHandler);
            // 
            // checkBox_showAsikW
            // 
            this.checkBox_showAsikW.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_showAsikW.AutoSize = true;
            this.checkBox_showAsikW.Checked = true;
            this.checkBox_showAsikW.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_showAsikW.Location = new System.Drawing.Point(1051, 49);
            this.checkBox_showAsikW.Name = "checkBox_showAsikW";
            this.checkBox_showAsikW.Size = new System.Drawing.Size(60, 17);
            this.checkBox_showAsikW.TabIndex = 21;
            this.checkBox_showAsikW.Text = "Asik W";
            this.checkBox_showAsikW.UseVisualStyleBackColor = true;
            this.checkBox_showAsikW.CheckedChanged += new System.EventHandler(this.showComponentEventHandler);
            // 
            // checkBox_showAsikZ
            // 
            this.checkBox_showAsikZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_showAsikZ.AutoSize = true;
            this.checkBox_showAsikZ.Checked = true;
            this.checkBox_showAsikZ.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_showAsikZ.Location = new System.Drawing.Point(1051, 26);
            this.checkBox_showAsikZ.Name = "checkBox_showAsikZ";
            this.checkBox_showAsikZ.Size = new System.Drawing.Size(56, 17);
            this.checkBox_showAsikZ.TabIndex = 21;
            this.checkBox_showAsikZ.Text = "Asik Z";
            this.checkBox_showAsikZ.UseVisualStyleBackColor = true;
            this.checkBox_showAsikZ.CheckedChanged += new System.EventHandler(this.showComponentEventHandler);
            // 
            // checkBox_showPanel
            // 
            this.checkBox_showPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_showPanel.AutoSize = true;
            this.checkBox_showPanel.Checked = true;
            this.checkBox_showPanel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_showPanel.Location = new System.Drawing.Point(1051, 3);
            this.checkBox_showPanel.Name = "checkBox_showPanel";
            this.checkBox_showPanel.Size = new System.Drawing.Size(53, 17);
            this.checkBox_showPanel.TabIndex = 21;
            this.checkBox_showPanel.Text = "Panel";
            this.checkBox_showPanel.UseVisualStyleBackColor = true;
            this.checkBox_showPanel.CheckedChanged += new System.EventHandler(this.showComponentEventHandler);
            // 
            // button_clear
            // 
            this.button_clear.Location = new System.Drawing.Point(109, 112);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(100, 23);
            this.button_clear.TabIndex = 20;
            this.button_clear.Text = "Clear";
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
            // 
            // label_info3
            // 
            this.label_info3.AutoSize = true;
            this.label_info3.Location = new System.Drawing.Point(106, 0);
            this.label_info3.Name = "label_info3";
            this.label_info3.Size = new System.Drawing.Size(24, 13);
            this.label_info3.TabIndex = 2;
            this.label_info3.Text = "info";
            // 
            // label_info2
            // 
            this.label_info2.AutoSize = true;
            this.label_info2.Location = new System.Drawing.Point(3, 0);
            this.label_info2.Name = "label_info2";
            this.label_info2.Size = new System.Drawing.Size(24, 13);
            this.label_info2.TabIndex = 2;
            this.label_info2.Text = "info";
            // 
            // button_applyPanelCount
            // 
            this.button_applyPanelCount.Location = new System.Drawing.Point(3, 112);
            this.button_applyPanelCount.Name = "button_applyPanelCount";
            this.button_applyPanelCount.Size = new System.Drawing.Size(100, 23);
            this.button_applyPanelCount.TabIndex = 1;
            this.button_applyPanelCount.Text = "Apply";
            this.button_applyPanelCount.UseVisualStyleBackColor = true;
            this.button_applyPanelCount.Click += new System.EventHandler(this.button_applyPanelCount_Click);
            // 
            // label_deviceDPI
            // 
            this.label_deviceDPI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_deviceDPI.AutoSize = true;
            this.label_deviceDPI.Location = new System.Drawing.Point(187, 759);
            this.label_deviceDPI.Name = "label_deviceDPI";
            this.label_deviceDPI.Size = new System.Drawing.Size(13, 13);
            this.label_deviceDPI.TabIndex = 8;
            this.label_deviceDPI.Text = "0";
            // 
            // label_labelDeviceDPI
            // 
            this.label_labelDeviceDPI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_labelDeviceDPI.AutoSize = true;
            this.label_labelDeviceDPI.Location = new System.Drawing.Point(116, 759);
            this.label_labelDeviceDPI.Name = "label_labelDeviceDPI";
            this.label_labelDeviceDPI.Size = new System.Drawing.Size(65, 13);
            this.label_labelDeviceDPI.TabIndex = 7;
            this.label_labelDeviceDPI.Text = "Device DPI:";
            // 
            // label_realDPI
            // 
            this.label_realDPI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_realDPI.AutoSize = true;
            this.label_realDPI.Location = new System.Drawing.Point(289, 759);
            this.label_realDPI.Name = "label_realDPI";
            this.label_realDPI.Size = new System.Drawing.Size(13, 13);
            this.label_realDPI.TabIndex = 10;
            this.label_realDPI.Text = "0";
            // 
            // label_labelRealDPI
            // 
            this.label_labelRealDPI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_labelRealDPI.AutoSize = true;
            this.label_labelRealDPI.Location = new System.Drawing.Point(230, 759);
            this.label_labelRealDPI.Name = "label_labelRealDPI";
            this.label_labelRealDPI.Size = new System.Drawing.Size(53, 13);
            this.label_labelRealDPI.TabIndex = 9;
            this.label_labelRealDPI.Text = "Real DPI:";
            // 
            // button_configureRealDPI
            // 
            this.button_configureRealDPI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_configureRealDPI.Location = new System.Drawing.Point(344, 754);
            this.button_configureRealDPI.Name = "button_configureRealDPI";
            this.button_configureRealDPI.Size = new System.Drawing.Size(116, 23);
            this.button_configureRealDPI.TabIndex = 11;
            this.button_configureRealDPI.Text = "Configure Real DPI";
            this.button_configureRealDPI.UseVisualStyleBackColor = true;
            // 
            // label_info
            // 
            this.label_info.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_info.AutoSize = true;
            this.label_info.Location = new System.Drawing.Point(523, 759);
            this.label_info.Name = "label_info";
            this.label_info.Size = new System.Drawing.Size(24, 13);
            this.label_info.TabIndex = 12;
            this.label_info.Text = "info";
            this.label_info.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_locationGlobalBigPX
            // 
            this.label_locationGlobalBigPX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_locationGlobalBigPX.AutoSize = true;
            this.label_locationGlobalBigPX.Location = new System.Drawing.Point(16, 759);
            this.label_locationGlobalBigPX.Name = "label_locationGlobalBigPX";
            this.label_locationGlobalBigPX.Size = new System.Drawing.Size(44, 13);
            this.label_locationGlobalBigPX.TabIndex = 15;
            this.label_locationGlobalBigPX.Text = "0 / 0 px";
            this.label_locationGlobalBigPX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 781);
            this.Controls.Add(this.label_locationGlobalBigPX);
            this.Controls.Add(this.label_info);
            this.Controls.Add(this.button_debug);
            this.Controls.Add(this.button_configureRealDPI);
            this.Controls.Add(this.label_realDPI);
            this.Controls.Add(this.label_labelRealDPI);
            this.Controls.Add(this.label_deviceDPI);
            this.Controls.Add(this.label_labelDeviceDPI);
            this.Controls.Add(this.panel_content);
            this.Controls.Add(this.panel_drawingBig);
            this.Controls.Add(this.panel_drawingSmall);
            this.Controls.Add(this.panel_header);
            this.MinimumSize = new System.Drawing.Size(1300, 820);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panel_header.ResumeLayout(false);
            this.panel_header.PerformLayout();
            this.panel_drawingSmall.ResumeLayout(false);
            this.panel_drawingSmall.PerformLayout();
            this.panel_drawingBig.ResumeLayout(false);
            this.panel_drawingBig.PerformLayout();
            this.panel_content.ResumeLayout(false);
            this.panel_content.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_header;
        private System.Windows.Forms.Panel panel_drawingSmall;
        private System.Windows.Forms.Panel panel_drawingBig;
        private System.Windows.Forms.Panel panel_content;
        private System.Windows.Forms.Label label_deviceDPI;
        private System.Windows.Forms.Label label_labelDeviceDPI;
        private System.Windows.Forms.Label label_locationBigPX;
        private System.Windows.Forms.Label label_locationBigMM;
        private System.Windows.Forms.Label label_realDPI;
        private System.Windows.Forms.Label label_labelRealDPI;
        private System.Windows.Forms.Button button_configureRealDPI;
        private System.Windows.Forms.Label label_info;
        private System.Windows.Forms.Button button_zoomIn;
        private System.Windows.Forms.Button button_zoomOut;
        private System.Windows.Forms.Label label_locationSmallPX;
        private System.Windows.Forms.Label label_locationSmallMM;
        private System.Windows.Forms.Button button_applyPanelCount;
        private System.Windows.Forms.Label label_locationGlobalBigPX;
        private System.Windows.Forms.Label label_info2;
        private System.Windows.Forms.Button button_debug;
        private System.Windows.Forms.Label label_scaleInfo;
        private System.Windows.Forms.Button button_clear;
        private System.Windows.Forms.Button button_configurations;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_info3;
        private System.Windows.Forms.CheckBox checkBox_showPanel;
        private System.Windows.Forms.CheckBox checkBox_showSideDirek;
        private System.Windows.Forms.CheckBox checkBox_showCenterDirek;
        private System.Windows.Forms.CheckBox checkBox_showMafsal;
        private System.Windows.Forms.CheckBox checkBox_showAksBirl;
        private System.Windows.Forms.CheckBox checkBox_showProfile;
        private System.Windows.Forms.CheckBox checkBox_showAsikW;
        private System.Windows.Forms.CheckBox checkBox_showAsikZ;
    }
}


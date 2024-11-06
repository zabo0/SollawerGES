using SollawerGES.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SollawerGES
{
    public partial class FormConfigurations : Form
    {
        public FormConfigurations()
        {
            InitializeComponent();
        }

        private void FormConfigurations_Load(object sender, EventArgs e)
        {
            Configurations.readConfiguration();

            checkBox_animation.Checked = Configurations.Animation;
            textBox_animationDelayTime.Text = Configurations.AnimationDelayTime.ToString();

            textBox_widthPanel.Text = Configurations.WidthPanel.ToString();
            textBox_heightPanel.Text = Configurations.HeightPanel.ToString();
            textBox_spacePanel.Text = Configurations.SpacePanel.ToString();
            textBox_countPanel.Text = Configurations.CountPanel.ToString();

            textBox_widthAsikZ.Text = Configurations.WidthZAsik.ToString();
            textBox_heightAsikZ.Text = Configurations.HeightZAsik.ToString();

            textBox_widthAsikW.Text = Configurations.WidthWAsik.ToString();
            textBox_heightAsikW.Text = Configurations.HeightWAsik.ToString();

            textBox_heightProfil.Text = Configurations.HeightProfil.ToString();
            textBox_spaceProfil.Text = Configurations.SpaceProfil.ToString();

            textBox_widthAksBirl.Text = Configurations.WidthAksBirl.ToString();
            textBox_heightAksBirl.Text = Configurations.HeightAksBirl.ToString();

            textBox_widthMafsal.Text = Configurations.WidthMafsal.ToString();
            textBox_heightMafsal.Text = Configurations.HeightMafsal.ToString();
            textBox_actuatorDistanceMafsal.Text = Configurations.ActuatorDistanceMafsal.ToString();

            textBox_widthDirek.Text = Configurations.WidthDirek.ToString();
            textBox_heightDirek.Text = Configurations.HeightDirek.ToString();

            
        }

        private void button_configurationsCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormConfigurations_Shown(object sender, EventArgs e)
        {
            button_configurationsCancel.Focus();
        }

        private void button_configurationsSave_Click(object sender, EventArgs e)
        {
            Configurations.Animation = checkBox_animation.Checked;
            Configurations.AnimationDelayTime = int.Parse(textBox_animationDelayTime.Text);

            Configurations.WidthPanel = double.Parse(textBox_widthPanel.Text);
            Configurations.HeightPanel = double.Parse(textBox_heightPanel.Text);
            Configurations.SpacePanel = double.Parse(textBox_spacePanel.Text);
            Configurations.CountPanel = int.Parse(textBox_countPanel.Text);

            Configurations.WidthZAsik = double.Parse(textBox_widthAsikZ.Text);
            Configurations.HeightZAsik = double.Parse(textBox_heightAsikZ.Text);

            Configurations.WidthWAsik = double.Parse(textBox_widthAsikW.Text);
            Configurations.HeightWAsik = double.Parse(textBox_heightAsikW.Text);

            Configurations.HeightProfil = double.Parse(textBox_heightProfil.Text);
            Configurations.SpaceProfil = double.Parse(textBox_spaceProfil.Text);

            Configurations.WidthAksBirl = double.Parse(textBox_widthAksBirl.Text);
            Configurations.HeightAksBirl = double.Parse(textBox_heightAksBirl.Text);

            Configurations.WidthMafsal = double.Parse(textBox_widthMafsal.Text);
            Configurations.HeightMafsal = double.Parse(textBox_heightMafsal.Text);
            Configurations.ActuatorDistanceMafsal = double.Parse(textBox_actuatorDistanceMafsal.Text);

            Configurations.WidthDirek = double.Parse(textBox_widthDirek.Text);
            Configurations.HeightDirek = double.Parse(textBox_heightDirek.Text);


            Configurations.writeConfigurations();
            this.Close();
        }

        private void button_exportPDF_Click(object sender, EventArgs e)
        {

            List<bool> checkList = new List<bool>();
            checkList.Add(checkBox_exportPanels.Checked);
            checkList.Add(checkBox_exportAsikZ.Checked);
            checkList.Add(checkBox_exportAsikW.Checked);
            checkList.Add(checkBox_exportProfil.Checked);
            checkList.Add(checkBox_exportAksBirl.Checked);
            checkList.Add(checkBox_exportMafsal.Checked);
            checkList.Add(checkBox_exportCenterDirek.Checked);
            checkList.Add(checkBox_exportSideDirek.Checked);


            PDFProcess pdfProcess = new PDFProcess("denemePDF", "C:\\Users\\scice\\Desktop\\sollawerGESPDF\\output.pdf", Components.Components.getComponentList(checkList));
        }
    }
}

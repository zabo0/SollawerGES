using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Advanced;
using SollawerGES.Components;
using SollawerGES.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SollawerGES.Utils
{
    public class PDFProcess
    {

		private PdfDocument pdfDocument;
        private string fileName;
        private string title;

        private List<List<Entities.Rectengle>> componentList;

		private XGraphics gfx;
        private PdfPage pdfPage;



        public PDFProcess(string title, string fileName, List<List<Entities.Rectengle>> componentList)
		{
			this.Title = title;
			this.FileName = fileName;

            this.componentList = componentList;

			initialize();
            saveToPDF();
		}

		private void initialize()
		{
            pdfDocument = new PdfDocument();
            pdfDocument.Info.Title = title;
            
            pdfPage = pdfDocument.AddPage();
            pdfPage.Size = PageSize.A4;
            pdfPage.Orientation = PageOrientation.Landscape;

            gfx = XGraphics.FromPdfPage(pdfPage);
        }

        public void saveToPDF()
        {
            try
            {
                DrawGraphics(gfx);
                pdfDocument.Save(fileName);
                OpenPdf(fileName);
            }
            catch(Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message + "\n\n Would you like set a new path?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {
                    OpenFileDialog openFileDialog1 = new OpenFileDialog();
                    DialogResult result1 = openFileDialog1.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        fileName = openFileDialog1.FileName;
                        saveToPDF();
                    }
                }
                else if (result == DialogResult.No)
                {
                }
            }
        }

        private void DrawGraphics(XGraphics gfx)
        {
            foreach(List<Entities.Rectengle> components in componentList)
            {
                foreach (Entities.Rectengle rectengle in components)
                {

                    double baseOrigin_X = pdfPage.Width / 2;
                    double baseOrigin_Y = pdfPage.Height / 2;

                    double posX;
                    double posY;

                    double scale = 70;


                    posX = baseOrigin_X + rectengle.CenterPosition.X.toPT(scale) - (rectengle.Width / 2).toPT(scale); //dikdortgenin baslangic noktalari
                    posY = baseOrigin_Y - rectengle.CenterPosition.Y.toPT(scale) - (rectengle.Height / 2).toPT(scale);

                    double rWidth = rectengle.Width.toPT(scale); //dikdortgenin genislik ve yuksekligi
                    double rHeight = rectengle.Height.toPT(scale);

                    XPen pen = new XPen(XColors.Black, 0.05);
                    gfx.DrawRectangle(pen, posX, posY, rWidth, rHeight);
                }
            }
        }

        private void OpenPdf(string fileName)
        {
            try
            {
                Process.Start(new ProcessStartInfo(fileName) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("PDF açılırken bir hata oluştu: " + ex.Message);
            }
        }


        public PdfDocument PdfDocument
		{
			get { return pdfDocument; }
			set { pdfDocument = value; }
		}

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

    }
}

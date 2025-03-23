using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace L1_Mini
{
    public partial class ViewDocs : Form
    {
        public ViewDocs()
        {
            InitializeComponent();
        }

        private void ViewDocs_Load(object sender, EventArgs e)
        {
            string docName = @"./Resources/MAU_STT_NHIETDOI.docx"; 
            printDocument1.DocumentName = docName;

            //using (FileStream stream = new FileStream(docPath + docName, FileMode.Open))
            //using (StreamReader reader = new StreamReader(stream))
            //{
            //    documentContents = reader.ReadToEnd();
            //}
            //stringToPrint = documentContents;


            PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog(); ;
        printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }
    }
}

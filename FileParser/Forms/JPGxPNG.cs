using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileParser.DedicClasses;

namespace FileParser.Forms
{
    public partial class JPGxPNG : Form
    {
        public JPGxPNG()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = FBD.SelectedPath;

            }
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Get the folder path from the command line arguments
            string folderPath = this.textBox1.Text;

            Converters.jpgToPng(folderPath);
        }
    }
}

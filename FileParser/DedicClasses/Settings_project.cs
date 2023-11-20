using System;
using System.Windows.Forms;
using System.IO;

namespace FileParser
{
    public partial class Settings_project : Form
    {
        string delim = "\r\n";
        public Settings_project()
        {
            InitializeComponent();
            string base_out_path = Properties.Settings.Default.basepath;

            
            
            textBox1.Text = base_out_path;
            
            if (!Directory.Exists(base_out_path))
            { //Directory.Delete(base_out_path, true);
                Directory.CreateDirectory(base_out_path);
            }
            

            

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = FBD.SelectedPath;
                //записываем настройки
                Properties.Settings.Default.basepath = this.textBox1.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void btnSaveAtype_Click(object sender, EventArgs e)
        {

            System.Collections.Specialized.StringCollection coll1 = new System.Collections.Specialized.StringCollection();
            
           
            MessageBox.Show("Изменения сохранены");
        }

        private void btnSaveIgnore_Click(object sender, EventArgs e)
        {
            System.Collections.Specialized.StringCollection coll1 = new System.Collections.Specialized.StringCollection();
            
            
            Properties.Settings.Default.Save();
            MessageBox.Show("Изменения сохранены");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "xlsx files (*.xlsx)|*.xlsx";
            if (OPF.ShowDialog() == DialogResult.OK)
            {
                
               
                
                Properties.Settings.Default.Save();

            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
            Properties.Settings.Default.Save();
        }

        private void chBxRemote_CheckedChanged(object sender, EventArgs e)
        {

           
        }

        private void chDwlOnLd_CheckedChanged(object sender, EventArgs e)
        {

            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click1(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            
        }
    }
}

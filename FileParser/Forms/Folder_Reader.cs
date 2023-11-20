using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using FileParser.DedicClasses;



namespace FileParser
{
    public partial class Folder_Reader : Form
    {
        string out_full_path_name = Properties.Settings.Default.fullpaths_csv;
        string out_name = Properties.Settings.Default.names_csv;

        FolderReader FolderReader = new FolderReader();
        public Folder_Reader()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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
            var targetDirectory = textBox1.Text;
            string[][] m = FolderReader.ProcessDirectory(targetDirectory);
            this.label2.Text = "Количество найденных файлов: " + m[0].Length.ToString();

            // Вываливаем все в CSV
            FileSaver.CSV_writer(m[0], out_name);
            FileSaver.CSV_writer(m[1], out_full_path_name);

            MessageBox.Show("Готово, результат в папке " + Properties.Settings.Default.basepath);
            button3.Visible = true;
            //Откроем файл с результатом
            //Process.Start(new ProcessStartInfo { FileName = "explorer", Arguments = $"/n,/select,{Properties.Settings.Default.basepath + @"\" + out_full_path_name}" });
            WinActions.OpenRes(Properties.Settings.Default.basepath + @"\" + out_full_path_name);
        }


        private void button3_Click(object sender, EventArgs e)
        {

            WinActions.OpenRes(Properties.Settings.Default.basepath + @"\" + out_full_path_name);
        }

        private void button4_Click(object sender, EventArgs e)
        {
           

            
        }
    }

}

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Excel = Microsoft.Office.Interop.Excel;

namespace FileParser
{
    public partial class MassRenamer : Form
    {
        public MassRenamer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "csv files (*.csv)|*.csv";
            if (OPF.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = OPF.FileName;
                this.button2.Enabled = true;

            }
        }
        //
        private void button2_Click(object sender, EventArgs e)
        {
            List<string> old_name = new List<string>();
            List<string> new_name = new List<string>();
            string[] str = { "\r\n" };
            using (StreamReader rd = new StreamReader(new FileStream(this.textBox1.Text, FileMode.Open), Encoding.GetEncoding("Windows-1251")))
            {
                str = rd.ReadToEnd().Split(str, StringSplitOptions.RemoveEmptyEntries);
            }
            //собираем в цикле список файлов для переименования
            for (int i = 0; i < str.Length; i++)
            {
                string[] words = str[i].Split(';');
                old_name.Add(words[0]);
                new_name.Add(words[1]);
            }


            for (int i = 0; i < old_name.Count; i++)
            {
                string directoryName = Path.GetDirectoryName(old_name[i]);
                new_name[i] = directoryName + @"\" + new_name[i];
                //чет делаем )
                //helloworld.MessageBox.Show("меняем " + old_name[i] + " на " + new_name[i]);
                System.IO.File.Move(old_name[i], new_name[i]);
            }
            MessageBox.Show("Готово. " + old_name.Count + " имен заменили.");
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Collections;

namespace FileParser
{
    public partial class URLExtractor : Form
    {
        public URLExtractor()
        {
            InitializeComponent();
            textBox1.TextChanged += textBox1_TextChanged;
            checkBox1.Text += Properties.Settings.Default.basepath + @"\download\";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //формируем массив с url
            string look = textBox1.Text;
            string delim = "\r\n";
            string[] looks = look.Split(new string[] { delim }, StringSplitOptions.RemoveEmptyEntries);

            Cache.saveUrls(looks, "download", checkBox1.Checked);

            //елси выходная папка не существует, создаем
            //DirectoryInfo dirInfo = new DirectoryInfo(Properties.Settings.Default.basepath + @"\download\");
            //if (!dirInfo.Exists)
            //{
            //    dirInfo.Create();
            //}
            //// Проверяем, нужно ли очищать папку
            //if (checkBox1.Checked == true)
            //{

            //    foreach (FileInfo file in dirInfo.GetFiles())
            //    {
            //        file.Delete();
            //    }
            //}

            //Console.WriteLine(dirInfo.ToString());

            //// пробегаем по каждому url и сохраняем
            //int count = 0;
            //foreach (string url in looks)
            //{
            //    try
            //    {
            //        Uri uri = new Uri(url);
            //        //получаем имя файла по URL
            //        string filename = System.IO.Path.GetFileName(uri.AbsolutePath);

            //        //записываем абсолютный путь файла, который будем сохранять у себя
            //        string fpath = dirInfo.ToString() + filename;

            //        WebClient webClient = new WebClient();
            //        webClient.DownloadFile(url, fpath);
            //        count++;
            //    }
            //    catch
            //    {
            //        MessageBox.Show("Что-то не то со списком url для скачивания");
            //    }
            //}

            //MessageBox.Show("Готово. Скачано " + count + " файлов в папку " + Properties.Settings.Default.basepath + @"\download\");

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
    }
}

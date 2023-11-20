using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using FileParser.DedicClasses;

namespace FileParser
{
    public partial class FolderSearcher : Form
    {
        string fldpath = Properties.Settings.Default.basepath + @"\result\"; // настрйка - базовая папка
        public FolderSearcher()
        {
            InitializeComponent();
            this.Size = new Size(820, 125);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)

        {
            Converters convert = new Converters();
            var targetDirectory = textBox1.Text;
            FolderReader FolderReader = new FolderReader();

            //doubles - в этот список будем записывать дубли
            List<string> doubles = new List<string>();

            string[][] m = FolderReader.ProcessDirectory(targetDirectory);
            string look = textBox2.Text;

            //задаем разделитель поля для поиска
            string delim = "\r\n";
            string[] looks = look.Split(new string[] { delim }, StringSplitOptions.RemoveEmptyEntries);

            int[] result = FolderReader.Searcher(looks, m[0]);

            
            if (checkBox1.Checked)
            {
                //елси выходная папка не существует, создаем
                DirectoryInfo dirInfo = new DirectoryInfo(this.fldpath);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }

                //Если указано очищать предварительно, то ощищаем и сразу создаем 
                if (checkBox2.Checked)
                {

                    dirInfo.Delete(true);
                    System.Threading.Thread.Sleep(150);
                    dirInfo.Create();

                }


            }
            //Пробегаемся по всем результатам по индексам в result
            int ry = 0;
            foreach (int i in result)
            {
                if (ry < 300) { 
                    richTextBox1.Text += m[1][i] + "\r\n";
                }
                // если стоит галочка, то копируем файлы туда
                if (checkBox1.Checked)
                {
                    // MessageBox.Show("Галочка установлена");

                    FileInfo fileInfo = new FileInfo(m[1][i]);
                    FileInfo fileInfo1 = new FileInfo(fldpath + fileInfo.Name);
                    if (!File.Exists(fileInfo1.FullName))
                    {
                        try {

                            if (chCleanSuxx.Checked) {
                                //запишем файл только если конечный файл не имеет суффикс
                                bool suff_exist = convert.contain_suffix(fileInfo1.FullName);
                                if (!suff_exist) fileInfo.CopyTo(fldpath + fileInfo.Name);
                            }
                            else {

                                //записываем файл
                                fileInfo.CopyTo(fldpath + fileInfo.Name);
                            }
                             
                        
                        
                        }

                        catch
                        {
                            MessageBox.Show("Какие-то проблемы с копированием:" + fldpath + fileInfo.Name);
                        }

                    }
                    else
                    {  //если такой файл уже есть, то записываем пусть дубля в список
                        doubles.Add(fileInfo.FullName);

                    }


                }
                ry++;   

            }
            // check if thereeare repeats of filenames
            if (doubles.Count > 0)
            {
                //if true make a list with double paths
                string[] doubles_arr = doubles.ToArray();
                FileSaver.whrite_files(doubles_arr, Properties.Settings.Default.doubles_file_name);
                MessageBox.Show("Нашлись дубли в указанной папке. Их пути записаны в файле " + Properties.Settings.Default.basepath + @"\" + Properties.Settings.Default.doubles_file_name);
            }

            this.label3.Text = "Количество найденных файлов: " + result.Length.ToString();
            FolderReader = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = FBD.SelectedPath;

            }
            button2.Enabled = true;
            checkBox1.Enabled = true;
            checkBox1.Text = "Скопировать результат в " + fldpath;
            this.Size = new Size(820, 345);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void chCleanSuxx_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

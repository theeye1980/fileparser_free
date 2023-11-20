using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using FileParser.DedicClasses;
using FileParser.Forms;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;


namespace FileParser
{
    public partial class MainForm : Form
    {
        public string chosen_opt = "rBtnRead"; //задаем выбранную по умолчанию опцию 
        public string[] description = new string[]{ "Вывести все пути или только имена файлов в csv файлик. Возможнен рекурсивный обход, либо только в выбранной папке.",
                                                 "По заданным ключвым словам можно найти файлы в указанной папке (или подпапках). Результат можно вывести на экран или экспортировать в файл.",
                                                "На входе файл со ссылками http. Можно скачать файлы. Опционально можно сохранить их имена или присвоить новые.",
                                                 "Удалить список файлов из выбранной папки или подпапок. Без рекурсивного обхода.",
                                                "Переименовать файлы. На входе csv, c 2 колонками: в левой адреса, в правой новые имена файлов с расширением. Разрешено переименовывать только при сохранении расширения файла. Разделитель ; и значения без кавычек (можно проверить текстовым редактором). ",
                                                "Разобрать стандартный YML по формату Яндекс и сохранить в csv в папку, указанную в настройках",
                                                "Универсальный парсер XML, позволяет полностью разобрать произвольные поля в XML документе",
                                                "По имени файла, определяет валидный артикул. На входе csv c колонкой только имен файлов"};

        public MainForm()
        {
            InitializeComponent();
            //InitializeTimer();
            //Создаем папку с выходными данными, если еще не создана

        }

        
        private void radioButton1_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton2_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton3_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton4_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton5_CheckedChanged(object sender, EventArgs e) { }

        private void button2_Click(object sender, EventArgs e)
        {
            //  Нескомпилированное приложение перестанет работать с 
            DateTime date1 = new DateTime(2026, 9, 16); 
            var periodNow = DateTime.Now;
            if (periodNow < date1)
            {
                
            }
            else
            {
                MessageBox.Show("срок действия программы закончился. Свяжитесь с разработчиком");
                Application.Exit();
            }
            switch (chosen_opt as string)
            {

                case "rBtnRead":
                    Folder_Reader form2 = new Folder_Reader
                    {
                        Visible = true
                    };
                    break;
                case "rBtnSearch":
                    FolderSearcher form3 = new FolderSearcher
                    {
                        Visible = true
                    };
                    break;
                case "rBtnDwld":
                    URLExtractor form5 = new URLExtractor
                    {
                        Visible = true
                    };
                    break;
                case "rBtnRename":
                    MassRenamer form4 = new MassRenamer
                    {
                        Visible = true
                    };
                    break;
                case "rBtnXML":
                    Form6 form6 = new Form6
                    {
                        Visible = true
                    };
                    break;
                case "rBtnXMLuniverse":
                    frmUniversalXMLParser frmUniversalXMLParser = new frmUniversalXMLParser
                    {
                        Visible = true
                    };
                    break;
                
                default:
                    //код, выполняемый если выражение не имеет ни одно из выше указанных значений
                    break;
            }
        }

 

        private void главнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(Properties.Settings.Default.basepath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.basepath);
                Console.WriteLine("Folder created successfully.");
            }
            else
            {
                Console.WriteLine("Folder already exists.");
            }
        }
        private void Form1_Closing(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void песочницаToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void label2_Click(object sender, EventArgs e)
        {
            //label2.Text = (sender as RadioButton).Text + (sender as RadioButton).Name;
            chosen_opt = (sender as RadioButton).Name;
            switch ((sender as RadioButton).Name as string)
            {

                case "rBtnRead":
                    richTextBox2.Text = description[0];
                    break;
                case "rBtnSearch":
                    richTextBox2.Text = description[1];
                    break;
                case "rBtnDwld":
                    richTextBox2.Text = description[2];
                    break;
                case "rBtnRename":
                    richTextBox2.Text = description[4];
                    break;
                case "rBtnXML":
                    richTextBox2.Text = description[5];
                    break;
                case "rBtnXMLuniverse":
                    richTextBox2.Text = description[6];
                    break;
                case "rBtnValidArts":
                    richTextBox2.Text = description[7];
                    break;
                default:
                    //код, выполняемый если выражение не имеет ни одно из выше указанных значений
                    break;
            }
        }

       
        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Settings_project = new Settings_project
            {
                Visible = true
            };
        }

        
        

        private void btnShowBasePath_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = "explorer", Arguments = $"/n,/select,{Properties.Settings.Default.basepath}" });
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form about = new About
            {
                Visible = true
            };
        }
    }

}

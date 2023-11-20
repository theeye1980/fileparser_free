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


//using Excel = Microsoft.Office.Interop.Excel;

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

        private DataGridView MDGW;
        private Grids mgrid;
        // зададим колонки
        DataGridViewColumn column2 = new DataGridViewColumn();
        DataGridViewColumn column1 = new DataGridViewColumn();
        DataGridViewColumn column3 = new DataGridViewColumn();

        

        public MainForm()
        {
            InitializeComponent();
            
            
            InitializeTimer();
            

        }

        
        private void radioButton1_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton2_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton3_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton4_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton5_CheckedChanged(object sender, EventArgs e) { }

        private void button2_Click(object sender, EventArgs e)
        {
            // Вставляем защиту - нескомпилированное приложение перестанет работать с 1 февраля
            DateTime date1 = new DateTime(2024, 9, 16); // до этого дня все еще будет работать
            var periodNow = DateTime.Now;
            if (periodNow < date1)
            {
                //MessageBox.Show("Порядок");
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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void главнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.chDwlOnLd) {  //проводим загрузку файлов, если установлен чекбокс
                try
                {
                    // папка все о товаре недоступна if (!Properties.Settings.Default.remote_usage) FileInfoGetter.ScanAllAboutGoodFld(false);
                    FileInfoGetter.ScanAllAboutGoodFldRemote();
                }
                catch (Exception ex) { MessageBox.Show("Ошибка при сканировании файлов" + ex.Message); }

                try { 
                    //reports.FormFileReportv2_parallel(false);
                    GetArticlesList.GetAllArticles1C(false);
               
                } catch { MessageBox.Show("Ошибка при загрузке артикулов 1С"); }
                
                try
                {
                    //Временно выключаем возможность получить информацию по моделям
                   // MLModelsGetter.IsTorchGetInfo(false);
                } catch { MessageBox.Show("Ошибка при получении информации моделей машинного обучения"); }

                try {

                    Cache.dwlContentInternalFld();

                } catch (Exception ex) { MessageBox.Show("Не удалось сохранить файлы отдела Контента" + ex.Message); }
            }

            if (Properties.Settings.Default.remote_usage)
            {
                //убираем возможности удаленного пользователя
                
                
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

        private void кПродажеНаМаркетплейсахИз1СToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void дашбоардToolStripMenuItem_Click(object sender, EventArgs e)
        {
                       
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Settings_project = new Settings_project
            {
                Visible = true
            };
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            

        }

        
        

        private void btnShowBasePath_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = "explorer", Arguments = $"/n,/select,{Properties.Settings.Default.basepath}" });
        }


        private void данныеПоФайлампараллельныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void записатьНовыйTempofarticlesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            GetArticlesList.GetAllArticles1C();

        }

        private void обновитьФайлыВсеОТовареToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfoGetter.ScanAllAboutGoodFld();
            MessageBox.Show("Скан запущен, займет примерно минуту. По мере готовности выскочит уведомление.");
        }

        private void данныеПоФайламартикулыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnRenewDates_Click(object sender, EventArgs e)
        {
           
        }

        private void выгрузкаПоЦенамостаткамИДоступностиССайтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e) // открытие в папке выбранного файла
        {
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            

        }
        private void InitializeTimer()
        {
            // Run this procedure in an appropriate event.  

            timer1.Interval = 30000;
            timer1.Enabled = true;
            // Hook up timer's tick event handler.  
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
        }

        private void получитьВходныеДанныеДляМодерацииСвечекToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void маркетплейсыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void основныеКомандыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void указатьФайлМодерацииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void загрузитьФайлыОтделаКонтентаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cache.dwlContentInternalFld();
        }

        private void pNG300x300ИзПапкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JPGxPNG jPGxPNG = new JPGxPNG
            {
                Visible = true
            };
        }

        private void обновитьФайлыМинифотобанкатолькоДляРазработчиковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }
    }

}

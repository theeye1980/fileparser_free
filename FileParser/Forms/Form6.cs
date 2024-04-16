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
using System.Xml;
using FileParser.DedicClasses;
using Excel = Microsoft.Office.Interop.Excel;

namespace FileParser
{
    public partial class Form6 : Form
    {
        protected string path = "";
        protected string csvFilePath = "";
        protected string StructurePath = "";
        public string[][] categories = new string[3][];
        public string[] distinct_arr;
        // подключаем объекты для управления сеткой
        OpenFileDialog OPF = new OpenFileDialog();
        Grids gr = new Grids();
        DataGridViewColumn column1 = new DataGridViewColumn();
        DataGridViewColumn column2 = new DataGridViewColumn();
        DataGridViewColumn column3 = new DataGridViewColumn();
        XmlDocument doc = new XmlDocument();
        public Form6()
        {
            InitializeComponent();
            gr.set_colump(column1, "свойства", 100, false, "name", false);
            gr.set_colump(column2, "N", 40, false, "num", true);
            //gr.set_colump(column3, "Значение", 190, false, "value", false);

            dataGridView1.Columns.Add(column2);
            dataGridView1.Columns.Add(column1);
            //dataGridView1.Columns.Add(column3);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                OPF.Filter = "XML and YML files (*.xml;*.yml)|*.xml;*.yml";
                if (OPF.ShowDialog() == DialogResult.OK)
                {
                    this.textBox1.Text = OPF.FileName;
                    this.path = OPF.FileName;

                    //переведем содержимое sad ;&Amp вdfdsf нижний регистр

                    string str = string.Empty;
                    using (System.IO.StreamReader reader = System.IO.File.OpenText(this.path))
                    {
                        str = reader.ReadToEnd();
                    }
                    str = str.Replace("&Amp;", "&amp;");

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(this.path))
                    {
                        file.Write(str);
                    }

                    // Загрузка XML из файла.
                    doc.Load(this.path);

                    XmlNodeList CatList = doc.GetElementsByTagName("category");
                    XmlNodeList OfferList = doc.GetElementsByTagName("offer");

                    this.label1.Enabled = true;
                    this.label2.Enabled = true;

                    this.label1.Text = "Найдено " + CatList.Count + " категорий";
                    this.label2.Text = "Найдено " + OfferList.Count + " товаров";


                    // Первый проход по товарам XmlNodeList OfferList - получаем стурктуру и значения свойств
                    this.distinct_arr = Xml_helper.Distinct_node_props(OfferList);

                    //выведем уникальные свойства в Грид
                    this.label3.Enabled = true;
                    this.label3.Text = distinct_arr.Length + " уникальных свойств";

                    //и выводим список уникальных свойств
                    int count = 0;
                    foreach (string d in distinct_arr)
                    {
                        dataGridView1.Rows.Add(count, d);
                        count++;
                    }

                }
            }
            catch
            {
                MessageBox.Show("Ошибка. Что-то не то со структурой файла. Убедитесь, что файл на входе выполнен по стандарту Яндекс XML");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { }

        private void button2_Click(object sender, EventArgs e)
        { }
        public void Clear(DataGridView dataGridView)
        {
            while (dataGridView.Rows.Count > 1)
                for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
                    dataGridView.Rows.Remove(dataGridView.Rows[i]);
        }


        private void button3_Click(object sender, EventArgs e)
        {

            // Check if 'this.path' is empty or null
            if (string.IsNullOrEmpty(this.path))
            {
                // Show message
                MessageBox.Show("Сначала надо указать файл. Нажмите на кнопку \"Выбрать\" наверху ");
                // You can also show a message box or any other appropriate method for your application

                return; // or you can use 'return;' to exit immediately
            }

            

            // Загрузка XML из файла.
            doc.Load(this.path);

            XmlNodeList CatList = doc.GetElementsByTagName("category");
            XmlNodeList OfferList = doc.GetElementsByTagName("offer");

            //Создаем массив для хранения категорий 

            categories[0] = new string[CatList.Count];
            categories[1] = new string[CatList.Count];
            categories[2] = new string[CatList.Count];

            for (int i = 0; i < CatList.Count; i++)
            {
                // работаем с аттрибутами категории
                XmlAttributeCollection attrColl = CatList[i].Attributes;

                for (int j = 0; j < attrColl.Count; j++)
                {
                    categories[j][i] = attrColl[j].Value;
                }

                //cобираем категории в массив
                categories[2][i] = CatList[i].InnerXml;
                categories[2][i] = categories[2][i].Replace("\n", ""); ;
                categories[2][i] = categories[2][i].Replace("\r", ""); ;
            }


            //Передаем процедуре объект, который надо распарсить и массив с уникальными столбцами и получаем данные ноды с потомками и их аттрибутами в виде двумерного массива
            string[,] goods = Xml_helper.Node_parser(OfferList, distinct_arr);

            //находим номер столбца с Категорией товара в goods
            int index_cat_id = Search.PropIndex(distinct_arr, "categoryId");

            //добавляем 2 столбца с Категорией товара
            int new_col_number = goods.GetLength(0) + 1;
            int new_string_number = goods.GetLength(1);

            // объявляем новый массив с дополнительным столбцом
            string[,] goods_add = new string[new_col_number, new_string_number];

            // запускаем цикл прохода по сточкам
            for (int m = 0; m < new_col_number - 1; m++)
            {
                for (int k = 0; k < new_string_number; k++)
                {
                    goods_add[m, k] = goods[m, k];
                }
            }
            // и проходим по всем строкам, записываем еще колонку с Категорией товаров
            //FileSaver.CSV_writer(Properties.Settings.Default.basepath + @"\yml1.csv", goods_add);
            for (int k = 0; k < new_string_number; k++)
            {
                if (k == 0)
                {
                    goods_add[new_col_number - 1, k] = "Category";
                }
                else
                {
                    //Ищем значение категории
                    try
                    {
                        int index = Search.PropIndex(categories[0], goods_add[index_cat_id, k]);
                        goods_add[new_col_number - 1, k] = categories[2][index];
                    }
                    catch { goods_add[new_col_number - 1, k] = ""; }
                }

            }


            //вываливаем все это говно в csv
            try
            {
                FileSaver.CSV_writer(Properties.Settings.Default.basepath + @"\yml.csv", goods_add);
                MessageBox.Show("Файл выгружен по адресу " + Properties.Settings.Default.basepath + @"\yml.csv");
                button3.Enabled = false; button1.Enabled = false;

            }
            catch
            {
                MessageBox.Show("Проблема с записью в csv. Убедитесь, что файл " + Properties.Settings.Default.basepath + @"\yml.csv не открыт в других программах");
                this.Close();
            }
        }

        private void label4_Click(object sender, EventArgs e) { }

        private void button2_Click_1(object sender, EventArgs e) //Сохранение структуры файла
        {
            string structurePath = this.textBox1.Text;
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(structurePath);

            string outputFilePath = Properties.Settings.Default.basepath + @"\" + fileNameWithoutExtension + "_structure.xml";
            
            
            // Check if 'this.path' is empty or null
            if (string.IsNullOrEmpty(this.path))
            {
                // Show message
                MessageBox.Show("Сначала надо указать файл. Нажмите на кнопку \"Выбрать\" наверху ");
                // You can also show a message box or any other appropriate method for your application

                // Exit the procedure or method
                return; // or you can use 'return;' to exit immediately
            }


            // Загрузка XML из файла.
            doc.Load(this.path);
            // Get the <shop> node
            XmlNode shopNode = doc.SelectSingleNode("/yml_catalog");

            // Create a new XML document to store the structure
            XmlDocument structureDoc = new XmlDocument();
            
            XmlNode importedNode = structureDoc.ImportNode(shopNode, true);
            structureDoc.AppendChild(importedNode);

            // Remove <offers> nodes
           
            XmlNodeList offersNodes = structureDoc.SelectNodes("/yml_catalog/shop/offers");
            foreach (XmlNode node in offersNodes)
            {
                node.ParentNode.RemoveChild(node);
            }
            // Save the modified structure to a file
            structureDoc.Save(outputFilePath);
            Console.WriteLine("Structure saved to: " + outputFilePath);
            MessageBox.Show("Файл структуры сохранен по адресу " + outputFilePath);
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Указываем CSV файл для восстановления
            OPF.Filter = "CSV files (*.csv)|*.csv";
            if (OPF.ShowDialog() == DialogResult.OK)
            {
               csvFilePath = OPF.FileName;
                
            }

                // Восстанавливаем XML из CSV
            //string csvFilePath = Properties.Settings.Default.basepath + @"\your_csv_file.csv";
            string xmlFilePath = Properties.Settings.Default.basepath + @"\output_xml_file.xml";

            // Load CSV file
            string[] csvLines;
            using (StreamReader reader = new StreamReader(csvFilePath, Encoding.GetEncoding("Windows-1251")))
            {
                csvLines = reader.ReadToEnd().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            }

            // Create XML document and load existing structure
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(StructurePath);

            // Navigate to the parent node where you want to add the "offers" element
            XmlNode shopNode = xmlDoc.SelectSingleNode("/yml_catalog/shop");

            // Create "offers" element
            XmlElement offersElement = xmlDoc.CreateElement("offers");

            // Extract field names from the first line
            string[] fieldNames = csvLines[0].Split(';');

            // Skip the header line and process each line of the CSV
            for (int i = 1; i < csvLines.Length; i++)
            {
                string[] fields = csvLines[i].Split(';');

                XmlElement offerElement = xmlDoc.CreateElement("offer");

                // Set attributes
                offerElement.SetAttribute("id", fields[0]);
                offerElement.SetAttribute("available", fields[1]);

                // Add child elements
                for (int j = 2; j < fields.Length; j++)
                {
                    bool conditionMet = false; // Variable to track whether any condition was met

                    if (string.IsNullOrEmpty(fieldNames[j]) || fieldNames[j] == "Category")
                    {
                        // Skip creating elements for empty or "category" fields
                        continue;
                    }

                    if (fieldNames[j].StartsWith("param"))
                    {
                        XmlElement paramElement = xmlDoc.CreateElement("param");
                        string paramName = fieldNames[j].Replace("param", ""); // Remove "param" from the field name
                        paramElement.SetAttribute("name", paramName);
                        paramElement.InnerText = fields[j];
                        offerElement.AppendChild(paramElement);
                        conditionMet = true; // Set the flag to true
                    }

                    if (fieldNames[j] == "picture" && fields[j].Contains("|"))
                    {
                        // Split the picture field by "|" separator
                        string[] pictureUrls = fields[j].Split('|');

                        // Start cycle for each picture URL
                        foreach (string pictureUrl in pictureUrls)
                        {
                            XmlElement pictureElement = xmlDoc.CreateElement("picture");
                            pictureElement.InnerText = pictureUrl;
                            offerElement.AppendChild(pictureElement);
                        }
                        conditionMet = true; // Set the flag to true
                    }

                    if (!conditionMet)
                    {
                        XmlElement childElement = xmlDoc.CreateElement(fieldNames[j]);
                        childElement.InnerText = fields[j];
                        offerElement.AppendChild(childElement);
                    }
                }

                offersElement.AppendChild(offerElement);
            }

            // Add <offers> to the XML document
            shopNode.AppendChild(offersElement);

            // Save XML document to file
            using (XmlTextWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.GetEncoding("Windows-1251")))
            {
                xmlWriter.Formatting = Formatting.Indented;
                xmlDoc.Save(xmlWriter);
            }

            Console.WriteLine("XML file generated successfully.");
            MessageBox.Show("Файл сгенерирован и положен по адресу " + xmlFilePath);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Specify XLSX file for restoration
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string xlsxFilePath = openFileDialog.FileName;
                string xmlFilePath = Properties.Settings.Default.basepath + @"\output_xml_file.xml";

                Xml_helper.Convert(xlsxFilePath, xmlFilePath, StructurePath);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML and YML files (*.xml;*.yml)|*.xml;*.yml";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StructurePath = openFileDialog.FileName;
                label6.Text = openFileDialog.FileName;
                button5.Enabled = true;
                button6.Enabled = true;
            }


        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}

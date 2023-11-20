using FileParser.DedicClasses;
using System;
using System.Windows.Forms;
using System.Xml;

namespace FileParser
{
    public partial class frmUniversalXMLParser : Form
    {
        string str = "\r\n" + "------------" + "\r\n";
        public string path = "";
        XmlDocument doc = new XmlDocument();
        public string[] distinct_arr;
        public frmUniversalXMLParser()
        {
            InitializeComponent();
        }

        private void btnSetXMLFilePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "xml files (*.xml)|*.xml";

            if (OPF.ShowDialog() == DialogResult.OK)
            {
                this.txtBxPathToFile.Text = OPF.FileName;
                this.path = OPF.FileName;
                // Загрузка XML из файла.
                doc.Load(this.path);
                Console.WriteLine(OPF.FileName);

                //проходим по корню
                XmlElement? xRoot = doc.DocumentElement;
                if (xRoot != null)
                {
                    // обход всех узлов в корневом элементе
                    foreach (XmlElement xnode in xRoot)
                    {
                        Console.WriteLine("Родительский узел называется " + xnode.Name);
                        if (xnode.HasChildNodes)
                        {
                            Console.WriteLine("Родительский узел имеет " + xnode.ChildNodes.Count + " элементов:" + str);
                            richTextBox1.Text += "Родительский узел " + xnode.Name + str;
                            richTextBox1.Text += "В нем " + xnode.ChildNodes.Count + " элементов:" + str;

                        }
                    }
                }
            }
            btnSetXMLFilePath.Enabled = false;
        }

        private void btnReseachNode_Click(object sender, EventArgs e)
        {
            //helloworld.MessageBox.Show(textBox1.Text);
            try
            {
                XmlNodeList CurrenNode = doc.GetElementsByTagName(textBox1.Text);
                //XmlElement CurrenNode_xml = CurrenNode.
                XmlElement? xRoot = doc.DocumentElement;

                string[][] a = Xml_helper.GetNamesAndAnscestors(xRoot, textBox1.Text);
                Console.WriteLine("готово");
                //запишем результат в лог

                richTextBox1.Text += str + textBox1.Text + " узел: " + str;
                richTextBox1.Text += "В нем " + a[0][0] + " элементов:" + str;
                foreach (string b in a[1])
                {
                    richTextBox1.Text += b + str;
                }

                if (a[2].Length > 0)
                {
                    foreach (string b in a[2])
                    {
                        richTextBox1.Text += b + str;
                    }
                }

            }
            catch
            {

                MessageBox.Show("Походу нет такого узла!");
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            XmlNodeList node = doc.GetElementsByTagName(textBox1.Text); //получаем имя узла
            distinct_arr = Xml_helper.Distinct_node_props(node); //Получаем уникальные значения всех свойств по узлу
            string[,] goods = Xml_helper.Node_parser(node, distinct_arr);//Передаем процедуре объект, который надо распарсить и массив с уникальными столбцами и получаем данные ноды с потомками и их аттрибутами в виде двумерного массива
            // и печатаем результат
            FileSaver.CSV_writer(Properties.Settings.Default.basepath + @"\xml.csv", goods);
            MessageBox.Show("Файл выгружен по адресу " + Properties.Settings.Default.basepath + @"\xml.csv");

        }
    }
}

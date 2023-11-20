using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace FileParser
{
    //Класс e работы с файлом отчета по поставщикам и сайта по данным 1С 

    //Класс для работы с объектами типа XmlNodeList
    public class Xml_helper
    {

        //Метод делает проход по ноде и возвращает строковый массив с уникальными значениями свойств ноды
        public static string[] Distinct_node_props(XmlNodeList node)
        {

            //Создаем список для хранения значений свойств

            List<string> props = new List<string>();
            for (int i = 0; i < node.Count; i++)
            {
                XmlAttributeCollection attrColl = node[i].Attributes;
                for (int j = 0; j < attrColl.Count; j++)
                {
                    props.Add(attrColl[j].Name);
                }
                if (node[i].HasChildNodes)
                {
                    if (!node[i].ChildNodes[0].HasChildNodes) continue; //елси нет вложенности, продолжаем просто
                    for (int k = 0; k < node[i].ChildNodes.Count; k++)
                    {
                        string prop = node[i].ChildNodes[k].Name;
                        // вытаскиваем аттбирут в переменную
                        XmlAttributeCollection attrOffer = node[i].ChildNodes[k].Attributes;
                        if (attrOffer.Count > 0)
                        {
                            for (int jj = 0; jj < attrOffer.Count; jj++)
                            {
                                // Вытаскиваем аттрибуты в переменную
                                prop += attrOffer[jj].InnerText + ",";
                            }

                            prop = prop.Remove(prop.Length - 1); //удаляем последнюю запятую
                        }

                        // записываем значение свойства
                        props.Add(prop);

                    }
                }
            }
            // выдергиваем уникальные значения свойств
            var distinct = props.Distinct();
            foreach (string d in distinct)
            {
                Console.WriteLine(d);
            }
            // формируем массив с  уникальными значениями свойств
            string[] distinct_arr = distinct.ToArray();
            return distinct_arr;
        }
        //Метод делает проход по ноде и возвращает двумерный строковый массив со всеми данными. на вход надо подать уникальные значения ноды
        public static string[,] Node_parser(XmlNodeList OfferList, string[] distinct_arr)
        {
            FolderReader program = new FolderReader();
            //СОЗдаем двумерный массив с числом элементов свойств и товаров
            string[,] goods = new string[distinct_arr.Length, OfferList.Count + 1];


            // Инициализируем  массив
            for (int m = 0; m < distinct_arr.Length; m++)
            {
                for (int k = 0; k < 1 + OfferList.Count; k++)
                {
                    goods[m, k] = "";
                }

            }
            //записываем в выходной массив имена значений
            for (int m = 0; m < distinct_arr.Length; m++)
            {
                goods[m, 0] = distinct_arr[m];
            }
            //записываем в выходной массив сами значения
            for (int i = 0; i < OfferList.Count; i++)
            {
                XmlAttributeCollection attrColl = OfferList[i].Attributes;
                for (int j = 0; j < attrColl.Count; j++)
                {
                    //записываем данные по аттрибутам
                    int index = program.PropIndex(distinct_arr, attrColl[j].Name);
                    goods[index, i + 1] = attrColl[j].InnerText;


                }

                if (OfferList[i].HasChildNodes)
                {
                    if (!OfferList[i].ChildNodes[0].HasChildNodes) continue; //елси нет вложенности, продолжаем просто
                    for (int k = 0; k < OfferList[i].ChildNodes.Count; k++)
                    {
                        string prop = OfferList[i].ChildNodes[k].Name;
                        // вытаскиваем аттбирут в переменную
                        XmlAttributeCollection attrOffer = OfferList[i].ChildNodes[k].Attributes;
                        if (attrOffer.Count > 0)
                        {
                            for (int jj = 0; jj < attrOffer.Count; jj++)
                            {
                                // Вытаскиваем аттрибуты в переменную
                                prop += attrOffer[jj].InnerText + ",";
                            }
                            //удаляем последнюю запятую
                            prop = prop.Remove(prop.Length - 1);
                        }

                        // записываем значение свойства
                        int index = program.PropIndex(distinct_arr, prop);
                        //Если уже что-то записано, то добавляем разделитель
                        if (goods[index, i + 1] != "")
                        {
                            goods[index, i + 1] += "|" + OfferList[i].ChildNodes[k].InnerText;
                        }
                        else
                        {
                            goods[index, i + 1] = OfferList[i].ChildNodes[k].InnerText;
                        }

                        //убираем ; из файла и знаки переносов
                        goods[index, i + 1] = goods[index, i + 1].Replace(";", "");
                        goods[index, i + 1] = goods[index, i + 1].Replace("\r\n", "");
                        goods[index, i + 1] = goods[index, i + 1].Replace("\n", " ");
                    }

                }

            }

            return goods;
        }

        //Метод смотрит, что внутри ноды и возвращает 2 вещи: сколько потомков и их имена
        public static string[][] GetNamesAndAnscestors(XmlElement root, string node)
        {
            string str = "\r\n" + "------------" + "\r\n";

            List<string> nodes_names = new List<string>();
            List<string> nodes_children_count = new List<string>();
            List<string> nodes_attr = new List<string>();

            //получаем узел, который надо исследовать
            XmlNodeList CatList = root.GetElementsByTagName(node);
            //пробегаем по всем узлам 

            if (CatList[0].HasChildNodes)
            {
                for (int k = 0; k < CatList[0].ChildNodes.Count; k++)
                {

                    Console.WriteLine(CatList[0].ChildNodes[k].Name);
                    //собираем имена детей этого узла в список
                    nodes_names.Add(CatList[0].ChildNodes[k].Name);
                    nodes_children_count.Add(CatList[0].ChildNodes.Count.ToString());
                }
            }
            XmlAttributeCollection attrColl = CatList[0].Attributes;
            for (int j = 0; j < attrColl.Count; j++)
            {
                //записываем данные по аттрибутам
                nodes_attr.Add(attrColl[j].Name);
            }

            Console.WriteLine("узел " + node + " содержит " + nodes_names.Count + " элементов:" + str);

            string[][] JaggedArray = new string[4][];
            // выдергиваем уникальные значения имен
            var distinct = nodes_names.Distinct();
            string[] result = distinct.ToArray();
            string[] result1 = { nodes_names.Count.ToString() };
            string[] result2 = nodes_attr.ToArray();
            string[] result3 = nodes_children_count.ToArray();

            JaggedArray[1] = result;
            JaggedArray[0] = result1;
            JaggedArray[2] = result2;
            JaggedArray[3] = result3;

            return JaggedArray;
        }

    }


    //Класс работы с файлами - считывание файлов, поиск индексов в массивах и т.п.
    public class FolderReader
    {
      

        List<string> names = new List<string>();
        List<string> paths = new List<string>();

        List<int> finds = new List<int>();
        public string base_out_path; // настрйка - базовая папка
        public string[][] a;


        //Метод считывает директорию и возвращает зубчатый массив со значенями имен и абсолютных путей найденных файлов в папке
        public string[][] ProcessDirectory(string targetDirectory)
        {
            try { 
                // Process the list of files found in the directory.
                string[] fileEntries = Directory.GetFiles(targetDirectory);
                foreach (string fileName in fileEntries)
                {
                    //ProcessFile(fileName);
                    paths.Add(fileName);
                    names.Add(Path.GetFileName(fileName));
                }

                // Recurse into subdirectories of this directory.
                string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
                foreach (string subdirectory in subdirectoryEntries)
                    ProcessDirectory(subdirectory);

                // ��������� ������ � ������ c ������� output 
                string[] output_n = names.ToArray();
                string[] output_p = paths.ToArray();

                string[][] JaggedArray = new string[2][];

                JaggedArray[0] = output_n;
                JaggedArray[1] = output_p;

                return JaggedArray;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка доступа к сканируемой папке: {ex.Message}");
                // Handle the exception or take any other appropriate action.
                string[][] JaggedArray = new string[2][];

                return JaggedArray;
            }

        }

        //out - array of files id in lookArray
        public int[] Searcher(string[] searchArray, string[] lookArray)
        {
            int startIndex = 0;
            foreach (string needle in searchArray)
            {
                startIndex = 0;
                foreach (string look in lookArray)
                {
                    if (look.Contains(needle))
                    {

                        int index = Array.IndexOf(lookArray, look, startIndex);

                        if (index == -1)
                        {
                            //MessageBox.Show("Вот!");
                        }
                        else
                        {
                            finds.Add(index);
                            startIndex = index + 1;
                        }
                    }

                }
            }
            int[] output = finds.ToArray();
            return output;
        }

        // Матод поиска индекса в массиве свойств данного свойства
        public int PropIndex(string[] distinct_arr, string needle)
        {
            int index = Array.IndexOf(distinct_arr, needle);
            return index;
        }


    }

    public class Search
    {
        // Мутод поиска индекса в массиве 
        public static int PropIndex(string[] distinct_arr, string needle)
        {
            int index = Array.IndexOf(distinct_arr, needle);
            return index;
        }
    }

    public class Grids
    {
        public void set_colump(DataGridViewColumn column, string header, int width, bool read_only, string name, bool frozen)
        {
            column.HeaderText = header;// "Значение"; //текст в шапке
            column.Width = width; // 190; //ширина колонки
            column.ReadOnly = read_only; // false; //значение в этой колонке нельзя править
            column.Name = name; // "value"; //текстовое имя колонки, его можно использовать вместо обращений по индексу
            column.Frozen = frozen; // false; //флаг, что данная колонка всегда отображается на своем месте
            column.CellTemplate = new DataGridViewTextBoxCell(); //тип нашей колонки
        }
    }







}

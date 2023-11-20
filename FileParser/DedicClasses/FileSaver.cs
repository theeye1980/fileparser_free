using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace FileParser.DedicClasses
{
    //Класс с методами записи в файл
    public class FileSaver
    {
        public static void whrite_files(string[] path_list, string filename) // записывает список файлов в csv 
        {
            StreamWriter g = new StreamWriter(Properties.Settings.Default.basepath + @"\" + filename, false, Encoding.GetEncoding("Windows-1251"));
            foreach (string fnam in path_list)
            {
                g.WriteLine(fnam);
            }
            g.Close();
        }
        public static void CSV_writer(string[] path_list, string filename) // записывает список файлов в csv 
        {
            StreamWriter g = new StreamWriter(FileParser.Properties.Settings.Default.basepath + @"\" + filename, false, Encoding.GetEncoding("Windows-1251"));
            foreach (string fnam in path_list)
            {
                g.WriteLine(fnam);
            }
            g.Close();
        }

        public static void CSV_writer(string out_path, string[,] goods) // записывает 2м массив в файл, чисто для xml годится
        {

            StreamWriter f = new StreamWriter(FileParser.Properties.Settings.Default.basepath + @"\xml.csv", false, Encoding.GetEncoding("Windows-1251"));
            string line = "";

            // бегаем по всем строкам массива
            for (int k = 0; k < goods.GetLength(1); k++)
            {

                for (int m = 0; m < goods.GetLength(0); m++)
                {
                    line += goods[m, k] + ";";

                }
                f.WriteLine(line);
                line = "";

            }
            f.Close();

        }
        public static void CSV_writer(string[,] goods, string out_file_name) // записывает 2м массив в файл, чисто для xml годится
        {

            StreamWriter f = new StreamWriter(FileParser.Properties.Settings.Default.basepath + @"\" + out_file_name, false, Encoding.GetEncoding("Windows-1251"));
            string line = "";

            // бегаем по всем строкам массива
            for (int k = 0; k < goods.GetLength(0); k++)
            {

                for (int m = 0; m < goods.GetLength(1); m++)
                {
                    line += goods[k, m] + ";";

                }
                f.WriteLine(line);
                line = "";

            }
            f.Close();

        }

        public static void CSV_writer(string[][] goods, string filename) // записывает зубчатый массив в файл по стобцам
        {

            StreamWriter f = new StreamWriter(FileParser.Properties.Settings.Default.basepath + @"\" + filename, false, Encoding.GetEncoding("Windows-1251"));
            string line = "";

            //  int k = goods.GetLength(1);
            // int g = goods.GetLength(0);


            for (int k = 0; k < goods[1].Length; k++)
            {


                //Console.WriteLine(goods[0][k] + " -- " + goods[1][k]);
                line += goods[0][k] + ";" + goods[1][k] + ";";
                f.WriteLine(line);
                line = "";
            }


            // бегаем по всем строкам массива

            f.Close();

        }
        public static void CSV_writer(Site_prices site_Prices_obj, string filename) // записывает CSV из объекта
        {
            StreamWriter f = new StreamWriter(Properties.Settings.Default.basepath + @"\" + filename, false, Encoding.GetEncoding("Windows-1251"));

            var res = site_Prices_obj.results.ToArray();

            string[] head_line = {
               "vendor_name",
               "article",
               "availible",
               "stocks_23",
               "sum_oll_stocks",
               "price",
               "price_sale",
               "published",
               "deleted"
            };
            f.WriteLine(String.Join(";", head_line));

            foreach (var item in res)
            {
                string[] str = {
                    item.vendor_name,
                    item.article,
                    item.availible.ToString(),
                    item.stocks_23.ToString(),
                    item.sum_oll_stocks.ToString(),
                    item.price.ToString(),
                    item.price_sale.ToString(),
                    item.published.ToString(),
                    item.deleted.ToString()
                };
                f.WriteLine(String.Join(";", str));
            }

            f.Close();
            MessageBox.Show("Готово. Результат в папке " + Properties.Settings.Default.basepath);

        }
        public static void CSV_writer(DataGridView DW, string filename)
        { //Пишем в CSV объект Datagrid
            try
            {

                StreamWriter f = new StreamWriter(Properties.Settings.Default.basepath + @"\" + filename, false, Encoding.GetEncoding("Windows-1251"));
                string line = "";

                foreach (DataGridViewColumn column in DW.Columns)
                {
                    line += column.HeaderText + ";";
                }
                f.WriteLine(line); line = "";
                //Console.WriteLine(column.HeaderText);

                for (int i = 0; i < DW.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < DW.Columns.Count; j++)
                    {
                        line += DW.Rows[i].Cells[j].Value.ToString() + ";";
                    }
                    f.WriteLine(line); line = "";
                }
                f.Close();
            }
            catch { MessageBox.Show("Не получилось записать в файл. Нет доступа к файлу"); }

        }

        public static void CSV_writer(Single_price[] Site_data, string filename)
        { //Пишем в CSV объект Datagrid
            try
            {

                StreamWriter f = new StreamWriter(Properties.Settings.Default.basepath + @"\" + filename, false, Encoding.GetEncoding("Windows-1251"));
                string line = string.Empty; ;

                //Single_price first = Site_data[0];
                //Получим перечень свойств объекта
                List<string> prop_lst = new List<string>();
                foreach (var prop in typeof(Single_price).GetProperties())
                {
                    prop_lst.Add(prop.Name);
                }

                //Запишем первыую строчку
                f.WriteLine(String.Join(";", prop_lst));

                /* Пробежимся по всему массиву и запишем в файлик */
                for (int i = 0; i < Site_data.Length; i++)
                {
                    //Создадим строчку
                    line = string.Empty;
                    List<string> values = new List<string>();
                    foreach (string prop in prop_lst)
                    {
                        var bn = Site_data[i].GetType().GetProperty(prop).GetValue(Site_data[i], null);
                        if (bn != null)
                        {
                            values.Add(bn.ToString());
                        }
                        else values.Add(String.Empty);


                    }
                    f.WriteLine(String.Join(";", values));

                    //f.WriteLine(line); line = "";
                }

                f.Close();
            }
            catch { MessageBox.Show("Не получилось записать в файл. Нет доступа к файлу"); }

        }

        public static void XLSX_writer(string filename, string[][] data) // записывает зубчатый массив в файл xls, по колонкам
        {

            Excel.Application application = null;
            Excel.Workbooks workbooks = null;
            Excel.Workbook workbook = null;
            Excel.Sheets worksheets = null;

            Excel.Worksheet worksheet = null;
            Excel.Range range;

            //переменная для хранения диапазона ячеек
            //в нашем случае - это будет одна ячейка
            Excel.Range cell = null;
            try
            {
                application = new Excel.Application
                {
                    Visible = true
                };
                workbooks = application.Workbooks;
                workbook = workbooks.Add();
                worksheets = workbook.Worksheets; //получаем доступ к коллекции рабочих листов
                worksheet = worksheets.Item[1];//получаем доступ к первому листу
                                               //cell = worksheet.Cells[1, 1];//получаем доступ к ячейке
                                               //cell.Value = "Hello Excel";//пишем строку в ячейку A1

                //range = worksheet.get_Range("A1", worksheet);
                //range.set_Value(worksheet, data);


                //работает, но медленно

                int cols = 1;

                foreach (string[] column in data)
                {
                    int rows = 1;
                    foreach (string number in column)
                    {
                        worksheet.Cells[rows, cols].value = number;
                        //Console.WriteLine(number);
                        rows++;
                    }
                    cols++;
                }

                workbook.SaveAs(Properties.Settings.Default.basepath + @"\" + filename);
                application.Quit();
                MessageBox.Show("готово");
            }
            catch { MessageBox.Show("Что-то пошло не так"); }




            /*
            // бегаем по всем строкам и колонкам массива
            
            for (int k = 0; k < data.GetLength(1); k++)
            {

                for (int m = 0; m < data.GetLength(0); m++)
                {
                    line += data[m][k] + ";";

                }
                f.WriteLine(line);
                line = "";

            }*/






        }

        public static int PropIndex(string[] distinct_arr, string needle)// поиск индекса в массиве по значению
        {
            int index = Array.IndexOf(distinct_arr, needle);
            return index;
        }
    }

}

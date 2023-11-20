using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace FileParser.DedicClasses
{
    class Excel_getter
    {
        public static string[,] get_compare_file_data()
        {

            Excel.Application xlApp = new Excel.Application(); //Excel
            Excel.Workbook xlWB; //рабочая книга              
            Excel.Worksheet xlSht; //лист Excel   
            xlWB = xlApp.Workbooks.Open(Properties.Settings.Default.basepath + @"\Внутренняя папка отдела контента\" + Properties.Settings.Default.path_to_compare); //название файла Excel                                             
            xlSht = xlWB.Worksheets["TDSheet"]; //название листа или 1-й лист в книге xlSht = xlWB.Worksheets[1];
            int iLastRow = xlSht.Cells[xlSht.Rows.Count, "B"].End[Excel.XlDirection.xlUp].Row;  //последняя заполненная строка в столбце А            
            var arrData = (object[,])xlSht.Range["B4:M" + iLastRow].Value; //берём данные с листа Excel
            //xlApp.Visible = true; //отображаем Excel     
            xlWB.Close(false); //закрываем книгу, изменения не сохраняем
            xlApp.Quit(); //закрываем Excel



            int RowsCount = arrData.GetUpperBound(0);
            int ColumnsCount = arrData.GetUpperBound(1);

            /* Избавляемся от null и приводим все к стокам */
            string[,] arrData_arr = new string[RowsCount, ColumnsCount];

            for (int i = 1; i <= RowsCount; i++)
            {
                for (int j = 1; j <= ColumnsCount; j++)
                {
                    if (arrData[i, j] != null)
                    {
                        arrData_arr[i - 1, j - 1] = arrData[i, j].ToString();
                    }
                    else
                    {
                        arrData_arr[i - 1, j - 1] = "";
                    }
                }
            }
            return arrData_arr;
        }
        public static string[][] get_Raw_data_list(string path) // получает несколько колонок файла в зубчатый массив по числу элеменотов в колонке B 
        {
            //   List<string> props = new List<string>();

            Excel.Application xlApp = new Excel.Application(); //Excel
            Excel.Workbook xlWB; //рабочая книга              
            Excel.Worksheet xlSht; //лист Excel   
            xlWB = xlApp.Workbooks.Open(path); //название файла Excel                                             
            xlSht = xlWB.Worksheets["TDSheet"]; //название листа или 1-й лист в книге xlSht = xlWB.Worksheets[1];
            int iLastRow = xlSht.Cells[xlSht.Rows.Count, "B"].End[Excel.XlDirection.xlUp].Row;  //последняя заполненная строка в столбце B            
                                                                                                //iLastRow = 99000;
            var arrData_B = (object[,])xlSht.Range["B4:B4" + iLastRow].Value; //берём данные с листа Excel
            var arrData_D = (object[,])xlSht.Range["D4:D4" + iLastRow].Value; //берём данные с листа Excel
            var arrData_E = (object[,])xlSht.Range["E4:E4" + iLastRow].Value; //берём данные с листа Excel
            var arrData_F = (object[,])xlSht.Range["F4:F4" + iLastRow].Value; //берём данные с листа Excel
            //xlApp.Visible = true; //отображаем Excel     
            xlWB.Close(false); //закрываем книгу, изменения не сохраняем
            xlApp.Quit(); //закрываем Excel



            int RowsCount = iLastRow;

            //string[] arrData_arr = new string[] arrData_arr;

            /* Избавляемся от null и приводим все к стокам */
            string[] arrData_B_arr = new string[RowsCount];
            string[] arrData_D_arr = new string[RowsCount];
            string[] arrData_E_arr = new string[RowsCount];
            string[] arrData_F_arr = new string[RowsCount];

            for (int i = 1; i <= RowsCount; i++)
            {



                if (arrData_B[i, 1] != null)
                {
                    arrData_B_arr[i - 1] = arrData_B[i, 1].ToString();

                    //Обеспечиваем E не null                    
                    if (arrData_E[i, 1] != null)
                    {
                        arrData_E_arr[i - 1] = arrData_E[i, 1].ToString();
                    }
                    else { arrData_E_arr[i - 1] = ""; }
                    //Обеспечиваем D не null 
                    if (arrData_D[i, 1] != null)
                    {
                        arrData_D_arr[i - 1] = arrData_D[i, 1].ToString();
                    }
                    else { arrData_D_arr[i - 1] = ""; }
                    //Обеспечиваем F не null 
                    if (arrData_F[i, 1] != null)
                    {
                        arrData_F_arr[i - 1] = arrData_F[i, 1].ToString();
                    }
                    else { arrData_F_arr[i - 1] = ""; }


                }
                else
                {

                }
            }
            //arrData_arr[0] = arrData_names_arr;
            string[][] JaggedArray = new string[4][];
            //var distinct_articles = props.Distinct();
            JaggedArray[0] = arrData_B_arr;
            JaggedArray[1] = arrData_D_arr;
            JaggedArray[2] = arrData_E_arr;
            JaggedArray[3] = arrData_F_arr;

            return JaggedArray;
        }

    }
}

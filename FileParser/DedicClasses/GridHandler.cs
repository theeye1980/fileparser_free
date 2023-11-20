using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileParser
{
    public class GridHandler
    {

        // добавляем колонки в Data
        public static void ColumnsAdder(ref DataGridView DW, ref DataGridViewColumn[] dataGridViewColumns)
        {
            foreach (DataGridViewColumn column in dataGridViewColumns)
            {
                DW.Columns.Add(column);
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }

        }
        public static void Clear_datagrid(ref DataGridView DW)
        {
            DW.Rows.Clear();
            DW.Columns.Clear();
        }
        /* Методы отображения на сетке по ценам 1 товара*/
        public static void DrawToGrid1GoodData(ref DataGridView DW, Single_price Single_site_Data, Price_out PriceInfo_arr)
        { //Отрисовать на Grid данные по цене одного товара

            //Выводим данные на сетку
            DW.Rows.Add("article", PriceInfo_arr.article);
            DW.Rows.Add("sale", PriceInfo_arr.sale);
            DW.Rows.Add("price", PriceInfo_arr.price);
            DW.Rows.Add("price_sale", PriceInfo_arr.price_sale);
            DW.Rows.Add("timestamp", PriceInfo_arr.timestamp);
            DW.Rows.Add("update_error", PriceInfo_arr.update_error);
            DW.Rows.Add("vendor_uuid", PriceInfo_arr.vendor_uuid);

            // данные с сайта
            DW.Rows.Add("Данные с сайта");
            DW.Rows.Add("price", Single_site_Data.price);
            DW.Rows.Add("price_sale", Single_site_Data.price_sale);
            DW.Rows.Add("deleted", Single_site_Data.deleted);
            DW.Rows.Add("published", Single_site_Data.published);
            DW.Rows.Add("availible", Single_site_Data.availible);


        }

        /* Конец методов отображения по 1 товару */
    }

}

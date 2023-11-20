using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileParser.DedicClasses
{
    public class Async
    {
        public static void GetSiteInfo()
        {

            try
            {
                string source = @"https://fandeco.ru/rest/1c/exportbuyall";
                Cache.get_web_page_to_json_file(source, "Site_Price_cache_path.json");

                MessageBox.Show("Файл с данными по ценам сайта обновлен");
            }
            catch { MessageBox.Show("Не удалось загрузить файл Цен сайта в кеш."); }

        }

    }
}

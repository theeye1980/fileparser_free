using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileParser.DedicClasses
{
    public static class FileInfoGetter
    {

        public static string GetFileChangeDate(string filepath)
        { // возвращает дату последнего изменения файла в виде строки

            DateTime dt = File.GetLastWriteTime(filepath);
            string result = dt.ToString();
            return result;
        }

        public async static void ScanAllAboutGoodFld(bool notify = true)
        { //записываем файл со всеми актуальными путями папки Все о товаре (для авторазбора)

            await Task.Run(() =>
            {
                try
                {
                    FolderReader folderReader = new FolderReader();
                    string[][] m = folderReader.ProcessDirectory(Properties.Settings.Default.all_about_products_path);
                    FileSaver.CSV_writer(m[1], Properties.Settings.Default.name_of_all_about_goods_folder);

                    //пробегаем по по всем именам, и оставляем только папки с нужными именами, также убираются "левые" файлы
                    List<string> auto_files_list = new List<string>();

                    //получим список разрешенных папок
                    System.Collections.Specialized.StringCollection coll = Properties.Settings.Default.all_about_products_auto_folders;

                    foreach (string file in m[1])
                    {
                        foreach (var item in coll)
                        {
                            if (file.Contains(item))
                            {
                                auto_files_list.Add(file);
                            }
                        }
                    }

                    //выполним запрос linq, чтобы убрать то, что в списке исключений
                    var auto_files_list_cleared = from fff in auto_files_list
                                                  where !fff.Contains("TECHNOLIGHT") && !fff.Contains("Thumbs.db") && !fff.Contains("=PARTS=")
                                                  select fff;


                    string[] auto_files_list_cleared_arr = auto_files_list_cleared.ToArray();
                    // вывалим все в csv с разрешенными для сканирования путями
                    
                    FileSaver.CSV_writer(auto_files_list_cleared_arr, Properties.Settings.Default.name_of_all_about_goods_auto_folder);

                    if (notify)
                    {
                        DialogResult result = MessageBox.Show("Открыть местоположение файла?", "Все файлы для автообновления просканированы", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            Process.Start(new ProcessStartInfo { FileName = "explorer", Arguments = $"/n,/select,{ Properties.Settings.Default.basepath + @"\" + Properties.Settings.Default.name_of_all_about_goods_auto_folder}" });
                        }

                    }
                }
                catch { MessageBox.Show("Ошибка при сканировании файлов. "); 
                }
            });

        }
        public async static void ScanAllAboutGoodFldRemote(bool notify = true){ //записываем файл со всеми актуальными путями папки Все о товаре (для авторазбора)
            await Task.Run(() =>
            {
                
                Cache.putStringsToFile("https://photobank.massive.ru/export/files.php", Properties.Settings.Default.name_of_all_about_goods_folder);

                //считываем только что сохраненный файл
                string[] filesInPhotoBank = File.ReadAllLines(Properties.Settings.Default.basepath + @"\" + Properties.Settings.Default.name_of_all_about_goods_folder, Encoding.GetEncoding("Windows-1251"));

                //пробегаем по по всем именам, и оставляем только папки с нужными именами, также убираются "левые" файлы
                List<string> auto_files_list = new List<string>();

                //получим список разрешенных папок
                System.Collections.Specialized.StringCollection coll = Properties.Settings.Default.all_about_products_auto_folders;

                foreach (string file in filesInPhotoBank)
                {
                    foreach (var item in coll)
                    {
                        if (file.Contains(item))
                        {
                            auto_files_list.Add(file);
                        }
                    }

                }

                //выполним запрос linq, чтобы убрать то, что в списке исключений
                var auto_files_list_cleared = from fff in auto_files_list
                                              where !fff.Contains("TECHNOLIGHT") && !fff.Contains("Thumbs.db") && !fff.Contains("=PARTS=")
                                              select fff;


                string[] auto_files_list_cleared_arr = auto_files_list_cleared.ToArray();
                // вывалим все в csv с разрешенными для сканирования путями
                FileSaver.CSV_writer(auto_files_list_cleared_arr, Properties.Settings.Default.name_of_all_about_goods_auto_folder);


                if (notify)
                {
                    DialogResult result = MessageBox.Show("Открыть местоположение файла?", "Все файлы для автообновления просканированы", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo { FileName = "explorer", Arguments = $"/n,/select,{ Properties.Settings.Default.basepath + @"\" + Properties.Settings.Default.name_of_all_about_goods_auto_folder}" });
                    }

                }
            });
        }
    }
}

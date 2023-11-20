using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Windows.Forms;
using FileParser.DedicClasses;
using System.Web;

namespace FileParser
{

    class Cache // Класс управления кэшированием запросов и еще проба
    {

        public static void cache_writer(string content, string filename) // записывает список файлов в csv 
        {
            StreamWriter g = new StreamWriter(FileParser.Properties.Settings.Default.basepath + @"\" + filename, false, Encoding.GetEncoding("Windows-1251"));
            g.WriteLine(content);
            g.Close();
        }
        //обовить файл json у Андрея и записывает в указанное имя файла
        public static async Task get_web_page_to_json_file(string source, string filename)
        {
            try
            {
                WebClient client = new WebClient();
                using (Stream stream = client.OpenRead(source))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string line = "";
                        while ((line = reader.ReadLine()) != null)
                        {
                            //записываем в файл
                            cache_writer(line, filename);
                        }



                    }
                }

            }
            catch(Exception e) { MessageBox.Show("Не удалось загрузить файл Цен сайта в кеш." + e.Message); }
        }
        public static async Task putStringsToFile(string source, string filename)
        {
            try
            {
                StreamWriter g = new StreamWriter(FileParser.Properties.Settings.Default.basepath + @"\" + filename, false, Encoding.GetEncoding("Windows-1251"));
                WebClient client = new WebClient();
                using (Stream stream = client.OpenRead(source))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string line = "";
                        while ((line = reader.ReadLine()) != null)
                        {
                            //записываем в файл
                            line = line.Replace("/home/photobank/files", @"W:\Всё о товаре\=Для клиентов="); //приводим к стилю винды по файлам
                            line = line.Replace("/","\\"); //приводим к стилю винды
                            
                            g.WriteLine(line);
                        }



                    }
                }
                g.Close();
            }
            catch (Exception e) { MessageBox.Show("Не удалось загрузить файл Цен сайта в кеш." + e.Message); }
        }

        public static Single_price[] Get_Site_Prices()
        { // Возвращает массив с объектами, содержащими данные по ценам по данным сайта
            
            string Site_Price_cache_path = Properties.Settings.Default.basepath + @"\" + Properties.Settings.Default.Site_Price_cache_path;
            //Получаем данные по товару на сайте
            string result = File.ReadAllText(Site_Price_cache_path, Encoding.GetEncoding("Windows-1251"));
            Site_prices site_Prices = new Site_prices();
            Site_prices site_Prices_obj = JsonSerializer.Deserialize<Site_prices>(result);

            List<Single_price> res = site_Prices_obj.results;
            Single_price[] res_arr = res.ToArray();

            return res_arr;
            

        }

        //получаем объект с магазинами
        public static Dubli.Stores GetStores() {
            Dubli.Stores stores = new Dubli.Stores();
            //считываем файлик
            string path = Properties.Settings.Default.basepath + @"\ff.json" ;
            string result = File.ReadAllText(path, Encoding.GetEncoding("Windows-1251"));

            stores = JsonSerializer.Deserialize<Dubli.Stores>(result);

            return stores;
        }

        public static Object[] get_sync1c_sync_brands()
        { // Возвращает массив с объектами, содержащими данные по брендам по сайту
            string sync1c_sync_path = Properties.Settings.Default.basepath + @"\" + "sync1c_sync.json";
            //Получаем данные по товару на сайте
            string result = File.ReadAllText(sync1c_sync_path, Encoding.GetEncoding("Windows-1251"));

            sync1c_sync s = new sync1c_sync();
            sync1c_sync s_obj = JsonSerializer.Deserialize<sync1c_sync>(result);
            List<Object> res = s_obj.@object;
            Object[] res_arr = res.ToArray();
            return res_arr;
        }

        //На входе массив с URL и папка в базовом хранилище, куда их сохранить
        public static void saveUrls(string[] urlList, string folder, bool cleanFld) {

            //елси выходная папка не существует, создаем
            DirectoryInfo dirInfo = new DirectoryInfo(Properties.Settings.Default.basepath + @"\" + folder + @"\");
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
                cleanFld = false;
            }
            // Проверяем, нужно ли очищать папку
            if (cleanFld == true)
            {

                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    file.Delete();
                }
            }

            Console.WriteLine(dirInfo.ToString());

            // пробегаем по каждому url и сохраняем
            int count = 0;
            foreach (string url in urlList)
            {
                try
                {
                    Uri uri = new Uri(url);
                    //получаем имя файла по URL
                    string filename = System.IO.Path.GetFileName(uri.AbsolutePath);
                   //Декодируем каракули
                    var filename1 = HttpUtility.UrlDecode(filename);
                    Console.WriteLine(filename1);

                    //записываем абсолютный путь файла, который будем сохранять у себя
                    string fpath = dirInfo.ToString() + filename1;

                    WebClient webClient = new WebClient();
                    webClient.DownloadFile(url, fpath);
                    count++;
                }
                catch
                {
                    MessageBox.Show("Что-то не то со списком url для скачивания");
                }
            }

            MessageBox.Show("Готово. Скачано " + count + " файлов в папку " + Properties.Settings.Default.basepath + @"\download\");


        }
        //Загружает в папку Папка_Контент_Отдела данные из удаленного сервера  
        public static async void dwlContentInternalFld(bool notify = true) {
            List<string> urlList = new List<string>();
            // выводим список брендов группы А
            System.Collections.Specialized.StringCollection coll = Properties.Settings.Default.voronkaFiles;

            foreach (string file in coll) {
                urlList.Add(file);
            }

            string[] urlList_arr = urlList.ToArray();

            await Task.Run( () =>
            { 
                    saveUrls(urlList_arr, "Внутренняя папка отдела контента", false);

            });

            if (notify)
            {
                DialogResult result = MessageBox.Show("Открыть местоположение файлов?", "Внутренние файлы отдела контента обновлены", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //Process.Start(new ProcessStartInfo { FileName = "explorer", Arguments = $"/n,/select,{ Properties.Settings.Default.basepath + @"\" + Properties.Settings.Default.exportbuyall_csv}" });
                    WinActions.OpenRes(Properties.Settings.Default.basepath + @"\Внутренняя папка отдела контента");
                }
                else { }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileParser.DedicClasses
{
    internal static class GetArticlesList
    {
        public async static void GetAllArticles1C(bool notify = true) //получаем актуальный список артикулов и пишем их в файлик
        {
            
                await Task.Run(() =>
                {
                try
                {
                    PhotobankParts photobankParts = new PhotobankParts();
                    //получаем список артикулов
                    string base_url = Properties.Settings.Default.base_rest_url;
                    var url = base_url + "submit_to_site";
                    var json = @"{
                " + "\n" +
                        @"   ""site"":""divinare.it"",
                " + "\n" +
                        @"   ""mode"": ""full""
                " + "\n" +
                    @"}";
                    // Запуск выполнения задачи:
                    var t = Task.Run(() => Knocker.GetData(json, url));
                    t.Wait();
                    string result = t.Result.ToString();

                    ListOf1CArts listOf1CArts = JsonSerializer.Deserialize<ListOf1CArts>(result);

                    string rax = listOf1CArts.response.array.ToString();
                    rax = rax.Replace("}", "");
                    rax = rax.Replace("{", "");
                    rax = rax.Replace("\"", "");

                    //Разбиваем на массив по запятой
                    string delim = "\r\n";
                    string[] arts = rax.Split(new string[] { delim }, StringSplitOptions.RemoveEmptyEntries);

                    List<string> list = new List<string>();
                    foreach (string art in arts)
                    {
                        delim = ":";
                        string[] a = art.Split(new string[] { delim }, StringSplitOptions.RemoveEmptyEntries);
                        list.Add(a[0]);

                    }
                    string[] artciles_arr = list.ToArray();

                    //запишем во временный файл
                    FileSaver.CSV_writer(artciles_arr, Properties.Settings.Default.temp_list_of_arts_1c);
                    if (notify)
                    {
                        DialogResult res = MessageBox.Show("Открыть местоположение файла?", "Список артикулов 1C получен", MessageBoxButtons.YesNo);
                        if (res == DialogResult.Yes)
                        {
                            Process.Start(new ProcessStartInfo { FileName = "explorer", Arguments = $"/n,/select,{ Properties.Settings.Default.basepath + @"\" + Properties.Settings.Default.temp_list_of_arts_1c}" });
                        }


                    }
                    }
                    catch { MessageBox.Show("Данные по артикулам 1С получить не удалось"); }
                });
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileParser
{
    public class Knocker // задаем класс, который будет отвечать за соединение с REST сервером
    {
        //Метод постановки асинхноррной задачи, работает только с нашим REST сервисом
        public static async Task<string> GetData(string json, string url)
        {
            try
            {
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var userName = "VXDTB9lg4Uz4vkKsASAx2";


                using var client = new HttpClient();
                var authToken = Encoding.ASCII.GetBytes($"{userName}:{userName}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(authToken));

                var response = await client.PostAsync(url, data);
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            catch {
                MessageBox.Show("не удается постучаться - возможно, нет интернета или удаленный узел не ответил");
                return "Ошибка"; 
            }
        }


    }
}

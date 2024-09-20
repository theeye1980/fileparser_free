using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Http;

namespace FileParser.DedicClasses
{
    public class NetworkHelper
    {
        public string GetIPAddress()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
            }
            return null;
        }

        public bool IsInternetAvailable()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public async Task SendGetRequest(string ip, string filename, string programName)
        {
            try
            {
                HttpClient client = new HttpClient();

                string url = $"http://kosarev.site/fileparseruse.php"; // Update the URL with your actual endpoint

                // Encode the filename if it may contain spaces
                string encodedFilename = Uri.EscapeDataString(filename);

                url += $"?ip={ip}&filename={encodedFilename}&programm={programName}";

                HttpResponseMessage response = await client.GetAsync(url);

                Console.WriteLine(url);

                if (response.IsSuccessStatusCode)
                {
                    // Request was successful
                    Console.WriteLine("GET request sent successfully.");
                }
                else
                {
                    // Request failed
                    Console.WriteLine("Failed to send GET request.");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}

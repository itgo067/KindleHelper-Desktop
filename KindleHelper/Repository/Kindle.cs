using KindleHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using static KindleHelper.Models.DeviceResp;

namespace KindleHelper.Repository
{
    public class Kindle
    {
        public readonly string HserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/102.0.5005.63 Safari/537.36 Edg/102.0.1245.33";

        private static HttpClient client = new HttpClient(); 

        private KindleUrl kindleUrl;
        private string csrfToken;
        private int totalToDownload;
        private string outDir;
        private int cutLength;

        public void setCookieFromString(string cookieStr)
        {
            var client = new HttpClient();
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new CookieContainer();

           
        }

        public List<String> GetAllBooks(int startIndex, string fileType = "EBOK")
        {
            //var ownershipData = fileType == "EBOK" ? new OwnershipDataEBook() : new OwnershipDataPDoc();
            //int batchSize = 100;
            //var payload = new
            //{
            //    param = new
            //    {
            //        OwnershipData = ownershipData
            //    }
            //};


            return null;
        }

        public void DownloadBooks(int startIndex = 0, string fileType = "EBOK")
        {
            //var devices = GetDevices(KindleUrlConst.CN).Result;
            //if (devices .Count ==0)
            //{
            //    Console.WriteLine("Can not find a device");
            //    return;
            //}
            //if (startIndex > 0)
            //{
            //    Console.WriteLine($"resuming the download {startIndex + 1}/{totalToDownload}");
            //}

        }

        public async Task<List<Device>> GetDevices(KindleUrl kindleUrl)
        {
            var playload = new
            {
                param = new { GetDevices = new { } }
            };
            var data = JsonSerializer.Serialize(playload);
            Console.WriteLine(data);
            
            HttpContent content = new StringContent(JsonSerializer.Serialize(new { data = data, csrfToken = csrfToken}));
            var resp = await client.PostAsync(kindleUrl.Payload, content);
            //if(resp.StatusCode == HttpStatusCode.OK) {
            //    var content = await resp.Content.ReadAsStringAsync();
            //    var devices = JsonSerializer.Deserialize<DevicesResp>(content);
            //    if (devices.GetDevices.Count > 0)
            //    {
            //        return devices.GetDevices.Devices;
            //    }
            //}
            return new List<Device>();
        }
    }

    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooDeeds_APP.Download
{
    public delegate void DownloadErrorEventHandler(int statusCode, string errorMessage);
    public delegate void DownloadSuccessventHandler();
    public class DownloadHelper
    {
        public event DownloadErrorEventHandler OnDownloadError;
        public event DownloadSuccessventHandler OnDownloadSuccess;

        public async Task StartDownload(string API_URL, string FilePath)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage message = await client.GetAsync(API_URL);

                // Handle the erros which can happen. Usually the UI wants to display it. You can register an event to it.
                // We do not continue executing if something went wrong.
                if (!message.IsSuccessStatusCode)
                {
                    // Invoke (call the event) if it got registered somewhere!
                    OnDownloadError?.Invoke((int)message.StatusCode, message.ReasonPhrase);
                }
                else
                {
                    // Read the returned json and save it into a local file.
                    string JSON_Data = await message.Content.ReadAsStringAsync();
                    File.WriteAllText(FilePath, JSON_Data);

                    // If something hooked to the event, call it!
                    OnDownloadSuccess?.Invoke();
                }
            }
            catch (Exception ex)
            {
                OnDownloadError?.Invoke(-100, "Something went totally wrong!");
            }
        }
        
        
    }
}

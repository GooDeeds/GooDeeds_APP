using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooDeeds_APP.Download
{
    public delegate void DownloadErrorEventHandler(int statusCode, string errorMessage);
    public delegate void DownloadSuccessventHandler();

    /// <summary>
    /// This little nifty class is a simplification to get downloads from our API.
    /// You create an object of it, register the events, and call the StartDownload method.
    /// Thats it!
    /// </summary>
    public class DownloadHelper
    {
        public event DownloadErrorEventHandler OnDownloadError;
        public event DownloadSuccessventHandler OnDownloadSuccess;

        /// <summary>
        /// With that function you'll start downloading any kind of data from the Web and save it into a given file.
        /// For simplyfication reasons we assume that the data is a string-format (json, xml, etc.).
        /// Byte-Date should be handled differently (in a not yet existing way).
        /// </summary>
        /// <param name="API_URL"></param>
        /// <param name="FilePath"></param>
        /// <returns></returns>
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

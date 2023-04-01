using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GooDeeds_APP.Deeds
{
    public delegate void DeedManagerDownloadErrorEventHandler(int statusCode, string errorMessage);
    public delegate void DeedManagerDownloadSuccessventHandler();
    public delegate void DeedManagerDownloadStartedHandler();

    public static class DeedManager
    {
        public static event DeedManagerDownloadErrorEventHandler OnDeedDownloadError;
        public static event DeedManagerDownloadSuccessventHandler OnDeedDownloadSuccess;
        public static event DeedManagerDownloadStartedHandler OnDeedDownloadStart;

        // The File will be a file in the internal storage of the device (it is only accessable by the app itself)!
        public static string DeedsFileName => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "deeds.json");

        public static async Task UpdateDeeds(string API_Url)
        {
            // To prevent spamcalling the API, we check if the file is older than 5 minutes.
            // If it is younger, we call the error event appropiatly and leave the update-function.
            if (DateTime.Now.Subtract(GetLastUpdateTime()).TotalMinutes < 5)
            {
                OnDeedDownloadError?.Invoke(-1, "The file is too young to update it again!");
                return;
            }

            try
            {
                OnDeedDownloadStart?.Invoke();
                HttpClient client = new HttpClient();
                HttpResponseMessage message = await client.GetAsync(API_Url + "/deed");

                // Handle the erros which can happen. Usually the UI wants to display it. You can register an event to it.
                // We do not continue executing if something went wrong.
                if (!message.IsSuccessStatusCode)
                {
                    // Invoke (call the event) if it got registered somewhere!
                    OnDeedDownloadError?.Invoke((int)message.StatusCode, message.ReasonPhrase);
                }
                else
                {
                    // Read the returned json and save it into a local file.
                    string JSON_Data = await message.Content.ReadAsStringAsync();
                    File.WriteAllText(DeedsFileName, JSON_Data);

                    // If something hooked to the event, call it!
                    OnDeedDownloadSuccess?.Invoke();
                }
            } catch(Exception ex)
            {
                OnDeedDownloadError?.Invoke(-100, "Something went totally wrong!");
            }
        }

        /// <summary>
        /// Returns a list of all deeds. If the file does not exist, an empty list will be returned.
        /// Use the function with caution since it will not cache the return but parse it every time.
        /// This will affect performance.
        /// </summary>
        /// <returns></returns>
        public static List<Deed> GetAllDeeds()
        {
            // Check if the deed-file exists. If not return an empty list
            if (!File.Exists(DeedsFileName))
                return new List<Deed>();

            // Load the file Contents and parse the JSON to a list of deeds!
            // If anything does happen during the parsing of the file which interrupts the code (throws an exception)
            // we catch the exception and return an empty list again.
            try
            {
                List<Deed> deeds = JsonConvert.DeserializeObject<List<Deed>>(File.ReadAllText(DeedsFileName));
                return deeds;
            } catch(Exception ex)
            {
                return new List<Deed>();
            }
        }

        /// <summary>
        /// Simple function to get the last write time of the local deeds-file.
        /// If the file does not exist, it returns DateTime.MinValue.
        /// If something unexpected happened, DateTime.MinValue will be returned too.
        /// </summary>
        public static DateTime GetLastUpdateTime()
        {
            try
            {
                if (File.Exists(DeedsFileName))
                    return File.GetLastWriteTime(DeedsFileName);
                return DateTime.MinValue;
            } catch(Exception ex)
            {
                return DateTime.MinValue;
            }
        }
    }
}

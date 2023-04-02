using GooDeeds_APP.Download;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooDeeds_APP.Achievements
{
    public delegate void AchievementManagerDownloadErrorEventHandler(int statusCode, string errorMessage);
    public delegate void AchievementManagerDownloadSuccessventHandler();
    public delegate void AchievementManagerDownloadStartedHandler();
    
    public static class AchievementManager
    {
        // ID description
        // 1  - Complete a certain amount of deeds
        // 2  - Complete a certain daily streak
        // 3  - Reach a certain level
        // 4  - Complete a volunteering deed
        // 5  - Complete a donation deed
        // 6  - Complete the blood donation deed
        // 7  - Complete a event organization deed
        // 8  - Share your completed deed
        // 9  - Complete a certain amount of deeds in one day
        // 10 - Complete a certain amount of deeds in one hour
        // 11 - Create an account
        // 12 - Do every deed at least once

        public static event AchievementManagerDownloadErrorEventHandler OnAchievementDownloadError;
        public static event AchievementManagerDownloadSuccessventHandler OnAchievementDownloadSuccess;
        public static event AchievementManagerDownloadStartedHandler OnAchievementDownloadStart;

        public static string AchievementFileName => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "achievements.json");

        public static bool AchievementExists => File.Exists(AchievementFileName);

        public static List<Achievement> LoadAchievements()
        {
            try
            {
                if (AchievementExists)
                {
                    var json = File.ReadAllText(AchievementFileName);
                    var achievements = JsonConvert.DeserializeObject<List<Achievement>>(json);
                    return achievements;
                }
                else
                {
                    return new List<Achievement>();
                }
            }
            catch (Exception ex)
            {
                return new List<Achievement>();
            }
        }

        public static async Task UpdateAchievements(string API_Url)
        {
            // To prevent spamcalling the API, we check if the file is older than 5 minutes.
            // If it is younger, we call the error event appropiatly and leave the update-function.
            if (DateTime.Now.Subtract(GetLastUpdateTime()).TotalMinutes < 5)
            {
                OnAchievementDownloadError?.Invoke(-1, "The file is too young to update it again!");
                return;
            }

            try
            {
                OnAchievementDownloadStart?.Invoke();

                DownloadHelper dh = new DownloadHelper();

                dh.OnDownloadError += (errorCode, statusText) =>
                {
                    OnAchievementDownloadError?.Invoke(errorCode, statusText);
                };

                dh.OnDownloadSuccess += () =>
                {
                    OnAchievementDownloadSuccess?.Invoke();
                };

                OnAchievementDownloadStart?.Invoke();
                await dh.StartDownload(API_Url + "/deed", AchievementFileName);
            }
            catch (Exception ex)
            {
                OnAchievementDownloadError?.Invoke(-100, "Something went totally wrong!");
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
                if (File.Exists(AchievementFileName))
                    return File.GetLastWriteTime(AchievementFileName);
                return DateTime.MinValue;
            }
            catch (Exception ex)
            {
                return DateTime.MinValue;
            }
        }
    }
}
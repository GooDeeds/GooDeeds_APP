using GooDeeds_APP.Download;
using Newtonsoft.Json;

namespace GooDeeds_APP.Achievements
{
    public delegate void AchievementManagerDownloadErrorEventHandler(int statusCode, string errorMessage);
    public delegate void AchievementManagerDownloadSuccessventHandler();
    public delegate void AchievementManagerDownloadStartedHandler();


    // ID | description
    // -------------------------------------------------------
    // 1  | Complete a certain amount of deeds
    // 2  | Complete a certain daily streak
    // 3  | Reach a certain level

    // Future ideas:
    // 4  | Complete a volunteering deed
    // 5  | Complete a donation deed
    // 6  | Complete the blood donation deed
    // 7  | Complete a event organization deed
    // 8  | Share your completed deed
    // 9  | Complete a certain amount of deeds in one day
    // 10 | Complete a certain amount of deeds in one hour
    // 11 | Create an account
    // 12 | Do every deed at least once

    public enum AchievementType
    {
        COMPLETE_DEED = 1,
        COMPLETE_STREAK_DAY,
        REACH_LEVEL
    }

    public static class AchievementManager
    {


        public static event AchievementManagerDownloadErrorEventHandler OnAchievementDownloadError;
        public static event AchievementManagerDownloadSuccessventHandler OnAchievementDownloadSuccess;
        public static event AchievementManagerDownloadStartedHandler OnAchievementDownloadStart;

        public static string AchievementFileName => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "achievements.json");

        public static bool AchievementExists => File.Exists(AchievementFileName);

        public static List<Achievement> GetAchievements()
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
                await dh.StartDownload(API_Url + "/achievement", AchievementFileName);
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


        public static void UpdateAvatarAchievements(Avatar.Avatar avatar)
        {
            var achievements = GetAchievements();
            var deedHistory = avatar.QuestHistory;

            int longestStreak = 0;
            int currentStreak = 0;
            DateTime LastDeedTime = DateTime.MinValue;
            foreach (var deed in deedHistory)
            {
                if (LastDeedTime.Day != deed.CompletedAt.Day)
                {
                    if ((LastDeedTime - deed.CompletedAt).TotalDays < 1)
                        currentStreak++;
                    else if (longestStreak < currentStreak)
                    {
                        longestStreak = currentStreak;
                        currentStreak = 0;
                    }
                }

                LastDeedTime = deed.CompletedAt;
                if (longestStreak < currentStreak)
                {
                    longestStreak = currentStreak;
                }
            }

            foreach (var achievement in achievements.Where(c => !avatar.Achievements.Any(x => x.AchievementId == c.Id)))
            {
                switch (achievement.Type)
                {
                    case AchievementType.COMPLETE_STREAK_DAY:
                        {
                            if (longestStreak >= achievement.Value)
                                avatar.AddAchievement(achievement);
                            break;
                        }
                    case AchievementType.COMPLETE_DEED:
                        {
                            if(deedHistory.Count >= achievement.Value)
                                avatar.AddAchievement(achievement);
                            break;
                        }
                    case AchievementType.REACH_LEVEL:
                        {
                            if (avatar.Level >= achievement.Value)
                                avatar.AddAchievement(achievement);
                            break;
                        }
                    default:
                        break;
                }
            }
        }
    }
}
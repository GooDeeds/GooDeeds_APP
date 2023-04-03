using GooDeeds_APP.Download;
using Newtonsoft.Json;

namespace GooDeeds_APP.Achievements
{
    // The following delegates are used as Event-handler for the events of the AchievementManager.
    public delegate void AchievementManagerDownloadErrorEventHandler(int statusCode, string errorMessage);
    public delegate void AchievementManagerDownloadSuccessventHandler();
    public delegate void AchievementManagerDownloadStartedHandler();


    // Currently we have the following achievements:
    // ID | description
    // -------------------------------------------------------
    // 1  | Complete a certain amount of deeds
    // 2  | Complete a certain daily streak
    // 3  | Reach a certain level

    // In the future we might also add following achievements (as of now, they're just some ideas and not yet implemented):
    // 4  | Complete a volunteering deed
    // 5  | Complete a donation deed
    // 6  | Complete the blood donation deed
    // 7  | Complete a event organization deed
    // 8  | Share your completed deed
    // 9  | Complete a certain amount of deeds in one day
    // 10 | Complete a certain amount of deeds in one hour
    // 11 | Create an account
    // 12 | Do every deed at least once

    /// <summary>
    /// This enum defines the different types of achievements.
    /// If you want to add a new achievement-type, you want to add it here.
    /// </summary>
    public enum AchievementType
    {
        COMPLETE_DEED = 1,
        COMPLETE_STREAK_DAY,
        REACH_LEVEL
    }

    /// <summary>
    /// The AchievementManager is used to manage the achievements.
    /// It is used to download the achievements from the API and to save them to the local file system.
    /// Also it provides an easy way to unlock achievements for a given avatar.
    /// </summary>
    public static class AchievementManager
    {

        #region events
        /*
        This whole region is used to define the events of the AchievementManager.
        The events are used to notify the UI about the progress of the download.
        Currently there is only a start, success and error event.
        In the future we might want to add more events. (Such like a event when a new achievement is unlocked)
         */
        public static event AchievementManagerDownloadErrorEventHandler OnAchievementDownloadError;
        public static event AchievementManagerDownloadSuccessventHandler OnAchievementDownloadSuccess;
        public static event AchievementManagerDownloadStartedHandler OnAchievementDownloadStart;
        #endregion

        /// <summary>
        /// The AchievementFileName is a readonly property which represents the file-name (including the Path to that file) on the device.
        /// It can be uniformly used across iOS, Android.
        /// </summary>
        public static string AchievementFileName => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "achievements.json");

        /// <summary>
        /// A simple readonly property which wraps up the check for the existence of the achievement file.
        /// </summary>
        public static bool AchievementExists => File.Exists(AchievementFileName);

        /// <summary>
        /// GetAchievements returns a list of every locally available achievements.
        /// Incase no achievements were yet downloaded or something unexpected happens, this function will always return an empty list.
        /// It will never return null.
        /// </summary>
        /// <returns>A list of achievements. Incase of errors or non existent local acievements, it'll return an empty list. Never returns null.</returns>
        public static List<Achievement> GetAchievements()
        {
            try
            {
                // If the achievement-file (on the device) exists, we can go further.
                if (AchievementExists)
                {
                    // Since the file exists, we can savely read all text out of it.
                    // The text is expected to be in JSON-Format.
                    // Just in case something wents wrong here.
                    // The whole thing is wrapped in try-catch (which, when kicks in (aka. an exception was thrown) retunts an empty list. 
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
            // NOTE: This is by no means an effective prevention of "spamcalling the API".
            // To make a real effective protection against that, the API itself has to implement a call limite (per Day/Hour/Minute/Second)
            // However, in our simple MVP a timed supression like this should suffice (and show that we thought about that :P)
            if (DateTime.Now.Subtract(GetLastUpdateTime()).TotalMinutes < 5)
            {
                // In case the last file write time is less then 5 minutes ago, we dont want to download it again.
                // If we got into herer, we want to "fire" (Invoke) all the hooked event-callbacks (which registerd to our event).
                // Since it can be null (which means, that no callback was registered onto that event), we'll use the nifty '?'-Notation.
                // This acutally does the same as a typical `if(OnAchievementDownloadError != null) OnAchievementDownloadError.Invoke(...)`.
                // But it is way cleaner and if you understand that once, I would argue that this is even more readable.
                OnAchievementDownloadError?.Invoke(-1, "The file is too young to update it again!");
                return;
            }

            // If we're able to download it (last file-write time is more than 5 minutes ago), we try to download it.
            try
            {
                // Again, the same '?'-Notation, as explained above, to call all callbacks to that event.
                OnAchievementDownloadStart?.Invoke();

                // We create a new DownloadHelper-Object to download everything for us.
                // It does provide 2 events, which we can (and will) hook into.
                DownloadHelper dh = new DownloadHelper();

                // The following 2 events are fired by the DownloadHelper appropriately.
                // We just hook onto them and "feed-foward" the events.
                dh.OnDownloadError += (errorCode, statusText) =>
                {
                    OnAchievementDownloadError?.Invoke(errorCode, statusText);
                };

                dh.OnDownloadSuccess += () =>
                {
                    OnAchievementDownloadSuccess?.Invoke();
                };

                // At the very end, we do start the download.
                // We will pass a URL (in this case our API-Endpoint) and a File-Path where the received data should be stored at.
                await dh.StartDownload(API_Url + "/achievement", AchievementFileName);
            }
            catch (Exception ex)
            {
                // If something wents wrong (which should not happen, but you never know) we'll still call the appropriate event.
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

        /// <summary>
        /// This function is the heart of the manager.
        /// It implements all the logic needed to give the user a new achievement.
        /// </summary>
        /// <param name="avatar"></param>
        public static void UpdateAvatarAchievements(Avatar.Avatar avatar)
        {
            // Load all achievements and the avatar-history.
            var achievements = GetAchievements();
            var deedHistory = avatar.QuestHistory;

            // If there are no achivements, we dont have to look further since we cannot unlock something.
            if (achievements.Count == 0) return;

            // For one achievement-type it is needed, that you keep up a daily stream without interrupting it.
            // The following code calculates the longest streak you achieved.
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

            // After all data has been loaded and calculated, we can loop through all achievements.
            // To make it a bit faster (in terms of execution time) we filter out every achievement which the avatar already completed.
            // The "filtering out" is done by the linq-expression `achievements.Where(...)`.
            // Since the Where-method returns a IEnumerable<T>, we can use this directly in the foreachloop (we dont need to cast it to a list)
            foreach (var achievement in achievements.Where(c => !avatar.Achievements.Any(x => x.AchievementId == c.Id)))
            {
                // We do now switch the type of the achievement and handle everything down below.
                // The code here should be pretty self explanatory.
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
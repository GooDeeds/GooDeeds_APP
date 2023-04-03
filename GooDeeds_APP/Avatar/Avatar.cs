using GooDeeds_APP.Achievements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooDeeds_APP.Avatar
{
    public delegate void AvatarQuestAddedToHistoryHandler();
    public delegate void AvatarDataChangedHandler();

    /// <summary>
    /// This class defines everything needed in order for the App to work with the Avatar.
    /// The Avatar gets Serialized (and obviously deserialized at some point again) into a json-formatted string.
    /// The serialization serialize every public field and property (with the exception of static fields and properties and private/protected getter).
    /// This string than gets stored into a file.
    /// </summary>
    public class Avatar
    {
        #region events
        public event AvatarDataChangedHandler AvatarDataChanged;
        public event AvatarQuestAddedToHistoryHandler QuestAddedToHistory;
        #endregion

        // The following fields are private. A representing public property is wrapping around them.
        // Such fields are called "backing fields". (They backup the public property)
        // We use that procedure to call the event "AvatarDataChanged" whenever the value of the property changes (which is really helpful for the UI)
        private string _name = "";
        private uint _experience = 0;
        private Profession _profession = new Profession();

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                AvatarDataChanged?.Invoke();
            }
        }
        public uint Experience
        {
            get => _experience;
            set
            {
                _experience = value;
                AvatarDataChanged?.Invoke();
            }
        }
        public Profession Profession
        {
            get => _profession;
            set
            {
                _profession = value;
                AvatarDataChanged?.Invoke();
            }
        }

        // A readonly property to get the Level of the Avatar.
        public uint Level => AvatarManager.GetLevel(Experience);

        // The QuestHistory is initialized with an empty list.
        // Otherwise a new Character would have a null-object as QuestHistory (and thus might cause confusion later).
        public List<QuestHistoryEntry> QuestHistory { get; set; } = new List<QuestHistoryEntry>();
        public void PopulateQuestHistory()
        {
            foreach (var quest in QuestHistory)
            {
                Experience += quest.EarnedExperience;
            }
        }
        
        public void AddQuestToHistory(QuestHistoryEntry quest)
        {
            QuestHistory.Add(quest);
            Experience += quest.EarnedExperience;
            QuestAddedToHistory?.Invoke();
            AvatarManager.SaveAvatar(this);
            AchievementManager.UpdateAvatarAchievements(this);
        }

        // The same thing as with the QuestHistory applies to the 
        public List<AchievementEntry> Achievements { get; set; } = new List<AchievementEntry>();

        public void AddAchievement(Achievement achievement)
        {
            if (Achievements.Any(a => a.AchievementId == achievement.Id))
                return;

            Achievements.Add(new AchievementEntry()
            {
                AchievementId = achievement.Id,
                CompletedAt = DateTime.Now
            });

            AvatarManager.SaveAvatar(this);
        }
    }
}

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
    /// Currently we only store the name, experience and profession of the Avatar.
    /// In the future we might want to add more properties to this class.
    /// Such as: achievement, titles (unlocked with achievements).
    /// Just imagine you can get called "Grimjaw, The unbreakable Dward Warrior".
    /// </summary>
    public class Avatar
    {
        public event AvatarDataChangedHandler AvatarDataChanged;

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

        public uint Level => AvatarManager.GetLevel(Experience);

        public List<QuestHistoryEntry> QuestHistory { get; set; } = new List<QuestHistoryEntry>();
        public void PopulateQuestHistory()
        {
            foreach (var quest in QuestHistory)
            {
                Experience += quest.EarnedExperience;
            }
        }

        public event AvatarQuestAddedToHistoryHandler QuestAddedToHistory;
        public void AddQuestToHistory(QuestHistoryEntry quest)
        {
            QuestHistory.Add(quest);
            Experience += quest.EarnedExperience;
            QuestAddedToHistory?.Invoke();
            AvatarManager.SaveAvatar(this);
            AchievementManager.UpdateAvatarAchievements(this);
        }

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

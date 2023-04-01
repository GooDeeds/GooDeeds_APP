using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooDeeds_APP.Avatar
{
    public delegate void AvatarQuestAddedToHistoryHandler();
    /// <summary>
    /// This class defines everything needed in order for the App to work with the Avatar.
    /// Currently we only store the name, experience and profession of the Avatar.
    /// In the future we might want to add more properties to this class.
    /// Such as: achievement, titles (unlocked with achievements).
    /// Just imagine you can get called "Grimjaw, The unbreakable Dward Warrior".
    /// </summary>
    public class Avatar
    {
        public string Name { get; set; }
        public uint Experience { get; set; }
        public Profession Profession { get; set; }

        public uint Level => AvatarManager.GetLevel(Experience);

        List<QuestHistoryEntry> QuestHistory { get; set; } = new List<QuestHistoryEntry>();
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
        }
    }
}

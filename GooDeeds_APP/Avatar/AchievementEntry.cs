using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooDeeds_APP.Avatar
{
    /// <summary>
    /// This class is a simple mapping class for the Avatars achievements.
    /// It is used to store the achievements in the avatar (which will get json-serialized).
    /// This is helpful to not save the overhead of achievements (which would duplicate them, since they're stored in a file seperately from the avatar).
    /// </summary>
    public class AchievementEntry
    {
        public int AchievementId { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}

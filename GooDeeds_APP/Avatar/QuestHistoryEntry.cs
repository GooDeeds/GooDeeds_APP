using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooDeeds_APP.Avatar
{
    /// <summary>
    /// This class is a simple mapping class for the avatar deed history.
    /// It is used to store the deed-history in the avatar (which will get json-serialized).
    /// </summary>
    public class QuestHistoryEntry
    {
        public int DeedId { get; set; }
        public uint EarnedExperience { get; set; }

        public DateTime CompletedAt { get; set; }
    }
}

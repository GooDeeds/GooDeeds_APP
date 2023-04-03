using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooDeeds_APP.Achievements
{
    /// <summary>
    /// This calls is used as a mapping-class for the achievements.
    /// It gets automatically mapped (via ORM) with the awesome Newtonsoft JSON lirbary.
    /// This is needed because the API returns a JSON-String which we need to map to this class.
    /// </summary>
    public class Achievement
    {
        public int Id { get; set; }
        public int Value { get; set; }
        /// <summary>
        /// This enum is defined in <see cref="AchievementManager"/>
        /// </summary>
        public AchievementType Type { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

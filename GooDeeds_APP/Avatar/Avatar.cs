using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooDeeds_APP.Avatar
{
    /// <summary>
    /// This class defines everything needed in order for the App to work with the Avatar.
    /// Currently we only store the name, experience and profession of the Avatar.
    /// In the future we might want to add more properties to this class.
    /// Such as: 
    /// </summary>
    public class Avatar
    {
        public string Name { get; set; }
        public uint Experience { get; set; }
        public Profession Profession { get; set; }

        public uint Level => AvatarManager.GetLevel(Experience);
    }
}

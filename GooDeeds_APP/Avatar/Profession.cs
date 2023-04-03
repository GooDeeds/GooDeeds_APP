using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooDeeds_APP.Avatar
{
    /// <summary>
    /// The enum contains every profession you can choose for your character.
    /// </summary>
    public enum ProfessionType
    {
        Assassin = 1,
        Berserk,
        Cleric,
        Druid,
        Knight,
        Mage,
        Paladin,
        Ranger,
        Rogue,
        Sorcerer,
        Warrior,
        Wizard
    }

    /// <summary>
    /// This enum contains every race you can choose for your character.
    /// </summary>
    public enum RaceType
    {
        Human = 1,
        Elf = 2,
        Dwarf = 3,
        Orc = 4,
        Gnome = 5,
        Troll = 6,
        Undead = 7
    }

    /// <summary>
    /// This is a very simple mapping class (for ORM) for the profession.
    /// We use the class since you cannot use null values for enums.
    /// TODO: In the future it might be wise to refactor the name or the usage of this class. (just bring the profession and race directly to the avatar class)
    /// </summary>
    public class Profession
    {
        public ProfessionType Type { get; set; }
        public RaceType Race { get; set; }
    }
}

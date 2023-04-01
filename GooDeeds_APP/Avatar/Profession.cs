using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooDeeds_APP.Avatar
{
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

    public class Profession
    {
        public ProfessionType Type { get; set; }
        public RaceType Race { get; set; }
    }
}

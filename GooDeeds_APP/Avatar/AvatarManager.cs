using Newtonsoft.Json;
using System.Text;

namespace GooDeeds_APP.Avatar
{
    public class AvatarManager
    {
        public static string AvatarFileName => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "avatar.json");

        public static bool AvatarExists => File.Exists(AvatarFileName);

        public static Avatar LoadAvatar()
        {
            try
            {
                if (AvatarExists)
                {
                    var json = File.ReadAllText(AvatarFileName);
                    return JsonConvert.DeserializeObject<Avatar>(json);
                }
                else
                {
                    return new Avatar();
                }
            } catch(Exception ex)
            {
                return new Avatar();
            }
        }

        public static void SaveAvatar(Avatar avatar)
        {
            try
            {
                File.WriteAllText(AvatarFileName, JsonConvert.SerializeObject(avatar));
            }
            catch (Exception ex)
            {
                
            }
        }

        public static string GetDescription(RaceType race, ProfessionType profession)
        {
            StringBuilder sb = new StringBuilder();
            if (race == RaceType.Human)
                sb.AppendLine("The Humans are adaptable and versatile, they are the most common race, capable of wielding both magic and weapons with equal skill.");
            else if (race == RaceType.Elf)
                sb.AppendLine("The elves are graceful and wise, they possess a deep connection to nature and the magic that flows through it. Their agility and archery skills make them deadly opponents in battle.");
            else if (race == RaceType.Dwarf)
                sb.AppendLine("The Dwarves are a sturdy and resilient race, with a natural affinity for crafting and mining. Their strength and endurance allow them to withstand even the most punishing of attacks.");
            else if (race == RaceType.Orc)
                sb.AppendLine("The Orcs are a fierce and brutal race, renowned for their ferocity in battle. Their raw physical power and proficiency in close combat make them a formidable force to be reckoned with.");
            else if (race == RaceType.Gnome)
                sb.AppendLine("Gnomes are diminutive and inventive, with a natural aptitude for tinkering and inventing. Their knowledge of technology and machines can prove invaluable in both combat and exploration.");
            else if (race == RaceType.Troll)
                sb.AppendLine("Trolls are massive and intimidating, with a natural resistance to magic and an insatiable appetite for destruction. Their regenerative abilities make them difficult to defeat, and their brute strength can cause even the mightiest foes to tremble.");
            else
                sb.AppendLine("Undead are a mysterious and enigmatic race, with the ability to harness dark magic and raise the dead to fight for their cause. Their very presence can strike fear into the hearts of their enemies.");

            sb.AppendLine();

            if (profession == ProfessionType.Assassin)
                sb.AppendLine("As an Assassin, your job is to sow chaos behind enemy lines with stealth and precision. You excel at infiltrating enemy strongholds undetected, taking out high-value targets with swift and deadly force, and disappearing just as quickly. Your expertise in poisons, traps, and subterfuge allows you to turn the tide of battle in your favor and strike fear into the hearts of your enemies.");
            else if (profession == ProfessionType.Berserk)
                sb.AppendLine("As a Berserker, you are a wild and fearsome warrior who channels your rage into unstoppable fury on the battlefield. With unyielding willpower, you charge into the fray, heedless of danger or pain, wielding your massive weapon with brutal force. Your enemies tremble at the sight of you, for they know that you are an unstoppable force of destruction.");
            else if (profession == ProfessionType.Cleric)
                sb.AppendLine("As a Cleric, you are a holy warrior devoted to your chosen deity and capable of wielding divine magic. You serve as a beacon of hope to your allies, providing healing and protection in the midst of battle, and banishing evil with your righteous fury. Your unwavering faith and devotion to your cause make you a formidable ally and a fearsome opponent.");
            else if (profession == ProfessionType.Druid)
                sb.AppendLine("As a Druid, you are a master of nature magic, with the ability to manipulate the elements and transform into animal forms. You draw your power from the natural world and use it to heal, protect, and summon powerful natural forces to aid you in combat. Your bond with the earth gives you a unique perspective on the world around you, and your connection to the spirits of the wild makes you a powerful ally to all who fight for the balance of nature.");
            else if (profession == ProfessionType.Knight)
                sb.AppendLine("As a Knight, you are a noble warrior, skilled in the art of combat and sworn to defend your kingdom's honor. You are heavily armored and wield powerful weapons, serving as the front line of defense against your enemies. You stand for chivalry, honor, and duty, and your unwavering dedication to your cause inspires those around you to fight with renewed vigor.");
            else if (profession == ProfessionType.Mage)
                sb.AppendLine("As a Mage, you are a master of arcane magic, with the power to bend reality to your will. You draw your power from the elements, the cosmos, or the mysterious forces that exist between worlds, and use it to cast spells that can level mountains or tear apart armies. Your knowledge of the arcane is vast and complex, and you are constantly seeking to unlock new secrets and push the boundaries of your power.");
            else if (profession == ProfessionType.Paladin)
                sb.AppendLine("As a Paladin, you are a holy warrior, devoted to your deity and sworn to uphold justice and righteousness in the world. You use your divine powers to heal the wounded, banish the undead, and smite the forces of darkness. You are a beacon of hope in a world of darkness, and your unwavering devotion to your cause inspires others to join you in the fight for good.");
            else if (profession == ProfessionType.Ranger)
                sb.AppendLine("As a Ranger, you are a skilled hunter and tracker, capable of surviving in even the harshest of wilderness environments. You are an expert in archery, and your ability to strike from a distance makes you a formidable opponent. You have a deep connection to the land, and can call upon the forces of nature to aid you in battle.");
            else if (profession == ProfessionType.Rogue)
                sb.AppendLine("As a Rogue, you are a master of stealth and deception, capable of infiltrating even the most heavily guarded locations. You are a skilled pickpocket, lockpicker, and trap-disarmer, and can use your cunning to turn the tide of battle in your favor. Your expertise in subterfuge and misdirection make you a valuable asset to any team.");
            else if (profession == ProfessionType.Sorcerer)
                sb.AppendLine("As a Sorcerer, you are a master of magic, with the ability to manipulate the very fabric of reality. You possess innate magical abilities that allow you to cast spells without the need for study or preparation. Your magic is fueled by raw emotion, and your spells are a reflection of your innermost desires and fears. You are capable of wielding tremendous power, but your magic comes with great risk, for the forces you command are often unpredictable and dangerous.");
            else if (profession == ProfessionType.Warrior)
                sb.AppendLine("As a Warrior, you are a battle-hardened fighter, skilled in the art of combat and equipped with the finest weapons and armor. You are a master of close combat, and can deliver devastating blows with your sword, axe, or mace. Your bravery and determination on the battlefield are an inspiration to your allies, and your unwavering resolve in the face of danger makes you a fearsome opponent.");
            else if (profession == ProfessionType.Wizard)
                sb.AppendLine("As a Wizard, you are a scholar of the arcane, with a vast knowledge of spells and magic. You spend years studying ancient tomes and delving into the mysteries of the cosmos in order to unlock the secrets of the universe. Your spells are meticulously crafted and powerful, capable of altering reality on a fundamental level. You are a master of both offensive and defensive magic, and your intellect and cunning are your greatest weapons in battle.");

            return sb.ToString();
        }
    }
}

using GooDeeds_APP.Avatar;
using GooDeeds_APP.Deeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooDeeds_APP.ShareHelper
{
    public static class ShareDeedGenerator
    {
        
        public static string GenerateDeedText(Deed d)
        {
            string text = "";

            var avatar = AvatarManager.LoadAvatar();
            
            string deedTitle = d.Title;
            string deedDescription = d.Description;
            string avatarName = avatar.Name;
            string avatarRace = Enum.GetName(avatar.Profession.Race);
            string avatarProfession = Enum.GetName(avatar.Profession.Type);
            if (d.DeedGenerator == 1)
            {
                text = $@"Dear Adventurer,

Your fellow GooDeed user, {avatarName}, a {avatarRace} {avatarProfession}, has just completed an incredible deed that we want to share with you. {deedTitle} is a {GetRandomAdjective(0)}, {GetRandomAdjective(1)}, and {GetRandomAdjective(2)} accomplishment that {avatarName} can be proud of.

{avatarName}'s deed involved {GetRandomVerb(0)} {GetRandomAdjective(3)} {GetRandomNoun(0)} by {GetRandomVerb(1)} {GetRandomAdjective(4)} {GetRandomNoun(1)}, which is truly inspiring. It takes generosity and compassion to make a difference in someone's life.

But {avatarName} didn't stop there. They went on to {GetRandomVerb(2)} {GetRandomAdjective(6)} {GetRandomNoun(2)} and {GetRandomVerb(3)} {GetRandomAdjective(7)} {GetRandomNoun(3)}, showing that they are truly committed to making the world a better place.

We hope that {avatarName}'s story motivates you to take action and donate to a charity that you are passionate about. Remember, every donation, no matter how small, can make a big impact in the lives of those in need.

Yours in good deeds,
The GooDeed Team";
            } else if (d.DeedGenerator == 2)
            {
                text = $@"Dear Adventurer,

We are thrilled to share with you an incredible deed accomplished by our fellow GooDeed user, {avatar.Name}, a {avatar.Profession.Race} {avatar.Profession.Type}. {deedTitle} is a {GetRandomAdjective(0)}, {GetRandomAdjective(1)}, and {GetRandomAdjective(2)} accomplishment that {avatar.Name} can be proud of.

{avatar.Name} took the initiative to {GetRandomVerb(0)} {GetRandomAdjective(3)} {GetRandomNoun(0)} and {GetRandomVerb(1)} {GetRandomNoun(1)}, giving a meal to a homeless chap and making his tummy go from grumbly to humbly jumbly! 

We hope this story inspires you to do a good deed for someone in need today.

Yours in good deeds,
The GooDeed Team";
            } else if (d.DeedGenerator == 3)
            {
                text = $@"Dear Adventurer,

Your fellow GooDeed user, {avatar.Name}, a {avatar.Profession.Race} {avatar.Profession.Type}, has just completed an incredible deed that we want to share with you. {deedTitle} is a {GetRandomAdjective(0)}, {GetRandomAdjective(1)}, and {GetRandomAdjective(2)} accomplishment that {avatar.Name} can be proud of.

{avatar.Name}'s deed involved {GetRandomVerb(0)} {GetRandomAdjective(3)} {GetRandomNoun(0)} by {GetRandomVerb(1)} {GetRandomAdjective(4)} {GetRandomNoun(1)}, which is truly inspiring. It takes generosity and compassion to make a difference in someone's life.

But {avatar.Name} didn't stop there. They went on to {GetRandomVerb(2)} {GetRandomAdjective(6)} {GetRandomNoun(2)} and {GetRandomVerb(3)} {GetRandomAdjective(7)} {GetRandomNoun(3)}, showing that they are truly committed to making the world a better place.

We hope that {avatar.Name}'s story motivates you to take action and give a homeless person a Euro. How about parting ways with a shiny coin to a homeless chap in need of some dough?

Yours in good deeds,
The GooDeed Team";
            }
            else if (d.DeedGenerator == 4)
            {
                text = $@"Dear Adventurer,

Your fellow GooDeed user, {avatar.Name}, a {avatar.Profession.Race} {avatar.Profession.Type}, has just completed an incredible deed that we want to share with you. '{d.Title}' is a {GetRandomAdjective(0)}, {GetRandomAdjective(1)}, and {GetRandomAdjective(2)} accomplishment that {avatar.Name} can be proud of.

{avatar.Name}'s deed involved {GetRandomVerb(0)} {GetRandomAdjective(3)} {GetRandomNoun(0)} by {GetRandomVerb(1)} {GetRandomAdjective(4)} {GetRandomNoun(1)}, which is truly inspiring.

But {avatar.Name} didn't stop there. They went on to {GetRandomVerb(2)} {GetRandomAdjective(6)} {GetRandomNoun(2)} and {GetRandomVerb(3)} {GetRandomAdjective(7)} {GetRandomNoun(3)}, showing that they are truly committed to making the world a better place.

We hope that {avatar.Name}'s story motivates you to take action and {d.Description}.

Yours in good deeds,
The GooDeed Team";
            }
            else if (d.DeedGenerator == 5)
            {
                text = $@"Dear Adventurer,

Your fellow GooDeed user, {avatar.Name}, a {avatar.Profession.Race} {avatar.Profession.Type}, has just completed an incredible deed that we want to share with you. {d.Title} is a {GetRandomAdjective(0)}, {GetRandomAdjective(1)}, and {GetRandomAdjective(2)} accomplishment that {avatar.Name} can be proud of.

{avatar.Name}'s deed involved {GetRandomVerb(0)} {GetRandomAdjective(3)} {GetRandomNoun(0)} by {GetRandomVerb(1)} {GetRandomAdjective(4)} {GetRandomNoun(1)}, which is truly inspiring. It takes bravery and kindness to give a part of yourself to help someone else.

But {avatar.Name} didn't stop there. They went on to {GetRandomVerb(2)} {GetRandomAdjective(6)} {GetRandomNoun(2)} and {GetRandomVerb(3)} {GetRandomAdjective(7)} {GetRandomNoun(3)}, showing that they are truly committed to making the world a better place.

We hope that {avatar.Name}'s story motivates you to consider donating blood and saving lives. It only takes a little bit of your time and it can make a big impact.

Yours in good deeds,
The GooDeed Team";
            }
            else if (d.DeedGenerator == 6)
            {
                text = $@"Dear Adventurer,

Your fellow GooDeed user, {avatar.Name}, a {avatar.Profession.Race} {avatar.Profession.Type}, has just completed an incredible deed that we want to share with you. '{d.Title}' is a {GetRandomAdjective(0)}, {GetRandomAdjective(1)}, and {GetRandomAdjective(2)} accomplishment that {avatar.Name} can be proud of.

{avatar.Name}'s deed involved {GetRandomVerb(0)} {GetRandomAdjective(3)} {GetRandomNoun(0)} by {GetRandomVerb(1)} {GetRandomAdjective(4)} {GetRandomNoun(1)}, which is truly inspiring. It takes generosity and compassion to make a difference in someone's life.

But {avatar.Name} didn't stop there. They went on to {GetRandomVerb(2)} {GetRandomAdjective(6)} {GetRandomNoun(2)} and {GetRandomVerb(3)} {GetRandomAdjective(7)} {GetRandomNoun(3)}, showing that they are truly committed to making the world a better place.

We hope that {avatar.Name}'s story motivates you to clean out your wardrobe and donate some clothes to those in need. Remember, every little bit helps make a difference in someone's life.

Yours in good deeds,
The GooDeed Team";
            }
            else if (d.DeedGenerator == 7)
            {
                text = $@"Dear Adventurer,

Your fellow GooDeed user, {avatar.Name}, a {avatar.Profession.Race} {avatar.Profession.Type}, has just completed an incredible deed that we want to share with you. '{d.Title}' is a {GetRandomAdjective(0)}, {GetRandomAdjective(1)}, and {GetRandomAdjective(2)} accomplishment that {avatar.Name} can be proud of.

{avatar.Name}'s deed involved {GetRandomVerb(0)} {GetRandomAdjective(3)} {GetRandomNoun(0)} by {GetRandomVerb(1)} {GetRandomAdjective(4)} {GetRandomNoun(1)}, which is truly inspiring. It takes patience and kindness to make a difference in the lives of these furry friends.

But {avatar.Name} didn't stop there. They went on to {GetRandomVerb(2)} {GetRandomAdjective(6)} {GetRandomNoun(2)} and {GetRandomVerb(3)} {GetRandomAdjective(7)} {GetRandomNoun(3)}, showing that they are truly committed to making the world a better place.

We hope that {avatar.Name}'s story motivates you to consider volunteering at an animal shelter near you. Not only will you be making a positive impact in the lives of these sweet creatures, but you'll also get to enjoy some cuddles and love in return!

Yours in good deeds,
The GooDeed Team";
            }
            else if (d.DeedGenerator == 8)
            {
                text = $@"Dear Adventurer,

Your fellow GooDeed user, {avatar.Name}, a {avatar.Profession.Race} {avatar.Profession.Type}, has just completed an incredible deed that we want to share with you..

Recently, {avatar.Name} volunteered at the local food bank known as Tafel. It may not have been a luxurious experience, as it involved sorting through piles of canned goods, scrubbing dirty dishes, and hauling heavy boxes around. But it was all worth it, as {avatar.Name} helped make a difference in the community.

We hope that {avatar.Name}'s story inspires you to take action and consider volunteering at a local food bank or organization that helps those in need.

Yours in good deeds,
The GooDeed Team";
            }

            return text;
        }

        private static string GetRandomAdjective(int index)
        {
            return Adjectives[index][new Random().Next(0, 5)];
        }
        private static string GetRandomVerb(int index)
        {
            return Verbs[index][new Random().Next(0, 5)];
        }
        private static string GetRandomNoun(int index)
        {
            return Nouns[index][new Random().Next(0,5)];
        }
        
        static List<List<String>> Adjectives = new List<List<string>>
        {
            new List<string>
            {
                "impressive",
                "remarkable",
                "astounding",
                "extraordinary",
                "outstanding"
            },
            new List<string>
            {
                "courageous",
                "inspiring",
                "admirable",
                "commendable",
                "valiant"
            },
            new List<string>
            {
                "magnificent",
                "splendid",
                "majestic",
                "glorious",
                "superb"
            },
            new List<string>
            {
                "beautifully",
                "creatively",
                "expertly",
                "diligently",
                "skillfully"
            },
            new List<string>
            {
                "deserving",
                "worthy",
                "noble",
                "honorable",
                "laudable"
            },
            new List<string>
            {
                "exceptional",
                "masterful",
                "proficient",
                "skillful",
                "talented"
            },
            new List<string>
            {
                "helping",
                "supporting",
                "assisting",
                "benefiting",
                "comforting"
            },
            new List<string>
            {
                "inspiring",
                "motivating",
                "encouraging",
                "uplifting",
                "empowering"
            }
        };

        static List<List<String>> Verbs = new List<List<string>>
        {
            new List<string>
            {
                "creating",
                "building",
                "designing",
                "crafting",
                "constructing"
            },
            new List<string>
            {
                "repairing",
                "restoring",
                "renewing",
                "refurbishing",
                "revitalizing"
            },
            new List<string>
            {
                "caring for",
                "nurturing",
                "cultivating",
                "developing",
                "supporting"
            },
            new List<string>
            {
                "caring for",
                "nurturing",
                "cultivating",
                "developing",
                "supporting"
            }
        };

        static List<List<String>> Nouns = new List<List<string>>
        {
            new List<string>
            {
                "garden",
                "community",
                "environment",
                "park",
                "habitat"
            },
            new List<string>
            {
                "wildlife",
                "nature",
                "ecosystem",
                "landscape",
                "biodiversity"
            },
            new List<string>
            {
                "shelter",
                "home",
                "house",
                "habitat",
                "residence"
            },
            new List<string>
            {
                "school",
                "hospital",
                "library",
                "orphanage",
                "senior center"
            }
        };
    }
}

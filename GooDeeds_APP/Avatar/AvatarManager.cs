using Newtonsoft.Json;

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
    }
}

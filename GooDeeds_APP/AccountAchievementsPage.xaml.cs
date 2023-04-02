using GooDeeds_APP.Achievements;
using GooDeeds_APP.Avatar;

namespace GooDeeds_APP;

public class AchievementListEntry
{
    public string Title { get; set; }
    public string Description { get; set; }

    public DateTime CompletedAt { get; set; }
}

public partial class AccountAchievementsPage : ContentPage
{
	public AccountAchievementsPage()
	{
		InitializeComponent();
        List<AchievementListEntry> data = new List<AchievementListEntry>();
        var achievements = AchievementManager.GetAchievements();
        foreach (var q in AvatarManager.LoadAvatar().Achievements)
        {
            var achievement = achievements.First(a => a.Id == q.AchievementId);
            data.Add(new AchievementListEntry()
            {
                Title = achievement.Name,
                Description = achievement.Description,
                CompletedAt = q.CompletedAt
            });
        }
        DeedList.ItemsSource = data;
    }

    private void DeedList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        DeedList.SelectedItem = null;
    }
}
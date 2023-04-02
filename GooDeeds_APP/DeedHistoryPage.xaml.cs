using GooDeeds_APP.Avatar;
using GooDeeds_APP.Deeds;

namespace GooDeeds_APP;

 public class QuestHistoryListEntry
{
    public string Title { get; set; }
    public string Description { get; set; }
    public uint EarnedExperience { get; set; }

    public DateTime CompletedAt { get; set; }
}

public partial class DeedHistoryPage : ContentPage
{
	public DeedHistoryPage()
	{
		InitializeComponent();
        List<QuestHistoryListEntry> data = new List<QuestHistoryListEntry>();
        var deeds = DeedManager.GetAllDeeds();
        foreach (var q in AvatarManager.LoadAvatar().QuestHistory)
        {
            var deed = deeds.First(d => d.Id == q.DeedId);
            if (deed == null) continue;
            data.Add(new QuestHistoryListEntry()
            {
                Title = deed.Title,
                Description = deed.Description,
                EarnedExperience = q.EarnedExperience,
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
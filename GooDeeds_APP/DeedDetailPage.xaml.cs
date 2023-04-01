using GooDeeds_APP.Deeds;

namespace GooDeeds_APP;

public partial class DeedDetailPage : ContentPage
{
    public Deed Deed { get; private set; }
    public DeedDetailPage()
	{
		InitializeComponent();
        IsVisible = false;
    }

    public void SetDeed(Deed deed)
    {
        Deed = deed;
        BindingContext = this;
        
        TitleText.Text = deed.Title;
        DescriptionText.Text = deed.Description;
        IsVisible = true;
    }

    public async void btn_Complete_Clicked(object sender, EventArgs e)
    {
        Avatar.QuestHistoryEntry questHistoryEntry = new Avatar.QuestHistoryEntry();
        questHistoryEntry.CompletedAt = DateTime.Now;
        questHistoryEntry.EarnedExperience = Deed.Experience;
        questHistoryEntry.DeedId = Deed.Id;

        Avatar.AvatarManager.LoadAvatar().AddQuestToHistory(questHistoryEntry);

        if (Parent is NavigationPage np)
        {
            DeedCompletedPage dcp = new DeedCompletedPage(Deed);
            await np.PushAsync(dcp);
        }
    }
}
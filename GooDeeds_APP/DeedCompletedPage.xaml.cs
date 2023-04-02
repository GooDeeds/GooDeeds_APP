using GooDeeds_APP.Avatar;
using GooDeeds_APP.Deeds;
using GooDeeds_APP.ShareHelper;

namespace GooDeeds_APP;

public partial class DeedCompletedPage : ContentPage
{
    Avatar.Avatar avatar;
    Deed deed = null;

    public DeedCompletedPage(Deed deed)
    {
        InitializeComponent();
        avatar = AvatarManager.LoadAvatar();
        SetPercentage(AvatarManager.GetLevelProgress(avatar.Experience));
        DeedText.Text = "You have completed: " + deed.Title;
        this.deed = deed;
    }

    public void SetPercentage(int percentage)
    {
        SetPercentage(percentage / 100.0);
    }

    public void SetPercentage(double percentage)
    {
        ProgressBar.Progress = percentage;
        ExperienceText.Text = avatar.Experience + " / " + AvatarManager.GetNeededExperience(avatar.Level + 1);
        LevelText.Text = avatar.Level.ToString();
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        ShareHelper.ShareHelper.ShareDeed(deed);
    }
}
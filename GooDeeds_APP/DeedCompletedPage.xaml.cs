using GooDeeds_APP.Avatar;
using GooDeeds_APP.Deeds;

namespace GooDeeds_APP;

public partial class DeedCompletedPage : ContentPage
{
    Avatar.Avatar avatar;

    public DeedCompletedPage(Deed deed)
    {
        InitializeComponent();
        avatar = AvatarManager.LoadAvatar();
        SetPercentage(AvatarManager.GetLevelProgress(avatar.Experience));
        DeedText.Text = "You have completed: " + deed.Title;
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
}
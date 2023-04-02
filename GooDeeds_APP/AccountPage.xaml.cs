using GooDeeds_APP.Avatar;

namespace GooDeeds_APP;

public partial class AccountPage : ContentPage
{
    Avatar.Avatar avatar;
    public AccountPage()
	{
		InitializeComponent();
        avatar = AvatarManager.LoadAvatar();
        avatar.QuestAddedToHistory += UpdateAvatarInformation;
        avatar.AvatarDataChanged += UpdateAvatarInformation;
        if (!string.IsNullOrEmpty(avatar.Name))
        {
            SetPercentage(AvatarManager.GetLevelProgress(avatar.Experience));
            GreetingsText.Text = "Greetings, " + avatar.Name + "!";
            AvatarImage.Source = "avatar_race_" + (int)avatar.Profession.Race + ".svg";
        }
    }

    private void UpdateAvatarInformation()
    {
        SetPercentage(AvatarManager.GetLevelProgress(avatar.Experience));
        GreetingsText.Text = "Greetings, " + avatar.Name + "!";
        AvatarImage.Source = "avatar_race_" + (int)avatar.Profession.Race + ".svg";
        SetPercentage(AvatarManager.GetLevelProgress(avatar.Experience));
    }

    public void SetPercentage(int percentage)
    {
        SetPercentage(percentage / 100.0);
    }

    public void SetPercentage(double percentage)
    {
        ProgressBar.Progress = percentage;
        ExperienceText.Text = avatar.Experience + " / " + AvatarManager.GetNeededExperience(avatar.Level + 1) + " EXP";
        LevelText.Text = "Level " + avatar.Level.ToString();
    }
    

    private void Settings_Clicked(object sender, EventArgs e)
    {

    }

    private async void HistoryButton_Clicked(object sender, EventArgs e)
    {
        if (Parent is TabbedPage tp && tp.Parent is NavigationPage np)
        {
            DeedHistoryPage dhp = new DeedHistoryPage();
            await np.PushAsync(dhp);
        }
    }

    private async void AchievementsButton_Clicked(object sender, EventArgs e)
    {
        if (Parent is TabbedPage tp && tp.Parent is NavigationPage np)
        {
            AccountAchievementsPage aap = new AccountAchievementsPage();
            await np.PushAsync(aap);
        }
    }
}
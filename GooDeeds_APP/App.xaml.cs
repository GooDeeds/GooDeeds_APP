using GooDeeds_APP.Avatar;

namespace GooDeeds_APP;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        
        MainPage = new AppShell();

        if (!AvatarManager.AvatarExists)
        {
            MainPage.Navigation.PushAsync(new AvatarCreationPage());
        }
        
		if (!Preferences.Get("introduction_shown", false))
        {
            MainPage.Navigation.PushAsync(new IntroductionPage());
            Preferences.Set("introduction_shown", true);
        }
    }
}

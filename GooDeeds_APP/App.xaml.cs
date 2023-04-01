using GooDeeds_APP.Avatar;
using Microsoft.Extensions.Configuration;

namespace GooDeeds_APP;

public partial class App : Application
{
    public static App Current => (App)Application.Current;
    private IConfiguration config;
    public static void UpdateMainPage()
    {
        Current.MainPage = new NavigationPage(new MainPage(Current.config));

        if (!Preferences.Get("introduction_shown", false))
        {
            Current.MainPage = new IntroductionPage();
            Preferences.Set("introduction_shown", true);
        } else if (!AvatarManager.AvatarExists)
        {
            Current.MainPage = new AvatarCreationPage();
        }
    }
    public App(IConfiguration config)
	{
		InitializeComponent();
        this.config = config;
        UpdateMainPage();
    }
}

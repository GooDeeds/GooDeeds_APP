using GooDeeds_APP.Avatar;
using Microsoft.Extensions.Configuration;

namespace GooDeeds_APP;

public partial class App : Application
{
    private static App _instance;
    private IConfiguration config;
    public static void UpdateMainPage()
    {
        _instance.MainPage = new MainPage(_instance.config);

        if (!Preferences.Get("introduction_shown", false))
        {
            _instance.MainPage = new IntroductionPage();
            Preferences.Set("introduction_shown", true);
        } else if (!AvatarManager.AvatarExists)
        {
            _instance.MainPage = new AvatarCreationPage();
        }
    }
    public App(IConfiguration config)
	{
		InitializeComponent();
        this.config = config;
        _instance = this;
        UpdateMainPage();
    }
}

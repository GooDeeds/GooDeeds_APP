using GooDeeds_APP.Deeds;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Bson;
using Plugin.LocalNotification;

namespace GooDeeds_APP;

public partial class MainPage : TabbedPage
{
    public IConfiguration config { get; private set; }

    public MainPage(IConfiguration config)
	{
		InitializeComponent();
        this.config = config;
    }
}


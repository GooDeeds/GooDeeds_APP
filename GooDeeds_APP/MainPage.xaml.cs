using GooDeeds_APP.Deeds;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Bson;
using Plugin.LocalNotification;

namespace GooDeeds_APP;

public partial class MainPage : ContentPage
{
    IConfiguration config;
	int count = 0;

	public MainPage(IConfiguration config)
	{
		InitializeComponent();
        InitializeNotification();

        this.config = config;
    }

    private void InitializeDeedDownload(string API_Url)
    {
        AnimateInfoBox(true);
        // Download newest Deeds!
        SetInfoBoxText("Downloading new Deeds!", true);
        AnimateInfoBox(true);

        new Thread(async () => await DeedManager.UpdateDeeds(API_Url)).Start();
    }

    private void SetInfoBoxText(string text, bool isGood)
    {
        DeedDownloadLabel.Text = text;
        DeedDownloadFrame.BackgroundColor = isGood ? Colors.ForestGreen : Colors.IndianRed;
    }
    
    private void DeedDownloadSuccess()
    {
        SetInfoBoxText("Downloaded newest Deeds!", true);
        HideInfoBox(3000);
    }

    private void HideInfoBox(int afterMilliseconds = 0)
    {
        if (afterMilliseconds == 0)
        {
            AnimateInfoBox(false);
        } else
        {
            new Thread(() =>
            {
                Thread.Sleep(afterMilliseconds);
                AnimateInfoBox(false);
            }).Start();
        }
    }

    private void DeedDownloadError(int statusCode, string errorMessage)
    {
        // Check if the code != -1 (which means that the Deed was not downloaded because our cache is still pretty new).
        if (statusCode != -1)
        {
            SetInfoBoxText("Ohh noo. There was a problem while downloading the newest deeds.", false);
            HideInfoBox(5000);
        } else
        {
            SetInfoBoxText("Downloaded newest Deeds!", true);
            HideInfoBox(3000);
        }
    }

    // A simple animation to show or hide the info box. (at the top of the screen)
    private async void AnimateInfoBox(bool isAppearing)
    {
        // if we want it to appear, than the animation shold go from top to bottom.
        // Otherwise (we want it to hide) it should go from bottom to top
        double startY = isAppearing ? -infoBoxContainer.Height : 0;
        double endY = isAppearing ? 0 : -infoBoxContainer.Height;

        await infoBoxContainer.TranslateTo(0, startY, 0);
        infoBoxContainer.Opacity = 1;

        await infoBoxContainer.TranslateTo(0, endY, 250, Easing.SinOut);
        infoBoxContainer.Opacity = isAppearing ? 1 : 0;
    }

    private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
	}

	private void InitializeNotification()
	{
#if ANDROID || IOS

        var cDT = DateTime.Now;

        var request = new NotificationRequest
        {
            NotificationId = 1000,
            Title = "Do something good",
            Subtitle = "",
            Description = "Its time for a good deed today!",
            BadgeNumber = 42,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = new DateTime(cDT.Year, cDT.Month, cDT.Day, 12, 00, 0).AddDays(1),
                NotifyRepeatInterval = TimeSpan.FromHours(24),
                RepeatType = NotificationRepeat.TimeInterval
            }
        };
        LocalNotificationCenter.Current.Show(request);
#endif
    }

    /// <summary>
    /// The function gets called when the Page is appearing.
    /// We use this to hook onto the needed Events from the DeedManager and start our Deed-Update routine.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ContentPage_Appearing(object sender, EventArgs e)
    {
        DeedManager.OnDeedDownloadError += DeedDownloadError;
        DeedManager.OnDeedDownloadSuccess += DeedDownloadSuccess;

        var Settings = config.GetRequiredSection("Settings").Get<Settings>();
        InitializeDeedDownload(Settings.API_Server_URL);
    }

    /// <summary>
    /// This function is called when the Page is disappearing.
    /// In that case we want to hide our download-box and unhook the events from the DeedManager.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ContentPage_Disappearing(object sender, EventArgs e)
    {
        DeedManager.OnDeedDownloadError -= DeedDownloadError;
        DeedManager.OnDeedDownloadSuccess -= DeedDownloadSuccess;
        AnimateInfoBox(true);
    }
}


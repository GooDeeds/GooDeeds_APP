using Plugin.LocalNotification;

namespace GooDeeds_APP;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
        InitializeNotification();

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
}


using GooDeeds_APP.Deeds;
using Microsoft.Extensions.Configuration;
using Plugin.LocalNotification;
using System.Collections.ObjectModel;

namespace GooDeeds_APP;

public partial class DeedPage : ContentPage
{
    public List<Deed> Deeds { get; private set; } = new List<Deed>();
    private ObservableCollection<Deed> FilteredDeeds { get; set; } = new ObservableCollection<Deed>();
    public DeedPage()
	{
		InitializeComponent();

        InitializeNotification();
        
        Deeds = DeedManager.GetAllDeeds();
        BindingContext = new { Deeds = FilteredDeeds };
        FilterDeeds();
    }

    private void FilterDeeds()
    {
        try
        {
            FilteredDeeds.Clear();
            if (string.IsNullOrEmpty(SearchBar.Text))
            {
                foreach(var d in Deeds)
                {
                    FilteredDeeds.Add(d);
                }
            }
            else
            {
                foreach (var d in Deeds.Where(deed => deed.Title.ToLower().Contains(SearchBar.Text.ToLower())))
                {
                    FilteredDeeds.Add(d);
                }
                foreach (var d in Deeds.Where(deed => deed.Description.ToLower().Contains(SearchBar.Text.ToLower())))
                {
                    FilteredDeeds.Add(d);
                }
            }
        } catch (Exception ex)
        {

        }
    }

    private void InitializeDeedDownload(string API_Url)
    {
        new Thread(async () =>
        {
            Thread.Sleep(2000);
            await DeedManager.UpdateDeeds(API_Url);
        }).Start();
    }

    private void SetInfoBoxText(string text, bool isGood)
    {
        DeedDownloadLabel.Text = text;
        DeedDownloadFrame.BackgroundColor = isGood ? Colors.ForestGreen : Colors.IndianRed;
    }

    private void HideInfoBox(int afterMilliseconds = 0)
    {
        if (afterMilliseconds == 0)
        {
            AnimateInfoBox(false);
        }
        else
        {
            new Thread(() =>
            {
                Thread.Sleep(afterMilliseconds);
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    AnimateInfoBox(false);
                });
            }).Start();
        }
    }

    private void DeedDownloadSuccess()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Deeds.Clear();
            Deeds.AddRange(DeedManager.GetAllDeeds());
            SetInfoBoxText("Downloaded newest Deeds!", true);
            FilterDeeds();
            HideInfoBox(3000);
        });
    }

    private void DeedDownloadError(int statusCode, string errorMessage)
    {
        // We did create a thread to download data.
        // Thus we cannot modify elements (only the thread where they got created can edit them, which is the mainthread in our case)
        // So we need to invoke the final results and UI-Changes on the mainthread.
        MainThread.BeginInvokeOnMainThread(() =>
        {
            // Check if the code != -1 (which means that the Deed was not downloaded because our cache is still pretty new).
            if (statusCode != -1)
            {
                SetInfoBoxText("Ohh noo. There was a problem while downloading the newest deeds.", false);
                HideInfoBox(5000);
            }
            else
            {
                SetInfoBoxText("Downloaded newest Deeds!", true);
                HideInfoBox(3000);
            }
        });
    }

    private void DeedDownloadStart()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            AnimateInfoBox(true);
            // Download newest Deeds!
            SetInfoBoxText("Downloading new Deeds!", true);
        });
    }

    // A simple animation to show or hide the info box. (at the top of the screen)
    private async void AnimateInfoBox(bool isAppearing)
    {
        // If we're already hidden (or shown) dont update the visibility at all. This just makes things weird and glitchy
        if (isAppearing && infoBoxContainer.Opacity == 1 || !isAppearing && infoBoxContainer.Opacity == 0)
            return;

        // if we want it to appear, than the animation shold go from top to bottom.
        // Otherwise (we want it to hide) it should go from bottom to top
        double startY = isAppearing ? -infoBoxContainer.Height : 0;
        double endY = isAppearing ? 0 : -infoBoxContainer.Height;

        await infoBoxContainer.TranslateTo(0, startY, 0);
        infoBoxContainer.Opacity = 1;

        await infoBoxContainer.TranslateTo(0, endY, 250, Easing.SinOut);
        infoBoxContainer.Opacity = isAppearing ? 1 : 0;
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
        LocalNotificationCenter.Current.CancelAll();
        LocalNotificationCenter.Current.ClearAll();
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
        DeedManager.OnDeedDownloadStart += DeedDownloadStart;
        if (Parent is MainPage mp)
        {
            var Settings = mp.config.GetRequiredSection("Settings").Get<Settings>();
            InitializeDeedDownload(Settings.API_Server_URL);
        }
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
        DeedManager.OnDeedDownloadStart -= DeedDownloadStart;
        AnimateInfoBox(false);
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        FilterDeeds();
    }

    private async void DeedList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (Parent is TabbedPage tp && tp.Parent is NavigationPage np && e.SelectedItem is Deed d)
        {
            DeedList.SelectedItem = null;
            DeedDetailPage dip = new DeedDetailPage();
            dip.SetDeed(d);
            await np.PushAsync(dip);
        }
    }
}
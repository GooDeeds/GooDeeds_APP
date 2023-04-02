using GooDeeds_APP.Deeds;
using Microsoft.Maui.ApplicationModel.DataTransfer;

namespace GooDeeds_APP.ShareHelper
{
    public class ShareHelper
    {
        public static async void ShareDeed(Deed d)
        {
            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Text = ShareDeedGenerator.GenerateDeedText(d),
                Title = "Share your deed!"
            });
        }
    }
}

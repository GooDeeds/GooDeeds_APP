using GooDeeds_APP.Deeds;
using Microsoft.Maui.ApplicationModel.DataTransfer;

namespace GooDeeds_APP.ShareHelper
{
    /// <summary>
    /// A simple Helper class to share a deed.
    /// It just wraps around some needed calls, so that they are then easier to use.
    /// </summary>
    public static class ShareHelper
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

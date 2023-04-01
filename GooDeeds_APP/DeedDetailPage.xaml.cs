using GooDeeds_APP.Deeds;

namespace GooDeeds_APP;

public partial class DeedDetailPage : ContentPage
{
    public Deed Deed { get; private set; }
    public DeedDetailPage()
	{
		InitializeComponent();
        IsVisible = false;
    }

    public void SetDeed(Deed deed)
    {
        Deed = deed;
        BindingContext = this;
        
        TitleText.Text = deed.Title;
        DescriptionText.Text = deed.Description;
        IsVisible = true;
    }
}
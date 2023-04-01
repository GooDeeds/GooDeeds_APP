using GooDeeds_APP.PageData;

namespace GooDeeds_APP;

public partial class IntroductionPage : ContentPage
{
	public IntroductionPage()
	{
		InitializeComponent();

        // Set the binding context for the page
        BindingContext = new {
            Introductions = new List<IntroductionData>{
                new IntroductionData()
                {
                    Title = "Hey there!",
                    Text = "GooDeed is an App to help you get motivated doing good deeds.\r\n" +
                    "It helps you by providing a lot of possibilities for good deeds.\r\n" +
                    "To keep you motivated, we designed the app to be a little side-adventure.\r\n" +
                    "You'll level up your character alongside your journey of good deeds.\r\n",
                    ImageUri = "avatar_race_1.svg"
                },
                new IntroductionData()
                {
                    Title = "There is a lot!",
                    Text = "GooDeed offers you a lot of possiblities.\r\n" +
                    "You can choose a character from many different races.\r\n" +
                    "And than you can give your character a profession.\r\n" +
                    "Do you want to be a legendary dwarf battlemage? Or prehaps a godly elf paladin?\r\n" +
                    "Go ahead! You're free to create your free character and join people on a journey of good deeds.",
                    ImageUri = "avatar_race_2.svg"
                },
                new IntroductionData()
                {
                    Title = "Why should you join the adventure?",
                    Text = "Many people, mostlikely you included, want to change the world and make it a better place.\r\n" +
                    "But at the same time those people do not really know on how to tackle this.\r\n" +
                    "One of the best ways to tackle such a problem is to doing good deeds, small or large.\r\n" +
                    "This will leave a \"heart print\" (rather than a foot print) around you in your local environment.",
                    ImageUri = "avatar_race_3.svg"
                }
            }
        };
    }

    private async void btn_continue_Clicked(object sender, EventArgs e)
    {
        await this.Navigation.PopAsync();
    }
}
using GooDeeds_APP.Avatar;

namespace GooDeeds_APP;

public partial class AvatarCreationPage : ContentPage
{
	public Avatar.Avatar avatar;
    
	public Avatar.Profession profession;

	public AvatarCreationPage()
	{
		InitializeComponent();

		BindingContext = new
        {
            RaceList = Enum.GetNames(typeof(Avatar.RaceType)),
            ProfessionList = Enum.GetNames(typeof(Avatar.ProfessionType))
        };
	}

	public async void CreateAvatar()
    {
        if (string.IsNullOrEmpty(uname.Text))
        {
            await DisplayAlert("Error", "Please enter a name", "OK");
            return;
        }
        if (ProfessionPicker.SelectedIndex == -1)
        {
            await DisplayAlert("Error", "Please select a race.", "OK");
            return;
        }
        if (RacePicker.SelectedIndex == -1)
        {
            await DisplayAlert("Error", "Please select a profession.", "OK");
            return;
        }

        profession = new Avatar.Profession()
		{
			Type = (Avatar.ProfessionType)(ProfessionPicker.SelectedIndex + 1),
			Race = (Avatar.RaceType)(RacePicker.SelectedIndex + 1),
		};

		avatar = new Avatar.Avatar()
		{
            Profession = profession,
			Name = uname.Text,
			Experience = 0,
		};
        
        Avatar.AvatarManager.SaveAvatar(avatar);
        App.UpdateMainPage();
    }

    private void UpdateDescriptionText()
    {
        if (RacePicker.SelectedIndex <= -1 || ProfessionPicker.SelectedIndex <= -1)
            return;

        CharacterDescriptionText.Text = AvatarManager.GetDescription((RaceType)(RacePicker.SelectedIndex + 1), (ProfessionType)(ProfessionPicker.SelectedIndex + 1));
    }

    private void RacePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
		if (RacePicker.SelectedIndex > -1)
        {
            RaceImage.Opacity = 1;
            RaceImage.Source = "avatar_race_" + (RacePicker.SelectedIndex + 1) + ".svg";
        } else
        {
            RaceImage.Opacity = 0;
            RaceImage.Source = null;
        }
        UpdateDescriptionText();
    }

    private void ProfessionPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateDescriptionText();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
		CreateAvatar();
    }

    protected override bool OnBackButtonPressed()
    {
        return false;
    }
}
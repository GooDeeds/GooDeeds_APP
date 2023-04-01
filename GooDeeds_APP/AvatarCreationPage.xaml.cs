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

	public void CreateAvatar()
	{
		profession = new Avatar.Profession()
		{
			Type = (Avatar.ProfessionType)ProfessionPicker.SelectedItem,
			Race = (Avatar.RaceType)RacePicker.SelectedItem,
		};

		avatar = new Avatar.Avatar()
		{
			Name = uname.Text,
			Experience = 0,
		};

		try
		{
            Avatar.AvatarManager.SaveAvatar(avatar);
        }
        catch (Exception ex)
		{

        }
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
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
		CreateAvatar();
    }
}
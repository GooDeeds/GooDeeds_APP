using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace GooDeeds_APP;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		var assembly = Assembly.GetExecutingAssembly();
		using (var stream = assembly.GetManifestResourceStream("GooDeeds_APP.appsettings.json"))
		{
			var config = new ConfigurationBuilder().AddJsonStream(stream).Build();
			builder.Configuration.AddConfiguration(config);
		}

#if DEBUG
		builder.Logging.AddDebug();
#endif
		
        builder.Services.AddTransient<App>();
        return builder.Build();
	}
}

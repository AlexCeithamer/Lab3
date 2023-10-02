using Microsoft.Extensions.Logging;
using Lab3.Model;
namespace Lab3;

/// <summary>
/// Name: Alex Ceithamer, Samuel Ayoade
/// Date: 10/2/23
/// Description: This was given as starter code - so not much to say about this.
/// Bugs: none known
/// </summary>

public static class MauiProgram
{
    public static IBusinessLogic BusinessLogic = new BusinessLogic();

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

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}


using CommunityToolkit.Maui;
using Syncfusion.Maui.Core.Hosting;


namespace Mobile_IP;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureSyncfusionCore()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
        {
            fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
            fonts.AddFont("Poppins-Semibold.ttf", "PoppinsSemibold");
        }).UseMauiCommunityToolkit();
        return builder.Build();
    }
}
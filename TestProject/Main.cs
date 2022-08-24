using AppUpdater;

namespace TestProject;

using static AppUpdater.AppUpdater;

static class Test
{
    public static void Main(string[] args)
    {
        if(!Downloader.IsApiAlive())
        {
            Console.WriteLine("Api Is Down");
        }
        else
        {
            Console.WriteLine("Api Is Running");
        }

        Console.WriteLine("Welcome to the AppUpdater test program");
        string? input = Console.ReadLine();
        if(input != null && input.Contains("quit"))
        {
            return;
        }
        
        /*Init(
         "your app id",
            "branch name to download from", 
            "folder to download the update to. Typically your app's root folder. ABSOLUTE PATH REQUIRED", 
            "path to your app's executable. Should probably be in the updateFolderPath", 
            "the current build id. not required, but the updater will always think a update is available", 
            "some app key. Not required if you disabled keys in your app updater settings"
            );*/
        
        SetDownloadCompletedCallback(Console.WriteLine);
        
        if (Updater.CheckForUpdates())
        {
            Console.WriteLine("Start Update");
            Downloader.DownloadUpdate(false);
        }
        Console.ReadKey();
    }
}
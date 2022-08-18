using AppUpdater;

namespace TestProject;

using static AppUpdater.AppUpdater;

class Test
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the AppUpdater test program");
        string? input = Console.ReadLine();
        if(input != null && input.Contains("quit"))
        {
            return;
        }
        
        //Init();
        if (Updater.CheckForUpdates())
        {
            Console.WriteLine("Start Update");
            Downloader.DownloadUpdate(true);
        }
        Console.ReadKey();
    }
}
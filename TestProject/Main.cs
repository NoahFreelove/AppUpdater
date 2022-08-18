using AppUpdater;

namespace TestProject;

using static AppUpdater.AppUpdater;

class Test
{
    public static void Main(string[] args)
    {
        Init();
        if (Updater.CheckForUpdates())
        {
            Console.WriteLine("Start Update");
            Downloader.DownloadUpdate(true);
        }
        Console.ReadKey();
    }
}
namespace AppUpdater;

public static class AppUpdater
{
    public static bool hasInit = false;
    
    public static string appID; // your app ID, acquired through the App Updater Website
    public static string currentBuildId; // Keep this in a config file. If it does not match the ID of the latest build CheckForUpdates will return true.
    public static string key; // Keep this in a config file if your app requires a key
    public static string branch; // The branch to download from

    public static string RelativeDownloadPath = string.Empty; // Path to download and unzip the build before merge
    
    public static string UpdaterFolderPath = string.Empty; // Path to the folder where updater.exe is located
    
    public static string appExePath = string.Empty; // Path to your app's executable
    public static bool updateAvailable;
    
    public static void Init(string appID, string branch, string UpdaterFolderPath, string appExecutablePath, string currentBuildId = "", string key = "")
    {
        AppUpdater.appID = appID;
        AppUpdater.currentBuildId = currentBuildId;
        AppUpdater.key = key;
        AppUpdater.branch = branch;
        AppUpdater.UpdaterFolderPath = UpdaterFolderPath;
        AppUpdater.appExePath = appExecutablePath;

        if (AppUpdater.UpdaterFolderPath.EndsWith("/") || AppUpdater.UpdaterFolderPath.EndsWith("\\"))
        {
            AppUpdater.UpdaterFolderPath = AppUpdater.UpdaterFolderPath.Substring(0, AppUpdater.UpdaterFolderPath.Length - 1);
        }
        
        if (!File.Exists(AppUpdater.UpdaterFolderPath + "/updater.exe"))
        {
            Console.WriteLine("Could not initialize app. The app executable path given does not end with .exe");
            hasInit = false;
            return;
        }

        if(!appExecutablePath.EndsWith(".exe")){
            Console.WriteLine("Could not initialize app. The app executable path given does not end with .exe");
            hasInit = false;
            return;
        }
        
        hasInit = true;
    }

    public static void SetDownloadCompletedCallback(DownloadCompletedCallback dcc)
    {
        Downloader.DownloadCallback = dcc;
    }
}
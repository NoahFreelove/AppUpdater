namespace AppUpdater;

public static class AppUpdater
{
    internal static bool HasInit;
    
    internal static string AppId = string.Empty; // your app ID, acquired through the App Updater Website
    internal static string CurrentBuildId = string.Empty; // Keep this in a config file. If it does not match the ID of the latest build CheckForUpdates will return true.
    internal static string Key = string.Empty; // Keep this in a config file if your app requires a key
    internal static string Branch = string.Empty; // The branch to download from

    internal static string RelativeDownloadPath = string.Empty; // Path to download and unzip the build before merge
    
    internal static string UpdaterFolderPath = string.Empty; // Path to the folder where updater.exe is located
    
    internal static string AppExePath = string.Empty; // Path to your app's executable
    internal static bool IsUpdateAvailable;
    
    public static void Init(string appId, string branch, string updaterFolderPath, string appExecutablePath, string currentBuildId = "", string key = "")
    {
        AppId = appId;
        CurrentBuildId = currentBuildId;
        Key = key;
        Branch = branch;
        UpdaterFolderPath = updaterFolderPath;
        AppExePath = appExecutablePath;

        if (UpdaterFolderPath.EndsWith("/") || UpdaterFolderPath.EndsWith("\\"))
        {
            UpdaterFolderPath = UpdaterFolderPath.Substring(0, UpdaterFolderPath.Length - 1);
        }
        
        if (!File.Exists(UpdaterFolderPath + "/updater.exe"))
        {
            Console.WriteLine("Could not initialize app. The app executable path given does not end with .exe");
            HasInit = false;
            return;
        }

        if(!appExecutablePath.EndsWith(".exe")){
            Console.WriteLine("Could not initialize app. The app executable path given does not end with .exe");
            HasInit = false;
            return;
        }
        
        HasInit = true;
    }

    public static void SetDownloadCompletedCallback(DownloadCompletedCallback dcc)
    {
        Downloader.DownloadCallback = dcc;
    }

    public static bool GetInitStatus()
    {
        return HasInit;
    }

    public static bool UpdateAvailable()
    {
        return IsUpdateAvailable;
    }
}
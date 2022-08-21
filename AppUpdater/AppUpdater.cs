namespace AppUpdater;

public static class AppUpdater
{
    public static bool hasInit = false;
    
    public static string appID; // your app ID, acquired through the App Updater Website
    public static string currentBuildId; // Keep this in a config file. If it does not match the ID of the latest build CheckForUpdates will return true.
    public static string key; // Keep this in a config file if your app requires a key
    public static string branch; // The branch to download from

    public static string RelativeDownloadPath = string.Empty; // Path to download and unzip the build before merge
    
    public static string UpdaterFolderPath = string.Empty; // Path to the updater config file
    
    public static string appExePath = string.Empty; // Path to the executable
    public static bool updateAvailable;
    
    public static void Init(string appID, string branch, string UpdaterFolderPath, string appExecutablePath, string currentBuildId = "", string key = "")
    {
        AppUpdater.appID = appID;
        AppUpdater.currentBuildId = currentBuildId;
        AppUpdater.key = key;
        AppUpdater.branch = branch;
        AppUpdater.UpdaterFolderPath = UpdaterFolderPath;
        AppUpdater.appExePath = appExecutablePath;
        hasInit = true;
    }
}
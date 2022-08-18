namespace AppUpdater;

public static class AppUpdater
{
    public static bool hasInit = false;
    
    public static string appID;
    public static string buildID;
    public static string key; // Optional, only required if set in build settings
    public static string branch;

    public static string RelativeDownloadPath = string.Empty; // Path to download and unzip the build before merge
    
    public static string UpdaterFolderPath = string.Empty; // Path to the updater config file
    
    public static bool updateAvailable;
    
    public static void Init(string appID, string buildID, string key, string branch, string UpdaterFolderPath)
    {
        AppUpdater.appID = appID;
        AppUpdater.buildID = buildID;
        AppUpdater.key = key;
        AppUpdater.branch = branch;
        AppUpdater.UpdaterFolderPath = UpdaterFolderPath;
        hasInit = true;
    }
}
namespace AppUpdater;

public static class AppUpdater
{
    public static bool hasInit = false;
    
    public static string appID;
    public static string buildID;
    public static string key; // Optional, only required if set in build settings
    public static string branch;

    public static string relativeDownloadPath = "/downloads/file"; // Path to download and unzip the build before merge
    
    public static bool updateAvailable;
    
    // ReSharper disable once MemberCanBePrivate.Global
    public static void Init(string appID, string buildID, string key, string branch)
    {
        AppUpdater.appID = appID;
        AppUpdater.buildID = buildID;
        AppUpdater.key = key;
        AppUpdater.branch = branch;
        hasInit = true;
    }

    public static void Main()
    {
        Init("dfc9XpALPQ9XLlmIvMTU", "notBuildID", "thisisakey", "master");
        if (Updater.CheckForUpdates())
        {
            Updater.StartUpdate();
        }
    }
}
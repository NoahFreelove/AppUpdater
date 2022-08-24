namespace AppUpdater;

public static class Downloader
{
    internal static DownloadCompletedCallback? DownloadCallback { get; set; }
    private static readonly HttpClient DownloadClient = new();
    public static bool IsDownloadingUpdate { get; private set; }

    public static void DownloadUpdate(bool updateAfterDownload = false)
    {
        if(!AppUpdater.HasInit)
            return;

        if (File.Exists(AppUpdater.RelativeDownloadPath + "build.zip"))
        {
            Console.WriteLine("Update already downloaded, skipping download");
            Updater.isUpdateReady = true;
            DownloadCallback?.Invoke(AppUpdater.RelativeDownloadPath + "build.zip");
            return;
        }

        Console.WriteLine("Download Update Async");
        
        DownloadFileAsync(GetBuildLink(), updateAfterDownload);
        
        IsDownloadingUpdate = true;
    }

    private static async void DownloadFileAsync(string uri
       , bool updateAfterDownload)
    {
        if (!Uri.TryCreate(uri, UriKind.Absolute, out _))
        {
            Console.WriteLine("Error getting resource from api. Check if the branch exists.");
            return;
        }

        try
        {
            var fileBytes = await DownloadClient.GetByteArrayAsync(uri);
        
            AppUpdater.RelativeDownloadPath = FormatUrl(AppUpdater.RelativeDownloadPath);

            string finalPath = AppUpdater.RelativeDownloadPath + "build.zip";
            await File.WriteAllBytesAsync(finalPath, fileBytes);
        
            DoneDownloadingUpdate(finalPath);
            if (updateAfterDownload)
            {
                Updater.StartUpdate();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error downloading update from servers. Check if the build is empty.");
        }
    }
    
    private static void DoneDownloadingUpdate(string filepath)
    {
        Console.WriteLine("Done Downloading Update");
        IsDownloadingUpdate = false;
        Updater.isUpdateReady = true;

        DownloadCallback?.Invoke(filepath);
    }
    
    private static string FormatUrl(string input)
    {
        if (input == string.Empty)
        {
            // set it to directory where the executable is
            input = Path.GetDirectoryName(AppUpdater.AppExePath);
            
        }
        
        if (!input.EndsWith("/"))
        {
            input += "/";
        }
        return input;
    }
    
    private static string GetBuildLink()
    {
        var url = "https://app-updater-api.herokuapp.com/apps/?appId=" + AppUpdater.AppId + "&branch=" + AppUpdater.Branch + "&key=" + AppUpdater.Key;
        return Updater.MakeHttpGetRequest(url);
    }

    public static bool IsApiAlive()
    {
        var result = Updater.MakeHttpGetRequest("https://app-updater-api.herokuapp.com/");

        
        return result != null;
    }

}
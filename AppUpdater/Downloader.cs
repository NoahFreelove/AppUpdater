namespace AppUpdater;

public static class Downloader
{
    public static DownloadCompletedCallback DownloadCallback;
    private static readonly HttpClient DownloadClient = new();
    
    public static void DownloadUpdate(bool updateAfterDownload = false)
    {
        if(!AppUpdater.hasInit)
            return;
        
        Console.WriteLine("Download Update Async");
        
        DownloadFileAsync(GetBuildLink(), updateAfterDownload);
        
        DownloadingUpdate = true;
    }

    private static async void DownloadFileAsync(string uri
       , bool updateAfterDownload)
    {
        if (!Uri.TryCreate(uri, UriKind.Absolute, out _))
            throw new InvalidOperationException("URI is invalid.");
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
    
    private static void DoneDownloadingUpdate(string filepath)
    {
        Console.WriteLine("Done Downloading Update");
        DownloadingUpdate = false;
        Updater.isUpdateReady = true;

        DownloadCallback?.Invoke(filepath);
    }
    
    private static string FormatUrl(string input)
    {
        if (input == string.Empty)
        {
            // set it to directory where the executable is
            input = Path.GetDirectoryName(AppUpdater.exeDirectory);
            
        }
        
        if (!input.EndsWith("/"))
        {
            input += "/";
        }
        return input;
    }
    
    private static string GetBuildLink()
    {
        var url = "https://app-updater-api.herokuapp.com/apps/?appId=" + AppUpdater.appID + "&branch=" + AppUpdater.branch + "&key=" + AppUpdater.key;
        return Updater.MakeHttpGetRequest(url);
    }
    
    public static bool DownloadingUpdate { get; set; }
}
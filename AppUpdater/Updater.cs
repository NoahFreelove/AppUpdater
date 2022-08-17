using System.Net;
using System.Net.Http.Headers;

namespace AppUpdater;

public static class Updater
{
    private static bool isUpdateReady;
    private static bool downloadingUpdate;
    private static float downloadProgress;
    
    private static HttpClient client = new HttpClient();

    public static bool CheckForUpdates(bool updateIfAvailable = false)
    {
        AppUpdater.updateAvailable = IsUpdateAvailable();

        if (updateIfAvailable && AppUpdater.updateAvailable)
        {
            StartUpdate();
        }

        return AppUpdater.updateAvailable;
    }

    public static void StartUpdate()
    {
        if(downloadingUpdate)
            return;

        if (isUpdateReady)
        {
            Update();
            return;
        }
        DownloadUpdate();
    }

    private static string GetBuildLink()
    {
        var url = "https://app-updater-api.herokuapp.com/apps/?appId=" + AppUpdater.appID + "&branch=" + AppUpdater.branch + "&key=" + AppUpdater.key;
        return makeHttpGETRequest(url);
    }

    private static void DownloadUpdate()
    {
        downloadProgress = 0;
        Console.WriteLine("Start Update");
        // Create new thread to download update
        DownloadFileAsync(GetBuildLink(), AppUpdater.relativeDownloadPath);
        downloadingUpdate = true;
    }

    public static async void DownloadFileAsync(string uri
        , string outputPath)
    {
        Uri uriResult;

        if (!Uri.TryCreate(uri, UriKind.Absolute, out uriResult))
            throw new InvalidOperationException("URI is invalid.");

        if (!File.Exists(outputPath))
            throw new FileNotFoundException("File not found."
                , nameof(outputPath));

        byte[] fileBytes = await client.GetByteArrayAsync(uri);
        File.WriteAllBytes(outputPath, fileBytes);
    }

    private static void Update()
    {
        
    }

    private static bool IsUpdateAvailable()
    {
        if(!AppUpdater.hasInit)
            return false;

        // Send GET request to the server
        var url = "https://app-updater-api.herokuapp.com/activebuild/?appId=" + AppUpdater.appID + "&branch=" + AppUpdater.branch + "&key=" + AppUpdater.key;

        var res = makeHttpGETRequest(url);
        
        if (res != "Error")
        { 
            return res != AppUpdater.buildID;
        }
        
        return false;
    }


    private static string makeHttpGETRequest(string url)
    {
        client.BaseAddress = new Uri(url);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        HttpResponseMessage responseMessage = client.GetAsync(url).Result;
        if (responseMessage.IsSuccessStatusCode)
        { 
            return responseMessage.Content.ReadAsStringAsync().Result;
        }
        
        return "Error";
    }
}
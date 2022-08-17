using System.Net;
using System.Net.Http.Headers;

namespace AppUpdater;

public static class Updater
{
    private static bool isUpdateReady;
    private static bool downloadingUpdate;
    private static float downloadProgress;
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

    private static void DownloadUpdate()
    {
        //
    }

    private static void Update()
    {
        
    }

    private static bool IsUpdateAvailable()
    {
        if(!AppUpdater.hasInit)
            return false;

        // Send GET request to the server
        var url = "http://localhost:3000/activebuild/?appId=" + AppUpdater.appID + "&branch=" + AppUpdater.branch + "&key=" + AppUpdater.key;
        
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(url);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        HttpResponseMessage responseMessage = client.GetAsync(url).Result;
        if (responseMessage.IsSuccessStatusCode)
        { 
            return responseMessage.Content.ReadAsStringAsync().Result != AppUpdater.buildID;
        }
        
        return false;
    }
}
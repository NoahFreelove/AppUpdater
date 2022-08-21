using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;

namespace AppUpdater;

public static class Updater
{
    public static bool isUpdateReady;
    private static HttpClient client = new();

    public static bool CheckForUpdates(bool downloadIfAvailable = false)
    {
        if (!AppUpdater.hasInit)
            return false;
        
        AppUpdater.updateAvailable = IsUpdateAvailable();

        if (AppUpdater.updateAvailable)
        {
            Console.WriteLine("Update Available");
        }
        else
        {
            Console.WriteLine("Up to date");
        }

        if (downloadIfAvailable && AppUpdater.updateAvailable)
        {
            Downloader.DownloadUpdate();
        }

        return AppUpdater.updateAvailable;
    }

    public static void StartUpdate()
    {
        if (!isUpdateReady || Downloader.DownloadingUpdate || !AppUpdater.hasInit) return;
        Update();
    }

    private static void Update()
    {
        Console.WriteLine("Check for path existence: " + AppUpdater.RelativeDownloadPath);
        
        if (!File.Exists(AppUpdater.RelativeDownloadPath + "build.zip"))
            return;
        Console.WriteLine("unzipping files");
        if(Directory.Exists(AppUpdater.RelativeDownloadPath + "build"))
            Directory.Delete(AppUpdater.RelativeDownloadPath + "build", true);
        
        System.IO.Compression.ZipFile.ExtractToDirectory(AppUpdater.RelativeDownloadPath + "build.zip", AppUpdater.RelativeDownloadPath + "build");
        
        if(File.Exists(AppUpdater.RelativeDownloadPath + "build.zip"))
            File.Delete(AppUpdater.RelativeDownloadPath + "build.zip");
        Debug.WriteLine("Removing zip file");

        // Get id of current process
        var currentProcess = Process.GetCurrentProcess().Id;
        
        
        string configFile = AppUpdater.RelativeDownloadPath + "build\n" + currentProcess + "\n" + AppUpdater.appExePath;
        // Write to config file
        File.WriteAllText(AppUpdater.UpdaterFolderPath + "config.txt", configFile);
        Console.WriteLine("Updated config file");
        
        Console.WriteLine("Launching Updater. This will close the current process.");
        Process.Start(AppUpdater.UpdaterFolderPath + "updater.exe");
        Process.GetCurrentProcess().Kill();

    }

    private static bool IsUpdateAvailable()
    {
        if(!AppUpdater.hasInit)
            return false;

        // Send GET request to the server
        var url = "https://app-updater-api.herokuapp.com/activebuild/?appId=" + AppUpdater.appID + "&branch=" + AppUpdater.branch + "&key=" + AppUpdater.key;

        var res = MakeHttpGetRequest(url);
        
        if (res != "Error")
        { 
            return res != AppUpdater.currentBuildId;
        }
        
        return false;
    }

    public static string MakeHttpGetRequest(string url)
    {
        client = new HttpClient();
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

public delegate void DownloadCompletedCallback(string downloadedPath);
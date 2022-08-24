using System.Diagnostics;
using System.Runtime.InteropServices;

[DllImport("kernel32.dll")]
static extern IntPtr GetConsoleWindow();

[DllImport("user32.dll")]
static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
ShowWindow(GetConsoleWindow(), 0);

ResetLog();
WriteLog("----App Updater----");
WriteLog("Fetching Config File");

var buildFolderPath = string.Empty;
var pId = string.Empty;
var exePath = string.Empty;

try
{
    var configFile = Directory.GetFiles(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/",
        "config.txt")[0];
    var lines = File.ReadAllLines(configFile);
    buildFolderPath = lines[0];
    pId = lines[1];
    exePath = lines[2];
}
catch
{
    WriteLog("Error loading config file. Cannot proceed until the config file is fixed.");
    Environment.Exit(1);
}

try
{
    Process.GetProcessById(int.Parse(pId)).Kill();
}
catch
{
    WriteLog("Process id: " + pId + " not found. Could not close app.\nMaybe it's already closed?");
}

WriteLog("Moving Files From Extracted ");
foreach (var file in new DirectoryInfo(buildFolderPath).GetFiles())
{
    try
    {
        var parentDir = file.FullName.Substring(0, file.FullName.LastIndexOf("\\", StringComparison.Ordinal)-5); // -5 to remove the "build" folder
        File.Move(file.FullName, parentDir + file.Name);
    }
    catch
    {
        WriteLog("Error moving file: " + file);
    }
}

try
{
    Directory.Delete(buildFolderPath,true);
}
catch
{
    WriteLog("Could not delete build folder: " + buildFolderPath + "\nPossibly already deleted?");
}

WriteLog("Attempting To Start App");

try
{
    var psi = new ProcessStartInfo(exePath)
    {
        UseShellExecute = true
    };
    Process.Start(psi);
    Process.GetCurrentProcess().Kill();
}
catch
{
    WriteLog("Could not start app: " + exePath + "\nLikely the file does not exist");
}
    

void WriteLog(string message)
{
    File.AppendAllText("log.txt", message + "\n");
}

void ResetLog()
{
    // Check if log file exists
    if (File.Exists("log.txt"))
    {
        // Delete log file
        File.Delete("log.txt");
    }
}
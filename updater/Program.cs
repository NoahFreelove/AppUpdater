using System.Diagnostics;

Console.WriteLine("----App Updater----");
Console.WriteLine("Fetching Config File");

try
{
    var configFile = Directory.GetFiles(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)+"/","config.txt" )[0];
    var lines = File.ReadAllLines(configFile);
    var buildFolderPath = lines[0];
    var pId = lines[1];
    var exePath = lines[2];

    Process.GetProcessById(int.Parse(pId)).Kill();
    
    Console.WriteLine("Updating Files");
    foreach (var file in Directory.GetFiles(buildFolderPath+"/"))
    {
        try
        {
            File.Move(file, Path.GetFileName(file), true);
        }
        catch
        {
            // ignore
        }
    }
    Directory.Delete(buildFolderPath,true);

    Console.WriteLine("Starting App");

    var psi = new ProcessStartInfo(exePath)
    {
        UseShellExecute = true
    };
    Process.Start(psi);
    Process.GetCurrentProcess().Kill();

}
catch(Exception e)
{
    Console.WriteLine("error occured: " + e + "\nExiting...");
    Console.WriteLine(e.Message);
    Console.ReadLine();
}

using System.Diagnostics;

Console.WriteLine("----App Updater----");
Console.WriteLine("Fetching Config File");

try
{
    var configFile = Directory.GetFiles(Directory.GetCurrentDirectory(), "config.txt")[0];
    // Get build path from config file
    var lines = File.ReadAllLines(configFile);
    var buildFolderPath = lines[0];
    var pId = lines[1];
    var exePath = lines[2];
    Console.WriteLine(buildFolderPath);
    Console.WriteLine(pId);
    Console.WriteLine(exePath);
}
catch
{
    Console.WriteLine("Config file not found! Exiting...");
    Console.ReadLine();
    Environment.Exit(0);
}

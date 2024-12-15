using FileSystem = Microsoft.Maui.Storage.FileSystem;

namespace MauiGame.Services;
internal static class DataAccess
{
    internal static void Save(string toSave)
    {
        string appDataDirectory = FileSystem.Current.AppDataDirectory;
        string file = Path.Combine(appDataDirectory, "data.txt");
        File.WriteAllText(file, toSave);
    }

    internal static string Load()
    {
        string appDataDirectory = FileSystem.Current.AppDataDirectory;
        string file = Path.Combine(appDataDirectory, "data.txt");
        if (File.Exists(file))
        {
            return File.ReadAllText(file);
        }
        else return string.Empty;
    }
}

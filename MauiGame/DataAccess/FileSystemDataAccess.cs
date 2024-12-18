using MauiGame.Services;
using System.Text.Json;
using FileSystem = Microsoft.Maui.Storage.FileSystem;

namespace MauiGame.DataAccess;
internal class FileSystemDataAccess : IDataAccess
{
    private static string DataFilePath
    {
        get
        {
            string appDataDirectory = FileSystem.Current.AppDataDirectory;
            return Path.Combine(appDataDirectory, "data.txt");
        }
    }

    public void Save<T>(T dataToSave)
    {
        string jsonString = JsonSerializer.Serialize(dataToSave);

        File.WriteAllText(DataFilePath, jsonString);
    }

    public bool TryLoad<T>(out T? loadedData)
    {
        loadedData = default;

        if (File.Exists(DataFilePath))
        {
            string jsonString = File.ReadAllText(DataFilePath);

            loadedData = JsonSerializer.Deserialize<T>(jsonString);

            if (loadedData != null)
            {
                return true;
            }
        }

        return false;
    }


}

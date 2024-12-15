namespace MauiGame.Services;
public interface IDataAccess
{
    void Save<T>(T dataToSave);
    bool TryLoad<T>(out T? loadedData);
}

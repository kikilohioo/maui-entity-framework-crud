namespace ComShopApp.Services;

public class DBPathService : IDBPathService
{
    public string GetPath(string fileName)
    {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        return Path.Combine(path, fileName);
    }
}

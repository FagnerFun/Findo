using System.IO;
using FindoApp.Domain.Interface.Service;
using FindoApp.Domain.Model.Const;

namespace FindoApp.Droid.Service
{
    public class DataBaseAccessService : IDataBaseAccessService
    {
        public string GetDataBasePath()
        {
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), AppSettings.OffLineDataBaseName);
            if (!File.Exists(path))
                File.Create(path).Dispose();
            return path;
        }
    }
}
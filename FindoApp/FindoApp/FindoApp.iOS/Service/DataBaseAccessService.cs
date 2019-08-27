using System;
using System.IO;
using FindoApp.Domain.Interface.Service;
using FindoApp.Domain.Model.Const;

namespace FindoApp.iOS.Service
{
    public class DataBaseAccessService : IDataBaseAccessService
    {
        public string GetDataBasePath()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
                Directory.CreateDirectory(libFolder);

            return Path.Combine(libFolder, AppSettings.OffLineDataBaseName);
        }
    }
}
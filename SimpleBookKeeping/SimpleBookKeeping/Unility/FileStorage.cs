using System;
using System.IO;

namespace SimpleBookKeeping.Unility
{
    public class FileStorage
    {
        private readonly string _dbPath;

        public FileStorage()
        {
            _dbPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/Files/");
        }
        public FileStream Create(string id)
        {
            if (!Directory.Exists(_dbPath))
            {
                Directory.CreateDirectory(_dbPath);
            }

            var file = Path.Combine(_dbPath, id);
            return File.Open(file, FileMode.Create);
        }

        public void Move(string id, string path)
        {
            var file = Path.Combine(_dbPath, id);
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            File.Move(path, file);
        }

        public FileStream Get(string id)
        {
            var file = Path.Combine(_dbPath, id);
            if (!File.Exists(file))
            {
                throw new Exception("File not found");
            }
            return File.Open(file, FileMode.Open);
        }

        public void Delete(string id)
        {
            var file = Path.Combine(_dbPath, id);
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }
    }
}
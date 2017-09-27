using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SimpleBookKeeping.Unility
{
    public class FileStorage
    {
        private readonly string _dbPath;

        public FileStorage()
        {
            _dbPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/Files/");
        }
        
        public void Move(string filename, string path)
        {
            var list = Find(Path.GetFileNameWithoutExtension(filename));
            foreach (var fileInfo in list)
            {
                fileInfo.Delete();
            }

            var file = GetFilePath(filename);
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            File.Move(path, file);
        }

        public FileInfo Get(string filename)
        {
            var file = GetFilePath(filename);
            if (!File.Exists(file))
            {
                throw new Exception("File not found");
            }
            return new FileInfo(file);
        }

        public List<FileInfo> Find(string id)
        {
            var file = GetFilePath(id);

            var dir = Path.GetDirectoryName(file);
            if (dir == null)
            {
                return null;
            }

            var fileItem = Directory.GetFiles(dir).Where(x => Path.GetFileNameWithoutExtension(x) == id);
            if (fileItem == null)
            {
                throw new Exception("File not found");
            }

            return fileItem.Select(x=> new FileInfo(x)).ToList();
        }

        public void Delete(string filename)
        {
            var file = GetFilePath(filename);
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }

        private string GetFilePath(string filename)
        {
            var item = filename.Trim(' ');
            var path = Path.Combine(_dbPath, item[0].ToString(), item[1].ToString());
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return Path.Combine(path, filename);
        }
    }
}
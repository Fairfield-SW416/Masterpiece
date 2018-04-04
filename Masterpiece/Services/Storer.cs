using System;
using System.IO;
using JSONStorage;

namespace Masterpiece.Services
{
    public class Storer : IContentProvider
    {
        public Storer()
        {
        }

        private string GetFullFilePath(string name)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, name);

            return filename;
        }


        string IContentProvider.LoadContent(string name)
        {
            var filename = GetFullFilePath(name);
            string content = null;

            if (File.Exists(filename))
            {
                using (var streamReader = new StreamReader(filename))
                {
                    content = streamReader.ReadToEnd();
                    System.Diagnostics.Debug.WriteLine(content);
                    streamReader.Close();
                }
            }

            return content;
        }

        void IContentProvider.StoreContent(string name, string jsonString)
        {
            var filename = GetFullFilePath(name);

            using (var streamWriter = new StreamWriter(filename, false))
            {
                streamWriter.WriteLine(jsonString);
                streamWriter.Close();
            }
        }
    }
}

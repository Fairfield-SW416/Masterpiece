using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace JSONStorage
{
    public interface IContentProvider
    {
        string LoadContent(string filname);
        void StoreContent(string filename, string jsonString);
    }

    public class FileStore<T>
    {
        public IList<T> Items { get; set; }
        private IContentProvider provider;

        private string fileName;

        public FileStore(IContentProvider contentProvider)
        {
            fileName = typeof(T).FullName + ".json";

            if (null != contentProvider)
            {
                provider = contentProvider;
                loadItems();
            }
            else
            {
                throw new ArgumentNullException("contentProvider cannot be null.");
            }

        }

        ~FileStore()
        {
            storeItems();
        }

        public IList<T> loadItems()
        {
            Items = new List<T>();

            var listJSON = provider.LoadContent(fileName);

            if (null != listJSON)
            {
                Items = JsonConvert.DeserializeObject<List<T>>(listJSON);
            }

            return Items;
        }

        public void storeItems()
        {
            var listJSON = JsonConvert.SerializeObject(Items);
            provider.StoreContent(fileName, listJSON);
        }
    }
}

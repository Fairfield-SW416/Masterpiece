using System;
using System.Globalization;
using System.Threading;
using Masterpiece.Services;

namespace Masterpiece
{
    public class App
    {
        public static bool UseMockDataStore = true;
        public static string BackendUrl = "http://localhost:5000";

        public static void Initialize()
        {
            var culture = new CultureInfo("es");
            Thread.CurrentThread.CurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            Localization.Language.Culture = culture;

            if (UseMockDataStore)
                ServiceLocator.Instance.Register<IDataStore<Item>, MockDataStore>();
            else
                ServiceLocator.Instance.Register<IDataStore<Item>, CloudDataStore>();

            //var twitterService = new TwitterService();

            var storer = new Masterpiece.Services.Storer();
            var jsonStorer = new JSONStorage.FileStore<Item>(storer);

            var items = jsonStorer.loadItems();

            items.Add(new Item
            {
                Id = "PJK" + items.Count,
                Text = "This is some text",
                Description = "This is a longer description of this Item."
            });

            jsonStorer.storeItems();
        }
    }
}

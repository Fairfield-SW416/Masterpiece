using System.Globalization;
using System.Threading;
using System.Windows.Input;

namespace Masterpiece
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = Localization.Language.AboutTitle;

            OpenWebCommand = new Command(() => Plugin.Share.CrossShare.Current.OpenBrowser("https://xamarin.com/platform"));
        }

        public ICommand OpenWebCommand { get; }
    }
}

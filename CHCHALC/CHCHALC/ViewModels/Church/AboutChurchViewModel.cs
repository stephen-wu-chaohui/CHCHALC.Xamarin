using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace CHCHALC.ViewModels
{
    public class AboutChurchViewModel : BaseViewModel
    {
        private string manifesto;
        public string Manifesto {
            get { return manifesto; }
            set { SetProperty(ref manifesto, value); }
        }

        public AboutChurchViewModel()
        {
            Title = "Welcome";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://nzalc.org/")));

            Manifesto = "to bring people to Jesus\nand membership in his family.\n\n" +
                "develop them Christlike maturity\nand equip them for their ministry in the church\n\n" +
                "and life mission in the world,\nin order to manify God's name.";
        }

        public ICommand OpenWebCommand { get; }
    }
}
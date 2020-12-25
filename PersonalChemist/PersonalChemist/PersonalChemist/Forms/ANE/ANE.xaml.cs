using PersonalChemist.Landing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PersonalChemist.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ANE : ContentPage
    {
        public ANE(string Email)
        {
            InitializeComponent();

            Device.StartTimer(new TimeSpan(0, 0, 15), () =>
            {
                // do something every 60 seconds
                Device.BeginInvokeOnMainThread(() =>
                {
                    // interact with UI elements
                    Navigation.PushModalAsync(new Home(""));
                });
                return false; // runs again, or false to stop
            });
        }
    }
}
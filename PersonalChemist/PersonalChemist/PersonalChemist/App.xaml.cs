using PersonalChemist.Landing;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PersonalChemist.Subscription;

namespace PersonalChemist
{
    public partial class App : Application
    {
       
        public App()
        {
            InitializeComponent();
            
            MainPage = new Login_Options();
           
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

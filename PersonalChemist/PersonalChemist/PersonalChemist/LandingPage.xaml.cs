using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PersonalChemist
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPage : TabbedPage
    {
        public LandingPage()
        {
            InitializeComponent();
            
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            CurrentPage = Children[1];

            WebViews.Source = "https://personalchemist.typeform.com/to/Vg3QeTyU";

             
        }
     
    }
}
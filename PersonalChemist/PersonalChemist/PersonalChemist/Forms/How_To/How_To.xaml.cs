using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PersonalChemist.Forms.How_To
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class How_To : ContentPage
    {
        public How_To(string Email)
        {
            InitializeComponent();
        }
    }
}
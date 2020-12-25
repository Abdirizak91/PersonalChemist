using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PersonalChemist.Forms.Asthma_COPD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Asthma_COPD : ContentPage
    {
        public Asthma_COPD(string Email)
        {
            InitializeComponent();
            
            
            Inhaler.Items.Add("Airomir");
            Inhaler.Items.Add("Aloflute");
            Inhaler.Items.Add("Anoro");
            Inhaler.Items.Add("Atrovent");
            Inhaler.Items.Add("Braltus");
            Inhaler.Items.Add("Bricanyl");
            Inhaler.Items.Add("Clenil");
            Inhaler.Items.Add("Combivent");
            Inhaler.Items.Add("Duaklir");
            Inhaler.Items.Add("Duoresp");
            Inhaler.Items.Add("Easyhaler");
            Inhaler.Items.Add("Eklira");
            Inhaler.Items.Add("Flixotide");
            Inhaler.Items.Add("Flutiform");
            Inhaler.Items.Add("Incruse");
            Inhaler.Items.Add("Oxis");
            Inhaler.Items.Add("Preventer");
            Inhaler.Items.Add("Pulmicort");
            Inhaler.Items.Add("Qvar");
            Inhaler.Items.Add("Reliever");
            Inhaler.Items.Add("Relvar");
            Inhaler.Items.Add("Salamol");
            Inhaler.Items.Add("Salbutamol");
            Inhaler.Items.Add("Seebri");
            Inhaler.Items.Add("Seretide");
            Inhaler.Items.Add("Serevent");
            Inhaler.Items.Add("Spoilta");
            Inhaler.Items.Add("Spiriva");
            Inhaler.Items.Add("Steroid");
            Inhaler.Items.Add("Symbicort");
            Inhaler.Items.Add("Trelegy");
            Inhaler.Items.Add("Ultibro");
            Inhaler.Items.Add("Ventolin");


            Condition.Items.Add("Asthma");
            Condition.Items.Add("COPD");
            Condition.Items.Add("Emphysema");
            Condition.Items.Add("Other");
        }

        private void SubmitButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}
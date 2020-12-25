using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PersonalChemist.DBModels;
using PersonalChemist.Landing;

namespace PersonalChemist.Forms.AboutMedicine
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class About_Medicine : ContentPage
    {
        SignalRService signalR;
        string FormName = "About Medicine";
        string SubmitDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string request;
        string UsersEmail;
        
        public About_Medicine(string Email)
        {

            UsersEmail = Email;
            InitializeComponent();
            signalR = new SignalRService();

            Medicines.Items.Add("Acamprosate");
            Medicines.Items.Add("Acarbose");
            Medicines.Items.Add("Acebutolol");
            Medicines.Items.Add("Acetazolamide");
            Medicines.Items.Add("Acetylcholine");
            Medicines.Items.Add("Aclidinium");
            Medicines.Items.Add("Acnecide");
            Medicines.Items.Add("Acrivastine");
            Medicines.Items.Add("Actifed");
            Medicines.Items.Add("Adenosine");
            Medicines.Items.Add("Adrenaline/Epinephrine");
            Medicines.Items.Add("Agomelatine");
            Medicines.Items.Add("Alendronic");
            Medicines.Items.Add("Alfuzosin");
            Medicines.Items.Add("Alimemazine");
            Medicines.Items.Add("Allopurinol");
            Medicines.Items.Add("Almotriptan");
            Medicines.Items.Add("Alogliptin");
            Medicines.Items.Add("Alphosyl");
            Medicines.Items.Add("Other");

            SubmitSection.IsVisible = false;
        }

        
      
        private void InteractionButton_Clicked(object sender, EventArgs e)
        {
            ButtonsSection.IsVisible = false;
            request = "Medicine Interaction";
            DisplayAlert("Note", $"Your enquiring about {request}, press 'Ok' and Submit button will appear", "Ok");
            SubmitSection.IsVisible = true;


        }

        private void SideAffectsButton_Clicked(object sender, EventArgs e)
        {
            ButtonsSection.IsVisible = false;
            request = "Side Affects";
            DisplayAlert("Note", $"Your enquiring about {request}, press 'Ok' and Submit button will appear", "Ok");
            SubmitSection.IsVisible = true;
        }

        private void PurchaseButton_Clicked(object sender, EventArgs e)
        {
            ButtonsSection.IsVisible = false;
            request = "Medicine Purchase";
            DisplayAlert("Note", $"Your enquiring about {request}, press 'Ok' and Submit button will appear", "Ok");
            SubmitSection.IsVisible = true;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            ButtonsSection.IsVisible = false;
            request = "Other";
            DisplayAlert("Note", $"Your enquiring about {request}, press 'Ok' and Submit button will appear", "Ok");
            SubmitSection.IsVisible = true;
        }

        async private void SubmitButton_Clicked(object sender, EventArgs e)
        {
            if(request == "" || request == null)
            {
                await DisplayAlert("Error", "Please Select An Option Above", "Ok");
            }

            else
            {
                Form form = new Form()
                {
                    Name = FormName,
                    SubmitDate = SubmitDate,
                    Request = request,
                    Email = UsersEmail,
                };

                using (var connection = new MySqlConnection("Server=personalchemistaws.cyrfxcwnlh2w.eu-west-2.rds.amazonaws.com;User ID=Zak;Password=146680Xagar;Database=PersonalChemist_DB;"))
                {
                    connection.Open();
                    var SubInsert = new MySqlCommand($"INSERT INTO FORM (FORM_NAME, SUBMIT_DATE, REQUEST, EMAIL) VALUES ('{form.Name}', '{form.SubmitDate}', '{form.Request}' , '{form.Email}');", connection);
                    SubInsert.ExecuteReader();


                    connection.Close();
                }

                await signalR.ConnectAsync(UsersEmail);

                await signalR.SendMessageAsync($"{Environment.NewLine}{UsersEmail}:  ", $"{Environment.NewLine}Form: {form.Name} {Environment.NewLine}Submit Date: {form.SubmitDate}{Environment.NewLine}Request: {form.Request}");

                await DisplayAlert("Sent", $"{UsersEmail}{Environment.NewLine}{Environment.NewLine}You Have Sent **{form.Name}** Form A Chemist Will Be In Touch Soon By In App Chat Or E-mail.", "Ok");


                await Navigation.PushModalAsync(new Home(UsersEmail));
            }

        }
    }
}
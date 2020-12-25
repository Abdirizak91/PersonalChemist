using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using PersonalChemist.DBModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PersonalChemist.Forms.GetInvolved
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Get_Involved : ContentPage
    {
        SignalRService signalR;

        string UsersEmail;
        public Get_Involved(string Email)
        {
            InitializeComponent();
            signalR = new SignalRService();
            SubmitButtonSection.IsVisible = false;
        }

        string FormName = "Get Involved";
        string SubmitDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string request;


        private void BloodTest_Clicked(object sender, EventArgs e)
        {

            SubmitButtonSection.IsVisible = true;
            request = "Blood Test";
            Buttons.IsVisible = false;
            DisplayAlert("Note", $"Your request is {request}, press 'Ok', submit button will appear ", "Ok");

        }

        private void CareCall_Clicked(object sender, EventArgs e)
        {
            SubmitButtonSection.IsVisible = true;
            request = "Care Call";
            Buttons.IsVisible = false;
            DisplayAlert("Note", $"Your request is {request}, press 'Ok', submit button will appear ", "Ok");

        }

        private void CognitiveTherapy_Clicked(object sender, EventArgs e)
        {
            SubmitButtonSection.IsVisible = true;
            request = "Cognitive Therapy";
            Buttons.IsVisible = false;
            DisplayAlert("Note", $"Your request is {request}, press 'Ok', submit button will appear ", "Ok");

        }

        private void DietaryPlan_Clicked(object sender, EventArgs e)
        {
            SubmitButtonSection.IsVisible = true;
            request = "Dietary Plan";
            Buttons.IsVisible = false;
            DisplayAlert("Note", $"Your request is {request}, press 'Ok', submit button will appear ", "Ok");
        }

        private void PrivatePrescription_Clicked(object sender, EventArgs e)
        {
            SubmitButtonSection.IsVisible = true;
            request = "Blood Test";
            Buttons.IsVisible = false;
            DisplayAlert("Note", $"Your request is {request}, press 'Ok', submit button will appear ", "Ok");

        }

        private void Vitamin_Clicked(object sender, EventArgs e)
        {
            SubmitButtonSection.IsVisible = true;
            request = "Vitamin";
            Buttons.IsVisible = false;
            DisplayAlert("Note", $"Your request is {request}, press 'Ok', submit button will appear ", "Ok");

        }

        async private void SubmitButton_Clicked(object sender, EventArgs e)
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

        }
    }
}
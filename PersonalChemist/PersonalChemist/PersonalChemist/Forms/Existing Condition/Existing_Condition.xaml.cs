using MySqlConnector;
using PersonalChemist.DBModels;
using PersonalChemist.Landing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PersonalChemist.Forms.Existing_Condition
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Existing_Condition : ContentPage
    {
        SignalRService signalR;

        string UsersEmail;
        public Existing_Condition(string Email)
        {
            UsersEmail = Email;
            InitializeComponent();
            signalR = new SignalRService();
            SubmitSection.IsVisible = false;
        }
        string FormName = "Existing Condition";
        string SubmitDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string request = "Review Condition";
        

        string Condition;



        private void Heart_Clicked(object sender, EventArgs e)
        {
            Condition = "Heart";
            SubmitSection.IsVisible = true;
            FirstSymptomSection.IsVisible = false;
            SecondSymptomSection.IsVisible = false;
            DisplayAlert("Note", $"You have selected your existing condition as {Condition}, press 'Ok' and 'Submit' will appear to submit your request ", "Ok");
        }

        private void Diabetes_Clicked(object sender, EventArgs e)
        {
            Condition = "Diabetes";
            SubmitSection.IsVisible = true;
            FirstSymptomSection.IsVisible = false;
            SecondSymptomSection.IsVisible = false;
            DisplayAlert("Note", $"You have selected your existing condition as {Condition}, press 'Ok' and 'Submit' will appear to submit your request ", "Ok");

        }

        private void Asthma_Clicked(object sender, EventArgs e)
        {
            Condition = "Asthma";
            SubmitSection.IsVisible = true;
            FirstSymptomSection.IsVisible = false;
            SecondSymptomSection.IsVisible = false;
            DisplayAlert("Note", $"You have selected your existing condition as {Condition}, press 'Ok' and 'Submit' will appear to submit your request ", "Ok");
        }

        private void HighBloodPressure_Clicked(object sender, EventArgs e)
        {
            Condition = "High Blood Pressure";
            SubmitSection.IsVisible = true;
            FirstSymptomSection.IsVisible = false;
            SecondSymptomSection.IsVisible = false;
            DisplayAlert("Note", $"You have selected your existing condition as {Condition}, press 'Ok' and 'Submit' will appear to submit your request ", "Ok");
        }

        private void Pain_Clicked(object sender, EventArgs e)
        {
            Condition = "Pain";
            SubmitSection.IsVisible = true;
            FirstSymptomSection.IsVisible = false;
            SecondSymptomSection.IsVisible = false;
            DisplayAlert("Note", $"You have selected your existing condition as {Condition}, press 'Ok' and 'Submit' will appear to submit your request ", "Ok");
        }

        private void Anxiety_Clicked(object sender, EventArgs e)
        {
            Condition = "Anxiety";
            SubmitSection.IsVisible = true;
            FirstSymptomSection.IsVisible = false;
            SecondSymptomSection.IsVisible = false;
            DisplayAlert("Note", $"You have selected your existing condition as {Condition}, press 'Ok' and 'Submit' will appear to submit your request ", "Ok");
        }

        private void Arthritis_Clicked(object sender, EventArgs e)
        {
            Condition = "Arthritis";
            SubmitSection.IsVisible = true;
            FirstSymptomSection.IsVisible = false;
            SecondSymptomSection.IsVisible = false;
            DisplayAlert("Note", $"You have selected your existing condition as {Condition}, press 'Ok' and 'Submit' will appear to submit your request ", "Ok");
        }

        private void StomachCramp_Clicked(object sender, EventArgs e)
        {
            Condition = "Stomach Cramp";
            SubmitSection.IsVisible = true;
            FirstSymptomSection.IsVisible = false;
            SecondSymptomSection.IsVisible = false;

            DisplayAlert("Note", $"You have selected your existing condition as {Condition}, press 'Ok' and 'Submit' will appear to submit your request ", "Ok");
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

            await Navigation.PushModalAsync(new Home(UsersEmail));
            
        }
    }
}
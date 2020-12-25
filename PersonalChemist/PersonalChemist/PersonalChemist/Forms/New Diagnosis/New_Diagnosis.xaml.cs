using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalChemist.DBModels;
using MySqlConnector;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PersonalChemist.Landing;

namespace PersonalChemist.Forms.New_Diagnosis
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class New_Diagnosis : ContentPage
    {
        SignalRService signalR;
        

        string FormName = "New Diagnosis";
        string SubmitDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string request;
        string UsersEmail;
        

        string patient;
        string duration;
        string actionTaken;
        public New_Diagnosis(string Email)
        {
            UsersEmail = Email;
            InitializeComponent();
            PatientSection.IsVisible = true;
            SymptomSection.IsVisible = false;
            ActionSection.IsVisible = false;
            HowLongSection.IsVisible = false;
            signalR = new SignalRService();
        }


        // first section
        private void BabyImage_Clicked(object sender, EventArgs e)
        {
            
            patient = "Baby";
            
            
            PatientSection.IsVisible = false;
            SymptomSection.IsVisible = true;
        }

        private void ChildImage_Clicked(object sender, EventArgs e)
        {
            patient = "Child";
            PatientSection.IsVisible = false;
            SymptomSection.IsVisible = true;
        }

        private void TeenImage_Clicked(object sender, EventArgs e)
        {
            patient = "Teen/Adult";
            PatientSection.IsVisible = false;
            SymptomSection.IsVisible = true;
        }

        private void ElderyImage_Clicked(object sender, EventArgs e)
        {
            patient = "Senior";
            PatientSection.IsVisible = false;
            SymptomSection.IsVisible = true;
        }

        



        // Symptoms


        async private void BowelSymptom_Clicked(object sender, EventArgs e)
        {
           
            request = "Bowel";
            SymptomSection.IsVisible = false;
            HowLongSection.IsVisible = true;
        }

        private void ColdSymptom_Clicked(object sender, EventArgs e)
        {
            request = "Cold Symptom";
            SymptomSection.IsVisible = false;
            HowLongSection.IsVisible = true;
        }

        private void DizzinessSymptom_Clicked(object sender, EventArgs e)
        {
            request = "Dizziness";
            SymptomSection.IsVisible = false;
            HowLongSection.IsVisible = true;
        }

        private void EarsSymptom_Clicked(object sender, EventArgs e)
        {
            request = "Ear Symptom";
            SymptomSection.IsVisible = false;
            HowLongSection.IsVisible = true;
        }

        private void EyesSymptom_Clicked(object sender, EventArgs e)
        {
            request = "Eye Symptom";
            SymptomSection.IsVisible = false;
            HowLongSection.IsVisible = true;
        }

        private void FungalSymtom_Clicked(object sender, EventArgs e)
        {
            request = "FungalSymptom";
            SymptomSection.IsVisible = false;
            HowLongSection.IsVisible = true;
        }

        private void NauseaSymptom_Clicked(object sender, EventArgs e)
        {
            request = "Nausea";
            SymptomSection.IsVisible = false;
            HowLongSection.IsVisible = true;
        }

        private void NoseSymptom_Clicked(object sender, EventArgs e)
        {
            request = "Nose";
            SymptomSection.IsVisible = false;
            HowLongSection.IsVisible = true;
        }

        private void OtherSymptom_Clicked(object sender, EventArgs e)
        {
            request = "Other";
            SymptomSection.IsVisible = false;
            HowLongSection.IsVisible = true;
        }

        private void PainSymptom_Clicked(object sender, EventArgs e)
        {
            request = "Pain";
            SymptomSection.IsVisible = false;
            HowLongSection.IsVisible = true;
        }

        private void SkinSymptom_Clicked(object sender, EventArgs e)
        {
            request = "Skin";
            SymptomSection.IsVisible = false;
            HowLongSection.IsVisible = true;
        }

        private void SleepingSymptom_Clicked(object sender, EventArgs e)
        {
            request = "sleeping";
            SymptomSection.IsVisible = false;
            HowLongSection.IsVisible = true;
        }





        // How Long 
        private void OneDay_Clicked(object sender, EventArgs e)
        {
            duration = "One Day";
            HowLongSection.IsVisible = false;
            ActionSection.IsVisible = true;
        }

        private void ThreeDays_Clicked(object sender, EventArgs e)
        {
            duration = "Three Days";
            HowLongSection.IsVisible = false;
            ActionSection.IsVisible = true;
        }

        private void OneWeek_Clicked(object sender, EventArgs e)
        {
            duration = "OneWeek";
            HowLongSection.IsVisible = false;
            ActionSection.IsVisible = true;
        }

        private void OverAWeek_Clicked(object sender, EventArgs e)
        {
            duration = "OverAWeek";
            HowLongSection.IsVisible = false;
            ActionSection.IsVisible = true;
        }














        // Action Taken

        private void NonePng_Clicked(object sender, EventArgs e)
        {
            actionTaken = "None";
            ActionSection.IsVisible = false;
        }

        private void RestPng_Clicked(object sender, EventArgs e)
        {
            actionTaken = "Rest";
            ActionSection.IsVisible = false;
        }

        private void MedPng_Clicked(object sender, EventArgs e)
        {
            actionTaken = "Medication";
            ActionSection.IsVisible = false;
        }

        private void GPPng_Clicked(object sender, EventArgs e)
        {
            actionTaken = "Seen a GP";
            ActionSection.IsVisible = false;
        }

        private void OtherPng_Clicked(object sender, EventArgs e)
        {
            actionTaken = "Other";
            
            ActionSection.IsVisible = false;
        }

        async private void SubmitForm_Clicked(object sender, EventArgs e)
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

            await signalR.SendMessageAsync($"{Environment.NewLine}{UsersEmail}:  ", $"{Environment.NewLine}Form: {form.Name} {Environment.NewLine}Submit Date: {form.SubmitDate}{Environment.NewLine}Patient: {patient}{Environment.NewLine}Request: {form.Request}{Environment.NewLine}Duringtion: {duration}{Environment.NewLine}Action Taken: {actionTaken}");

            await Navigation.PushModalAsync(new Home(UsersEmail));
        }
    }
}
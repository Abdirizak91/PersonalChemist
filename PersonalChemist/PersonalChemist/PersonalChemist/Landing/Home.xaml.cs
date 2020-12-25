using PersonalChemist.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Xaml;
using PersonalChemist.Forms.Existing_Condition;
using PersonalChemist.Forms.New_Diagnosis;
using PersonalChemist.Forms.AboutMedicine;
using PersonalChemist.Forms.Asthma_COPD;
using PersonalChemist.Forms.BookAReview;
using PersonalChemist.Forms.How_To;
using PersonalChemist.Forms.GetInvolved;
using PersonalChemist.DependencyServices;

namespace PersonalChemist.Landing
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : TabbedPage
    {
        string USERSEMAIL;

        SignalRService signalR;

      
        public Home(string Email)
        {
            

            InitializeComponent();
            USERSEMAIL = Email;


            signalR = new SignalRService();
            signalR.Connected += SignalR_ConnectionChanged;
            signalR.ConnectionFailed += SignalR_ConnectionChanged;
            signalR.NewMessageReceived += SignalR_NewMessageReceived;

        }









        // SIGNAL-R 
        void AddMessage(string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Label label = new Label
                {
                    Text = message,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Start
                };

                messageList.Children.Add(label);
            });
        }

      

        void SignalR_NewMessageReceived(object sender, Model.Message message)
        {
            string msg = $"{message.Name} {Environment.NewLine} {message.Text}";
            AddMessage(msg);
        }


        void SignalR_ConnectionChanged(object sender, bool success, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                connectButton.Text = "Connect";
                connectButton.IsEnabled = !success;
                sendButton.IsEnabled = success;
                AddMessage($"Server connection changed: {message}");
            });
        }

        async void ConnectButton_ClickedAsync(object sender, EventArgs e)
        {
            connectButton.Text = "Connecting...";
            connectButton.IsEnabled = false;
            await signalR.ConnectAsync(USERSEMAIL);
        }



        async void SendButton_ClickedAsync(object sender, EventArgs e)
        {
            await signalR.SendMessageAsync(Constants.Username, messageEntry.Text);
            messageEntry.Text = "";
        }

        private void CustomerMain_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new LandingPage());

        }

        private void Logoutbutton_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IClearCookies>().Clear();
        }

        private void AE_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ANE(USERSEMAIL));
        }

        private void NewDiagnosis_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new New_Diagnosis(USERSEMAIL));

        }

        private void ExistingHealthCondition_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Existing_Condition(USERSEMAIL));
        }

        private void AboutMedicine_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new About_Medicine(USERSEMAIL));
        }

        private void Asthma_COPD_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Asthma_COPD(USERSEMAIL));
        }

        private void BookReview_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new BookAReview(USERSEMAIL));
        }

        private void HowTo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new How_To(USERSEMAIL));
        }

        private void GetInvolved_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Get_Involved(USERSEMAIL));
        }
    }
}
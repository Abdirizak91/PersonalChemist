using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Auth.OAuth2;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Diagnostics;
using PersonalChemist.Auth.Helpers;
using System.Windows.Input;
using PersonalChemist;
using PersonalChemist.DependencyServices;
using System.Xml.XPath;
using System.Net.Http.Headers;
using MySqlConnector;
using PersonalChemist.Landing;
using Stripe;
using PersonalChemist.Subscription;
using System.Threading;

namespace PersonalChemist
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login_Options : ContentPage
    {
        public static string TrialType;
        public static string SubType;
        bool UserFound;
        public static string LoggedEmail;
        string googleEmail;
        string Home = "Home";
        string Business = "Business";
        string HealthPro = "HealthPro"; 
        

        public event EventHandler ThresholdReached;
        protected virtual void OnThresholdReached(EventArgs e)
        {
            EventHandler handler = ThresholdReached;
            handler?.Invoke(this, e);
        }


        public delegate void ThresholdReachedEventHandler(object sender, ThresholdReachedEventArgs e);


        public Login_Options()
        {
            InitializeComponent();
           
        }


        private void fbLogin_Clicked(object sender, EventArgs e)
        {

            WebViews.IsVisible = true;
            //Navigation.PushModalAsync(new LandingPage());
            WebViews.Source = "https://www.facebook.com/v8.0/dialog/oauth?client_id=753675685448600&response_type=token&redirect_uri=https://www.facebook.com/connect/login_success.html";


            WebViews.Navigated += WebView_Navigated;

            //string clientId = string.Empty;
            //string redirectUri = string.Empty;
            //switch (Device.RuntimePlatform)
            //{
            //    case Device.iOS:
            //        clientId = FB_Constants.FacebookClientId;
            //        redirectUri = FB_Constants.FacebookiOSRedirectUrl;

            //        break;

            //    case Device.Android:
            //        clientId = FB_Constants.FacebookClientId;
            //        redirectUri = FB_Constants.FacebookAndroidRedirectUrl;
            //        break;
            //};





            //// OAuth2 Xamarin.Auth Authenticator using FB_Constants.cs fields
            //var Authenticator = new Xamarin.Auth.OAuth2Authenticator(clientId, FB_Constants.FacebookScope, new Uri(FB_Constants.FacebookAuthorizeUrl), new Uri(FB_Constants.FacebookiOSRedirectUrl), null);



            //// Login presenter
            //var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            //presenter.Login(Authenticator);

            //// When Authenticator is complete raise 'Authenticator_Completed' method
            //Authenticator.Completed += FB_Authenticator_Completed;



        }

        private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            //the navigated url 
            var AccessURL = e.Url;

            // checking if the navigated url contains the string access_token
            if (AccessURL.Contains("access_token"))
            {
                AccessURL = AccessURL.Replace("https://www.facebook.com/connect/login_success.html#access_token=", string.Empty);

                var AccessToken = AccessURL.Split('&')[0];

                HttpClient client = new HttpClient();


                var response = client.GetStringAsync("https://graph.facebook.com/me?fields=email&access_token=" + AccessToken).Result;

                //convert user json object to a string
                var Data = JsonConvert.DeserializeObject<User>(response);

                // storing user EMAIL 
                LoggedEmail = Data.Email;

                // holding user ID
                var Id = Data.Id;

                WebViews.IsVisible = false;

                // sql connection string to AWS DB using MYSQL 
                using (var connection = new MySqlConnection("Server=personalchemistaws.cyrfxcwnlh2w.eu-west-2.rds.amazonaws.com;User ID=Zak;Password=146680Xagar;Database=PersonalChemist_DB;"))
                {

                    connection.Open();

                    // SELECT STATEMENT ON AWS MYSQL DATABASE, getting list of users on database
                    using (var selectCommand = new MySqlCommand("SELECT EMAIL FROM USERS;", connection))

                    // reading the above statement and executing it
                    using (var reader = selectCommand.ExecuteReader())
                        
                        while(reader.Read())
                        { 
                            // the results of the executed statement is gathered by reader.read, reading executed query
                          

                                // if the content that is being read contains the string of user email, i.e. if the user exists in the database
                                if (reader.GetString(0).Contains($"{LoggedEmail}"))
                                {

                                    UserFound = true;

                                    // previous connection must be closed and reopened to query the database again
                                    connection.Close();
                                    connection.Open();

                                    // command for getting the user subscription id 
                                    var whereCommand = new MySqlCommand($"select SUBID from USERS where EMAIL = '{LoggedEmail}';", connection);

                                    
                                    // executing the above command
                                    var readers = whereCommand.ExecuteReader();

                                    
                                    // reading the executed above command
                                    while (readers.Read())
                                    {
                                        // storing subID 
                                        int USERSUBID = readers.GetInt32(0);

                                        //previous connection must be closed and reopened to query the database again
                                        connection.Close();
                                        connection.Open();

                                        // getting USER_TYPE from subscription table where SUB_ID is the same as the USER subscription ID
                                        var getUserType = new MySqlCommand($"SELECT SUB_TYPE FROM SUBSCRIPTION WHERE SUB_ID = {USERSUBID}",connection);

                                        // executing the above statement
                                        var readID = getUserType.ExecuteReader();

                                        //reading the results of the executed statement 
                                        while(readID.Read())
                                        { 
                                            //USER_TYPE is Home
                                            if (readID.GetString(0).Contains(Home))
                                            {
                                                Navigation.PushModalAsync(new Home(LoggedEmail));

                                                return;
                                            }

                                            //USER_TYPE is Business
                                            else if (readID.GetString(0).Contains(Business))
                                            {
                                                Navigation.PushModalAsync(new Business(LoggedEmail));
                                                return; 
                                            }

                                            //USER_TYPE is HealthPro
                                            else if (readID.GetString(0).Contains(HealthPro))
                                            {
                                                Navigation.PushModalAsync(new HealthPro(LoggedEmail));
                                                return;
                                            }
                                        }
                                    }
                                }

                              



                            
                        }

                    //  USER DONT EXIST NEW USER, NAVIGATE TO NEW USER PAGE PASSING THE EMAIL OF THE USER
                    if (UserFound == false)
                    {

                        Navigation.PushModalAsync(new NewUser(LoggedEmail));


                    };



                }

                

            }

            else
            {
                WebViews.IsVisible = true;
                return;

            }
        }





        void googleLogins_Clicked(object sender, EventArgs e)
        {

            string clientId = string.Empty;
            string redirectUri = string.Empty;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = Google_Constants.clientIDiOS;
                    redirectUri = Google_Constants.iOSRedirectUrl;

                    break;

                case Device.Android:
                    clientId = Google_Constants.clientID;
                    redirectUri = Google_Constants.AndroidRedirectUrl;
                    break;
            };




            var authenticator = new Xamarin.Auth.OAuth2Authenticator(clientId, null, Google_Constants.scope, new Uri(Google_Constants.AuthorizeUrl), new Uri(redirectUri), new Uri(Google_Constants.AccessTokenUrl), null, true);



            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();

            presenter.Login(authenticator);

            authenticator.Completed += Google_Authenticator_Completed;
        }




        async void Google_Authenticator_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {

            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= Google_Authenticator_Completed;
                authenticator.Error -= OnAuthError;
            }


            if (e.IsAuthenticated)
            {
                // call google api to fetch logged in user profile info
                var request = new OAuth2Request("GET", new Uri("https://www.googleapis.com/oauth2/v2/userinfo"), null, e.Account);

                var response = await request.GetResponseAsync();

                if (response != null)
                {
                    string user = await response.GetResponseTextAsync();

                    var userObject = JsonConvert.DeserializeObject<User>(user);

                    googleEmail = userObject.Email;

                    LoggedEmail = googleEmail;

                    var Id = userObject.Id;

                }




                // sql connection string to AWS DB using MYSQL 
                using (var connection = new MySqlConnection("Server=personalchemistaws.cyrfxcwnlh2w.eu-west-2.rds.amazonaws.com;User ID=Zak;Password=146680Xagar;Database=PersonalChemist_DB;"))
                {

                    connection.Open();

                    // SELECT STATEMENT ON AWS MYSQL DATABASE, getting list of users on database
                    using (var selectCommand = new MySqlCommand("SELECT EMAIL FROM USERS;", connection))

                    // reading the above statement and executing it
                    using (var reader = selectCommand.ExecuteReader())

                        
                        // the results of the executed statement is gathered by reader.read, reading executed query
                        while (reader.Read())
                        {
                            

                            // if the content that is being read contains the string of user email, i.e. if the user exists in the database
                            if (reader.GetString(0).Contains($"{LoggedEmail}"))
                            {
                                UserFound = true;
                                // previous connection must be closed and reopened to query the database again
                                connection.Close();
                                connection.Open();

                                // command for getting the user subscription id 
                                var whereCommand = new MySqlCommand($"select SUBID from USERS where EMAIL = '{LoggedEmail}';", connection);


                                // executing the above command
                                var readers = whereCommand.ExecuteReader();


                                // reading the executed above command
                                while (readers.Read())
                                {
                                    // storing subID 
                                    int USERSUBID = readers.GetInt32(0);

                                    //previous connection must be closed and reopened to query the database again
                                    connection.Close();
                                    connection.Open();

                                    // getting USER_TYPE from subscription table where SUB_ID is the same as the USER subscription ID
                                    var getUserType = new MySqlCommand($"SELECT SUB_TYPE FROM SUBSCRIPTION WHERE SUB_ID = {USERSUBID}", connection);

                                    // executing the above statement
                                    var readID = getUserType.ExecuteReader();

                                    
                                    //reading the results of the executed statement 
                                    while (readID.Read())
                                    {


                                        //USER_TYPE is Home

                                        
                                        if (readID.GetString(0) == "Trial_Home" || readID.GetString(0) == "Trial_Business" || readID.GetString(0) == "Trial_Professional")
                                        {
                                            SubType = readID.GetString(0);
                                            TrialType = readID.GetString(0);

                                            connection.Close();
                                            connection.Open();
                                            var SelectCommandTrial = new MySqlCommand($"SELECT SUB_START FROM SUBSCRIPTION WHERE SUB_ID = {USERSUBID};", connection);

                                            var CheckTrial = SelectCommandTrial.ExecuteReader();
                                                while (CheckTrial.Read())
                                                {
                                                
                                                    var StartTime = DateTime.Now;

                                                    var startDate = CheckTrial.GetDateTime(0);

                                                    



                                                    TimeSpan DateDifference = StartTime - startDate;


                                                    var datedifferecedString = DateDifference.ToString();

                                                    var ExactDays = datedifferecedString.Split('.')[0];

                                                    if(ExactDays.Length > 2)
                                                    {
                                                        ExactDays = datedifferecedString.Split(':')[0];
                                                    }


                                                    if (Int32.Parse(ExactDays) > 14)
                                                    {
                                                        DisplayAlert("Alert", "Your Trial Period Is Over", "Ok");
                                                        Navigation.PushModalAsync(new Subscriptions());
                                                        return;
                                                        
                                                    }

                                                    else
                                                    {
                                                        

                                                        if (TrialType.Contains("Trial_Home"))
                                                        {
                                                            
                                                            Navigation.PushModalAsync(new Home(LoggedEmail));

                                                            return;
                                                        }

                                                        //USER_TYPE is Business
                                                        else if (TrialType.Contains("Trial_Business"))
                                                        {
                                                            Navigation.PushModalAsync(new Business(LoggedEmail));
                                                            return;
                                                        }

                                                        //USER_TYPE is HealthPro
                                                        else if (TrialType.Contains("Trial_Professional"))
                                                        {
                                                            
                                                            Navigation.PushModalAsync(new HealthPro(LoggedEmail));
                                                            return;
                                                        }
                                                    }



                                                }



                                        }



                                        if (SubType.Contains(Home))
                                        {
                                            Navigation.PushModalAsync(new Home(LoggedEmail));

                                            return;
                                        }

                                        //USER_TYPE is Business
                                        else if (SubType.Contains(Business))
                                        {
                                            Navigation.PushModalAsync(new Business(LoggedEmail));
                                            return;
                                        }

                                        //USER_TYPE is HealthPro
                                        else if (SubType.Contains(HealthPro))
                                        {
                                            Navigation.PushModalAsync(new HealthPro(LoggedEmail));
                                            return;
                                        }


                                    }
                                }
                            }

                           

                        }

                    if (UserFound == false)
                    {

                        Navigation.PushModalAsync(new NewUser(LoggedEmail));


                    };


                }




            }


        }


        void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= Google_Authenticator_Completed;
                authenticator.Error -= OnAuthError;
            }

            Debug.WriteLine("Authentication error: " + e.Message);
        }

     

        public class ThresholdReachedEventArgs : EventArgs
        {
            public string WebFormsURL { get; set; }
        }

        //private void Button_Clicked(object sender, EventArgs e)
        //{

        //    Navigation.PushModalAsync(new ChatMessage());
        //}

        private void WebViews_Navigated(object sender, WebNavigatedEventArgs e)
        {

        }
    }
}
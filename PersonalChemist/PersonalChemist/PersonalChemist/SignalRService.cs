using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PersonalChemist.Model;
using System.Configuration;
using MySqlConnector;
using System.Data;
using System.Collections;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;

namespace PersonalChemist
{
    public class SignalRService
    {
        string UsersEmail;
        HubConnection connection;


        HttpClient client;

        static string connectedUser;

        public delegate void MessageReceivedHandler(object sender, Message message);
        public delegate void ConnectionHandler(object sender, bool successful, string message);

        public event MessageReceivedHandler NewMessageReceived;
        public event ConnectionHandler Connected;
        public event ConnectionHandler ConnectionFailed;
        public bool IsConnected { get; private set; }
        public bool IsBusy { get; private set; }

        public SignalRService()
        {
            client = new HttpClient();
            
        }

        

        // send message to hub
        public async Task SendMessageAsync(string username, string message)
        {

            string ChemistConnectionID;
            string connectionID;
            ArrayList connectionIDList = new ArrayList();


            // SUBSCRIBED 
            if(Login_Options.SubType == "Home")
            {


                using (var connection = new MySqlConnection("Server=personalchemistaws.cyrfxcwnlh2w.eu-west-2.rds.amazonaws.com;User ID=Zak;Password=146680Xagar;Database=PersonalChemist_DB;"))
                {

                    connection.Open();

                    // SELECT STATEMENT ON AWS MYSQL DATABASE, getting list of users on database
                    using (var selectCommand = new MySqlCommand($"SELECT cONNECTIONID FROM CHEMISTSONLINE;", connection))
                    {
                        // reading the above statement and executing it
                        var reader = selectCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            connectionIDList.Add(reader.GetString(0));
                        }
                    }
                    connection.Close();

                    ChemistConnectionID = connectionIDList[0].ToString();


                    IsBusy = true;

                    var newMessage = new Message
                    {
                        ConnectionID = ChemistConnectionID,
                        Name = username,
                        Text = message
                    };

                    var json = JsonConvert.SerializeObject(newMessage);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var result = await client.PostAsync($"{Constants.HostName}/api/talk", content);

                    IsBusy = false;


                    connection.Open();

                    // SELECT STATEMENT ON AWS MYSQL DATABASE, getting list of users on database
                    using (var InsertCommand = new MySqlCommand($"INSERT INTO ACTIVECHATS(CUSTOMERSEMAIL, CUSTOMERS_CON_ID, CHEMISTS_CON_ID) VALUES('{UsersEmail}','{connectedUser}','{ChemistConnectionID}');", connection))
                    {
                        // reading the above statement and executing it
                        var reader_activeChat = InsertCommand.ExecuteReader();

                    }
                    connection.Close();


                }

            }



            if (Login_Options.SubType == "Business")
            {

            }



            if(Login_Options.SubType == "HealthPro")
            {
                string customersIDcon = "";
                using (var connection = new MySqlConnection("Server=personalchemistaws.cyrfxcwnlh2w.eu-west-2.rds.amazonaws.com;User ID=Zak;Password=146680Xagar;Database=PersonalChemist_DB;"))
                {
                    connection.Open();

                    using (var selectCommand = new MySqlCommand($"SELECT CUSTOMERS_CON_ID FROM ACTIVECHATS WHERE CHEMISTS_CON_ID = '{connectedUser}';", connection))
                    {
                        // reading the above statement and executing it
                        var reader = selectCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            customersIDcon = reader.GetString(0);

                        }
                    }

                    IsBusy = true;

                    var newMessage = new Message
                    {
                        ConnectionID = customersIDcon,
                        Name = username,
                        Text = message
                    };

                    var json = JsonConvert.SerializeObject(newMessage);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var result = await client.PostAsync($"{Constants.HostName}/api/talk", content);

                    IsBusy = false;
                    connection.Close();
                }

            }



            //TRIALS

            if (Login_Options.TrialType == "Trial_Home")
            {
                using (var connection = new MySqlConnection("Server=personalchemistaws.cyrfxcwnlh2w.eu-west-2.rds.amazonaws.com;User ID=Zak;Password=146680Xagar;Database=PersonalChemist_DB;"))
                {

                    connection.Open();

                    // SELECT STATEMENT ON AWS MYSQL DATABASE, getting list of users on database
                    using (var selectCommand = new MySqlCommand($"SELECT cONNECTIONID FROM CHEMISTSONLINE;", connection))
                    {
                        // reading the above statement and executing it
                        var reader = selectCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            connectionIDList.Add(reader.GetString(0));
                        }
                    }
                    connection.Close();

                    ChemistConnectionID = connectionIDList[0].ToString();


                    IsBusy = true;

                    var newMessage = new Message
                    {
                        ConnectionID = ChemistConnectionID,
                        Name = username,
                        Text = message
                    };

                    var json = JsonConvert.SerializeObject(newMessage);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var result = await client.PostAsync($"{Constants.HostName}/api/talk", content);

                    IsBusy = false;


                    connection.Open();

                    // SELECT STATEMENT ON AWS MYSQL DATABASE, getting list of users on database
                    using (var InsertCommand = new MySqlCommand($"INSERT INTO ACTIVECHATS(CUSTOMERSEMAIL, CUSTOMERS_CON_ID, CHEMISTS_CON_ID) VALUES('{UsersEmail}','{connectedUser}','{ChemistConnectionID}');", connection))
                    {
                        // reading the above statement and executing it
                        var reader_activeChat = InsertCommand.ExecuteReader();

                    }
                    connection.Close();


                }
            }



            if (Login_Options.TrialType == "Trial_Business")
            {

            }



            if (Login_Options.TrialType == "Trial_Professional")
            {
                string customersIDcon = "";
                using (var connection = new MySqlConnection("Server=personalchemistaws.cyrfxcwnlh2w.eu-west-2.rds.amazonaws.com;User ID=Zak;Password=146680Xagar;Database=PersonalChemist_DB;"))
                {
                    connection.Open();

                    using (var selectCommand = new MySqlCommand($"SELECT CUSTOMERS_CON_ID FROM ACTIVECHATS WHERE CHEMISTS_CON_ID = '{connectedUser}';", connection))
                    {
                        // reading the above statement and executing it
                        var reader = selectCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            customersIDcon = reader.GetString(0);
                            
                        }
                    }

                    IsBusy = true;

                    var newMessage = new Message
                    {
                        ConnectionID = customersIDcon,
                        Name = username,
                        Text = message
                    };

                    var json = JsonConvert.SerializeObject(newMessage);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var result = await client.PostAsync($"{Constants.HostName}/api/talk", content);

                    IsBusy = false;
                    connection.Close();
                }

                
            }



        }//End of Method



        public async Task ConnectAsync(string Email)
        {

            UsersEmail = Email;
            try
            {
                IsBusy = true;

                string negotiateJson = await client.GetStringAsync($"{Constants.HostName}/api/negotiate");
                NegotiateInfo negotiate = JsonConvert.DeserializeObject<NegotiateInfo>(negotiateJson);

                     connection = new HubConnectionBuilder()
                    .AddNewtonsoftJsonProtocol()
                    .WithUrl(negotiate.Url, options =>
                    {
                        
            
                        options.AccessTokenProvider = async () => negotiate.AccessToken;


                    })
                    .Build();
                
                connection.On<JObject>(Constants.MessageName, AddNewMessage);
             
                await connection.StartAsync();
                
                connectedUser = connection.ConnectionId.ToString();
                

                IsConnected = true;
                IsBusy = false;

                Connected?.Invoke(this, true, "Connection successful.");
            }
            catch (Exception ex)
            {
                ConnectionFailed?.Invoke(this, false, ex.Message);
            }


                                                            // CHECK USER TYPE AND MAKE AND POPULATE RESPECTIVE ONLINE TABLES
            //CUSTOMERS
            if (Login_Options.SubType == "Home")
            {
                // Add user to CUSTOMERS ONLINE TABLE
                using (var connection = new MySqlConnection("Server=personalchemistaws.cyrfxcwnlh2w.eu-west-2.rds.amazonaws.com;User ID=Zak;Password=146680Xagar;Database=PersonalChemist_DB;"))
                {

                    connection.Open();

                    // SELECT STATEMENT ON AWS MYSQL DATABASE, getting list of users on database
                    using (var selectCommand = new MySqlCommand($"INSERT INTO CUSTOMERSONLINE (EMAIL, cONNECTIONID) VALUES('{UsersEmail}','{connectedUser}');", connection))
                    {
                        // reading the above statement and executing it
                        var reader = selectCommand.ExecuteReader();
                    }


                    connection.Close();

                }
            }


            //if (Login_Options.TrialType == "Trial_Home")
            //{
            //    // Add user to CUSTOMERS ONLINE TABLE
            //    using (var connection = new MySqlConnection("Server=personalchemistaws.cyrfxcwnlh2w.eu-west-2.rds.amazonaws.com;User ID=Zak;Password=146680Xagar;Database=PersonalChemist_DB;"))
            //    {

            //        connection.Open();

            //        // SELECT STATEMENT ON AWS MYSQL DATABASE, getting list of users on database
            //        using (var selectCommand = new MySqlCommand($"INSERT INTO CUSTOMERSONLINE (EMAIL, cONNECTIONID) VALUES('{UsersEmail}','{connectedUser}');", connection))
            //        {
            //            // reading the above statement and executing it
            //            var reader = selectCommand.ExecuteReader();
            //        }


            //        connection.Close();

            //    }
            //}



            if (Login_Options.SubType == "HealthPro")
            {
                // Add user to PROFESIONAL ONLINE TABLE
                using (var connection = new MySqlConnection("Server=personalchemistaws.cyrfxcwnlh2w.eu-west-2.rds.amazonaws.com;User ID=Zak;Password=146680Xagar;Database=PersonalChemist_DB;"))
                {

                    connection.Open();

                    // SELECT STATEMENT ON AWS MYSQL DATABASE, getting list of users on database
                    using (var selectCommand = new MySqlCommand($"INSERT INTO CHEMISTSONLINE (EMAIL, cONNECTIONID) VALUES('{UsersEmail}','{connectedUser}');", connection))
                    {
                        // reading the above statement and executing it
                        var reader = selectCommand.ExecuteReader();
                    }

                    connection.Close();

                }
            }

            if (Login_Options.TrialType == "Trial_Professional")
            {
                bool ChemistIsOnline = false;
                string changedConnectionID;

                // Add user to PROFESIONAL ONLINE TABLE
                using (var connection = new MySqlConnection("Server=personalchemistaws.cyrfxcwnlh2w.eu-west-2.rds.amazonaws.com;User ID=Zak;Password=146680Xagar;Database=PersonalChemist_DB;"))
                {

                    connection.Open();

                    // SELECT STATEMENT ON AWS MYSQL DATABASE, getting list of users on database
                    using (var selectCommand = new MySqlCommand($"SELECT EMAIL FROM CHEMISTSONLINE WHERE EMAIL = '{UsersEmail}';", connection))
                    {
                        // reading the above statement and executing it
                        var Selectreader = selectCommand.ExecuteReader();
                        while(Selectreader.Read())
                        {
                            if(Selectreader.GetString(0) != "" || Selectreader.GetString(0) != null)
                            {
                                ChemistIsOnline = true;
                                connection.Close();
                                connection.Open();

                                // SELECT STATEMENT ON AWS MYSQL DATABASE, getting list of users on database
                                using (var InsertCommand = new MySqlCommand($"UPDATE CHEMISTSONLINE SET cONNECTIONID = '{connectedUser}' WHERE EMAIL = '{UsersEmail}';", connection))
                                {
                                    // reading the above statement and executing it
                                    var Insertreader = InsertCommand.ExecuteReader();
                                }
                                connection.Close();
                                return;

                            }

                            if (Selectreader.GetString(0) == "" || Selectreader.GetString(0) == null)
                            {
                                connection.Close();
                                connection.Open();

                                // SELECT STATEMENT ON AWS MYSQL DATABASE, getting list of users on database
                                using (var InsertCommand = new MySqlCommand($"INSERT INTO CHEMISTSONLINE (EMAIL, cONNECTIONID) VALUES('{UsersEmail}','{connectedUser}');", connection))
                                {
                                    // reading the above statement and executing it
                                    var Insertreader = InsertCommand.ExecuteReader();
                                }
                                connection.Close();

                            }


                        }

                    }
                    


                    
                    

                }
            }



        }
        


        public async Task disconnect()
        {
            // Disconnect user from Hub
            await connection.StopAsync();

            // Add user to CUSTOMERS ONLINE TABLE
            using (var connection = new MySqlConnection("Server=personalchemistaws.cyrfxcwnlh2w.eu-west-2.rds.amazonaws.com;User ID=Zak;Password=146680Xagar;Database=PersonalChemist_DB;"))
            {

                connection.Open();

                // DELETE USER FROM CUSTOMERONLINE TABLE
                using (var selectCommand = new MySqlCommand($"DELETE FROM ACTIVECHATS WHERE CHEMISTS_CON_ID = '{connectedUser}';", connection))
                {
                    // reading the above statement and executing it
                    var reader = selectCommand.ExecuteReader();
                }


                connection.Close();

            }

        }




        Task Connection_Closed(Exception arg)
        {
            ConnectionFailed?.Invoke(this, false, arg.Message);
            IsConnected = false;
            IsBusy = false;
            return Task.CompletedTask;
        }



        public void AddNewMessage(JObject message)
        {
            Message messageModel = new Message
            {
                
                Name = message.GetValue("name").ToString(),
                Text = message.GetValue("text").ToString(),
                //TimeReceived = DateTime.Now
            };

            NewMessageReceived?.Invoke(this, messageModel);
        }



     










    }
}

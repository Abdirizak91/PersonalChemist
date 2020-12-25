using MySqlConnector;
using PersonalChemist.DBModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PersonalChemist.Landing
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewUser : ContentPage
    {
        string Users_Email;
        string Users_Name;
        int SUBID;
        public NewUser(string Email)
        {
            InitializeComponent();

            UserEmail.Text = Email;

            Users_Email = Email;
        }



          







        private void Home_Clicked(object sender, EventArgs e)
        {


            

            if (Number.Text != "")
            {
                var StartTime = DateTime.Now;
                var EndTime = DateTime.Now.AddDays(14);

                string StartformatForMySql = StartTime.ToString("yyyy-MM-dd HH:mm:ss");
                string EndformateForMySql = EndTime.ToString("yyyy-MM-dd HH:mm:ss");

                var Subcription = new Sub()
                {
                    SUB_TYPE = "Trial_Home",
                    SUB_COST = 0,
                    SUB_START = StartformatForMySql,
                    SUB_END = EndformateForMySql,
                    CATERS_FOR = 6
                };

                using (var connection = new MySqlConnection("Server=personalchemistaws.cyrfxcwnlh2w.eu-west-2.rds.amazonaws.com;User ID=Zak;Password=146680Xagar;Database=PersonalChemist_DB;"))
                {
                    connection.Open();
                    var SubInsert = new MySqlCommand($"INSERT INTO SUBSCRIPTION(SUB_TYPE,SUB_COST,CATERS_FOR,SUB_START,SUB_END) VALUES ('{Subcription.SUB_TYPE}', {Subcription.SUB_COST}, {Subcription.CATERS_FOR} , '{Subcription.SUB_START}', '{Subcription.SUB_END}');", connection);
                    SubInsert.ExecuteReader();


                    connection.Close();
                    connection.Open();

                    var selectCommand = new MySqlCommand($"SELECT SUB_ID FROM SUBSCRIPTION WHERE SUB_START = '{Subcription.SUB_START}';", connection);
                    using (var reader = selectCommand.ExecuteReader())

                        while (reader.Read())
                        {

                            SUBID = reader.GetInt32(0);





                            var USER_OBJECT = new PersonalChemist.DBModels.User()
                            {
                                EMAIL = Users_Email,
                                NAME = Users_Name,
                                PHONE_NUM = Number.Text,
                                SUBSCRIPTION_ID = SUBID


                            };


                            connection.Close();
                            connection.Open();

                            var UserInsert = new MySqlCommand($"INSERT INTO USERS(EMAIL, USER_NAME, PHONE_NUM, SUBID) VALUES('{USER_OBJECT.EMAIL}','{USER_OBJECT.NAME}','{USER_OBJECT.PHONE_NUM}',{USER_OBJECT.SUBSCRIPTION_ID})", connection);
                            var read = UserInsert.ExecuteReader();

                            Navigation.PushModalAsync(new Home(Users_Email));
                            connection.Close();

                            return;

                        }

                }
            }

            else
            {
                DisplayAlert("Error", "Please Provide a Number", "Ok");
            };


        }


        private void Business_Clicked(object sender, EventArgs e)
        {

        }

        private void HealthPro_Clicked(object sender, EventArgs e)
        {

            if (Number.Text != "")
            {
                var StartTime = DateTime.Now;
                var EndTime = DateTime.Now.AddDays(14);

                string StartformatForMySql = StartTime.ToString("yyyy-MM-dd HH:mm:ss");
                string EndformateForMySql = EndTime.ToString("yyyy-MM-dd HH:mm:ss");

                var Subcription = new Sub()
                {
                    SUB_TYPE = "Trial_Professional",
                    SUB_COST = 0,
                    SUB_START = StartformatForMySql,
                    SUB_END = EndformateForMySql,
                    CATERS_FOR = 24
                };

                using (var connection = new MySqlConnection("Server=personalchemistaws.cyrfxcwnlh2w.eu-west-2.rds.amazonaws.com;User ID=Zak;Password=146680Xagar;Database=PersonalChemist_DB;"))
                {
                    connection.Open();
                    var SubInsert = new MySqlCommand($"INSERT INTO SUBSCRIPTION(SUB_TYPE,SUB_COST,CATERS_FOR,SUB_START,SUB_END) VALUES ('{Subcription.SUB_TYPE}', {Subcription.SUB_COST}, {Subcription.CATERS_FOR} , '{Subcription.SUB_START}', '{Subcription.SUB_END}');", connection);
                    SubInsert.ExecuteReader();


                    connection.Close();
                    connection.Open();

                    var selectCommand = new MySqlCommand($"SELECT SUB_ID FROM SUBSCRIPTION WHERE SUB_START = '{Subcription.SUB_START}';", connection);
                    using (var reader = selectCommand.ExecuteReader())

                        while (reader.Read())
                        {

                            SUBID = reader.GetInt32(0);





                            var USER_OBJECT = new PersonalChemist.DBModels.User()
                            {
                                EMAIL = Users_Email,
                                NAME = Users_Name,
                                PHONE_NUM = Number.Text,
                                SUBSCRIPTION_ID = SUBID


                            };


                            connection.Close();
                            connection.Open();

                            var UserInsert = new MySqlCommand($"INSERT INTO USERS(EMAIL, USER_NAME, PHONE_NUM, SUBID) VALUES('{USER_OBJECT.EMAIL}','{USER_OBJECT.NAME}','{USER_OBJECT.PHONE_NUM}',{USER_OBJECT.SUBSCRIPTION_ID})", connection);
                            var read = UserInsert.ExecuteReader();

                            Navigation.PushModalAsync(new Business(Users_Email));
                            connection.Close();

                            return;

                        }

                }
            }

            else
            {
                DisplayAlert("Error", "Please Provide a Number", "Ok");
            };

        }
    }
}
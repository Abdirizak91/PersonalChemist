using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalChemist
{
    public static class Google_Constants
    {

        public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/v2/auth";
        public static string scope = "https://www.googleapis.com/auth/userinfo.email";
        public static string response_type = "code";
        public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";

        //public static string RedirectUrl for web application = "https://www.google.co.uk/";
        


        //Android 
        //public static string clientID = "13868942270-oq2krso8dcc6h91jd8cuaf40obgccgr8.apps.googleusercontent.com";
        public static string clientID = "60149538697-ijl0r7lhi9l1mjb83bgrdl4j9j6ub8ve.apps.googleusercontent.com";
        public static string ClientSecret = "abE3wcmEqD1ZtUkd67QgpdyW";
        public static string AndroidRedirectUrl = "com.googleusercontent.apps.60149538697-ijl0r7lhi9l1mjb83bgrdl4j9j6ub8ve:/oauth2redirect";


        // iOS
        //public static string clientIDiOS = "652262507370-a563jdojuaueiav0k48juvjd0fqrjq5v.apps.googleusercontent.com";
        

        public static string clientIDiOS = "8904010168-misgs16c6pe7tig6s8jhdb74uoepabo5.apps.googleusercontent.com";
        public static string clientSecretiOS = "HBCvEU4iF_i_RjraF5w9bjq6";
        public static string iOSRedirectUrl = "com.googleusercontent.apps.8904010168-misgs16c6pe7tig6s8jhdb74uoepabo5:/oauth2redirect";

    }
}

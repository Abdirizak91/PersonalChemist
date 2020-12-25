using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalChemist
{
    public static class FB_Constants
    {
        public static string FacebookClientId = "753675685448600";
        // These values do not need changing 
        public static string FacebookScope = "email";
        public static string FacebookAuthorizeUrl = "https://www.faceook.com/dialog/oauth/";
        public static string FacebookAccessTokenUrl = "https://www.facebook.com/connect/login_success.html";
        public static string FacebookUserInfoUrl = "https://graph.facebook.com/me?fields=email&access_token={accessToken}";


        // Set these to reversed iOS/Android cliend ID's, with :/oauth2redirect appended
        public static string FacebookiOSRedirectUrl = "https://www.facebook.com/connect/login_success.html";
        public static string FacebookAndroidRedirectUrl = "https://www.facebook.com/connect/login_success.html";
    


    
    
    }
}

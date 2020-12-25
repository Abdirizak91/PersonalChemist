using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PersonalChemist
{
    public static class Constants
    {
        // NOTE: If testing locally, use http://localhost:7071
        // otherwise enter your Azure Function App url
        // For example: http://YOUR_FUNCTION_APP_NAME.azurewebsites.net
        public static string HostName { get; set; } = "https://functionappsignalrpc.azurewebsites.net";

        public static string MessageName { get; set; } = "newMessage";

        public static string Username
        {
            get
            {
                return $"{Login_Options.LoggedEmail}";
                //return $"{Device.RuntimePlatform} User";
            }
        }
    }
}

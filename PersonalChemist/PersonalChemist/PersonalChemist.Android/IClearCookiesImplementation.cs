using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;
using PersonalChemist.Droid;
using System.Net;
using System.Reflection;
using PersonalChemist.DependencyServices;

[assembly: Dependency(typeof(IClearCookiesImplementation))]
namespace PersonalChemist.Droid
{
    class IClearCookiesImplementation : IClearCookies
    {
        public void Clear()
        {
            var cookieManager = CookieManager.Instance;
            cookieManager.RemoveAllCookie();
        }
    }
}
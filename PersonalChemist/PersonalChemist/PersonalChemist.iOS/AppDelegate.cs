using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using Xamarin.Auth;
using PersonalChemist.Auth.Helpers;
using System.Net;
using Xamarin.Forms;
using PersonalChemist.DependencyServices;
using PersonalChemist.iOS;

[assembly: Dependency(typeof(AppDelegate))]
namespace PersonalChemist.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IClearCookies
    {
        public void Clear()
        {
            NSHttpCookieStorage CookieStorage = NSHttpCookieStorage.SharedStorage;
            foreach (var cookie in CookieStorage.Cookies)
            {
                CookieStorage.DeleteCookie(cookie);
            }
                
        }

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {

            
            ServicePointManager.ServerCertificateValidationCallback += (o, cert, chain, errors) => true;
            global::Xamarin.Forms.Forms.Init();

            global::Xamarin.Auth.Presenters.XamarinIOS.AuthenticationConfiguration.Init();

            
            
            LoadApplication(new App());


            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl
           (
               UIApplication application,
               NSUrl url,
               NSDictionary options
           )
        {
            // Convert iOS NSUrl to C#/netxf/BCL System.Uri - common API
            Uri uri_netfx = new Uri(url.AbsoluteString);

            // load redirect_url Page for parsing
            AuthenticationState.Authenticator.OnPageLoading(uri_netfx);

            return true;
        }




        //public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        //{
        //    // custom stuff here using different properties of the url passed in
        //    return true;
        //}

        //public override void OnActivated(UIApplication uiApplication)
        //{
        //    base.OnActivated(uiApplication);

        //}
    }
}

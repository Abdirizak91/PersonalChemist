using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PersonalChemist.Auth.Helpers;

namespace PersonalChemist.Droid
{
	[Activity(Label = "CustomUrlSchemeInterceptorActivity", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
	[IntentFilter(
	new[] { Intent.ActionView },
	Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
	DataSchemes = new[] { "com.googleusercontent.apps.60149538697-ijl0r7lhi9l1mjb83bgrdl4j9j6ub8ve" },
	DataPath = "/oauth2redirect")]
	public class CustomUrlSchemeInterceptorActivity :  Activity
    {
		protected override void OnCreate(Bundle savedInstanceState)
		{
			//base.OnCreate(savedInstanceState);

			//// Convert Android.Net.Url to Uri
			//var uri = new Uri(Intent.Data.ToString());

			//// Load redirectUrl page
			//AuthenticationState.Authenticator.OnPageLoading(uri);

			//Finish();



			//auto close the tab after Google Oauth2 login 

			base.OnCreate(savedInstanceState);
			global::Android.Net.Uri uri_android = Intent.Data;

			Uri uri_netfx = new Uri(uri_android.ToString());

			// load redirect_url Page
			AuthenticationState.Authenticator.OnPageLoading(uri_netfx);

			var intent = new Intent(this, typeof(MainActivity));
			intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
			StartActivity(intent);

			this.Finish();

			return;
		}
	}
}
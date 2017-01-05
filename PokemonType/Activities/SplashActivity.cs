
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace PokemonType
{
	[Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
	public class SplashActivity : AppCompatActivity
	{
		protected override void OnResume()
		{
			base.OnResume();

			SendData.SDKNum = (int)Build.VERSION.SdkInt;

			SaveController.Instance.SetContext(this);

			if (SaveController.GetSaveController().GetSavedLanguage() == "")
				SaveController.GetSaveController().SetSavedLanguage("English");
			if (SaveController.GetSaveController().GetSavedLanguage() == "English")
			{
				GetTypeLists.GetEnglishLists(Assets);
				SendData.isJapanese = false;
			}
			else if (SaveController.GetSaveController().GetSavedLanguage() == "Japanese")
			{
				GetTypeLists.GetJapaneseLists(Assets);
				SendData.isJapanese = true;
			}

			if (SaveController.GetSaveController().GetSavedHelp() == "" ||
			   SaveController.GetSaveController().GetSavedHelp() == "Show")
				SendData.showHelp = true;
			else
				SendData.showHelp = false;

			StartActivity(new Intent(Application.Context, typeof(MainActivity)));
		}
	}
}

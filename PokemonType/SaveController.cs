using System;
using Android.Content;

namespace PokemonType
{
	public class SaveController
	{
		private Context context;

		public static SaveController _instance;

		private SaveController() { }

		public static SaveController Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new SaveController();
				}
				return _instance;
			}
		}

		public void SetContext(Context context_in)
		{
			context = context_in;
		}

		public void SetSavedLanguage(string language)
		{
			var prefs = context.GetSharedPreferences("pref", FileCreationMode.Private);
			var editor = prefs.Edit();
			editor.PutString("Language", language);
			editor.Commit();
		}

		public string GetSavedLanguage()
		{
			var prefs = context.GetSharedPreferences("pref", FileCreationMode.Private);
			string token = prefs.GetString("Language", "");
			return token;
		}

		public static SaveController GetSaveController()
		{
			return _instance;
		}
	}

}

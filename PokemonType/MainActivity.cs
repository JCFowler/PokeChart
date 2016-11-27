using Android.App;
using Android.Widget;
using Android.OS;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PokemonType
{
	[Activity(Label = "PokemonType", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Main);
			Button btn1 = FindViewById<Button>(Resource.Id.btn1);
			Button btn2 = FindViewById<Button>(Resource.Id.btn2);
			Button btn3 = FindViewById<Button>(Resource.Id.btn3);
			Button btn4 = FindViewById<Button>(Resource.Id.btn4);
			Button btn5 = FindViewById<Button>(Resource.Id.btn5);
			Button btn6 = FindViewById<Button>(Resource.Id.btn6);
			Button btn7 = FindViewById<Button>(Resource.Id.btn7);
			Button btn8 = FindViewById<Button>(Resource.Id.btn8);
			Button btn9 = FindViewById<Button>(Resource.Id.btn9);
			Button btn10 = FindViewById<Button>(Resource.Id.btn10);
			Button btn11 = FindViewById<Button>(Resource.Id.btn11);
			Button btn12 = FindViewById<Button>(Resource.Id.btn12);
			Button btn13 = FindViewById<Button>(Resource.Id.btn13);
			Button btn14 = FindViewById<Button>(Resource.Id.btn14);
			Button btn15 = FindViewById<Button>(Resource.Id.btn15);
			Button btn16 = FindViewById<Button>(Resource.Id.btn16);
			Button btn17 = FindViewById<Button>(Resource.Id.btn17);
			Button btn18 = FindViewById<Button>(Resource.Id.btn18);

			List<Button> btns = new List<Button>
			{ btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9, btn10,
				btn11, btn12, btn13, btn14, btn15, btn16, btn17, btn18};

			string response;
			StreamReader strm = new StreamReader(Assets.Open("AttackTypeChart.txt"));
			response = strm.ReadToEnd();

			List<Type> types = JsonConvert.DeserializeObject<List<Type>>(response);
			for (int i = 0; i < types.Count;i++) 
			{
				btns[i].Text = types[i].type;
				//btns[i].SetBackgroundColor(Android.Graphics.Color.AliceBlue);
				string weakness = "";
				for (int j = 0; j < types[i].resistance.Count; j++)
				{
					if (j == 0)
						weakness = types[i].resistance[j];
					else
						weakness += ", " + types[i].resistance[j];
				}
				btns[i].Click += (sender, e) =>
				{
					Toast.MakeText(this, weakness, ToastLength.Long).Show();
				};
			}
		}
	}
}


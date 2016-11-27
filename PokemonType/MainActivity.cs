using Android.App;
using Android.Widget;
using Android.OS;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Android.Graphics;

namespace PokemonType
{
	[Activity(Label = "PokemonType", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Main);
			TextView textView1 = FindViewById<TextView>(Resource.Id.textView1);
			TextView textView2 = FindViewById<TextView>(Resource.Id.textView2);
			TextView textView3 = FindViewById<TextView>(Resource.Id.textView3);
			TextView textView4 = FindViewById<TextView>(Resource.Id.textView4);
			TextView textView5 = FindViewById<TextView>(Resource.Id.textView5);
			TextView textView6 = FindViewById<TextView>(Resource.Id.textView6);
			TextView textView7 = FindViewById<TextView>(Resource.Id.textView7);
			TextView textView8 = FindViewById<TextView>(Resource.Id.textView8);
			TextView textView9 = FindViewById<TextView>(Resource.Id.textView9);
			TextView textView10 = FindViewById<TextView>(Resource.Id.textView10);
			TextView textView11 = FindViewById<TextView>(Resource.Id.textView11);
			TextView textView12 = FindViewById<TextView>(Resource.Id.textView12);
			TextView textView13 = FindViewById<TextView>(Resource.Id.textView13);
			TextView textView14 = FindViewById<TextView>(Resource.Id.textView14);
			TextView textView15 = FindViewById<TextView>(Resource.Id.textView15);
			TextView textView16 = FindViewById<TextView>(Resource.Id.textView16);
			TextView textView17 = FindViewById<TextView>(Resource.Id.textView17);
			TextView textView18 = FindViewById<TextView>(Resource.Id.textView18);

			List<TextView> textViews = new List<TextView>
			{ textView1, textView2, textView3, textView4, textView5, textView6, textView7, textView8, textView9, textView10,
				textView11, textView12, textView13, textView14, textView15, textView16, textView17, textView18};

			string response;
			StreamReader strm = new StreamReader(Assets.Open("AttackTypeChart.txt"));
			response = strm.ReadToEnd();

			List<Type> types = JsonConvert.DeserializeObject<List<Type>>(response);
			for (int i = 0; i < types.Count;i++) 
			{
				textViews[i].Tag = i;
				textViews[i].Text = types[i].type;
				textViews[i].Gravity = Android.Views.GravityFlags.Center;
				textViews[i].SetBackgroundColor(Color.ParseColor(Colors.TypeToColor[types[i].type]));
				string weakness = "";
				for (int j = 0; j < types[i].resistance.Count; j++)
				{
					if (j == 0)
						weakness = types[i].resistance[j];
					else
						weakness += ", " + types[i].resistance[j];
				}
				textViews[i].Click += (sender, e) =>
				{
					int num = (int)((TextView)sender).Tag;
					SendData.sendType = types[num];
					StartActivity(typeof(TypeDetailActivity));
				};
			}
		}

		public string getColor(string type)
		{
			string color = "";

			return color;
		}
	}
}


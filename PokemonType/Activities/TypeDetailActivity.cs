
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Android.Graphics;

namespace PokemonType
{
	[Activity(Label = "TypeDetailActivity")]
	public class TypeDetailActivity : Activity
	{
		List<Type> types;
		LinearLayout layout1;
		LinearLayout layout2;
		LinearLayout layout3;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.type_detail_layout);

			layout1 = FindViewById<LinearLayout>(Resource.Id.layout1);
			layout2 = FindViewById<LinearLayout>(Resource.Id.layout2);
			layout3 = FindViewById<LinearLayout>(Resource.Id.layout3);

			List<string> weakness = SendData.sendType[0].effective;
			List<string> resistance = SendData.sendType[0].resistance;
			List<string> immune = SendData.sendType[0].immune;

			if (SendData.sendType.Count > 1)
			{
				weakness.Concat(SendData.sendType[1].effective).ToList();
				resistance.Concat(SendData.sendType[1].effective).ToList();
				immune.Concat(SendData.sendType[1].effective).ToList();
			}
			//TextView weakness = FindViewById<TextView>(Resource.Id.weakness);
			//TextView resistance = FindViewById<TextView>(Resource.Id.resistance);
			//TextView immune = FindViewById<TextView>(Resource.Id.immune);

			for (int i = 0; i < weakness.Count; i++)
			{
				TextView tf = new TextView(this);
				tf.Text = weakness[i];
				tf.Gravity = GravityFlags.Center;
				tf.SetBackgroundColor(Color.ParseColor(Colors.TypeToColor[weakness[i]]));
				layout1.AddView(tf);
			}
			for (int i = 0; i < resistance.Count; i++)
			{
				TextView tf = new TextView(this);
				tf.Text = resistance[i];
				tf.Gravity = GravityFlags.Center;
				tf.SetBackgroundColor(Color.ParseColor(Colors.TypeToColor[resistance[i]]));
				layout2.AddView(tf);;
			}
			for (int i = 0; i < immune.Count; i++)
			{
				TextView tf = new TextView(this);
				tf.Text = immune[i];
				tf.Gravity = GravityFlags.Center;
				tf.SetBackgroundColor(Color.ParseColor(Colors.TypeToColor[immune[i]]));
				layout3.AddView(tf);;
			}
		}

		public string createString(List<string> list)
		{
			var newString = "";
			foreach (var item in list)
			{
				newString += item + " ";
			}
			return newString;
		}
	}
}

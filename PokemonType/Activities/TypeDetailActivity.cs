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

			TextView leftTitle = FindViewById<TextView>(Resource.Id.leftTitle);
			TextView middleTitle = FindViewById<TextView>(Resource.Id.middleTitle);
			TextView rightTitle = FindViewById<TextView>(Resource.Id.rightTitle);

			if (SendData.kind == "Attack")
			{
				leftTitle.Text = "Effective";
				middleTitle.Text = "Weak";
				//rightTitle.Text = "Immune";
			}
			else
			{
				leftTitle.Text = "Weak";
				middleTitle.Text = "Resistance";
				//rightTitle.Text = "Immune"
			}

			TextView topLeft = FindViewById<TextView>(Resource.Id.leftTop);
			TextView topRight = FindViewById<TextView>(Resource.Id.rightTop);

			layout1 = FindViewById<LinearLayout>(Resource.Id.layout1);
			layout2 = FindViewById<LinearLayout>(Resource.Id.layout2);
			layout3 = FindViewById<LinearLayout>(Resource.Id.layout3);

			List<string> weakness = SendData.sendType[0].effective;
			List<string> resistance = SendData.sendType[0].resistance;
			List<string> immune = SendData.sendType[0].immune;

			if (SendData.sendType.Count > 1)
			{
				topLeft.Text = SendData.sendType[0].type;
				topLeft.SetBackgroundColor(Color.ParseColor(Colors.TypeToColor[topLeft.Text]));
				topRight.Text = SendData.sendType[1].type;
				topRight.SetBackgroundColor(Color.ParseColor(Colors.TypeToColor[topRight.Text]));

				weakness.AddRange(SendData.sendType[1].effective);
				resistance.AddRange(SendData.sendType[1].resistance);
				immune.AddRange(SendData.sendType[1].immune);

				weakness.Sort();
				resistance.Sort();
				immune.Sort();

				RemoveDoubles(weakness, resistance, immune);
				RemoveDoubles(resistance, weakness, immune);
			}
			else
			{
				topLeft.Text = SendData.sendType[0].type;
				topLeft.SetBackgroundColor(Color.ParseColor(Colors.TypeToColor[topLeft.Text]));
				topRight.Visibility = ViewStates.Gone;
			}
			//TextView weakness = FindViewById<TextView>(Resource.Id.weakness);
			//TextView resistance = FindViewById<TextView>(Resource.Id.resistance);
			//TextView immune = FindViewById<TextView>(Resource.Id.immune);

			PopulateTF(weakness, layout1, 2);
			PopulateTF(resistance, layout2, .5);
			PopulateTF(immune, layout3, 0);
		}

		void RemoveDoubles(List<string> main, List<string> first, List<string> immuneList)
		{
			for (int i = 0; i < main.Count; i++)
			{
				for (int j = 0; j < first.Count; j++)
				{
					if (main[i] == first[j])
					{
						main.RemoveAt(i);
						first.RemoveAt(j);
					}
				}
			}

			for (int i = 0; i < main.Count; i++)
			{
				for (int j = 0; j < immuneList.Count; j++)
				{
					if (main[i] == immuneList[j])
						main.RemoveAt(i);
				}
			}
		}

		void PopulateTF(List<string> list, LinearLayout layout, double degree)
		{
			List<double> counter = new List<double>();
			for (int i = 0; i < list.Count; i++)
			{
				double num = degree;
				if (i == 0) { }
				else if (i+1 == list.Count) { }
				else if (list[i] == list[i - 1])
				{
					if (degree == .5)
						num = degree / 2;
					else
						num = degree * 2;
					list.RemoveAt(i - 1);
				}
				else if (list[i] == list[i + 1])
				{
					if (degree == .5)
						num = degree / 2;
					else
						num = degree * 2;
					list.RemoveAt(i + 1);
				}

				counter.Add(num);
			}
			
			for (int i = 0; i < list.Count; i++)
			{
				TextView tf = new TextView(this);
				tf.Text = "x" + counter[i].ToString() + " " + list[i];
				tf.Gravity = GravityFlags.Center;
				tf.SetBackgroundColor(Color.ParseColor(Colors.TypeToColor[list[i]]));
				layout.AddView(tf); ;
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

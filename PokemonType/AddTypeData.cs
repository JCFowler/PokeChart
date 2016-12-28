using System;
using System.Collections.Generic;
using Android.Graphics;
using Android.Views;
using Android.Widget;

namespace PokemonType
{
	public class AddTypeData
	{
		public static void RemoveDoubles(List<string> main, List<string> first, List<string> immuneList)
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

			for (int i = main.Count - 1; i > 0; i--)
			{
				for (int j = immuneList.Count - 1; j > 0; j--)
				{
					if (main[i] == immuneList[j])
						main.RemoveAt(i);
				}
			}
		}

		public static void PopulateTF(List<string> list, LinearLayout layout, double degree, Android.Content.Context owner)
		{
			List<double> counter = new List<double>();
			for (int i = 0; i < list.Count; i++)
			{
				double num = degree;
				if (i == 0) { }
				else if (i + 1 == list.Count) { }
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
				TextView tf = new TextView(owner);

				if (counter[i] > 2 || counter[i] < 0.5)
				{
					tf.SetTypeface(null, TypefaceStyle.Bold);
					tf.Text = "**x" + counter[i].ToString() + " " + list[i];
				}
				else
					tf.Text = "x" + counter[i].ToString() + " " + list[i];
				tf.SetTextSize(Android.Util.ComplexUnitType.Dip, 16);
				tf.Gravity = GravityFlags.Center;
				tf.SetBackgroundColor(Color.ParseColor(Colors.TypeToColor[list[i]]));
				layout.AddView(tf); ;
			}
		}
	}
}

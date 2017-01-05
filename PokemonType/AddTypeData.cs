using System;
using System.Collections.Generic;
using System.Linq;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;

namespace PokemonType
{
	public class AddTypeData
	{
		public static void RemoveDoubles(List<string> main, List<string> first, List<string> immuneList)
		{
			Doubles(main, first, immuneList);
			Doubles(first, main, immuneList);
		}

		private static void Doubles(List<string> main, List<string> first, List<string> immuneList)
		{
			var mainSts = new List<string>();
			var firstSts = new List<string>();

			foreach (var m in main)
			{
				foreach (var f in first)
				{
					if (m == f)
					{
						mainSts.Add(m);
						firstSts.Add(f);
					}
				}
			}
			foreach (var s in mainSts)
				main.Remove(s);
			foreach (var s in firstSts)
				first.Remove(s);

			mainSts.Clear();
			foreach (var m in main)
			{
				foreach (var i in immuneList)
				{
					if (m == i)
					{
						mainSts.Add(m);
					}
				}
			}
			foreach (var s in mainSts)
				main.Remove(s);
		}

		public static void PopulateTF(List<string> list, LinearLayout layout, double degree, Android.Content.Context owner)
		{
			List<double> counter = new List<double>();
			for (int i = 0; i < list.Count; i++)
			{
				double num = degree;
				if (i == 0) { }
				else if (list[i] == list[i - 1])
				{
					if (degree == .5)
						num = degree / 2;
					else
						num = degree * 2;
					list.RemoveAt(i - 1);
					counter.RemoveAt(i - 1);
					i--;
				}
				else if (i + 1 == list.Count) { }
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

				tf.SetBackgroundResource(Resource.Drawable.types);

				var gradient = (GradientDrawable)tf.Background;
				gradient.SetColor(Color.ParseColor(Colors.TypeToColor[list[i]]));

				if (SendData.SDKNum < 23)
					tf.SetTextAppearance(owner, Resource.Style.type_text);
				else
					tf.SetTextAppearance(Resource.Style.type_text);

				if (counter[i] > 2 || counter[i] < 0.5)
				{
					tf.SetTypeface(null, TypefaceStyle.Bold);
					tf.Text = "**x" + counter[i].ToString() + " " + list[i];
				}
				else
					tf.Text = "x" + counter[i].ToString() + " " + list[i];
				tf.SetTextSize(Android.Util.ComplexUnitType.Dip, 16);
				tf.Gravity = GravityFlags.Center;

				LinearLayout.LayoutParams llp = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
				llp.SetMargins(3, 0, 3, 5);
				tf.LayoutParameters = llp;

				tf.SetPadding(0, 0, 0, 10);

				tf.Measure(0, 0);
				SendData.typeTextSize = tf.MeasuredHeight;

				layout.AddView(tf); ;
			}
		}
	}
}

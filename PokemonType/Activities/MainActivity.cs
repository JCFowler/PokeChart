using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Graphics;
using Android.Graphics.Drawables;
using System;
using Android.Views;
using System.Linq;

namespace PokemonType
{
	[Activity(Label = "PokemonType", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		DateTime clickedTime;
		TextView clickedView;
		bool leftTurn = true;
		GradientDrawable gradient;
		List<TextView> textViews = new List<TextView>();
		TextView left;
		TextView right;
		List<Type> types = new List<Type>();

		SingleType leftSide = new SingleType();
		SingleType rightSide = new SingleType();

		LinearLayout layout1;
		LinearLayout layout2;
		LinearLayout layout3;

		protected override void OnResume()
		{
			base.OnResume();
			clickedView = null;
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Main);

			layout1 = FindViewById<LinearLayout>(Resource.Id.layout1);
			layout2 = FindViewById<LinearLayout>(Resource.Id.layout2);
			layout3 = FindViewById<LinearLayout>(Resource.Id.layout3);
			left = FindViewById<TextView>(Resource.Id.leftTop);
			right = FindViewById<TextView>(Resource.Id.rightTop);
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
			textViews = new List<TextView>
			{ textView1, textView2, textView3, textView4, textView5, textView6, textView7, textView8, textView9, textView10,
				textView11, textView12, textView13, textView14, textView15, textView16, textView17, textView18};

			GetTypeLists.GetEnglishLists(Assets);
			types = allTypes.defenseTypes;

			for (int i = 0; i < types.Count;i++) 
			{
				textViews[i].Tag = i;
				textViews[i].Text = types[i].type;
				textViews[i].Gravity = GravityFlags.Center;

				gradient = (GradientDrawable)textViews[i].Background;
				gradient.SetColor(Color.ParseColor(Colors.TypeToColor[types[i].type]));

				textViews[i].Click += Handle_Click;
			}
		}

		void Handle_Click(object sender, EventArgs e)
		{
			int num = (int)((TextView)sender).Tag;

			if (!GetTimeSpan(textViews[num], num))
			{
				gradient = (GradientDrawable)textViews[num].Background;

				removeChilren(layout1);
				removeChilren(layout2);
				removeChilren(layout3);

				if (leftSide.num == num)
					removeTop(left, leftSide);
				else if (rightSide.num == num)
					removeTop(right, rightSide);
				else
				{
					gradient.SetColor(Color.Aqua);

					if (left.Text == "")
					{
						left.Text = types[num].type;
						left.SetBackgroundColor(Color.ParseColor(Colors.TypeToColor[left.Text]));
						leftSide.type = types[num];
						leftSide.num = num;
					}
					else if (right.Text == "")
					{
						right.Text = types[num].type;
						right.SetBackgroundColor(Color.ParseColor(Colors.TypeToColor[right.Text]));
						rightSide.type = types[num];
						rightSide.num = num;
					}
					else if (leftTurn)
					{
						left.Text = types[num].type;
						left.SetBackgroundColor(Color.ParseColor(Colors.TypeToColor[left.Text]));
						leftTurn = false;
						gradient = (GradientDrawable)textViews[leftSide.num].Background;
						gradient.SetColor(Color.ParseColor(Colors.TypeToColor[types[leftSide.num].type]));
						leftSide.type = types[num];
						leftSide.num = num;
					}
					else if (!leftTurn)
					{
						right.Text = types[num].type;
						right.SetBackgroundColor(Color.ParseColor(Colors.TypeToColor[right.Text]));
						leftTurn = true;
						gradient = (GradientDrawable)textViews[rightSide.num].Background;
						gradient.SetColor(Color.ParseColor(Colors.TypeToColor[types[rightSide.num].type]));
						rightSide.type = types[num];
						rightSide.num = num;
					}
				}

				List<string> weakness = new List<string>();
				List<string> resistance = new List<string>();
				List<string> immune = new List<string>();

				if (leftSide.type != null && rightSide.type != null)
				{
					weakness = leftSide.type.effective.Concat(rightSide.type.effective).ToList();
					resistance = leftSide.type.resistance.Concat(rightSide.type.resistance).ToList();
					immune = leftSide.type.immune.Concat(rightSide.type.immune).ToList();

					weakness.Sort();
					resistance.Sort();
					immune.Sort();

					AddTypeData.RemoveDoubles(weakness, resistance, immune);
					AddTypeData.RemoveDoubles(resistance, weakness, immune);
				}
				else if (leftSide.type != null && rightSide.type == null)
				{
					weakness = leftSide.type.effective;
					resistance = leftSide.type.resistance;
					immune = leftSide.type.immune;
				}
				else if (rightSide.type != null && leftSide.type == null)
				{
					weakness = rightSide.type.effective;
					resistance = rightSide.type.resistance;
					immune = rightSide.type.immune;
				}

				AddTypeData.PopulateTF(weakness, layout1, 2, this);
				AddTypeData.PopulateTF(resistance, layout2, .5, this);
				AddTypeData.PopulateTF(immune, layout3, 0, this);
			}
		}

		public void removeChilren(LinearLayout layout)
		{
			if (layout.ChildCount > 0)
				for (int i = layout.ChildCount - 1; i > 0; i--)
					layout.RemoveViewAt(i);
		}

		public void removeTop(TextView top, SingleType side)
		{
			gradient.SetColor(Color.ParseColor(Colors.TypeToColor[top.Text]));
			top.Text = "";
			top.SetBackgroundColor(Color.Transparent);

			side.type = null;
			side.num = -1;
		}

		public bool GetTimeSpan(TextView TF, int num)
		{
			if (clickedView != null)
			{
				if (clickedView == TF)
				{
					TimeSpan timespan = DateTime.Now - clickedTime;
					if (timespan.Milliseconds < 200)
					{
						var sendTypes = allTypes.attackTypes;

						if (SendData.sendAttackType.Count > 0)
							SendData.sendAttackType[0] = sendTypes[num];
						else
							SendData.sendAttackType.Add(sendTypes[num]);
						
						StartActivity(typeof(TypeDetailActivity));
						return true;
					}
				}
			}
			clickedView = TF;
			clickedTime = DateTime.Now;
			return false;
		}
	}
}


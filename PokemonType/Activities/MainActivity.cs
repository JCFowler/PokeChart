using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Graphics;
using Android.Graphics.Drawables;
using System;

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

		LinearLayout layout1;
		LinearLayout layout2;
		LinearLayout layout3;

		protected override void OnResume()
		{
			base.OnResume();

			if (SendData.sendType != null)
				SendData.sendType.Clear();
		}

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

			textViews = new List<TextView>
			{ textView1, textView2, textView3, textView4, textView5, textView6, textView7, textView8, textView9, textView10,
				textView11, textView12, textView13, textView14, textView15, textView16, textView17, textView18};

			layout1 = FindViewById<LinearLayout>(Resource.Id.layout1);
			layout2 = FindViewById<LinearLayout>(Resource.Id.layout2);
			layout3 = FindViewById<LinearLayout>(Resource.Id.layout3);

			left = FindViewById<TextView>(Resource.Id.leftTop);
			right = FindViewById<TextView>(Resource.Id.rightTop);

			GetTypeLists.GetEnglishLists(Assets);

			var types = allTypes.defenseTypes;
			for (int i = 0; i < types.Count;i++) 
			{
				textViews[i].Tag = i;
				textViews[i].Text = types[i].type;
				textViews[i].Gravity = Android.Views.GravityFlags.Center;

				gradient = (GradientDrawable)textViews[i].Background;
				gradient.SetColor(Color.ParseColor(Colors.TypeToColor[types[i].type]));

				textViews[i].Click += Handle_Click;
			}
			//goBtn.Click += (sender, e) =>
			//{
			//	if (SendData.sendType != null && SendData.typeNum.Count > 0)
			//	{
			//		SendData.kind = "Defense";
			//		var sendTypes = allTypes.defenseTypes;
			//		for (int i = 0; i < SendData.typeNum.Count; i++)
			//		{
			//			SendData.sendType.Add(sendTypes[SendData.typeNum[i]]);
			//		}
			//		StartActivity(typeof(test));
			//	}
			//	else
			//		Toast.MakeText(this, "Nothing selected", ToastLength.Short).Show();
			//};
		}

		void Handle_Click(object sender, EventArgs e)
		{
			var types = allTypes.defenseTypes;
			int num = (int)((TextView)sender).Tag;

			GetTimeSpan(textViews[num], num);

			gradient = (GradientDrawable)textViews[num].Background;

			int delete = -1;
			for (int j = 0; j < SendData.typeNum.Count; j++)
			{
				if (SendData.typeNum[j] == num)
					delete = j;
			}

			if (delete != -1)
			{
				if (delete == 1)
					removeTop(right, 1);
				else if (delete == 0 && SendData.typeNum.Count == 2)
					removeTop(left, 0);

				else if (delete == 0 && SendData.typeNum.Count == 1 && right.Text != "")
					removeTop(right, 0);
				else
					removeTop(left, 0);
			}
			else
			{
				gradient.SetColor(Color.Aqua);

				if (left.Text == "")
				{
					left.Text = types[num].type;
					left.SetBackgroundColor(Color.ParseColor(Colors.TypeToColor[left.Text]));
					//SendData.sendType.Add(types[num]);
					SendData.typeNum.Add(num);
				}
				else if (right.Text == "")
				{
					right.Text = types[num].type;
					right.SetBackgroundColor(Color.ParseColor(Colors.TypeToColor[right.Text]));
					//SendData.sendType.Add(types[num]);
					SendData.typeNum.Add(num);
				}
				else if (leftTurn)
				{
					left.Text = types[num].type;
					left.SetBackgroundColor(Color.ParseColor(Colors.TypeToColor[left.Text]));
					leftTurn = false;
					gradient = (GradientDrawable)textViews[SendData.typeNum[0]].Background;
					gradient.SetColor(Color.ParseColor(Colors.TypeToColor[types[SendData.typeNum[0]].type]));
					SendData.typeNum[0] = num;
					//SendData.sendType[0] = types[num];
				}
				else if (!leftTurn)
				{
					right.Text = types[num].type;
					right.SetBackgroundColor(Color.ParseColor(Colors.TypeToColor[right.Text]));
					leftTurn = true;
					gradient = (GradientDrawable)textViews[SendData.typeNum[1]].Background;
					gradient.SetColor(Color.ParseColor(Colors.TypeToColor[types[SendData.typeNum[1]].type]));
					SendData.typeNum[1] = num;
					//SendData.sendType[1] = types[num];
				}
			}
		}

		public void removeTop(TextView top, int space)
		{
			gradient.SetColor(Color.ParseColor(Colors.TypeToColor[top.Text]));
			top.Text = "";
			top.SetBackgroundColor(Color.Transparent);

			SendData.typeNum.RemoveAt(space);
			//SendData.sendType.RemoveAt(space);
		}

		public bool GetTimeSpan(TextView TF, int num)
		{
			if (clickedView != null)
			{
				if (clickedView == TF)
				{
					TimeSpan timespan = DateTime.Now - clickedTime;
					if (timespan.Milliseconds < 300)
					{
						var sendTypes = allTypes.attackTypes;

						SendData.kind = "Attack";
						SendData.sendType.Add(sendTypes[num]);
						StartActivity(typeof(TypeDetailActivity));
					}
				}
			}

			clickedView = TF;
			clickedTime = DateTime.Now;
			return false;
		}
	}
}


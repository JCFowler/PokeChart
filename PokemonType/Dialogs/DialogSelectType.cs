using System;
using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace PokemonType
{
	public class DialogSelectType : DialogFragment
	{
		List<TextView> textViews = new List<TextView>();
		List<Type> types = new List<Type>();
		GradientDrawable gradient;
		bool leftTurn = true;

		Android.Content.Context owner;
		public DialogSelectType(Android.Content.Context owner)
		{
			this.owner = owner;
			SendData.showType1.type = null;
			SendData.showType2.type = null;
			SendData.showType1.num = -1;
			SendData.showType2.num = -1;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
			base.OnCreateView(inflater, container, savedInstanceState);

			var view = inflater.Inflate(Resource.Layout.select_type_layout, container, false);

			TextView textView1 = view.FindViewById<TextView>(Resource.Id.textView1);
			TextView textView2 = view.FindViewById<TextView>(Resource.Id.textView2);
			TextView textView3 = view.FindViewById<TextView>(Resource.Id.textView3);
			TextView textView4 = view.FindViewById<TextView>(Resource.Id.textView4);
			TextView textView5 = view.FindViewById<TextView>(Resource.Id.textView5);
			TextView textView6 = view.FindViewById<TextView>(Resource.Id.textView6);
			TextView textView7 = view.FindViewById<TextView>(Resource.Id.textView7);
			TextView textView8 = view.FindViewById<TextView>(Resource.Id.textView8);
			TextView textView9 = view.FindViewById<TextView>(Resource.Id.textView9);
			TextView textView10 = view.FindViewById<TextView>(Resource.Id.textView10);
			TextView textView11 = view.FindViewById<TextView>(Resource.Id.textView11);
			TextView textView12 = view.FindViewById<TextView>(Resource.Id.textView12);
			TextView textView13 = view.FindViewById<TextView>(Resource.Id.textView13);
			TextView textView14 = view.FindViewById<TextView>(Resource.Id.textView14);
			TextView textView15 = view.FindViewById<TextView>(Resource.Id.textView15);
			TextView textView16 = view.FindViewById<TextView>(Resource.Id.textView16);
			TextView textView17 = view.FindViewById<TextView>(Resource.Id.textView17);
			TextView textView18 = view.FindViewById<TextView>(Resource.Id.textView18);
			TextView textView19 = view.FindViewById<TextView>(Resource.Id.textView19);
			TextView textView20 = view.FindViewById<TextView>(Resource.Id.textView20);
			textViews = new List<TextView>
			{ textView1, textView2, textView3, textView4, textView5, textView6, textView7, textView8, textView9, textView10,
				textView11, textView12, textView13, textView14, textView15, textView16, textView17, textView18, textView19, textView20};

			types = allTypes.defenseTypes;

			for (int i = 0; i < textViews.Count; i++)
			{
				textViews[i].Tag = i;
				//textViews[i].Gravity = GravityFlags.Center;

				int h = Resources.DisplayMetrics.HeightPixels / 100;
				if (h > 18)
					h = 18;

				textViews[i].SetTextSize(Android.Util.ComplexUnitType.Dip, h + 11);

				gradient = (GradientDrawable)textViews[i].Background;

				if (i == 18)
				{
					textViews[i].Text = "Cancel";
					textViews[i].SetPadding(0, 30, 0, 0);
					gradient.SetColor(Color.IndianRed);

					textViews[i].Click += Cancel_Click;
				}
				else if (i == 19)
				{
					textViews[i].Text = "Accept";
					textViews[i].SetPadding(0, 30, 0, 0);
					gradient.SetColor(Color.LimeGreen);

					textViews[i].Click += Finish_Click;
				}
				else
				{
					textViews[i].Text = types[i].type;
					textViews[i].SetPadding(10, 0, 0, 0);
					gradient.SetColor(Color.ParseColor(Colors.TypeToColor[types[i].type]));

					textViews[i].Click += Type_Click;
				}
			}

			if (SendData.isJapanese)
			{
				textViews[18].Text = "(Japanese)";
				textViews[19].Text = "(Yes)Japanese";
			}
			return view;
		}

		public override void OnResume()
		{
			base.OnResume();

			textViews[0].Measure(0, 0);

			int h = (textViews[0].MeasuredHeight + 40) * 7;

			int w = Resources.DisplayMetrics.WidthPixels;

			Dialog.Window.SetLayout(w - 50, 1000);
		}

		void Type_Click(object sender, EventArgs e)
		{
			int num = (int)((TextView)sender).Tag;

			gradient = (GradientDrawable)textViews[num].Background;

			if (SendData.showType1.num == num)
			{
				gradient.SetColor(Color.ParseColor(Colors.TypeToColor[types[SendData.showType1.num].type]));
				SendData.showType1.type = null;
				SendData.showType1.num = -1;
			}
			else if (SendData.showType2.num == num)
			{
				gradient.SetColor(Color.ParseColor(Colors.TypeToColor[types[SendData.showType2.num].type]));
				SendData.showType2.type = null;
				SendData.showType2.num = -1;
			}
			else
			{
				gradient.SetColor(Color.Aqua);

				if (SendData.showType1.type == null)
				{
					SendData.showType1.type = types[num];
					SendData.showType1.num = num;
				}
				else if (SendData.showType2.type == null)
				{
					SendData.showType2.type = types[num];
					SendData.showType2.num = num;
				}
				else if (leftTurn)
				{
					leftTurn = false;
					gradient = (GradientDrawable)textViews[SendData.showType1.num].Background;
					gradient.SetColor(Color.ParseColor(Colors.TypeToColor[types[SendData.showType1.num].type]));
					SendData.showType1.type = types[num];
					SendData.showType1.num = num;
				}
				else if (!leftTurn)
				{
					leftTurn = true;
					gradient = (GradientDrawable)textViews[SendData.showType2.num].Background;
					gradient.SetColor(Color.ParseColor(Colors.TypeToColor[types[SendData.showType2.num].type]));
					SendData.showType2.type = types[num];
					SendData.showType2.num = num;
				}
			}
		}

		void Cancel_Click(object sender, EventArgs e)
		{
			SendData.showType1.type = null;
			SendData.showType2.type = null;
			SendData.showType1.num = -1;
			SendData.showType2.num = -1;
			Dismiss();
		}

		void Finish_Click(object sender, EventArgs e)
		{
			Dismiss();
		}

		public override void OnDismiss(Android.Content.IDialogInterface dialog)
		{
			base.OnDismiss(dialog);

			if (SendData.showType1.type != null || SendData.showType2.type != null)
			{
				Toast.MakeText(owner, SendData.showType1.type.type, ToastLength.Short).Show();
				Toast.MakeText(owner, SendData.showType2.type.type, ToastLength.Short).Show();
			}
		}
	}
}

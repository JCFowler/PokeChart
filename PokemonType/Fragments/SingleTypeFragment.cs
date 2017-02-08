
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace PokemonType
{
	public class SingleTypeFragment : Fragment
	{
		bool leftTurn = true;
		GradientDrawable gradient;
		List<TextView> textViews = new List<TextView>();
		TextView left;
		TextView right;
		List<Type> types = new List<Type>();

		SingleType leftSide = new SingleType();
		SingleType rightSide = new SingleType();

		LinearLayout mainLayout;
		LinearLayout layout1;
		LinearLayout layout2;
		LinearLayout layout3;
		List<LinearLayout> layouts = new List<LinearLayout>();
		StartActivity owner;

		public SingleTypeFragment(StartActivity owner)
		{
			this.owner = owner;
		}

		public override void OnResume()
		{
			base.OnResume();

			leftSide.num = -1;
			rightSide.num = -1;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.single_type_layout, container, false);

			mainLayout = view.FindViewById<LinearLayout>(Resource.Id.mainLayout);
			layout1 = view.FindViewById<LinearLayout>(Resource.Id.layout1);
			layout2 = view.FindViewById<LinearLayout>(Resource.Id.layout2);
			layout3 = view.FindViewById<LinearLayout>(Resource.Id.layout3);
			layouts = new List<LinearLayout> { layout1, layout2, layout3, mainLayout };
			left = view.FindViewById<TextView>(Resource.Id.leftTop);
			right = view.FindViewById<TextView>(Resource.Id.rightTop);
			TextView leftTitle = view.FindViewById<TextView>(Resource.Id.leftTitle);
			TextView middleTitle = view.FindViewById<TextView>(Resource.Id.middleTitle);
			TextView rightTitle = view.FindViewById<TextView>(Resource.Id.rightTitle);
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
			textViews = new List<TextView>
			{ textView1, textView2, textView3, textView4, textView5, textView6, textView7, textView8, textView9, textView10,
				textView11, textView12, textView13, textView14, textView15, textView16, textView17, textView18};

			if (SendData.isJapanese)
			{
				leftTitle.Text = Convert.LanguageDic[leftTitle.Text];
				middleTitle.Text = Convert.LanguageDic[middleTitle.Text];
				rightTitle.Text = Convert.LanguageDic[rightTitle.Text];
				owner.Title = "防衛";
			}
			else
				owner.Title = "Defense";

			types = allTypes.defenseTypes;

			for (int i = 0; i < types.Count; i++)
			{
				textViews[i].Tag = i;
				textViews[i].Text = types[i].type;
				textViews[i].Gravity = GravityFlags.Center;
				textViews[i].SetPadding(0, 0, 0, 10);

				int h = Resources.DisplayMetrics.HeightPixels / 100;
				if (h > 18)
					h = 18;

				textViews[i].SetTextSize(Android.Util.ComplexUnitType.Dip, h + 11);

				gradient = (GradientDrawable)textViews[i].Background;
				gradient.SetColor(Color.ParseColor(Colors.TypeToColor[types[i].type]));

				textViews[i].Click += Handle_Click;
				textViews[i].LongClick += Handle_LongClick;
			}

			var adLayout = view.FindViewById<LinearLayout>(Resource.Id.googleAds);

			//if (SendData.isConnected)
			//{
			//	var ad = new AdView(this);
			//	ad.AdSize = AdSize.SmartBanner;
			//	ad.AdUnitId = "ca-app-pub-2288808768882490/2788554960";
			//	var requestBuilder = new AdRequest.Builder();
			//	ad.LoadAd(requestBuilder.Build());
			//	adLayout.AddView(ad);
			//}
			//else
			mainLayout.RemoveView(adLayout);

			return view;
		}

		void Handle_Click(object sender, EventArgs e)
		{
			int num = (int)((TextView)sender).Tag;

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

			AddTypeData.PopulateTF(weakness, layout1, 2, owner);
			AddTypeData.PopulateTF(resistance, layout2, .5, owner);
			AddTypeData.PopulateTF(immune, layout3, 0, owner);
		}

		void Handle_LongClick(object sender, View.LongClickEventArgs e)
		{
			int num = (int)((TextView)sender).Tag;

			var sendTypes = allTypes.attackTypes;

			if (SendData.sendAttackType.Count > 0)
				SendData.sendAttackType[0] = sendTypes[num];
			else
				SendData.sendAttackType.Add(sendTypes[num]);

			owner.LongClick();
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
	}
}

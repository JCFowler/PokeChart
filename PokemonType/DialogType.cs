using System;
using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace PokemonType
{
	public class DialogType : DialogFragment
	{
		LinearLayout layout1;
		LinearLayout layout2;
		LinearLayout layout3;
		List<LinearLayout> layouts = new List<LinearLayout>();

		TextView leftTitle;
		TextView middleTitle;
		TextView rightTitle;
		TextView attackTitle;

		TextView topLeft;

		Android.Content.Context owner;
		public DialogType(Android.Content.Context owner)
		{
			this.owner = owner;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
			base.OnCreateView(inflater, container, savedInstanceState);

			var view = inflater.Inflate(Resource.Layout.dialog_layout, container, false);

			leftTitle = view.FindViewById<TextView>(Resource.Id.leftTitle);
			middleTitle = view.FindViewById<TextView>(Resource.Id.middleTitle);
			rightTitle = view.FindViewById<TextView>(Resource.Id.rightTitle);

			topLeft = view.FindViewById<TextView>(Resource.Id.leftTop);
			attackTitle = view.FindViewById<TextView>(Resource.Id.attackText);

			layout1 = view.FindViewById<LinearLayout>(Resource.Id.layout1);
			layout2 = view.FindViewById<LinearLayout>(Resource.Id.layout2);
			layout3 = view.FindViewById<LinearLayout>(Resource.Id.layout3);
			layouts = new List<LinearLayout> { layout1, layout2, layout3 };

			List<string> weakness = SendData.sendAttackType[0].effective;
			List<string> resistance = SendData.sendAttackType[0].resistance;
			List<string> immune = SendData.sendAttackType[0].immune;

			topLeft.Text = SendData.sendAttackType[0].type;
			topLeft.SetBackgroundColor(Color.ParseColor(Colors.TypeToColor[topLeft.Text]));

			AddTypeData.PopulateTF(weakness, layout1, 2, owner);
			AddTypeData.PopulateTF(resistance, layout2, .5, owner);
			AddTypeData.PopulateTF(immune, layout3, 0, owner);

			return view;
		}

		public override void OnResume()
		{
			base.OnResume();

			if (SendData.isJapanese)
			{
				attackTitle.Text = "攻撃";
				leftTitle.Text = "強い";
				middleTitle.Text = "弱い";
				rightTitle.Text = "免疫";
			}
			else
			{
				attackTitle.Text = "Attack";
				leftTitle.Text = "Effective";
				middleTitle.Text = "Weak";
				rightTitle.Text = "Immune";
			}

			attackTitle.Measure(0, 0);
			leftTitle.Measure(0, 0);

			int px = (attackTitle.MeasuredHeight + leftTitle.MeasuredHeight) * 2;

			int w = Resources.DisplayMetrics.WidthPixels;
			int h = ((SendData.sendAttackType[0].resistance.Count) * SendData.typeTextSize);
			if(SendData.sendAttackType[0].effective.Count > SendData.sendAttackType[0].resistance.Count)
				h = ((SendData.sendAttackType[0].effective.Count) * SendData.typeTextSize);

			Dialog.Window.SetLayout(w - 50, px + h + 50);
		}
	}
}

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
	public class DialogHelp : DialogFragment
	{
		CheckBox check;
		Button btnOk;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
			base.OnCreateView(inflater, container, savedInstanceState);

			var view = inflater.Inflate(Resource.Layout.help_layout, container, false);

			btnOk = view.FindViewById<Button>(Resource.Id.btnOk);
			check = view.FindViewById<CheckBox>(Resource.Id.checkBox1);

			TextView textDouble = view.FindViewById<TextView>(Resource.Id.text_double);
			TextView textLanguage = view.FindViewById<TextView>(Resource.Id.text_language);
			TextView textHelp = view.FindViewById<TextView>(Resource.Id.text_help);

			if (!SendData.showHelp)
				check.Checked = true;

			if (SendData.isJapanese)
			{
				textDouble.Text = "攻撃タイプをみたい場合は、長いプレスしてください。";
				textLanguage.Text = "言語変更（英語、日本語）";
				textHelp.Text = "このニューを表示する";
				check.Text = "開始時には表示しない。";
			}

			btnOk.Click += (sender, e) =>
			{
				Dismiss();
			};

			return view;
		}

		public override void OnResume()
		{
			base.OnResume();

			btnOk.Measure(0, 0);

			int h = btnOk.MeasuredHeight * 8;

			int w = Resources.DisplayMetrics.WidthPixels;

			Dialog.Window.SetLayout(w - 50, h);
		}

		public override void OnDismiss(Android.Content.IDialogInterface dialog)
		{
			base.OnDismiss(dialog);

			if (check.Checked)
			{
				SaveController.GetSaveController().SetSavedHelp("Stop");
				SendData.showHelp = false;
			}
			else
			{
				SaveController.GetSaveController().SetSavedHelp("Show");
				SendData.showHelp = true;
			}
		}
	}
}

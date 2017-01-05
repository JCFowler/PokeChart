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

			if (!SendData.showHelp)
				check.Checked = true;

			btnOk.Click += BtnOk_Click;

			return view;
		}

		public override void OnResume()
		{
			base.OnResume();

			btnOk.Measure(0, 0);

			int h = btnOk.MeasuredHeight * 8;

			int w = Resources.DisplayMetrics.WidthPixels;
			//int h = Resources.DisplayMetrics.HeightPixels;

			Dialog.Window.SetLayout(w - 50, h);
		}

		void BtnOk_Click(object sender, EventArgs e)
		{
			if (check.Checked)
			{
				SaveController.GetSaveController().SetSavedHelp("Stop");
				SendData.showHelp = false;
			}
			else
				SaveController.GetSaveController().SetSavedHelp("Show");
			
			Dismiss();
		}
	}
}

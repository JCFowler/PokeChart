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
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;

namespace PokemonType
{
	[Activity(Label = "TypeDetailActivity", Theme = "@style/MyTheme")]
	public class TypeDetailActivity : AppCompatActivity
	{
		LinearLayout layout1;
		LinearLayout layout2;
		LinearLayout layout3;
		List<LinearLayout> layouts = new List<LinearLayout>();

		TextView leftTitle;
		TextView middleTitle;
		TextView rightTitle;

		TextView topLeft;

		protected override void OnResume()
		{
			base.OnResume();

			if (SendData.isJapanese)
			{
				SupportActionBar.Title = "攻撃";
				leftTitle.Text = "強い";
				middleTitle.Text = "弱い";
				rightTitle.Text = "免疫";
			}
			else
			{
				SupportActionBar.Title = "Attack";
				leftTitle.Text = "Effective";
				middleTitle.Text = "Weak";
				rightTitle.Text = "Immune";
			}
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.type_detail_layout);

			var toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
			SetSupportActionBar(toolbar);

			leftTitle = FindViewById<TextView>(Resource.Id.leftTitle);
			middleTitle = FindViewById<TextView>(Resource.Id.middleTitle);
			rightTitle = FindViewById<TextView>(Resource.Id.rightTitle);

			topLeft = FindViewById<TextView>(Resource.Id.leftTop);

			layout1 = FindViewById<LinearLayout>(Resource.Id.layout1);
			layout2 = FindViewById<LinearLayout>(Resource.Id.layout2);
			layout3 = FindViewById<LinearLayout>(Resource.Id.layout3);
			layouts = new List<LinearLayout>{ layout1, layout2, layout3};

			List<string> weakness = SendData.sendAttackType[0].effective;
			List<string> resistance = SendData.sendAttackType[0].resistance;
			List<string> immune = SendData.sendAttackType[0].immune;

			topLeft.Text = SendData.sendAttackType[0].type;
			topLeft.SetBackgroundColor(Color.ParseColor(Colors.TypeToColor[topLeft.Text]));

			AddTypeData.PopulateTF(weakness, layout1, 2, this);
			AddTypeData.PopulateTF(resistance, layout2, .5, this);
			AddTypeData.PopulateTF(immune, layout3, 0, this);
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.action_toolbar, menu);
			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			getData();

			return base.OnOptionsItemSelected(item);
		}

		public void getData()
		{
			SendData.isJapanese = !SendData.isJapanese;

			if (SendData.isJapanese)
			{
				GetTypeLists.GetJapaneseLists(Assets);

				SupportActionBar.Title = "攻撃";
				SaveController.GetSaveController().SetSavedLanguage("Japanese");
				topLeft.Text = Convert.LanguageDic[topLeft.Text];
				Convert.ConvertTextViews(layouts);
			}
			else
			{
				GetTypeLists.GetEnglishLists(Assets);

				SupportActionBar.Title = "Attack";
				SaveController.GetSaveController().SetSavedLanguage("English");
				topLeft.Text = Convert.LanguageDic[topLeft.Text];
				Convert.ConvertTextViews(layouts);
			}
		}
	}
}

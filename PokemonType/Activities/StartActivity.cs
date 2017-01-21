
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using SupportActionBar = Android.Support.V7.App.ActionBar;

namespace PokemonType
{
	[Activity(Theme = "@style/MyTheme", ScreenOrientation = ScreenOrientation.Portrait)]
	public class StartActivity : AppCompatActivity
	{
		DrawerLayout mDrawerLayout;
		ArrayAdapter mLeftAdapter;
		List<String> mDataSet;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.main_layout);

			mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
			var navView = FindViewById<NavigationView>(Resource.Id.nav_view);
			SupportToolbar toolbar = this.FindViewById<SupportToolbar>(Resource.Id.toolbar);

			var trans = SupportFragmentManager.BeginTransaction();
			trans.Add(Resource.Id.fragmentContainer, new SingleTypeFragment(this));
			trans.Commit();

			if (navView != null)
			{
				navView.NavigationItemSelected += (sender, e) =>
				{
					e.MenuItem.SetChecked(true);
					mDrawerLayout.CloseDrawers();
				};
			}

			if (SendData.showHelp)
			{
				FragmentTransaction transaction = FragmentManager.BeginTransaction();
				DialogHelp dialog = new DialogHelp();
				dialog.Show(transaction, "Help");
			}


			SetSupportActionBar(toolbar);
			SupportActionBar ab = SupportActionBar;
			//SupportActionBar.SetHomeButtonEnabled(true);
			ab.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
			ab.SetDisplayHomeAsUpEnabled(true);
			//SupportActionBar.SetHomeButtonEnabled(true);
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.action_toolbar, menu);
			return base.OnCreateOptionsMenu(menu);
		}
		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
				case Android.Resource.Id.Home:
					mDrawerLayout.OpenDrawer((int)GravityFlags.Left);
					break;
				case Resource.Id.action_language:
					SendData.isJapanese = !SendData.isJapanese;

					if (SendData.isJapanese)
					{
						GetTypeLists.GetJapaneseLists(Assets);
						SupportActionBar.Title = "防衛";
						SaveController.GetSaveController().SetSavedLanguage("Japanese");
					}
					else
					{
						GetTypeLists.GetEnglishLists(Assets);
						SupportActionBar.Title = "Defense";
						SaveController.GetSaveController().SetSavedLanguage("English");
					}

					//types = allTypes.defenseTypes;
					//Convert.ConvertInsideTypes(leftSide, rightSide);
					//Convert.ConvertTextViews(layouts);
					break;
				case Resource.Id.action_help:
					FragmentTransaction transaction = FragmentManager.BeginTransaction();
					DialogHelp dialog = new DialogHelp();
					dialog.Show(transaction, "Help");
					break;
			}

			return base.OnOptionsItemSelected(item);
		}

		public void LongClick()
		{
			FragmentTransaction transaction = FragmentManager.BeginTransaction();
			DialogType dialog = new DialogType(this);
			dialog.Show(transaction, "Attack Type");
		}
	}
}

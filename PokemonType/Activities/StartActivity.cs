
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;

namespace PokemonType
{
	[Activity(Theme = "@style/MyTheme", ScreenOrientation = ScreenOrientation.Portrait)]
	public class StartActivity : AppCompatActivity
	{
		MyActionBarDrawerToggle mDrawerToggle;
		ArrayAdapter mLeftAdapter;
		List<String> mDataSet;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.main_layout);

			var toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
			SetSupportActionBar(toolbar);

			var mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
			var mDrawer = FindViewById<ListView>(Resource.Id.drawer_list);

			var trans = SupportFragmentManager.BeginTransaction();
			trans.Add(Resource.Id.fragmentContainer, new SingleTypeFragment(this));
			trans.Commit();

			mDrawerToggle = new MyActionBarDrawerToggle(this, mDrawerLayout, 0, 1);

			mDataSet = new List<string> { "Single Type", "Team Type"};
			mLeftAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, mDataSet);
			mDrawer.Adapter = mLeftAdapter;

			if (SendData.showHelp)
			{
				FragmentTransaction transaction = FragmentManager.BeginTransaction();
				DialogHelp dialog = new DialogHelp();
				dialog.Show(transaction, "Help");
			}

			mDrawerLayout.AddDrawerListener(mDrawerToggle);
			mDrawerToggle.SyncState();		

			SupportActionBar.SetHomeButtonEnabled(true);
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
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
					mDrawerToggle.OnOptionsItemSelected(item);
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

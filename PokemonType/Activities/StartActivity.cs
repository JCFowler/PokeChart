
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
		NavigationView navView;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.main_layout);

			mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
			navView = FindViewById<NavigationView>(Resource.Id.nav_view);
			var toolbar = this.FindViewById<SupportToolbar>(Resource.Id.toolbar);

			var trans = SupportFragmentManager.BeginTransaction();
			trans.Add(Resource.Id.fragmentContainer, new SingleTypeFragment(this));
			trans.Commit();

			if (navView != null)
				navView.NavigationItemSelected += NavView_NavigationItemSelected;
			navView.Menu.GetItem(0).SetChecked(true);

			if (SendData.showHelp)
			{
				FragmentTransaction transaction = FragmentManager.BeginTransaction();
				DialogHelp dialog = new DialogHelp();
				dialog.Show(transaction, "Help");
			}

			SetSupportActionBar(toolbar);
			SupportActionBar ab = SupportActionBar;
			ab.SetHomeAsUpIndicator(Resource.Mipmap.ic_menu);
			ab.SetDisplayHomeAsUpEnabled(true);

			if (SendData.isJapanese)
			{
				for (int i = 0; i < navView.Menu.Size(); i++)
				{
					navView.Menu.GetItem(i).SetTitle(Convert.LanguageDic[navView.Menu.GetItem(i).ToString()]);
					if (i < 2)
					{
						navView.Menu.GetItem(2).SubMenu.GetItem(i).SetTitle(
							Convert.LanguageDic[navView.Menu.GetItem(2).SubMenu.GetItem(i).ToString()]);
					}
				}
			}
		}

		void NavView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
		{
			var trans = SupportFragmentManager.BeginTransaction();
			switch (e.MenuItem.ItemId)
			{
				case Resource.Id.nav_single:
					e.MenuItem.SetChecked(true);
					trans.Replace(Resource.Id.fragmentContainer, new SingleTypeFragment(this));
					trans.Commit();
					break;
				case Resource.Id.nav_team:
					e.MenuItem.SetChecked(true);
					trans.Replace(Resource.Id.fragmentContainer, new TeamTypeFragment(this));
					trans.Commit();
					break;
				case Resource.Id.nav_language:
					ChangeLanguage();
					break;
				case Resource.Id.nav_help:
					FragmentTransaction transaction = FragmentManager.BeginTransaction();
					DialogHelp dialog = new DialogHelp();
					dialog.Show(transaction, "Help");
					break;
			}
			mDrawerLayout.CloseDrawers();
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			if(SendData.isJapanese)
				MenuInflater.Inflate(Resource.Menu.japanese_action_toolbar, menu);
			else
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
					ChangeLanguage();

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

		public void ChangeLanguage()
		{
			SendData.isJapanese = !SendData.isJapanese;
			InvalidateOptionsMenu();

			for (int i = 0; i < navView.Menu.Size(); i++)
			{
				navView.Menu.GetItem(i).SetTitle(Convert.LanguageDic[navView.Menu.GetItem(i).ToString()]);
				if (i < 2)
				{
					navView.Menu.GetItem(2).SubMenu.GetItem(i).SetTitle(
						Convert.LanguageDic[navView.Menu.GetItem(2).SubMenu.GetItem(i).ToString()]);
				}
			}

			if (SendData.isJapanese)
			{
				GetTypeLists.GetJapaneseLists(Assets);
				SaveController.GetSaveController().SetSavedLanguage("Japanese");
			}
			else
			{
				GetTypeLists.GetEnglishLists(Assets);
				SaveController.GetSaveController().SetSavedLanguage("English");
			}

			var frag = SupportFragmentManager.FindFragmentById(Resource.Id.fragmentContainer);
			var trans = SupportFragmentManager.BeginTransaction();
			trans.Detach(frag).Attach(frag).Commit();
		}

		public void LongClick()
		{
			FragmentTransaction transaction = FragmentManager.BeginTransaction();
			DialogType dialog = new DialogType(this);
			dialog.Show(transaction, "Attack Type");
		}

		public void ShowSelectType()
		{
			FragmentTransaction transaction = FragmentManager.BeginTransaction();
			DialogSelectType dialog = new DialogSelectType(this);
			dialog.Show(transaction, "Select Type");
		}
	}
}

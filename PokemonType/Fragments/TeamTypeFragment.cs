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
	public class TeamTypeFragment : Fragment
	{
		StartActivity owner;
		public TeamTypeFragment(StartActivity owner)
		{
			this.owner = owner;
			if (SendData.isJapanese)
			{
				//Convert.ConvertTextViews(layouts);

				owner.Title = "チーム";
			}
			else
				owner.Title = "Team";
		}

		public override void OnResume()
		{
			base.OnResume();
			if (SendData.isJapanese)
				owner.Title = "チーム";
			else
				owner.Title = "Team";
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.team_type_layout, container, false);

			Button btn = view.FindViewById<Button>(Resource.Id.button1);

			btn.Click += (sender, e) =>
			{
				owner.ShowSelectType();
			};

			return view;
		}
	}
}

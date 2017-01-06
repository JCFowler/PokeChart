using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Graphics;
using Android.Graphics.Drawables;
using System;
using Android.Views;
using System.Linq;
using Android.Support.V7.App;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;

namespace PokemonType
{
	[Activity(Label = "PokemonType", Icon = "@mipmap/icon", Theme ="@style/MyTheme")]
	public class MainActivity : AppCompatActivity
	{
		DateTime clickedTime;
		TextView clickedView;
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

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Main);

			var toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
			SetSupportActionBar(toolbar);

			mainLayout = FindViewById<LinearLayout>(Resource.Id.mainLayout);
			layout1 = FindViewById<LinearLayout>(Resource.Id.layout1);
			layout2 = FindViewById<LinearLayout>(Resource.Id.layout2);
			layout3 = FindViewById<LinearLayout>(Resource.Id.layout3);
			layouts = new List<LinearLayout> { layout1, layout2, layout3, mainLayout };
			left = FindViewById<TextView>(Resource.Id.leftTop);
			right = FindViewById<TextView>(Resource.Id.rightTop);
			TextView textView1 = FindViewById<TextView>(Resource.Id.textView1);
			TextView textView2 = FindViewById<TextView>(Resource.Id.textView2);
			TextView textView3 = FindViewById<TextView>(Resource.Id.textView3);
			TextView textView4 = FindViewById<TextView>(Resource.Id.textView4);
			TextView textView5 = FindViewById<TextView>(Resource.Id.textView5);
			TextView textView6 = FindViewById<TextView>(Resource.Id.textView6);
			TextView textView7 = FindViewById<TextView>(Resource.Id.textView7);
			TextView textView8 = FindViewById<TextView>(Resource.Id.textView8);
			TextView textView9 = FindViewById<TextView>(Resource.Id.textView9);
			TextView textView10 = FindViewById<TextView>(Resource.Id.textView10);
			TextView textView11 = FindViewById<TextView>(Resource.Id.textView11);
			TextView textView12 = FindViewById<TextView>(Resource.Id.textView12);
			TextView textView13 = FindViewById<TextView>(Resource.Id.textView13);
			TextView textView14 = FindViewById<TextView>(Resource.Id.textView14);
			TextView textView15 = FindViewById<TextView>(Resource.Id.textView15);
			TextView textView16 = FindViewById<TextView>(Resource.Id.textView16);
			TextView textView17 = FindViewById<TextView>(Resource.Id.textView17);
			TextView textView18 = FindViewById<TextView>(Resource.Id.textView18);
			textViews = new List<TextView>
			{ textView1, textView2, textView3, textView4, textView5, textView6, textView7, textView8, textView9, textView10,
				textView11, textView12, textView13, textView14, textView15, textView16, textView17, textView18};

			if (SendData.isJapanese)
			{
				Convert.ConvertTextViews(layouts);
				SupportActionBar.Title = "防衛";
				SupportActionBar.Title = "Defense";
			}
			else
				SupportActionBar.Title = "Defense";
			
			types = allTypes.defenseTypes;

			for (int i = 0; i < types.Count; i++)
			{
				textViews[i].Tag = i;
				textViews[i].Text = types[i].type;
				textViews[i].Gravity = GravityFlags.Center;
				textViews[i].SetPadding(0, 0, 0, 10);

				int h = Resources.DisplayMetrics.HeightPixels / 100;

				textViews[i].SetTextSize(Android.Util.ComplexUnitType.Dip, h + 11);

				gradient = (GradientDrawable)textViews[i].Background;
				gradient.SetColor(Color.ParseColor(Colors.TypeToColor[types[i].type]));

				textViews[i].Click += Handle_Click;
			}

			if (SendData.showHelp)
			{
				FragmentTransaction transaction = FragmentManager.BeginTransaction();
				DialogHelp dialog = new DialogHelp();
				dialog.Show(transaction, "Help");
			}
		}

		void Handle_Click(object sender, EventArgs e)
		{
			int num = (int)((TextView)sender).Tag;

			if (!GetTimeSpan(textViews[num], num))
			{
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

				AddTypeData.PopulateTF(weakness, layout1, 2, this);
				AddTypeData.PopulateTF(resistance, layout2, .5, this);
				AddTypeData.PopulateTF(immune, layout3, 0, this);
			}
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

		public bool GetTimeSpan(TextView TF, int num)
		{
			if (clickedView != null)
			{
				if (clickedView == TF)
				{
					TimeSpan timespan = DateTime.Now - clickedTime;
					if (timespan.Milliseconds < 200)
					{
						var sendTypes = allTypes.attackTypes;

						if (SendData.sendAttackType.Count > 0)
							SendData.sendAttackType[0] = sendTypes[num];
						else
							SendData.sendAttackType.Add(sendTypes[num]);

						FragmentTransaction transaction = FragmentManager.BeginTransaction();
						DialogType dialog = new DialogType(this);
						dialog.Show(transaction, "Attack Type");
						return true;
					}
				}
			}
			clickedView = TF;
			clickedTime = DateTime.Now;
			return false;
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

					types = allTypes.defenseTypes;
					Convert.ConvertInsideTypes(leftSide, rightSide);
					Convert.ConvertTextViews(layouts);
					break;
				case Resource.Id.action_help:
					FragmentTransaction transaction = FragmentManager.BeginTransaction();
					DialogHelp dialog = new DialogHelp();
					dialog.Show(transaction, "Help");
					break;
			}

			return base.OnOptionsItemSelected(item);
		}
	}
}
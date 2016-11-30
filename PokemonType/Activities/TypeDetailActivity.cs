
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

namespace PokemonType
{
	[Activity(Label = "TypeDetailActivity")]
	public class TypeDetailActivity : Activity
	{
		List<Type> types;
		RecyclerView recyclerView;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.type_detail_layout);

			recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

			List<string> weakness = SendData.sendType[0].effective;
			List<string> resistance = SendData.sendType[0].resistance;
			List<string> immune = SendData.sendType[0].immune;
			//TextView weakness = FindViewById<TextView>(Resource.Id.weakness);
			//TextView resistance = FindViewById<TextView>(Resource.Id.resistance);
			//TextView immune = FindViewById<TextView>(Resource.Id.immune);

			//for (int i = 0; i < SendData.sendType.Count; i++)
			//{
			//	weakness.Text += createString(SendData.sendType[i].effective);
			//	resistance.Text += createString(SendData.sendType[i].resistance);
			//	immune.Text += createString(SendData.sendType[i].immune);
			//}

			recyclerView.SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Vertical, false));

			var adapter = new TypeAdapter(resistance);
			adapter.ItemClick += OnItemClick;
			recyclerView.SetAdapter(adapter);
		}

		void OnItemClick(object sender, int position)
		{
			Toast.MakeText(this, "Hi" + position.ToString(), ToastLength.Long).Show();
		}

		public string createString(List<string> list)
		{
			var newString = "";
			foreach (var item in list)
			{
				newString += item + " ";
			}
			return newString;
		}
	}
}

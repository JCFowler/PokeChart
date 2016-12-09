using System;
using Android.Widget;
using Android.Views;
using Android.Support.V7.Widget;

namespace PokemonType
{
	public class TypeViewHolder : RecyclerView.ViewHolder
	{
		public TextView typeName { get; set; }
		public CardView card { get; set; }
		public TypeViewHolder(View item, Action<int> listener) :base(item)
		{
			card = item.FindViewById<CardView>(Resource.Id.cardView);
			typeName = item.FindViewById<TextView>(Resource.Id.newTypes);
			item.Click += (sender, e) => listener(base.AdapterPosition);
		}
	}
}

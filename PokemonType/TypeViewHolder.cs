using System;
using Android.Widget;
using Android.Views;
using Android.Support.V7.Widget;

namespace PokemonType
{
	public class TypeViewHolder : RecyclerView.ViewHolder
	{
		public TextView typeName { get; set; }
		public TypeViewHolder(View item, Action<int> listener) :base(item)
		{
			typeName = item.FindViewById<TextView>(Resource.Id.newTypes);
			item.Click += (sender, e) => listener(base.AdapterPosition);
		}
	}
}

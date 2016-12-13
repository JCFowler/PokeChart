using System;
using Android.Views;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using Android.Graphics;
using Android.Widget;

namespace PokemonType
{
	public class TypeAdapter : RecyclerView.Adapter
	{
		public event EventHandler<int> ItemClick;
		List<string> types;

		void OnItemClick(int position)
		{
			if (ItemClick != null)
			{
				ItemClick(this, position);
			}
		}

		public TypeAdapter(List<string> types)
		{
			this.types = types;
		}


		public override int ItemCount
		{
			get
			{
				return types.Count;
			}
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			var layout = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.TypeLayout, parent, false);

			return new TypeViewHolder(layout, OnItemClick);
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			var newHolder = (TypeViewHolder)holder;

			newHolder.typeName.Text = types[position];
			newHolder.card.SetCardBackgroundColor(Color.ParseColor(Colors.TypeToColor[newHolder.typeName.Text]));
		}
	}
}

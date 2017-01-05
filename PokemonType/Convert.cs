using System;
using System.Collections.Generic;
using Android.Widget;

namespace PokemonType
{
	public class Convert
	{
		public static Dictionary<string, string> LanguageDic = new Dictionary<string, string>
		{
			{ "Normal", "ノーマル"},
			{ "Fire", "ほのお"},
			{ "Water", "みず"},
			{ "Electric", "でんき"},
			{ "Grass", "くさ"},
			{ "Ice", "こおり"},
			{ "Fighting", "かくとう"},
			{ "Poison", "どく"},
			{ "Ground", "じめん"},
			{ "Flying", "ひこう"},
			{ "Psychic", "エスパー"},
			{ "Bug", "むし"},
			{ "Rock", "いわ"},
			{ "Ghost", "ゴースト"},
			{ "Dragon", "ドラゴン"},
			{ "Dark", "あく"},
			{ "Steel", "はがね"},
			{ "Fairy", "フェアリー"},

			{ "Weakness", "弱点"},
			{ "Resistance", "抵抗"},
			{ "Effective", "強い"},
			{ "Weak", "弱い"},
			{ "Immune", "免疫"},

			{ "ノーマル", "Normal"},
			{ "ほのお", "Fire"},
			{ "みず", "Water"},
			{ "でんき", "Electric"},
			{ "くさ", "Grass"},
			{ "こおり", "Ice"},
			{ "かくとう", "Fighting"},
			{ "どく", "Poison"},
			{ "じめん", "Ground"},
			{ "ひこう", "Flying"},
			{ "エスパー", "Psychic"},
			{ "むし", "Bug"},
			{ "いわ", "Rock"},
			{ "ゴースト", "Ghost"},
			{ "ドラゴン", "Dragon"},
			{ "あく", "Dark"},
			{ "はがね", "Steel"},
			{ "フェアリー", "Fairy"},

			{ "弱点", "Weakness"},
			{ "抵抗", "Resistance"},
			{ "強い", "Effective"},
			{ "弱い", "Weak"},
			{ "免疫", "Immune"},

			{ "防衛", "Defense"},
			{ "Defense", "防衛"},
			{ "攻撃", "Attack"},
			{ "Attack", "攻撃"}
		};

		public static void ConvertTextViews(List<LinearLayout> sentLayouts)
		{
			foreach (var sentLayout in sentLayouts)
			{
				for (int i = 0; i < sentLayout.ChildCount; i++)
				{
					var layoutI = sentLayout.GetChildAt(i);
					if (layoutI is LinearLayout)
					{
						var layoutJ = (LinearLayout)sentLayout.GetChildAt(i);
						for (int j = 0; j < layoutJ.ChildCount; j++)
						{
							var lay = layoutJ.GetChildAt(j);
							if (lay is LinearLayout)
							{
								var layoutK = (LinearLayout)layoutJ.GetChildAt(j);
								for (int k = 0; k < layoutK.ChildCount; k++)
								{
									if (layoutK.GetChildAt(k) is TextView)
									{
										changeChildText(layoutK, k);
									}
								}
							}
							else if (lay is TextView)
							{
								changeChildText(layoutJ, i);
							}
						}
					}
					else if (layoutI is TextView)
					{
						changeChildText(sentLayout, i);
					}
				}
			}
		}

		private static void changeChildText(LinearLayout layout, int i)
		{
			TextView child = (TextView)layout.GetChildAt(i);
			string[] words = child.Text.Split(' ');

			if (words[words.Length - 1] != "")
				words[words.Length - 1] = Convert.LanguageDic[words[words.Length - 1]];
			child.Text = String.Join(" ", words);
		}

		public static void ConvertInsideTypes(SingleType left, SingleType right)
		{
			changeInside(left);
			changeInside(right);
		}

		private static void changeInside(SingleType mType)
		{
			for (int i = 0; i < mType.type.effective.Count;i++)
				mType.type.effective[i] = LanguageDic[mType.type.effective[i]];
			for (int i = 0; i < mType.type.resistance.Count; i++)
				mType.type.resistance[i] = LanguageDic[mType.type.resistance[i]];
			for (int i = 0; i < mType.type.immune.Count; i++)
				mType.type.immune[i] = LanguageDic[mType.type.immune[i]];
		}
	}
}

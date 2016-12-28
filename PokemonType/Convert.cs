using System;
using System.Collections.Generic;

namespace PokemonType
{
	public class Convert
	{
		public static Dictionary<string, string> EnglishToJapanese = new Dictionary<string, string>
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
			{ "Immune", "免疫"}
		};

		public static Dictionary<string, string> JapaneseToEnglish = new Dictionary<string, string>
		{
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
			{ "免疫", "Immune"}
		};
	}
}

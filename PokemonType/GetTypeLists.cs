using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace PokemonType
{
	public class GetTypeLists
	{
		public static void GetEnglishLists(Android.Content.Res.AssetManager Assets)
		{
			string response;
			StreamReader strm = new StreamReader(Assets.Open("AttackTypeChart.txt"));
			response = strm.ReadToEnd();
			allTypes.attackTypes = JsonConvert.DeserializeObject<List<Type>>(response);

			string response2;
			StreamReader strm2 = new StreamReader(Assets.Open("DefenseTypeChart.txt"));
			response2 = strm2.ReadToEnd();
			allTypes.defenseTypes = JsonConvert.DeserializeObject<List<Type>>(response2);
		}

		public static void GetJapaneseLists(Android.Content.Res.AssetManager Assets)
		{
			string response;
			StreamReader strm = new StreamReader(Assets.Open("JapaneseAttackType.txt"));
			response = strm.ReadToEnd();
			allTypes.attackTypes = JsonConvert.DeserializeObject<List<Type>>(response);

			string response2;
			StreamReader strm2 = new StreamReader(Assets.Open("JapaneseDefenseType.txt"));
			response2 = strm2.ReadToEnd();
			allTypes.defenseTypes = JsonConvert.DeserializeObject<List<Type>>(response2);
		}
	}
}

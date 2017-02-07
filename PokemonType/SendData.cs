using System;
using System.Collections.Generic;

namespace PokemonType
{
	public class allTypes
	{
		public static List<Type> defenseTypes = new List<Type>();
		public static List<Type> attackTypes = new List<Type>();
	}

	public class SendData
	{
		public static bool isJapanese;
		public static bool showHelp;
		public static List<Type> sendAttackType = new List<Type>();

		public static int typeTextSize;
		public static int SDKNum;
		public static bool isConnected;

		public static SingleType showType1 = new SingleType();
		public static SingleType showType2 = new SingleType();
	}

	public class SingleType
	{
		public Type type = new Type();
		public int num = -1;
	}
}

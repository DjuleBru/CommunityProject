using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("dungeonIsComplete", "humanoidsAssigned")]
	public class ES3UserType_DungeonEntrance : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_DungeonEntrance() : base(typeof(DungeonEntrance)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (DungeonEntrance)obj;
			
			writer.WritePrivateField("dungeonIsComplete", instance);
			writer.WritePrivateField("humanoidsAssigned", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (DungeonEntrance)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "dungeonIsComplete":
					instance = (DungeonEntrance)reader.SetPrivateField("dungeonIsComplete", reader.Read<System.Boolean>(), instance);
					break;
					case "humanoidsAssigned":
					instance = (DungeonEntrance)reader.SetPrivateField("humanoidsAssigned", reader.Read<System.Collections.Generic.List<Humanoid>>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_DungeonEntranceArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_DungeonEntranceArray() : base(typeof(DungeonEntrance[]), ES3UserType_DungeonEntrance.Instance)
		{
			Instance = this;
		}
	}
}
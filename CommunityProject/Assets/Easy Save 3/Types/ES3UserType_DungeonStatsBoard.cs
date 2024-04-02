using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("recordedDungeonLoot", "recordedDungeonTime")]
	public class ES3UserType_DungeonStatsBoard : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_DungeonStatsBoard() : base(typeof(DungeonStatsBoard)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (DungeonStatsBoard)obj;
			
			writer.WritePrivateField("recordedDungeonLoot", instance);
			writer.WritePrivateField("recordedDungeonTime", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (DungeonStatsBoard)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "recordedDungeonLoot":
					instance = (DungeonStatsBoard)reader.SetPrivateField("recordedDungeonLoot", reader.Read<System.Collections.Generic.List<Item>>(), instance);
					break;
					case "recordedDungeonTime":
					instance = (DungeonStatsBoard)reader.SetPrivateField("recordedDungeonTime", reader.Read<System.Single>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_DungeonStatsBoardArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_DungeonStatsBoardArray() : base(typeof(DungeonStatsBoard[]), ES3UserType_DungeonStatsBoard.Instance)
		{
			Instance = this;
		}
	}
}
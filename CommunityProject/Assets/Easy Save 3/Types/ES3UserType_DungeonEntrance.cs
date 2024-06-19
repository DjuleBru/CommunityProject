using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("playerIsInEntranceArea", "dungeonEntranceUI", "enterUI", "dungeonSO", "exitDungeonSpawnPoint", "dungeonChest", "dungeonStatsBoard", "dungeonIsComplete", "dungeonEntranceColliderForDungeoneers", "humanoidsAssigned", "m_CancellationTokenSource")]
	public class ES3UserType_DungeonEntrance : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_DungeonEntrance() : base(typeof(DungeonEntrance)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (DungeonEntrance)obj;
			
			writer.WritePrivateField("playerIsInEntranceArea", instance);
			writer.WritePrivateFieldByRef("dungeonEntranceUI", instance);
			writer.WritePrivateFieldByRef("enterUI", instance);
			writer.WritePrivateFieldByRef("dungeonSO", instance);
			writer.WritePrivateFieldByRef("exitDungeonSpawnPoint", instance);
			writer.WritePrivateFieldByRef("dungeonChest", instance);
			writer.WritePrivateFieldByRef("dungeonStatsBoard", instance);
			writer.WritePrivateField("dungeonIsComplete", instance);
			writer.WritePrivateFieldByRef("dungeonEntranceColliderForDungeoneers", instance);
			writer.WritePrivateField("humanoidsAssigned", instance);
			writer.WritePrivateField("m_CancellationTokenSource", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (DungeonEntrance)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "playerIsInEntranceArea":
					instance = (DungeonEntrance)reader.SetPrivateField("playerIsInEntranceArea", reader.Read<System.Boolean>(), instance);
					break;
					case "dungeonEntranceUI":
					instance = (DungeonEntrance)reader.SetPrivateField("dungeonEntranceUI", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "enterUI":
					instance = (DungeonEntrance)reader.SetPrivateField("enterUI", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "dungeonSO":
					instance = (DungeonEntrance)reader.SetPrivateField("dungeonSO", reader.Read<DungeonSO>(), instance);
					break;
					case "exitDungeonSpawnPoint":
					instance = (DungeonEntrance)reader.SetPrivateField("exitDungeonSpawnPoint", reader.Read<UnityEngine.Transform>(), instance);
					break;
					case "dungeonChest":
					instance = (DungeonEntrance)reader.SetPrivateField("dungeonChest", reader.Read<Chest>(), instance);
					break;
					case "dungeonStatsBoard":
					instance = (DungeonEntrance)reader.SetPrivateField("dungeonStatsBoard", reader.Read<DungeonStatsBoard>(), instance);
					break;
					case "dungeonIsComplete":
					instance = (DungeonEntrance)reader.SetPrivateField("dungeonIsComplete", reader.Read<System.Boolean>(), instance);
					break;
					case "dungeonEntranceColliderForDungeoneers":
					instance = (DungeonEntrance)reader.SetPrivateField("dungeonEntranceColliderForDungeoneers", reader.Read<UnityEngine.Collider2D>(), instance);
					break;
					case "humanoidsAssigned":
					instance = (DungeonEntrance)reader.SetPrivateField("humanoidsAssigned", reader.Read<System.Collections.Generic.List<Humanoid>>(), instance);
					break;
					case "m_CancellationTokenSource":
					instance = (DungeonEntrance)reader.SetPrivateField("m_CancellationTokenSource", reader.Read<System.Threading.CancellationTokenSource>(), instance);
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
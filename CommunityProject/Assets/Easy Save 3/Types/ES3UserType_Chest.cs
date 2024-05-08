using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("chestInventory", "itemsInChest", "isDungeonChest", "buildingSO", "buildingPlaced", "assignedHumanoid", "assignedInputHauliers", "assignedOutputHauliers", "enabled")]
	public class ES3UserType_Chest : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Chest() : base(typeof(Chest)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Chest)obj;
			
			writer.WritePrivateField("chestInventory", instance);
			writer.WritePrivateField("itemsInChest", instance);
			writer.WritePrivateField("isDungeonChest", instance);
			writer.WritePrivateFieldByRef("buildingSO", instance);
			writer.WritePrivateField("buildingPlaced", instance);
			writer.WritePrivateFieldByRef("assignedHumanoid", instance);
			writer.WritePrivateField("assignedInputHauliers", instance);
			writer.WritePrivateField("assignedOutputHauliers", instance);
			writer.WriteProperty("enabled", instance.enabled, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Chest)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "chestInventory":
					instance = (Chest)reader.SetPrivateField("chestInventory", reader.Read<Inventory>(), instance);
					break;
					case "itemsInChest":
					instance = (Chest)reader.SetPrivateField("itemsInChest", reader.Read<System.Collections.Generic.List<Item>>(), instance);
					break;
					case "isDungeonChest":
					instance = (Chest)reader.SetPrivateField("isDungeonChest", reader.Read<System.Boolean>(), instance);
					break;
					case "buildingSO":
					instance = (Chest)reader.SetPrivateField("buildingSO", reader.Read<BuildingSO>(), instance);
					break;
					case "buildingPlaced":
					instance = (Chest)reader.SetPrivateField("buildingPlaced", reader.Read<System.Boolean>(), instance);
					break;
					case "assignedHumanoid":
					instance = (Chest)reader.SetPrivateField("assignedHumanoid", reader.Read<Humanoid>(), instance);
					break;
					case "assignedInputHauliers":
					instance = (Chest)reader.SetPrivateField("assignedInputHauliers", reader.Read<System.Collections.Generic.List<Humanoid>>(), instance);
					break;
					case "assignedOutputHauliers":
					instance = (Chest)reader.SetPrivateField("assignedOutputHauliers", reader.Read<System.Collections.Generic.List<Humanoid>>(), instance);
					break;
					case "enabled":
						instance.enabled = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_ChestArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_ChestArray() : base(typeof(Chest[]), ES3UserType_Chest.Instance)
		{
			Instance = this;
		}
	}
}
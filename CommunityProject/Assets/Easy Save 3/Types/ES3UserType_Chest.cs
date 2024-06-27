using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("chestInventory", "isDungeonChest", "itemCategoryToStore", "buildingSO", "buildingVisual", "buildingPlaced", "assignedHumanoid", "assignedInputHauliers", "assignedOutputHauliers")]
	public class ES3UserType_Chest : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Chest() : base(typeof(Chest)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Chest)obj;
			
			writer.WritePrivateField("chestInventory", instance);
			writer.WritePrivateField("isDungeonChest", instance);
			writer.WritePrivateField("itemCategoryToStore", instance);
			writer.WritePrivateFieldByRef("buildingSO", instance);
			writer.WritePrivateFieldByRef("buildingVisual", instance);
			writer.WritePrivateField("buildingPlaced", instance);
			writer.WritePrivateFieldByRef("assignedHumanoid", instance);
			writer.WritePrivateField("assignedInputHauliers", instance);
			writer.WritePrivateField("assignedOutputHauliers", instance);
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
					case "isDungeonChest":
					instance = (Chest)reader.SetPrivateField("isDungeonChest", reader.Read<System.Boolean>(), instance);
					break;
					case "itemCategoryToStore":
					instance = (Chest)reader.SetPrivateField("itemCategoryToStore", reader.Read<Item.ItemCategory>(), instance);
					break;
					case "buildingSO":
					instance = (Chest)reader.SetPrivateField("buildingSO", reader.Read<BuildingSO>(), instance);
					break;
					case "buildingVisual":
					instance = (Chest)reader.SetPrivateField("buildingVisual", reader.Read<BuildingVisual>(), instance);
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
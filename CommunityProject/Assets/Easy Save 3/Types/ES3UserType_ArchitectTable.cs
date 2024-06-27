using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("selectedRecipeSO", "productionTimer", "inputInventoryList", "outputInventoryList", "itemWorldProducedList", "working", "inputItemsMissing", "outputInventoryFull", "productionBuildingUIWorld", "buildingHaulersUI_World", "productionBuildingvisual", "buildingSO", "buildingSizeX", "buildingSizeY", "buildingCamera", "buildingVisual", "interactionCollider", "buildingPlaced", "assignedHumanoid", "assignedInputHauliers", "assignedOutputHauliers", "workerInteractingWithBuilding")]
	public class ES3UserType_ArchitectTable : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_ArchitectTable() : base(typeof(ArchitectTable)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (ArchitectTable)obj;
			
			writer.WritePrivateFieldByRef("selectedRecipeSO", instance);
			writer.WritePrivateField("productionTimer", instance);
			writer.WritePrivateField("inputInventoryList", instance);
			writer.WritePrivateField("outputInventoryList", instance);
			writer.WritePrivateField("itemWorldProducedList", instance);
			writer.WritePrivateField("working", instance);
			writer.WritePrivateField("inputItemsMissing", instance);
			writer.WritePrivateField("outputInventoryFull", instance);
			writer.WritePrivateFieldByRef("productionBuildingUIWorld", instance);
			writer.WritePrivateFieldByRef("buildingHaulersUI_World", instance);
			writer.WritePrivateFieldByRef("productionBuildingvisual", instance);
			writer.WritePrivateFieldByRef("buildingSO", instance);
			writer.WritePrivateField("buildingSizeX", instance);
			writer.WritePrivateField("buildingSizeY", instance);
			writer.WritePrivateFieldByRef("buildingCamera", instance);
			writer.WritePrivateFieldByRef("buildingVisual", instance);
			writer.WritePrivateFieldByRef("interactionCollider", instance);
			writer.WritePrivateField("buildingPlaced", instance);
			writer.WritePrivateFieldByRef("assignedHumanoid", instance);
			writer.WritePrivateField("assignedInputHauliers", instance);
			writer.WritePrivateField("assignedOutputHauliers", instance);
			writer.WritePrivateField("workerInteractingWithBuilding", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (ArchitectTable)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "selectedRecipeSO":
					instance = (ArchitectTable)reader.SetPrivateField("selectedRecipeSO", reader.Read<RecipeSO>(), instance);
					break;
					case "productionTimer":
					instance = (ArchitectTable)reader.SetPrivateField("productionTimer", reader.Read<System.Single>(), instance);
					break;
					case "inputInventoryList":
					instance = (ArchitectTable)reader.SetPrivateField("inputInventoryList", reader.Read<System.Collections.Generic.List<Inventory>>(), instance);
					break;
					case "outputInventoryList":
					instance = (ArchitectTable)reader.SetPrivateField("outputInventoryList", reader.Read<System.Collections.Generic.List<Inventory>>(), instance);
					break;
					case "itemWorldProducedList":
					instance = (ArchitectTable)reader.SetPrivateField("itemWorldProducedList", reader.Read<System.Collections.Generic.List<ItemWorld>>(), instance);
					break;
					case "working":
					instance = (ArchitectTable)reader.SetPrivateField("working", reader.Read<System.Boolean>(), instance);
					break;
					case "inputItemsMissing":
					instance = (ArchitectTable)reader.SetPrivateField("inputItemsMissing", reader.Read<System.Boolean>(), instance);
					break;
					case "outputInventoryFull":
					instance = (ArchitectTable)reader.SetPrivateField("outputInventoryFull", reader.Read<System.Boolean>(), instance);
					break;
					case "productionBuildingUIWorld":
					instance = (ArchitectTable)reader.SetPrivateField("productionBuildingUIWorld", reader.Read<ProductionBuildingUI_World>(), instance);
					break;
					case "buildingHaulersUI_World":
					instance = (ArchitectTable)reader.SetPrivateField("buildingHaulersUI_World", reader.Read<BuildingHaulersUI_World>(), instance);
					break;
					case "productionBuildingvisual":
					instance = (ArchitectTable)reader.SetPrivateField("productionBuildingvisual", reader.Read<ProductionBuildingVisual>(), instance);
					break;
					case "buildingSO":
					instance = (ArchitectTable)reader.SetPrivateField("buildingSO", reader.Read<BuildingSO>(), instance);
					break;
					case "buildingSizeX":
					instance = (ArchitectTable)reader.SetPrivateField("buildingSizeX", reader.Read<System.Int32>(), instance);
					break;
					case "buildingSizeY":
					instance = (ArchitectTable)reader.SetPrivateField("buildingSizeY", reader.Read<System.Int32>(), instance);
					break;
					case "buildingCamera":
					instance = (ArchitectTable)reader.SetPrivateField("buildingCamera", reader.Read<Cinemachine.CinemachineVirtualCamera>(), instance);
					break;
					case "buildingVisual":
					instance = (ArchitectTable)reader.SetPrivateField("buildingVisual", reader.Read<BuildingVisual>(), instance);
					break;
					case "interactionCollider":
					instance = (ArchitectTable)reader.SetPrivateField("interactionCollider", reader.Read<UnityEngine.Collider2D>(), instance);
					break;
					case "buildingPlaced":
					instance = (ArchitectTable)reader.SetPrivateField("buildingPlaced", reader.Read<System.Boolean>(), instance);
					break;
					case "assignedHumanoid":
					instance = (ArchitectTable)reader.SetPrivateField("assignedHumanoid", reader.Read<Humanoid>(), instance);
					break;
					case "assignedInputHauliers":
					instance = (ArchitectTable)reader.SetPrivateField("assignedInputHauliers", reader.Read<System.Collections.Generic.List<Humanoid>>(), instance);
					break;
					case "assignedOutputHauliers":
					instance = (ArchitectTable)reader.SetPrivateField("assignedOutputHauliers", reader.Read<System.Collections.Generic.List<Humanoid>>(), instance);
					break;
					case "workerInteractingWithBuilding":
					instance = (ArchitectTable)reader.SetPrivateField("workerInteractingWithBuilding", reader.Read<System.Boolean>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_ArchitectTableArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_ArchitectTableArray() : base(typeof(ArchitectTable[]), ES3UserType_ArchitectTable.Instance)
		{
			Instance = this;
		}
	}
}
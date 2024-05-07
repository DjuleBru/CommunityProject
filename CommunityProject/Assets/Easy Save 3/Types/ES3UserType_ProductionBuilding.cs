using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("selectedRecipeSO", "productionTimer", "inputInventoryList", "outputInventoryList", "itemWorldProducedList", "working", "inputItemsMissing", "outputInventoryFull", "productionBuildingUIWorld", "buildingHaulersUI_World", "productionBuildingvisual", "buildingSO", "buildingSizeX", "buildingSizeY", "buildingCamera", "buildingVisual", "interactionCollider", "assignedHumanoid", "assignedInputHauliers", "assignedOutputHauliers", "workerInteractingWithBuilding")]
	public class ES3UserType_ProductionBuilding : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_ProductionBuilding() : base(typeof(ProductionBuilding)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (ProductionBuilding)obj;
			
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
			writer.WritePrivateFieldByRef("assignedHumanoid", instance);
			writer.WritePrivateField("assignedInputHauliers", instance);
			writer.WritePrivateField("assignedOutputHauliers", instance);
			writer.WritePrivateField("workerInteractingWithBuilding", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (ProductionBuilding)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "selectedRecipeSO":
					instance = (ProductionBuilding)reader.SetPrivateField("selectedRecipeSO", reader.Read<RecipeSO>(), instance);
					break;
					case "productionTimer":
					instance = (ProductionBuilding)reader.SetPrivateField("productionTimer", reader.Read<System.Single>(), instance);
					break;
					case "inputInventoryList":
					instance = (ProductionBuilding)reader.SetPrivateField("inputInventoryList", reader.Read<System.Collections.Generic.List<Inventory>>(), instance);
					break;
					case "outputInventoryList":
					instance = (ProductionBuilding)reader.SetPrivateField("outputInventoryList", reader.Read<System.Collections.Generic.List<Inventory>>(), instance);
					break;
					case "itemWorldProducedList":
					instance = (ProductionBuilding)reader.SetPrivateField("itemWorldProducedList", reader.Read<System.Collections.Generic.List<ItemWorld>>(), instance);
					break;
					case "working":
					instance = (ProductionBuilding)reader.SetPrivateField("working", reader.Read<System.Boolean>(), instance);
					break;
					case "inputItemsMissing":
					instance = (ProductionBuilding)reader.SetPrivateField("inputItemsMissing", reader.Read<System.Boolean>(), instance);
					break;
					case "outputInventoryFull":
					instance = (ProductionBuilding)reader.SetPrivateField("outputInventoryFull", reader.Read<System.Boolean>(), instance);
					break;
					case "productionBuildingUIWorld":
					instance = (ProductionBuilding)reader.SetPrivateField("productionBuildingUIWorld", reader.Read<ProductionBuildingUI_World>(), instance);
					break;
					case "buildingHaulersUI_World":
					instance = (ProductionBuilding)reader.SetPrivateField("buildingHaulersUI_World", reader.Read<BuildingHaulersUI_World>(), instance);
					break;
					case "productionBuildingvisual":
					instance = (ProductionBuilding)reader.SetPrivateField("productionBuildingvisual", reader.Read<ProductionBuildingVisual>(), instance);
					break;
					case "buildingSO":
					instance = (ProductionBuilding)reader.SetPrivateField("buildingSO", reader.Read<BuildingSO>(), instance);
					break;
					case "buildingSizeX":
					instance = (ProductionBuilding)reader.SetPrivateField("buildingSizeX", reader.Read<System.Int32>(), instance);
					break;
					case "buildingSizeY":
					instance = (ProductionBuilding)reader.SetPrivateField("buildingSizeY", reader.Read<System.Int32>(), instance);
					break;
					case "buildingCamera":
					instance = (ProductionBuilding)reader.SetPrivateField("buildingCamera", reader.Read<Cinemachine.CinemachineVirtualCamera>(), instance);
					break;
					case "buildingVisual":
					instance = (ProductionBuilding)reader.SetPrivateField("buildingVisual", reader.Read<BuildingVisual>(), instance);
					break;
					case "interactionCollider":
					instance = (ProductionBuilding)reader.SetPrivateField("interactionCollider", reader.Read<UnityEngine.Collider2D>(), instance);
					break;
					case "assignedHumanoid":
					instance = (ProductionBuilding)reader.SetPrivateField("assignedHumanoid", reader.Read<Humanoid>(), instance);
					break;
					case "assignedInputHauliers":
					instance = (ProductionBuilding)reader.SetPrivateField("assignedInputHauliers", reader.Read<System.Collections.Generic.List<Humanoid>>(), instance);
					break;
					case "assignedOutputHauliers":
					instance = (ProductionBuilding)reader.SetPrivateField("assignedOutputHauliers", reader.Read<System.Collections.Generic.List<Humanoid>>(), instance);
					break;
					case "workerInteractingWithBuilding":
					instance = (ProductionBuilding)reader.SetPrivateField("workerInteractingWithBuilding", reader.Read<System.Boolean>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_ProductionBuildingArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_ProductionBuildingArray() : base(typeof(ProductionBuilding[]), ES3UserType_ProductionBuilding.Instance)
		{
			Instance = this;
		}
	}
}
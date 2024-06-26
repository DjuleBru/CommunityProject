using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("missingItemsUI", "missingSelectedRecipeUI", "missingWorkerUI", "outputInventoryFull", "productionBuilding", "progressionBarGameObject", "progressionBarFill", "working", "m_CancellationTokenSource")]
	public class ES3UserType_ProductionBuildingUI_World : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_ProductionBuildingUI_World() : base(typeof(ProductionBuildingUI_World)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (ProductionBuildingUI_World)obj;
			
			writer.WritePrivateFieldByRef("missingItemsUI", instance);
			writer.WritePrivateFieldByRef("missingSelectedRecipeUI", instance);
			writer.WritePrivateFieldByRef("missingWorkerUI", instance);
			writer.WritePrivateFieldByRef("outputInventoryFull", instance);
			writer.WritePrivateFieldByRef("productionBuilding", instance);
			writer.WritePrivateFieldByRef("progressionBarGameObject", instance);
			writer.WritePrivateFieldByRef("progressionBarFill", instance);
			writer.WritePrivateField("working", instance);
			writer.WritePrivateField("m_CancellationTokenSource", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (ProductionBuildingUI_World)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "missingItemsUI":
					instance = (ProductionBuildingUI_World)reader.SetPrivateField("missingItemsUI", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "missingSelectedRecipeUI":
					instance = (ProductionBuildingUI_World)reader.SetPrivateField("missingSelectedRecipeUI", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "missingWorkerUI":
					instance = (ProductionBuildingUI_World)reader.SetPrivateField("missingWorkerUI", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "outputInventoryFull":
					instance = (ProductionBuildingUI_World)reader.SetPrivateField("outputInventoryFull", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "productionBuilding":
					instance = (ProductionBuildingUI_World)reader.SetPrivateField("productionBuilding", reader.Read<ProductionBuilding>(), instance);
					break;
					case "progressionBarGameObject":
					instance = (ProductionBuildingUI_World)reader.SetPrivateField("progressionBarGameObject", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "progressionBarFill":
					instance = (ProductionBuildingUI_World)reader.SetPrivateField("progressionBarFill", reader.Read<UnityEngine.UI.Image>(), instance);
					break;
					case "working":
					instance = (ProductionBuildingUI_World)reader.SetPrivateField("working", reader.Read<System.Boolean>(), instance);
					break;
					case "m_CancellationTokenSource":
					instance = (ProductionBuildingUI_World)reader.SetPrivateField("m_CancellationTokenSource", reader.Read<System.Threading.CancellationTokenSource>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_ProductionBuildingUI_WorldArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_ProductionBuildingUI_WorldArray() : base(typeof(ProductionBuildingUI_World[]), ES3UserType_ProductionBuildingUI_World.Instance)
		{
			Instance = this;
		}
	}
}
using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("buildingAnimator", "characterAnimator", "propVisual", "placingBuildingBackgroundSprite", "buildingHoveredVisual", "validPlacementColor", "solidBuildingCollider", "interactionBuildingCollider", "building", "playerInTriggerArea", "interactingWithBuilding", "buildingScoreText", "m_CancellationTokenSource")]
	public class ES3UserType_ProductionBuildingVisual : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_ProductionBuildingVisual() : base(typeof(ProductionBuildingVisual)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (ProductionBuildingVisual)obj;
			
			writer.WritePrivateFieldByRef("buildingAnimator", instance);
			writer.WritePrivateFieldByRef("characterAnimator", instance);
			writer.WritePrivateFieldByRef("propVisual", instance);
			writer.WritePrivateFieldByRef("placingBuildingBackgroundSprite", instance);
			writer.WritePrivateFieldByRef("buildingHoveredVisual", instance);
			writer.WritePrivateField("validPlacementColor", instance);
			writer.WritePrivateFieldByRef("solidBuildingCollider", instance);
			writer.WritePrivateFieldByRef("interactionBuildingCollider", instance);
			writer.WritePrivateFieldByRef("building", instance);
			writer.WritePrivateField("playerInTriggerArea", instance);
			writer.WritePrivateField("interactingWithBuilding", instance);
			writer.WritePrivateFieldByRef("buildingScoreText", instance);
			writer.WritePrivateField("m_CancellationTokenSource", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (ProductionBuildingVisual)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "buildingAnimator":
					instance = (ProductionBuildingVisual)reader.SetPrivateField("buildingAnimator", reader.Read<UnityEngine.Animator>(), instance);
					break;
					case "characterAnimator":
					instance = (ProductionBuildingVisual)reader.SetPrivateField("characterAnimator", reader.Read<UnityEngine.Animator>(), instance);
					break;
					case "propVisual":
					instance = (ProductionBuildingVisual)reader.SetPrivateField("propVisual", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "placingBuildingBackgroundSprite":
					instance = (ProductionBuildingVisual)reader.SetPrivateField("placingBuildingBackgroundSprite", reader.Read<UnityEngine.SpriteRenderer>(), instance);
					break;
					case "buildingHoveredVisual":
					instance = (ProductionBuildingVisual)reader.SetPrivateField("buildingHoveredVisual", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "validPlacementColor":
					instance = (ProductionBuildingVisual)reader.SetPrivateField("validPlacementColor", reader.Read<UnityEngine.Color>(), instance);
					break;
					case "solidBuildingCollider":
					instance = (ProductionBuildingVisual)reader.SetPrivateField("solidBuildingCollider", reader.Read<UnityEngine.Collider2D>(), instance);
					break;
					case "interactionBuildingCollider":
					instance = (ProductionBuildingVisual)reader.SetPrivateField("interactionBuildingCollider", reader.Read<UnityEngine.Collider2D>(), instance);
					break;
					case "building":
					instance = (ProductionBuildingVisual)reader.SetPrivateField("building", reader.Read<Building>(), instance);
					break;
					case "playerInTriggerArea":
					instance = (ProductionBuildingVisual)reader.SetPrivateField("playerInTriggerArea", reader.Read<System.Boolean>(), instance);
					break;
					case "interactingWithBuilding":
					instance = (ProductionBuildingVisual)reader.SetPrivateField("interactingWithBuilding", reader.Read<System.Boolean>(), instance);
					break;
					case "buildingScoreText":
					instance = (ProductionBuildingVisual)reader.SetPrivateField("buildingScoreText", reader.Read<TMPro.TextMeshProUGUI>(), instance);
					break;
					case "m_CancellationTokenSource":
					instance = (ProductionBuildingVisual)reader.SetPrivateField("m_CancellationTokenSource", reader.Read<System.Threading.CancellationTokenSource>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_ProductionBuildingVisualArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_ProductionBuildingVisualArray() : base(typeof(ProductionBuildingVisual[]), ES3UserType_ProductionBuildingVisual.Instance)
		{
			Instance = this;
		}
	}
}
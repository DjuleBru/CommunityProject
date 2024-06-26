using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("placingBuildingBackgroundSprite", "buildingHoveredVisual", "validPlacementColor", "unValidPlacementColor", "solidBuildingCollider", "interactionBuildingCollider", "building", "playerInTriggerArea", "interactingWithBuilding", "buildingScoreText", "groundTileMap", "m_CancellationTokenSource")]
	public class ES3UserType_BuildingVisual : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_BuildingVisual() : base(typeof(BuildingVisual)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (BuildingVisual)obj;
			
			writer.WritePrivateFieldByRef("placingBuildingBackgroundSprite", instance);
			writer.WritePrivateFieldByRef("buildingHoveredVisual", instance);
			writer.WritePrivateField("validPlacementColor", instance);
			writer.WritePrivateField("unValidPlacementColor", instance);
			writer.WritePrivateFieldByRef("solidBuildingCollider", instance);
			writer.WritePrivateFieldByRef("interactionBuildingCollider", instance);
			writer.WritePrivateFieldByRef("building", instance);
			writer.WritePrivateField("playerInTriggerArea", instance);
			writer.WritePrivateField("interactingWithBuilding", instance);
			writer.WritePrivateFieldByRef("buildingScoreText", instance);
			writer.WritePrivateFieldByRef("groundTileMap", instance);
			writer.WritePrivateField("m_CancellationTokenSource", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (BuildingVisual)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "placingBuildingBackgroundSprite":
					instance = (BuildingVisual)reader.SetPrivateField("placingBuildingBackgroundSprite", reader.Read<UnityEngine.SpriteRenderer>(), instance);
					break;
					case "buildingHoveredVisual":
					instance = (BuildingVisual)reader.SetPrivateField("buildingHoveredVisual", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "validPlacementColor":
					instance = (BuildingVisual)reader.SetPrivateField("validPlacementColor", reader.Read<UnityEngine.Color>(), instance);
					break;
					case "unValidPlacementColor":
					instance = (BuildingVisual)reader.SetPrivateField("unValidPlacementColor", reader.Read<UnityEngine.Color>(), instance);
					break;
					case "solidBuildingCollider":
					instance = (BuildingVisual)reader.SetPrivateField("solidBuildingCollider", reader.Read<UnityEngine.Collider2D>(), instance);
					break;
					case "interactionBuildingCollider":
					instance = (BuildingVisual)reader.SetPrivateField("interactionBuildingCollider", reader.Read<UnityEngine.Collider2D>(), instance);
					break;
					case "building":
					instance = (BuildingVisual)reader.SetPrivateField("building", reader.Read<Building>(), instance);
					break;
					case "playerInTriggerArea":
					instance = (BuildingVisual)reader.SetPrivateField("playerInTriggerArea", reader.Read<System.Boolean>(), instance);
					break;
					case "interactingWithBuilding":
					instance = (BuildingVisual)reader.SetPrivateField("interactingWithBuilding", reader.Read<System.Boolean>(), instance);
					break;
					case "buildingScoreText":
					instance = (BuildingVisual)reader.SetPrivateField("buildingScoreText", reader.Read<TMPro.TextMeshProUGUI>(), instance);
					break;
					case "groundTileMap":
					instance = (BuildingVisual)reader.SetPrivateField("groundTileMap", reader.Read<UnityEngine.Transform>(), instance);
					break;
					case "m_CancellationTokenSource":
					instance = (BuildingVisual)reader.SetPrivateField("m_CancellationTokenSource", reader.Read<System.Threading.CancellationTokenSource>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_BuildingVisualArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_BuildingVisualArray() : base(typeof(BuildingVisual[]), ES3UserType_BuildingVisual.Instance)
		{
			Instance = this;
		}
	}
}
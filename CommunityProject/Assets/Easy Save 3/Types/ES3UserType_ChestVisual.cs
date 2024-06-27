using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("chest", "chestRenderer", "chestShadowRenderer", "chestHoveredVisual", "openedSprite", "closedSprite", "openedSpriteShadow", "closedSpriteShadow", "solidChestCollider", "placingBuildingBackgroundSprite", "buildingHoveredVisual", "validPlacementColor", "unValidPlacementColor", "solidBuildingCollider", "interactionBuildingCollider", "building", "buildingScoreText")]
	public class ES3UserType_ChestVisual : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_ChestVisual() : base(typeof(ChestVisual)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (ChestVisual)obj;
			
			writer.WritePrivateFieldByRef("chest", instance);
			writer.WritePrivateFieldByRef("chestRenderer", instance);
			writer.WritePrivateFieldByRef("chestShadowRenderer", instance);
			writer.WritePrivateFieldByRef("chestHoveredVisual", instance);
			writer.WritePrivateFieldByRef("openedSprite", instance);
			writer.WritePrivateFieldByRef("closedSprite", instance);
			writer.WritePrivateFieldByRef("openedSpriteShadow", instance);
			writer.WritePrivateFieldByRef("closedSpriteShadow", instance);
			writer.WritePrivateFieldByRef("solidChestCollider", instance);
			writer.WritePrivateFieldByRef("placingBuildingBackgroundSprite", instance);
			writer.WritePrivateFieldByRef("buildingHoveredVisual", instance);
			writer.WritePrivateField("validPlacementColor", instance);
			writer.WritePrivateField("unValidPlacementColor", instance);
			writer.WritePrivateFieldByRef("solidBuildingCollider", instance);
			writer.WritePrivateFieldByRef("interactionBuildingCollider", instance);
			writer.WritePrivateFieldByRef("building", instance);
			writer.WritePrivateFieldByRef("buildingScoreText", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (ChestVisual)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "chest":
					instance = (ChestVisual)reader.SetPrivateField("chest", reader.Read<Chest>(), instance);
					break;
					case "chestRenderer":
					instance = (ChestVisual)reader.SetPrivateField("chestRenderer", reader.Read<UnityEngine.SpriteRenderer>(), instance);
					break;
					case "chestShadowRenderer":
					instance = (ChestVisual)reader.SetPrivateField("chestShadowRenderer", reader.Read<UnityEngine.SpriteRenderer>(), instance);
					break;
					case "chestHoveredVisual":
					instance = (ChestVisual)reader.SetPrivateField("chestHoveredVisual", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "openedSprite":
					instance = (ChestVisual)reader.SetPrivateField("openedSprite", reader.Read<UnityEngine.Sprite>(), instance);
					break;
					case "closedSprite":
					instance = (ChestVisual)reader.SetPrivateField("closedSprite", reader.Read<UnityEngine.Sprite>(), instance);
					break;
					case "openedSpriteShadow":
					instance = (ChestVisual)reader.SetPrivateField("openedSpriteShadow", reader.Read<UnityEngine.Sprite>(), instance);
					break;
					case "closedSpriteShadow":
					instance = (ChestVisual)reader.SetPrivateField("closedSpriteShadow", reader.Read<UnityEngine.Sprite>(), instance);
					break;
					case "solidChestCollider":
					instance = (ChestVisual)reader.SetPrivateField("solidChestCollider", reader.Read<UnityEngine.Collider2D>(), instance);
					break;
					case "placingBuildingBackgroundSprite":
					instance = (ChestVisual)reader.SetPrivateField("placingBuildingBackgroundSprite", reader.Read<UnityEngine.SpriteRenderer>(), instance);
					break;
					case "buildingHoveredVisual":
					instance = (ChestVisual)reader.SetPrivateField("buildingHoveredVisual", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "validPlacementColor":
					instance = (ChestVisual)reader.SetPrivateField("validPlacementColor", reader.Read<UnityEngine.Color>(), instance);
					break;
					case "unValidPlacementColor":
					instance = (ChestVisual)reader.SetPrivateField("unValidPlacementColor", reader.Read<UnityEngine.Color>(), instance);
					break;
					case "solidBuildingCollider":
					instance = (ChestVisual)reader.SetPrivateField("solidBuildingCollider", reader.Read<UnityEngine.Collider2D>(), instance);
					break;
					case "interactionBuildingCollider":
					instance = (ChestVisual)reader.SetPrivateField("interactionBuildingCollider", reader.Read<UnityEngine.Collider2D>(), instance);
					break;
					case "building":
					instance = (ChestVisual)reader.SetPrivateField("building", reader.Read<Building>(), instance);
					break;
					case "buildingScoreText":
					instance = (ChestVisual)reader.SetPrivateField("buildingScoreText", reader.Read<TMPro.TextMeshProUGUI>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_ChestVisualArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_ChestVisualArray() : base(typeof(ChestVisual[]), ES3UserType_ChestVisual.Instance)
		{
			Instance = this;
		}
	}
}
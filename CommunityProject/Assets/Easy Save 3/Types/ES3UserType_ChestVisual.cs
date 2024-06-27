using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("chestRenderer", "chestShadowRenderer", "openedSprite", "closedSprite", "openedSpriteShadow", "closedSpriteShadow")]
	public class ES3UserType_ChestVisual : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_ChestVisual() : base(typeof(ChestVisual)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (ChestVisual)obj;
			
			writer.WritePrivateFieldByRef("chestRenderer", instance);
			writer.WritePrivateFieldByRef("chestShadowRenderer", instance);
			writer.WritePrivateFieldByRef("openedSprite", instance);
			writer.WritePrivateFieldByRef("closedSprite", instance);
			writer.WritePrivateFieldByRef("openedSpriteShadow", instance);
			writer.WritePrivateFieldByRef("closedSpriteShadow", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (ChestVisual)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "chestRenderer":
					instance = (ChestVisual)reader.SetPrivateField("chestRenderer", reader.Read<UnityEngine.SpriteRenderer>(), instance);
					break;
					case "chestShadowRenderer":
					instance = (ChestVisual)reader.SetPrivateField("chestShadowRenderer", reader.Read<UnityEngine.SpriteRenderer>(), instance);
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
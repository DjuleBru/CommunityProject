using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("sprite", "overrideSprite", "type", "preserveAspect", "fillCenter", "fillMethod", "fillAmount", "fillClockwise", "fillOrigin", "alphaHitTestMinimumThreshold", "useSpriteMesh", "pixelsPerUnitMultiplier", "material", "onCullStateChanged", "maskable", "color", "raycastTarget", "useLegacyMeshGeneration", "material", "enabled")]
	public class ES3UserType_Image : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Image() : base(typeof(UnityEngine.UI.Image)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (UnityEngine.UI.Image)obj;
			
			writer.WritePropertyByRef("sprite", instance.sprite);
			writer.WritePropertyByRef("overrideSprite", instance.overrideSprite);
			writer.WriteProperty("type", instance.type, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(UnityEngine.UI.Image.Type)));
			writer.WriteProperty("preserveAspect", instance.preserveAspect, ES3Type_bool.Instance);
			writer.WriteProperty("fillCenter", instance.fillCenter, ES3Type_bool.Instance);
			writer.WriteProperty("fillMethod", instance.fillMethod, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(UnityEngine.UI.Image.FillMethod)));
			writer.WriteProperty("fillAmount", instance.fillAmount, ES3Type_float.Instance);
			writer.WriteProperty("fillClockwise", instance.fillClockwise, ES3Type_bool.Instance);
			writer.WriteProperty("fillOrigin", instance.fillOrigin, ES3Type_int.Instance);
			writer.WriteProperty("alphaHitTestMinimumThreshold", instance.alphaHitTestMinimumThreshold, ES3Type_float.Instance);
			writer.WriteProperty("useSpriteMesh", instance.useSpriteMesh, ES3Type_bool.Instance);
			writer.WriteProperty("pixelsPerUnitMultiplier", instance.pixelsPerUnitMultiplier, ES3Type_float.Instance);
			writer.WritePropertyByRef("material", instance.material);
			writer.WriteProperty("onCullStateChanged", instance.onCullStateChanged, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(UnityEngine.UI.MaskableGraphic.CullStateChangedEvent)));
			writer.WriteProperty("maskable", instance.maskable, ES3Type_bool.Instance);
			writer.WriteProperty("color", instance.color, ES3Type_Color.Instance);
			writer.WriteProperty("raycastTarget", instance.raycastTarget, ES3Type_bool.Instance);
			writer.WritePrivateProperty("useLegacyMeshGeneration", instance);
			writer.WritePropertyByRef("material", instance.material);
			writer.WriteProperty("enabled", instance.enabled, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (UnityEngine.UI.Image)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "sprite":
						instance.sprite = reader.Read<UnityEngine.Sprite>(ES3Type_Sprite.Instance);
						break;
					case "overrideSprite":
						instance.overrideSprite = reader.Read<UnityEngine.Sprite>(ES3Type_Sprite.Instance);
						break;
					case "type":
						instance.type = reader.Read<UnityEngine.UI.Image.Type>(ES3Type_enum.Instance);
						break;
					case "preserveAspect":
						instance.preserveAspect = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "fillCenter":
						instance.fillCenter = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "fillMethod":
						instance.fillMethod = reader.Read<UnityEngine.UI.Image.FillMethod>(ES3Type_enum.Instance);
						break;
					case "fillAmount":
						instance.fillAmount = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "fillClockwise":
						instance.fillClockwise = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "fillOrigin":
						instance.fillOrigin = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "alphaHitTestMinimumThreshold":
						instance.alphaHitTestMinimumThreshold = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "useSpriteMesh":
						instance.useSpriteMesh = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "pixelsPerUnitMultiplier":
						instance.pixelsPerUnitMultiplier = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "material":
						instance.material = reader.Read<UnityEngine.Material>(ES3Type_Material.Instance);
						break;
					case "onCullStateChanged":
						instance.onCullStateChanged = reader.Read<UnityEngine.UI.MaskableGraphic.CullStateChangedEvent>();
						break;
					case "maskable":
						instance.maskable = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "color":
						instance.color = reader.Read<UnityEngine.Color>(ES3Type_Color.Instance);
						break;
					case "raycastTarget":
						instance.raycastTarget = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "useLegacyMeshGeneration":
					instance = (UnityEngine.UI.Image)reader.SetPrivateProperty("useLegacyMeshGeneration", reader.Read<System.Boolean>(), instance);
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


	public class ES3UserType_ImageArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_ImageArray() : base(typeof(UnityEngine.UI.Image[]), ES3UserType_Image.Instance)
		{
			Instance = this;
		}
	}
}
using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("outputInventoryFull", "building", "assignedInputHaulersTemplate", "assignedInputHaulersContainer", "assignedOutputHaulersTemplate", "assignedOutputHaulersContainer", "inputArrowImage", "outputArrowImage", "m_CancellationTokenSource")]
	public class ES3UserType_BuildingHaulersUI_World : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_BuildingHaulersUI_World() : base(typeof(BuildingHaulersUI_World)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (BuildingHaulersUI_World)obj;
			
			writer.WritePrivateFieldByRef("outputInventoryFull", instance);
			writer.WritePrivateFieldByRef("building", instance);
			writer.WritePrivateFieldByRef("assignedInputHaulersTemplate", instance);
			writer.WritePrivateFieldByRef("assignedInputHaulersContainer", instance);
			writer.WritePrivateFieldByRef("assignedOutputHaulersTemplate", instance);
			writer.WritePrivateFieldByRef("assignedOutputHaulersContainer", instance);
			writer.WritePrivateFieldByRef("inputArrowImage", instance);
			writer.WritePrivateFieldByRef("outputArrowImage", instance);
			writer.WritePrivateField("m_CancellationTokenSource", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (BuildingHaulersUI_World)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "outputInventoryFull":
					instance = (BuildingHaulersUI_World)reader.SetPrivateField("outputInventoryFull", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "building":
					instance = (BuildingHaulersUI_World)reader.SetPrivateField("building", reader.Read<Building>(), instance);
					break;
					case "assignedInputHaulersTemplate":
					instance = (BuildingHaulersUI_World)reader.SetPrivateField("assignedInputHaulersTemplate", reader.Read<UnityEngine.Transform>(), instance);
					break;
					case "assignedInputHaulersContainer":
					instance = (BuildingHaulersUI_World)reader.SetPrivateField("assignedInputHaulersContainer", reader.Read<UnityEngine.Transform>(), instance);
					break;
					case "assignedOutputHaulersTemplate":
					instance = (BuildingHaulersUI_World)reader.SetPrivateField("assignedOutputHaulersTemplate", reader.Read<UnityEngine.Transform>(), instance);
					break;
					case "assignedOutputHaulersContainer":
					instance = (BuildingHaulersUI_World)reader.SetPrivateField("assignedOutputHaulersContainer", reader.Read<UnityEngine.Transform>(), instance);
					break;
					case "inputArrowImage":
					instance = (BuildingHaulersUI_World)reader.SetPrivateField("inputArrowImage", reader.Read<UnityEngine.UI.Image>(), instance);
					break;
					case "outputArrowImage":
					instance = (BuildingHaulersUI_World)reader.SetPrivateField("outputArrowImage", reader.Read<UnityEngine.UI.Image>(), instance);
					break;
					case "m_CancellationTokenSource":
					instance = (BuildingHaulersUI_World)reader.SetPrivateField("m_CancellationTokenSource", reader.Read<System.Threading.CancellationTokenSource>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_BuildingHaulersUI_WorldArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_BuildingHaulersUI_WorldArray() : base(typeof(BuildingHaulersUI_World[]), ES3UserType_BuildingHaulersUI_World.Instance)
		{
			Instance = this;
		}
	}
}
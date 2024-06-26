using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("humanoidsAssigned", "humanoidsSleeping", "sleepingPS", "housingBuildingUIWorld", "buildingSO", "buildingSizeX", "buildingSizeY", "buildingCamera", "buildingVisual", "interactionCollider", "buildingPlaced")]
	public class ES3UserType_House : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_House() : base(typeof(House)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (House)obj;
			
			writer.WritePrivateField("humanoidsAssigned", instance);
			writer.WritePrivateField("humanoidsSleeping", instance);
			writer.WritePrivateFieldByRef("sleepingPS", instance);
			writer.WritePrivateFieldByRef("housingBuildingUIWorld", instance);
			writer.WritePrivateFieldByRef("buildingSO", instance);
			writer.WritePrivateField("buildingSizeX", instance);
			writer.WritePrivateField("buildingSizeY", instance);
			writer.WritePrivateFieldByRef("buildingCamera", instance);
			writer.WritePrivateFieldByRef("buildingVisual", instance);
			writer.WritePrivateFieldByRef("interactionCollider", instance);
			writer.WritePrivateField("buildingPlaced", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (House)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "humanoidsAssigned":
					instance = (House)reader.SetPrivateField("humanoidsAssigned", reader.Read<System.Collections.Generic.List<Humanoid>>(), instance);
					break;
					case "humanoidsSleeping":
					instance = (House)reader.SetPrivateField("humanoidsSleeping", reader.Read<System.Collections.Generic.List<System.Boolean>>(), instance);
					break;
					case "sleepingPS":
					instance = (House)reader.SetPrivateField("sleepingPS", reader.Read<UnityEngine.ParticleSystem>(), instance);
					break;
					case "housingBuildingUIWorld":
					instance = (House)reader.SetPrivateField("housingBuildingUIWorld", reader.Read<HousingBuildingUIWorld>(), instance);
					break;
					case "buildingSO":
					instance = (House)reader.SetPrivateField("buildingSO", reader.Read<BuildingSO>(), instance);
					break;
					case "buildingSizeX":
					instance = (House)reader.SetPrivateField("buildingSizeX", reader.Read<System.Int32>(), instance);
					break;
					case "buildingSizeY":
					instance = (House)reader.SetPrivateField("buildingSizeY", reader.Read<System.Int32>(), instance);
					break;
					case "buildingCamera":
					instance = (House)reader.SetPrivateField("buildingCamera", reader.Read<Cinemachine.CinemachineVirtualCamera>(), instance);
					break;
					case "buildingVisual":
					instance = (House)reader.SetPrivateField("buildingVisual", reader.Read<BuildingVisual>(), instance);
					break;
					case "interactionCollider":
					instance = (House)reader.SetPrivateField("interactionCollider", reader.Read<UnityEngine.Collider2D>(), instance);
					break;
					case "buildingPlaced":
					instance = (House)reader.SetPrivateField("buildingPlaced", reader.Read<System.Boolean>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_HouseArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_HouseArray() : base(typeof(House[]), ES3UserType_House.Instance)
		{
			Instance = this;
		}
	}
}
using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("buildingSO", "buildingCamera", "buildingVisual", "rb", "buildingCollider", "interactionCollider", "isValidBuildingPlacement", "buildingPlaced", "collideCount", "assignedHumanoid", "assignedInputHauliers", "assignedOutputHauliers", "playerInteractingWithBuilding", "workerInteractingWithBuilding")]
	public class ES3UserType_Building : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Building() : base(typeof(Building)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Building)obj;
			
			writer.WritePrivateFieldByRef("buildingSO", instance);
			writer.WritePrivateFieldByRef("buildingCamera", instance);
			writer.WritePrivateFieldByRef("buildingVisual", instance);
			writer.WritePrivateFieldByRef("rb", instance);
			writer.WritePrivateFieldByRef("buildingCollider", instance);
			writer.WritePrivateFieldByRef("interactionCollider", instance);
			writer.WritePrivateField("isValidBuildingPlacement", instance);
			writer.WritePrivateField("buildingPlaced", instance);
			writer.WritePrivateField("collideCount", instance);
			writer.WritePrivateFieldByRef("assignedHumanoid", instance);
			writer.WritePrivateField("assignedInputHauliers", instance);
			writer.WritePrivateField("assignedOutputHauliers", instance);
			writer.WritePrivateField("playerInteractingWithBuilding", instance);
			writer.WritePrivateField("workerInteractingWithBuilding", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Building)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "buildingSO":
					instance = (Building)reader.SetPrivateField("buildingSO", reader.Read<BuildingSO>(), instance);
					break;
					case "buildingCamera":
					instance = (Building)reader.SetPrivateField("buildingCamera", reader.Read<Cinemachine.CinemachineVirtualCamera>(), instance);
					break;
					case "buildingVisual":
					instance = (Building)reader.SetPrivateField("buildingVisual", reader.Read<BuildingVisual>(), instance);
					break;
					case "rb":
					instance = (Building)reader.SetPrivateField("rb", reader.Read<UnityEngine.Rigidbody2D>(), instance);
					break;
					case "buildingCollider":
					instance = (Building)reader.SetPrivateField("buildingCollider", reader.Read<UnityEngine.Collider2D>(), instance);
					break;
					case "interactionCollider":
					instance = (Building)reader.SetPrivateField("interactionCollider", reader.Read<UnityEngine.Collider2D>(), instance);
					break;
					case "isValidBuildingPlacement":
					instance = (Building)reader.SetPrivateField("isValidBuildingPlacement", reader.Read<System.Boolean>(), instance);
					break;
					case "buildingPlaced":
					instance = (Building)reader.SetPrivateField("buildingPlaced", reader.Read<System.Boolean>(), instance);
					break;
					case "collideCount":
					instance = (Building)reader.SetPrivateField("collideCount", reader.Read<System.Int32>(), instance);
					break;
					case "assignedHumanoid":
					instance = (Building)reader.SetPrivateField("assignedHumanoid", reader.Read<Humanoid>(), instance);
					break;
					case "assignedInputHauliers":
					instance = (Building)reader.SetPrivateField("assignedInputHauliers", reader.Read<System.Collections.Generic.List<Humanoid>>(), instance);
					break;
					case "assignedOutputHauliers":
					instance = (Building)reader.SetPrivateField("assignedOutputHauliers", reader.Read<System.Collections.Generic.List<Humanoid>>(), instance);
					break;
					case "playerInteractingWithBuilding":
					instance = (Building)reader.SetPrivateField("playerInteractingWithBuilding", reader.Read<System.Boolean>(), instance);
					break;
					case "workerInteractingWithBuilding":
					instance = (Building)reader.SetPrivateField("workerInteractingWithBuilding", reader.Read<System.Boolean>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_BuildingArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_BuildingArray() : base(typeof(Building[]), ES3UserType_Building.Instance)
		{
			Instance = this;
		}
	}
}
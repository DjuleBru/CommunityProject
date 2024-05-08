using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("path", "velocity", "rb", "theGScoreToStopAt", "currentWaypoint", "reachedEndOfPath", "pathCalculationTimer", "roamCalculationTimer", "roamCalculationRate", "humanoid", "humanoidVisual", "moveSpeed", "roaming", "moveDirNormalized", "moveDir2DNormalized")]
	public class ES3UserType_HumanoidMovement : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_HumanoidMovement() : base(typeof(HumanoidMovement)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (HumanoidMovement)obj;
			
			writer.WritePrivateField("path", instance);
			writer.WritePrivateField("velocity", instance);
			writer.WritePrivateFieldByRef("rb", instance);
			writer.WritePrivateField("theGScoreToStopAt", instance);
			writer.WritePrivateField("currentWaypoint", instance);
			writer.WritePrivateField("reachedEndOfPath", instance);
			writer.WritePrivateField("pathCalculationTimer", instance);
			writer.WritePrivateField("roamCalculationTimer", instance);
			writer.WritePrivateField("roamCalculationRate", instance);
			writer.WritePrivateFieldByRef("humanoid", instance);
			writer.WritePrivateFieldByRef("humanoidVisual", instance);
			writer.WritePrivateField("moveSpeed", instance);
			writer.WritePrivateField("roaming", instance);
			writer.WritePrivateField("moveDirNormalized", instance);
			writer.WritePrivateField("moveDir2DNormalized", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (HumanoidMovement)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "path":
					instance = (HumanoidMovement)reader.SetPrivateField("path", reader.Read<Pathfinding.Path>(), instance);
					break;
					case "velocity":
					instance = (HumanoidMovement)reader.SetPrivateField("velocity", reader.Read<UnityEngine.Vector3>(), instance);
					break;
					case "rb":
					instance = (HumanoidMovement)reader.SetPrivateField("rb", reader.Read<UnityEngine.Rigidbody2D>(), instance);
					break;
					case "theGScoreToStopAt":
					instance = (HumanoidMovement)reader.SetPrivateField("theGScoreToStopAt", reader.Read<System.Int32>(), instance);
					break;
					case "currentWaypoint":
					instance = (HumanoidMovement)reader.SetPrivateField("currentWaypoint", reader.Read<System.Int32>(), instance);
					break;
					case "reachedEndOfPath":
					instance = (HumanoidMovement)reader.SetPrivateField("reachedEndOfPath", reader.Read<System.Boolean>(), instance);
					break;
					case "pathCalculationTimer":
					instance = (HumanoidMovement)reader.SetPrivateField("pathCalculationTimer", reader.Read<System.Single>(), instance);
					break;
					case "roamCalculationTimer":
					instance = (HumanoidMovement)reader.SetPrivateField("roamCalculationTimer", reader.Read<System.Single>(), instance);
					break;
					case "roamCalculationRate":
					instance = (HumanoidMovement)reader.SetPrivateField("roamCalculationRate", reader.Read<System.Single>(), instance);
					break;
					case "humanoid":
					instance = (HumanoidMovement)reader.SetPrivateField("humanoid", reader.Read<Humanoid>(), instance);
					break;
					case "humanoidVisual":
					instance = (HumanoidMovement)reader.SetPrivateField("humanoidVisual", reader.Read<HumanoidVisual>(), instance);
					break;
					case "moveSpeed":
					instance = (HumanoidMovement)reader.SetPrivateField("moveSpeed", reader.Read<System.Single>(), instance);
					break;
					case "roaming":
					instance = (HumanoidMovement)reader.SetPrivateField("roaming", reader.Read<System.Boolean>(), instance);
					break;
					case "moveDirNormalized":
					instance = (HumanoidMovement)reader.SetPrivateField("moveDirNormalized", reader.Read<UnityEngine.Vector3>(), instance);
					break;
					case "moveDir2DNormalized":
					instance = (HumanoidMovement)reader.SetPrivateField("moveDir2DNormalized", reader.Read<UnityEngine.Vector2>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_HumanoidMovementArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_HumanoidMovementArray() : base(typeof(HumanoidMovement[]), ES3UserType_HumanoidMovement.Instance)
		{
			Instance = this;
		}
	}
}
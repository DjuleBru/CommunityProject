using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("rb", "nextWaypointDistance", "roamPointRadius", "roamCalculationRate")]
	public class ES3UserType_HumanoidMovement : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_HumanoidMovement() : base(typeof(HumanoidMovement)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (HumanoidMovement)obj;
			
			writer.WritePrivateFieldByRef("rb", instance);
			writer.WritePrivateField("nextWaypointDistance", instance);
			writer.WritePrivateField("roamPointRadius", instance);
			writer.WritePrivateField("roamCalculationRate", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (HumanoidMovement)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "rb":
					instance = (HumanoidMovement)reader.SetPrivateField("rb", reader.Read<UnityEngine.Rigidbody2D>(), instance);
					break;
					case "nextWaypointDistance":
					instance = (HumanoidMovement)reader.SetPrivateField("nextWaypointDistance", reader.Read<System.Single>(), instance);
					break;
					case "roamPointRadius":
					instance = (HumanoidMovement)reader.SetPrivateField("roamPointRadius", reader.Read<System.Single>(), instance);
					break;
					case "roamCalculationRate":
					instance = (HumanoidMovement)reader.SetPrivateField("roamCalculationRate", reader.Read<System.Single>(), instance);
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
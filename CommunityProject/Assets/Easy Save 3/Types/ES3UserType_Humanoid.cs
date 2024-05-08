using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("assignedBuilding", "roamDistanceToBuilding", "humanoidSO", "job", "collider2D", "humanoidName", "humanoidActionDesriprion", "workingSpeed", "jobAssigned", "autoAssign", "m_CancellationTokenSource")]
	public class ES3UserType_Humanoid : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Humanoid() : base(typeof(Humanoid)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Humanoid)obj;
			
			writer.WritePrivateFieldByRef("assignedBuilding", instance);
			writer.WritePrivateField("roamDistanceToBuilding", instance);
			writer.WritePrivateFieldByRef("humanoidSO", instance);
			writer.WritePrivateField("job", instance);
			writer.WritePrivateFieldByRef("collider2D", instance);
			writer.WritePrivateField("humanoidName", instance);
			writer.WritePrivateField("humanoidActionDesriprion", instance);
			writer.WritePrivateField("workingSpeed", instance);
			writer.WritePrivateField("jobAssigned", instance);
			writer.WritePrivateField("autoAssign", instance);
			writer.WritePrivateField("m_CancellationTokenSource", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Humanoid)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "assignedBuilding":
					instance = (Humanoid)reader.SetPrivateField("assignedBuilding", reader.Read<Building>(), instance);
					break;
					case "roamDistanceToBuilding":
					instance = (Humanoid)reader.SetPrivateField("roamDistanceToBuilding", reader.Read<System.Single>(), instance);
					break;
					case "humanoidSO":
					instance = (Humanoid)reader.SetPrivateField("humanoidSO", reader.Read<HumanoidSO>(), instance);
					break;
					case "job":
					instance = (Humanoid)reader.SetPrivateField("job", reader.Read<Humanoid.Job>(), instance);
					break;
					case "collider2D":
					instance = (Humanoid)reader.SetPrivateField("collider2D", reader.Read<UnityEngine.Collider2D>(), instance);
					break;
					case "humanoidName":
					instance = (Humanoid)reader.SetPrivateField("humanoidName", reader.Read<System.String>(), instance);
					break;
					case "humanoidActionDesriprion":
					instance = (Humanoid)reader.SetPrivateField("humanoidActionDesriprion", reader.Read<System.String>(), instance);
					break;
					case "workingSpeed":
					instance = (Humanoid)reader.SetPrivateField("workingSpeed", reader.Read<System.Single>(), instance);
					break;
					case "jobAssigned":
					instance = (Humanoid)reader.SetPrivateField("jobAssigned", reader.Read<Humanoid.Job>(), instance);
					break;
					case "autoAssign":
					instance = (Humanoid)reader.SetPrivateField("autoAssign", reader.Read<System.Boolean>(), instance);
					break;
					case "m_CancellationTokenSource":
					instance = (Humanoid)reader.SetPrivateField("m_CancellationTokenSource", reader.Read<System.Threading.CancellationTokenSource>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_HumanoidArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_HumanoidArray() : base(typeof(Humanoid[]), ES3UserType_Humanoid.Instance)
		{
			Instance = this;
		}
	}
}
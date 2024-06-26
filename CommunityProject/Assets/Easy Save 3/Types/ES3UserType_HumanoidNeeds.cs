using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("bestFoodSourceBuilding", "assignedHousing", "humanoid", "humanoidVisual", "humanoidAnimatorManager", "itemEating", "hunger", "eatingRate", "energy", "hungry", "fullBelly", "sleeping", "exhausted", "fullEnergy", "hungerDepletionRate", "energyDepletionRate", "baseHealRate", "energyFillRate", "housingAssignmentAttemptRate", "housingAssignmentAttemptTimer", "m_CancellationTokenSource")]
	public class ES3UserType_HumanoidNeeds : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_HumanoidNeeds() : base(typeof(HumanoidNeeds)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (HumanoidNeeds)obj;
			
			writer.WritePrivateFieldByRef("bestFoodSourceBuilding", instance);
			writer.WritePrivateFieldByRef("assignedHousing", instance);
			writer.WritePrivateFieldByRef("humanoid", instance);
			writer.WritePrivateFieldByRef("humanoidVisual", instance);
			writer.WritePrivateFieldByRef("humanoidAnimatorManager", instance);
			writer.WritePrivateField("itemEating", instance);
			writer.WritePrivateField("hunger", instance);
			writer.WritePrivateField("eatingRate", instance);
			writer.WritePrivateField("energy", instance);
			writer.WritePrivateField("hungry", instance);
			writer.WritePrivateField("fullBelly", instance);
			writer.WritePrivateField("sleeping", instance);
			writer.WritePrivateField("exhausted", instance);
			writer.WritePrivateField("fullEnergy", instance);
			writer.WritePrivateField("hungerDepletionRate", instance);
			writer.WritePrivateField("energyDepletionRate", instance);
			writer.WritePrivateField("baseHealRate", instance);
			writer.WritePrivateField("energyFillRate", instance);
			writer.WritePrivateField("housingAssignmentAttemptRate", instance);
			writer.WritePrivateField("housingAssignmentAttemptTimer", instance);
			writer.WritePrivateField("m_CancellationTokenSource", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (HumanoidNeeds)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "bestFoodSourceBuilding":
					instance = (HumanoidNeeds)reader.SetPrivateField("bestFoodSourceBuilding", reader.Read<Building>(), instance);
					break;
					case "assignedHousing":
					instance = (HumanoidNeeds)reader.SetPrivateField("assignedHousing", reader.Read<Building>(), instance);
					break;
					case "humanoid":
					instance = (HumanoidNeeds)reader.SetPrivateField("humanoid", reader.Read<Humanoid>(), instance);
					break;
					case "humanoidVisual":
					instance = (HumanoidNeeds)reader.SetPrivateField("humanoidVisual", reader.Read<HumanoidVisual>(), instance);
					break;
					case "humanoidAnimatorManager":
					instance = (HumanoidNeeds)reader.SetPrivateField("humanoidAnimatorManager", reader.Read<HumanoidAnimatorManager>(), instance);
					break;
					case "itemEating":
					instance = (HumanoidNeeds)reader.SetPrivateField("itemEating", reader.Read<Item>(), instance);
					break;
					case "hunger":
					instance = (HumanoidNeeds)reader.SetPrivateField("hunger", reader.Read<System.Single>(), instance);
					break;
					case "eatingRate":
					instance = (HumanoidNeeds)reader.SetPrivateField("eatingRate", reader.Read<System.Single>(), instance);
					break;
					case "energy":
					instance = (HumanoidNeeds)reader.SetPrivateField("energy", reader.Read<System.Single>(), instance);
					break;
					case "hungry":
					instance = (HumanoidNeeds)reader.SetPrivateField("hungry", reader.Read<System.Boolean>(), instance);
					break;
					case "fullBelly":
					instance = (HumanoidNeeds)reader.SetPrivateField("fullBelly", reader.Read<System.Boolean>(), instance);
					break;
					case "sleeping":
					instance = (HumanoidNeeds)reader.SetPrivateField("sleeping", reader.Read<System.Boolean>(), instance);
					break;
					case "exhausted":
					instance = (HumanoidNeeds)reader.SetPrivateField("exhausted", reader.Read<System.Boolean>(), instance);
					break;
					case "fullEnergy":
					instance = (HumanoidNeeds)reader.SetPrivateField("fullEnergy", reader.Read<System.Boolean>(), instance);
					break;
					case "hungerDepletionRate":
					instance = (HumanoidNeeds)reader.SetPrivateField("hungerDepletionRate", reader.Read<System.Single>(), instance);
					break;
					case "energyDepletionRate":
					instance = (HumanoidNeeds)reader.SetPrivateField("energyDepletionRate", reader.Read<System.Single>(), instance);
					break;
					case "baseHealRate":
					instance = (HumanoidNeeds)reader.SetPrivateField("baseHealRate", reader.Read<System.Single>(), instance);
					break;
					case "energyFillRate":
					instance = (HumanoidNeeds)reader.SetPrivateField("energyFillRate", reader.Read<System.Single>(), instance);
					break;
					case "housingAssignmentAttemptRate":
					instance = (HumanoidNeeds)reader.SetPrivateField("housingAssignmentAttemptRate", reader.Read<System.Single>(), instance);
					break;
					case "housingAssignmentAttemptTimer":
					instance = (HumanoidNeeds)reader.SetPrivateField("housingAssignmentAttemptTimer", reader.Read<System.Single>(), instance);
					break;
					case "m_CancellationTokenSource":
					instance = (HumanoidNeeds)reader.SetPrivateField("m_CancellationTokenSource", reader.Read<System.Threading.CancellationTokenSource>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_HumanoidNeedsArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_HumanoidNeedsArray() : base(typeof(HumanoidNeeds[]), ES3UserType_HumanoidNeeds.Instance)
		{
			Instance = this;
		}
	}
}
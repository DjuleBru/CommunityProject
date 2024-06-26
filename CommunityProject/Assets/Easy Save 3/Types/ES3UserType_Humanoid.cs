using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("assignedBuilding", "roamDistanceToBuilding", "humanoidSO", "job", "debugJob", "humanoidWork", "humanoidHaul", "humanoidNeeds", "humanoidDungeonCrawl", "humanoidAnimatorManager", "humanoidMovement", "humanoidCarry", "humanoidVisual", "humanoidInteraction", "collider2D", "humanoidName", "humanoidActionDesriprion", "strength", "intelligence", "moveSpeed", "agility", "damage", "armor", "carryCapacity", "jobAssigned", "autoAssign", "autoAssignBestEquipment", "freedFromDungeon", "health", "maxHealth", "healing", "OnHealingStarted", "OnHealingStopped", "behaviorTree", "OnEquipmentChanged", "mainHandItem", "secondaryHandItem", "helmetItem", "bootsItem", "necklaceItem", "ringItem", "mainHandItemDurability", "secondaryHandItemDurability", "helmetItemDurability", "bootsItemDurability", "necklaceItemDurability", "ringItemDurability", "mainHandItemMaxDurability", "secondaryHandItemMaxDurability", "helmetItemMaxDurability", "bootsItemMaxDurability", "necklaceItemMaxDurability", "ringItemMaxDurability", "equippedItems", "m_CancellationTokenSource")]
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
			writer.WritePrivateField("debugJob", instance);
			writer.WritePrivateFieldByRef("humanoidWork", instance);
			writer.WritePrivateFieldByRef("humanoidHaul", instance);
			writer.WritePrivateFieldByRef("humanoidNeeds", instance);
			writer.WritePrivateFieldByRef("humanoidDungeonCrawl", instance);
			writer.WritePrivateFieldByRef("humanoidAnimatorManager", instance);
			writer.WritePrivateFieldByRef("humanoidMovement", instance);
			writer.WritePrivateFieldByRef("humanoidCarry", instance);
			writer.WritePrivateFieldByRef("humanoidVisual", instance);
			writer.WritePrivateFieldByRef("humanoidInteraction", instance);
			writer.WritePrivateFieldByRef("collider2D", instance);
			writer.WritePrivateField("humanoidName", instance);
			writer.WritePrivateField("humanoidActionDesriprion", instance);
			writer.WritePrivateField("strength", instance);
			writer.WritePrivateField("intelligence", instance);
			writer.WritePrivateField("moveSpeed", instance);
			writer.WritePrivateField("agility", instance);
			writer.WritePrivateField("damage", instance);
			writer.WritePrivateField("armor", instance);
			writer.WritePrivateField("carryCapacity", instance);
			writer.WritePrivateField("jobAssigned", instance);
			writer.WritePrivateField("autoAssign", instance);
			writer.WritePrivateField("autoAssignBestEquipment", instance);
			writer.WritePrivateField("freedFromDungeon", instance);
			writer.WritePrivateField("health", instance);
			writer.WritePrivateField("maxHealth", instance);
			writer.WritePrivateField("healing", instance);
			writer.WritePrivateField("OnHealingStarted", instance);
			writer.WritePrivateField("OnHealingStopped", instance);
			writer.WritePrivateFieldByRef("behaviorTree", instance);
			writer.WritePrivateField("OnEquipmentChanged", instance);
			writer.WritePrivateField("mainHandItem", instance);
			writer.WritePrivateField("secondaryHandItem", instance);
			writer.WritePrivateField("helmetItem", instance);
			writer.WritePrivateField("bootsItem", instance);
			writer.WritePrivateField("necklaceItem", instance);
			writer.WritePrivateField("ringItem", instance);
			writer.WritePrivateField("mainHandItemDurability", instance);
			writer.WritePrivateField("secondaryHandItemDurability", instance);
			writer.WritePrivateField("helmetItemDurability", instance);
			writer.WritePrivateField("bootsItemDurability", instance);
			writer.WritePrivateField("necklaceItemDurability", instance);
			writer.WritePrivateField("ringItemDurability", instance);
			writer.WritePrivateField("mainHandItemMaxDurability", instance);
			writer.WritePrivateField("secondaryHandItemMaxDurability", instance);
			writer.WritePrivateField("helmetItemMaxDurability", instance);
			writer.WritePrivateField("bootsItemMaxDurability", instance);
			writer.WritePrivateField("necklaceItemMaxDurability", instance);
			writer.WritePrivateField("ringItemMaxDurability", instance);
			writer.WritePrivateField("equippedItems", instance);
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
					case "debugJob":
					instance = (Humanoid)reader.SetPrivateField("debugJob", reader.Read<System.Boolean>(), instance);
					break;
					case "humanoidWork":
					instance = (Humanoid)reader.SetPrivateField("humanoidWork", reader.Read<HumanoidWork>(), instance);
					break;
					case "humanoidHaul":
					instance = (Humanoid)reader.SetPrivateField("humanoidHaul", reader.Read<HumanoidHaul>(), instance);
					break;
					case "humanoidNeeds":
					instance = (Humanoid)reader.SetPrivateField("humanoidNeeds", reader.Read<HumanoidNeeds>(), instance);
					break;
					case "humanoidDungeonCrawl":
					instance = (Humanoid)reader.SetPrivateField("humanoidDungeonCrawl", reader.Read<HumanoidDungeonCrawl>(), instance);
					break;
					case "humanoidAnimatorManager":
					instance = (Humanoid)reader.SetPrivateField("humanoidAnimatorManager", reader.Read<HumanoidAnimatorManager>(), instance);
					break;
					case "humanoidMovement":
					instance = (Humanoid)reader.SetPrivateField("humanoidMovement", reader.Read<HumanoidMovement>(), instance);
					break;
					case "humanoidCarry":
					instance = (Humanoid)reader.SetPrivateField("humanoidCarry", reader.Read<HumanoidCarry>(), instance);
					break;
					case "humanoidVisual":
					instance = (Humanoid)reader.SetPrivateField("humanoidVisual", reader.Read<HumanoidVisual>(), instance);
					break;
					case "humanoidInteraction":
					instance = (Humanoid)reader.SetPrivateField("humanoidInteraction", reader.Read<HumanoidInteraction>(), instance);
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
					case "strength":
					instance = (Humanoid)reader.SetPrivateField("strength", reader.Read<System.Single>(), instance);
					break;
					case "intelligence":
					instance = (Humanoid)reader.SetPrivateField("intelligence", reader.Read<System.Single>(), instance);
					break;
					case "moveSpeed":
					instance = (Humanoid)reader.SetPrivateField("moveSpeed", reader.Read<System.Single>(), instance);
					break;
					case "agility":
					instance = (Humanoid)reader.SetPrivateField("agility", reader.Read<System.Single>(), instance);
					break;
					case "damage":
					instance = (Humanoid)reader.SetPrivateField("damage", reader.Read<System.Single>(), instance);
					break;
					case "armor":
					instance = (Humanoid)reader.SetPrivateField("armor", reader.Read<System.Single>(), instance);
					break;
					case "carryCapacity":
					instance = (Humanoid)reader.SetPrivateField("carryCapacity", reader.Read<System.Int32>(), instance);
					break;
					case "jobAssigned":
					instance = (Humanoid)reader.SetPrivateField("jobAssigned", reader.Read<Humanoid.Job>(), instance);
					break;
					case "autoAssign":
					instance = (Humanoid)reader.SetPrivateField("autoAssign", reader.Read<System.Boolean>(), instance);
					break;
					case "autoAssignBestEquipment":
					instance = (Humanoid)reader.SetPrivateField("autoAssignBestEquipment", reader.Read<System.Boolean>(), instance);
					break;
					case "freedFromDungeon":
					instance = (Humanoid)reader.SetPrivateField("freedFromDungeon", reader.Read<System.Boolean>(), instance);
					break;
					case "health":
					instance = (Humanoid)reader.SetPrivateField("health", reader.Read<System.Single>(), instance);
					break;
					case "maxHealth":
					instance = (Humanoid)reader.SetPrivateField("maxHealth", reader.Read<System.Single>(), instance);
					break;
					case "healing":
					instance = (Humanoid)reader.SetPrivateField("healing", reader.Read<System.Boolean>(), instance);
					break;
					case "OnHealingStarted":
					instance = (Humanoid)reader.SetPrivateField("OnHealingStarted", reader.Read<System.EventHandler>(), instance);
					break;
					case "OnHealingStopped":
					instance = (Humanoid)reader.SetPrivateField("OnHealingStopped", reader.Read<System.EventHandler>(), instance);
					break;
					case "behaviorTree":
					instance = (Humanoid)reader.SetPrivateField("behaviorTree", reader.Read<BehaviorDesigner.Runtime.BehaviorTree>(), instance);
					break;
					case "OnEquipmentChanged":
					instance = (Humanoid)reader.SetPrivateField("OnEquipmentChanged", reader.Read<System.EventHandler>(), instance);
					break;
					case "mainHandItem":
					instance = (Humanoid)reader.SetPrivateField("mainHandItem", reader.Read<Item>(), instance);
					break;
					case "secondaryHandItem":
					instance = (Humanoid)reader.SetPrivateField("secondaryHandItem", reader.Read<Item>(), instance);
					break;
					case "helmetItem":
					instance = (Humanoid)reader.SetPrivateField("helmetItem", reader.Read<Item>(), instance);
					break;
					case "bootsItem":
					instance = (Humanoid)reader.SetPrivateField("bootsItem", reader.Read<Item>(), instance);
					break;
					case "necklaceItem":
					instance = (Humanoid)reader.SetPrivateField("necklaceItem", reader.Read<Item>(), instance);
					break;
					case "ringItem":
					instance = (Humanoid)reader.SetPrivateField("ringItem", reader.Read<Item>(), instance);
					break;
					case "mainHandItemDurability":
					instance = (Humanoid)reader.SetPrivateField("mainHandItemDurability", reader.Read<System.Single>(), instance);
					break;
					case "secondaryHandItemDurability":
					instance = (Humanoid)reader.SetPrivateField("secondaryHandItemDurability", reader.Read<System.Single>(), instance);
					break;
					case "helmetItemDurability":
					instance = (Humanoid)reader.SetPrivateField("helmetItemDurability", reader.Read<System.Single>(), instance);
					break;
					case "bootsItemDurability":
					instance = (Humanoid)reader.SetPrivateField("bootsItemDurability", reader.Read<System.Single>(), instance);
					break;
					case "necklaceItemDurability":
					instance = (Humanoid)reader.SetPrivateField("necklaceItemDurability", reader.Read<System.Single>(), instance);
					break;
					case "ringItemDurability":
					instance = (Humanoid)reader.SetPrivateField("ringItemDurability", reader.Read<System.Single>(), instance);
					break;
					case "mainHandItemMaxDurability":
					instance = (Humanoid)reader.SetPrivateField("mainHandItemMaxDurability", reader.Read<System.Single>(), instance);
					break;
					case "secondaryHandItemMaxDurability":
					instance = (Humanoid)reader.SetPrivateField("secondaryHandItemMaxDurability", reader.Read<System.Single>(), instance);
					break;
					case "helmetItemMaxDurability":
					instance = (Humanoid)reader.SetPrivateField("helmetItemMaxDurability", reader.Read<System.Single>(), instance);
					break;
					case "bootsItemMaxDurability":
					instance = (Humanoid)reader.SetPrivateField("bootsItemMaxDurability", reader.Read<System.Single>(), instance);
					break;
					case "necklaceItemMaxDurability":
					instance = (Humanoid)reader.SetPrivateField("necklaceItemMaxDurability", reader.Read<System.Single>(), instance);
					break;
					case "ringItemMaxDurability":
					instance = (Humanoid)reader.SetPrivateField("ringItemMaxDurability", reader.Read<System.Single>(), instance);
					break;
					case "equippedItems":
					instance = (Humanoid)reader.SetPrivateField("equippedItems", reader.Read<System.Collections.Generic.List<Item>>(), instance);
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
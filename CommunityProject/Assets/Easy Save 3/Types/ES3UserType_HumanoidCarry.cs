using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("humanoidCarryInventory", "maxCarryAmount", "itemCarrying", "itemToCarry", "itemCarryingList")]
	public class ES3UserType_HumanoidCarry : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_HumanoidCarry() : base(typeof(HumanoidCarry)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (HumanoidCarry)obj;
			
			writer.WritePrivateField("humanoidCarryInventory", instance);
			writer.WritePrivateField("maxCarryAmount", instance);
			writer.WritePrivateField("itemCarrying", instance);
			writer.WritePrivateField("itemToCarry", instance);
			writer.WritePrivateField("itemCarryingList", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (HumanoidCarry)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "humanoidCarryInventory":
					instance = (HumanoidCarry)reader.SetPrivateField("humanoidCarryInventory", reader.Read<Inventory>(), instance);
					break;
					case "maxCarryAmount":
					instance = (HumanoidCarry)reader.SetPrivateField("maxCarryAmount", reader.Read<System.Int32>(), instance);
					break;
					case "itemCarrying":
					instance = (HumanoidCarry)reader.SetPrivateField("itemCarrying", reader.Read<Item>(), instance);
					break;
					case "itemToCarry":
					instance = (HumanoidCarry)reader.SetPrivateField("itemToCarry", reader.Read<Item>(), instance);
					break;
					case "itemCarryingList":
					instance = (HumanoidCarry)reader.SetPrivateField("itemCarryingList", reader.Read<System.Collections.Generic.List<Item>>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_HumanoidCarryArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_HumanoidCarryArray() : base(typeof(HumanoidCarry[]), ES3UserType_HumanoidCarry.Instance)
		{
			Instance = this;
		}
	}
}
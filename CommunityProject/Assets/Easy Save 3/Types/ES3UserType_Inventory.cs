using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("itemList", "restrictedItemList", "hasLimitedSlots", "restrictedInventory", "slotNumberX", "slotNumberY", "totalSlotNumber", "inventoryMaxStackAmount")]
	public class ES3UserType_Inventory : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Inventory() : base(typeof(Inventory)){ Instance = this; priority = 1; }


		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (Inventory)obj;
			
			writer.WritePrivateField("itemList", instance);
			writer.WritePrivateField("restrictedItemList", instance);
			writer.WritePrivateField("hasLimitedSlots", instance);
			writer.WritePrivateField("restrictedInventory", instance);
			writer.WritePrivateField("slotNumberX", instance);
			writer.WritePrivateField("slotNumberY", instance);
			writer.WritePrivateField("totalSlotNumber", instance);
			writer.WritePrivateField("inventoryMaxStackAmount", instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (Inventory)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "itemList":
					instance = (Inventory)reader.SetPrivateField("itemList", reader.Read<System.Collections.Generic.List<Item>>(), instance);
					break;
					case "restrictedItemList":
					instance = (Inventory)reader.SetPrivateField("restrictedItemList", reader.Read<System.Collections.Generic.List<Item>>(), instance);
					break;
					case "hasLimitedSlots":
					instance = (Inventory)reader.SetPrivateField("hasLimitedSlots", reader.Read<System.Boolean>(), instance);
					break;
					case "restrictedInventory":
					instance = (Inventory)reader.SetPrivateField("restrictedInventory", reader.Read<System.Boolean>(), instance);
					break;
					case "slotNumberX":
					instance = (Inventory)reader.SetPrivateField("slotNumberX", reader.Read<System.Int32>(), instance);
					break;
					case "slotNumberY":
					instance = (Inventory)reader.SetPrivateField("slotNumberY", reader.Read<System.Int32>(), instance);
					break;
					case "totalSlotNumber":
					instance = (Inventory)reader.SetPrivateField("totalSlotNumber", reader.Read<System.Int32>(), instance);
					break;
					case "inventoryMaxStackAmount":
					instance = (Inventory)reader.SetPrivateField("inventoryMaxStackAmount", reader.Read<System.Int32>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new Inventory(true,3,3,0);
			ReadObject<T>(reader, instance);
			return instance;
		}
	}


	public class ES3UserType_InventoryArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_InventoryArray() : base(typeof(Inventory[]), ES3UserType_Inventory.Instance)
		{
			Instance = this;
		}
	}
}
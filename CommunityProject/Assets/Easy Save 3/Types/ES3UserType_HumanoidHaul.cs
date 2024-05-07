using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("destinationBuilding", "sourceBuilding", "itemToCarry")]
	public class ES3UserType_HumanoidHaul : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_HumanoidHaul() : base(typeof(HumanoidHaul)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (HumanoidHaul)obj;
			
			writer.WritePrivateFieldByRef("destinationBuilding", instance);
			writer.WritePrivateFieldByRef("sourceBuilding", instance);
			writer.WritePrivateField("itemToCarry", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (HumanoidHaul)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "destinationBuilding":
					instance = (HumanoidHaul)reader.SetPrivateField("destinationBuilding", reader.Read<Building>(), instance);
					break;
					case "sourceBuilding":
					instance = (HumanoidHaul)reader.SetPrivateField("sourceBuilding", reader.Read<Building>(), instance);
					break;
					case "itemToCarry":
					instance = (HumanoidHaul)reader.SetPrivateField("itemToCarry", reader.Read<Item>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_HumanoidHaulArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_HumanoidHaulArray() : base(typeof(HumanoidHaul[]), ES3UserType_HumanoidHaul.Instance)
		{
			Instance = this;
		}
	}
}
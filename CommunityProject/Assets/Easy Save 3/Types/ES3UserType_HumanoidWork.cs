using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("working")]
	public class ES3UserType_HumanoidWork : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_HumanoidWork() : base(typeof(HumanoidWork)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (HumanoidWork)obj;
			
			writer.WritePrivateField("working", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (HumanoidWork)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "working":
					instance = (HumanoidWork)reader.SetPrivateField("working", reader.Read<System.Boolean>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_HumanoidWorkArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_HumanoidWorkArray() : base(typeof(HumanoidWork[]), ES3UserType_HumanoidWork.Instance)
		{
			Instance = this;
		}
	}
}
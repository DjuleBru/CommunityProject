using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("animator")]
	public class ES3UserType_HumanoidAnimatorManager : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_HumanoidAnimatorManager() : base(typeof(HumanoidAnimatorManager)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (HumanoidAnimatorManager)obj;
			
			writer.WritePrivateFieldByRef("animator", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (HumanoidAnimatorManager)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "animator":
					instance = (HumanoidAnimatorManager)reader.SetPrivateField("animator", reader.Read<UnityEngine.Animator>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_HumanoidAnimatorManagerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_HumanoidAnimatorManagerArray() : base(typeof(HumanoidAnimatorManager[]), ES3UserType_HumanoidAnimatorManager.Instance)
		{
			Instance = this;
		}
	}
}
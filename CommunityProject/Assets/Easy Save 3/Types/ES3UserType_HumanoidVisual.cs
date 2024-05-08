using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute()]
	public class ES3UserType_HumanoidVisual : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_HumanoidVisual() : base(typeof(HumanoidVisual)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (HumanoidVisual)obj;
			
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (HumanoidVisual)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_HumanoidVisualArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_HumanoidVisualArray() : base(typeof(HumanoidVisual[]), ES3UserType_HumanoidVisual.Instance)
		{
			Instance = this;
		}
	}
}
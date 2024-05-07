using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute()]
	public class ES3UserType_HumanoidsManager : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_HumanoidsManager() : base(typeof(HumanoidsManager)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (HumanoidsManager)obj;
			
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (HumanoidsManager)obj;
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


	public class ES3UserType_HumanoidsManagerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_HumanoidsManagerArray() : base(typeof(HumanoidsManager[]), ES3UserType_HumanoidsManager.Instance)
		{
			Instance = this;
		}
	}
}
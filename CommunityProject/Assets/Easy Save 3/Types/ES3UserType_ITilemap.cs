using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute()]
	public class ES3UserType_ITilemap : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_ITilemap() : base(typeof(UnityEngine.Tilemaps.ITilemap)){ Instance = this; priority = 1; }


		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (UnityEngine.Tilemaps.ITilemap)obj;
			
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (UnityEngine.Tilemaps.ITilemap)obj;
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

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new UnityEngine.Tilemaps.Tilemap();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}


	public class ES3UserType_ITilemapArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_ITilemapArray() : base(typeof(UnityEngine.Tilemaps.ITilemap[]), ES3UserType_ITilemap.Instance)
		{
			Instance = this;
		}
	}
}
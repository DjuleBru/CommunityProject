using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute()]
	public class ES3UserType_Tilemap : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Tilemap() : base(typeof(UnityEngine.Tilemaps.Tilemap)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (UnityEngine.Tilemaps.Tilemap)obj;
			
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (UnityEngine.Tilemaps.Tilemap)obj;
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


	public class ES3UserType_TilemapArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_TilemapArray() : base(typeof(UnityEngine.Tilemaps.Tilemap[]), ES3UserType_Tilemap.Instance)
		{
			Instance = this;
		}
	}
}
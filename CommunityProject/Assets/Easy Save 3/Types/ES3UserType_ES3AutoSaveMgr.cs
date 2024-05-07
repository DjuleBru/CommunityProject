using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("_current", "key", "saveEvent", "loadEvent", "settings", "autoSaves")]
	public class ES3UserType_ES3AutoSaveMgr : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_ES3AutoSaveMgr() : base(typeof(ES3AutoSaveMgr)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (ES3AutoSaveMgr)obj;
			
			writer.WritePropertyByRef("_current", ES3AutoSaveMgr._current);
			writer.WriteProperty("key", instance.key, ES3Type_string.Instance);
			writer.WriteProperty("saveEvent", instance.saveEvent, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(ES3AutoSaveMgr.SaveEvent)));
			writer.WriteProperty("loadEvent", instance.loadEvent, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(ES3AutoSaveMgr.LoadEvent)));
			writer.WriteProperty("settings", instance.settings, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(ES3SerializableSettings)));
			writer.WriteProperty("autoSaves", instance.autoSaves, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.HashSet<ES3AutoSave>)));
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (ES3AutoSaveMgr)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "_current":
						ES3AutoSaveMgr._current = reader.Read<ES3AutoSaveMgr>();
						break;
					case "key":
						instance.key = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "saveEvent":
						instance.saveEvent = reader.Read<ES3AutoSaveMgr.SaveEvent>();
						break;
					case "loadEvent":
						instance.loadEvent = reader.Read<ES3AutoSaveMgr.LoadEvent>();
						break;
					case "settings":
						instance.settings = reader.Read<ES3SerializableSettings>();
						break;
					case "autoSaves":
						instance.autoSaves = reader.Read<System.Collections.Generic.HashSet<ES3AutoSave>>();
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_ES3AutoSaveMgrArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_ES3AutoSaveMgrArray() : base(typeof(ES3AutoSaveMgr[]), ES3UserType_ES3AutoSaveMgr.Instance)
		{
			Instance = this;
		}
	}
}
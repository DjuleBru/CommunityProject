using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("dungeonEntranceAssigned", "humanoidCarry", "humanoid", "crawlTimer", "crawling")]
	public class ES3UserType_HumanoidDungeonCrawl : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_HumanoidDungeonCrawl() : base(typeof(HumanoidDungeonCrawl)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (HumanoidDungeonCrawl)obj;
			
			writer.WritePrivateFieldByRef("dungeonEntranceAssigned", instance);
			writer.WritePrivateFieldByRef("humanoidCarry", instance);
			writer.WritePrivateFieldByRef("humanoid", instance);
			writer.WritePrivateField("crawlTimer", instance);
			writer.WritePrivateField("crawling", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (HumanoidDungeonCrawl)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "dungeonEntranceAssigned":
					instance = (HumanoidDungeonCrawl)reader.SetPrivateField("dungeonEntranceAssigned", reader.Read<DungeonEntrance>(), instance);
					break;
					case "humanoidCarry":
					instance = (HumanoidDungeonCrawl)reader.SetPrivateField("humanoidCarry", reader.Read<HumanoidCarry>(), instance);
					break;
					case "humanoid":
					instance = (HumanoidDungeonCrawl)reader.SetPrivateField("humanoid", reader.Read<Humanoid>(), instance);
					break;
					case "crawlTimer":
					instance = (HumanoidDungeonCrawl)reader.SetPrivateField("crawlTimer", reader.Read<System.Single>(), instance);
					break;
					case "crawling":
					instance = (HumanoidDungeonCrawl)reader.SetPrivateField("crawling", reader.Read<System.Boolean>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_HumanoidDungeonCrawlArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_HumanoidDungeonCrawlArray() : base(typeof(HumanoidDungeonCrawl[]), ES3UserType_HumanoidDungeonCrawl.Instance)
		{
			Instance = this;
		}
	}
}
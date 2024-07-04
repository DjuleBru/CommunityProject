using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("itemsToRemoveGrid", "obstacleInventory", "itemsRequiredContainer", "itemsRequiredTemplate", "obstacleRemoved")]
	public class ES3UserType_Obstacle : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Obstacle() : base(typeof(Obstacle)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Obstacle)obj;
			
			writer.WritePrivateField("itemsToRemoveGrid", instance);
			writer.WritePrivateField("obstacleInventory", instance);
			writer.WritePrivateFieldByRef("itemsRequiredContainer", instance);
			writer.WritePrivateFieldByRef("itemsRequiredTemplate", instance);
			writer.WritePrivateField("obstacleRemoved", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Obstacle)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "itemsToRemoveGrid":
					instance = (Obstacle)reader.SetPrivateField("itemsToRemoveGrid", reader.Read<System.Collections.Generic.List<Item>>(), instance);
					break;
					case "obstacleInventory":
					instance = (Obstacle)reader.SetPrivateField("obstacleInventory", reader.Read<Inventory>(), instance);
					break;
					case "itemsRequiredContainer":
					instance = (Obstacle)reader.SetPrivateField("itemsRequiredContainer", reader.Read<UnityEngine.Transform>(), instance);
					break;
					case "itemsRequiredTemplate":
					instance = (Obstacle)reader.SetPrivateField("itemsRequiredTemplate", reader.Read<UnityEngine.Transform>(), instance);
					break;
					case "obstacleRemoved":
					instance = (Obstacle)reader.SetPrivateField("obstacleRemoved", reader.Read<System.Boolean>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_ObstacleArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_ObstacleArray() : base(typeof(Obstacle[]), ES3UserType_Obstacle.Instance)
		{
			Instance = this;
		}
	}
}
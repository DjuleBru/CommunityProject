using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("runtimeAnimatorController")]
	public class ES3UserType_Animator : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Animator() : base(typeof(UnityEngine.Animator)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (UnityEngine.Animator)obj;
			
			writer.WritePropertyByRef("runtimeAnimatorController", instance.runtimeAnimatorController);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (UnityEngine.Animator)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "runtimeAnimatorController":
						instance.runtimeAnimatorController = reader.Read<UnityEngine.RuntimeAnimatorController>();
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_AnimatorArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_AnimatorArray() : base(typeof(UnityEngine.Animator[]), ES3UserType_Animator.Instance)
		{
			Instance = this;
		}
	}
}
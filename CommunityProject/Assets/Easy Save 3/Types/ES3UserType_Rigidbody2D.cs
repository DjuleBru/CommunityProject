using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("position", "rotation", "velocity", "angularVelocity", "useAutoMass", "mass", "sharedMaterial", "centerOfMass", "inertia", "drag", "angularDrag", "gravityScale", "bodyType", "useFullKinematicContacts", "isKinematic", "freezeRotation", "constraints", "simulated", "interpolation", "sleepMode", "collisionDetectionMode", "totalForce", "totalTorque", "excludeLayers", "includeLayers", "name")]
	public class ES3UserType_Rigidbody2D : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Rigidbody2D() : base(typeof(UnityEngine.Rigidbody2D)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (UnityEngine.Rigidbody2D)obj;
			
			writer.WriteProperty("position", instance.position, ES3Type_Vector2.Instance);
			writer.WriteProperty("rotation", instance.rotation, ES3Type_float.Instance);
			writer.WriteProperty("velocity", instance.velocity, ES3Type_Vector2.Instance);
			writer.WriteProperty("angularVelocity", instance.angularVelocity, ES3Type_float.Instance);
			writer.WriteProperty("useAutoMass", instance.useAutoMass, ES3Type_bool.Instance);
			writer.WriteProperty("mass", instance.mass, ES3Type_float.Instance);
			writer.WritePropertyByRef("sharedMaterial", instance.sharedMaterial);
			writer.WriteProperty("centerOfMass", instance.centerOfMass, ES3Type_Vector2.Instance);
			writer.WriteProperty("inertia", instance.inertia, ES3Type_float.Instance);
			writer.WriteProperty("drag", instance.drag, ES3Type_float.Instance);
			writer.WriteProperty("angularDrag", instance.angularDrag, ES3Type_float.Instance);
			writer.WriteProperty("gravityScale", instance.gravityScale, ES3Type_float.Instance);
			writer.WriteProperty("bodyType", instance.bodyType, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(UnityEngine.RigidbodyType2D)));
			writer.WriteProperty("useFullKinematicContacts", instance.useFullKinematicContacts, ES3Type_bool.Instance);
			writer.WriteProperty("isKinematic", instance.isKinematic, ES3Type_bool.Instance);
			writer.WriteProperty("freezeRotation", instance.freezeRotation, ES3Type_bool.Instance);
			writer.WriteProperty("constraints", instance.constraints, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(UnityEngine.RigidbodyConstraints2D)));
			writer.WriteProperty("simulated", instance.simulated, ES3Type_bool.Instance);
			writer.WriteProperty("interpolation", instance.interpolation, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(UnityEngine.RigidbodyInterpolation2D)));
			writer.WriteProperty("sleepMode", instance.sleepMode, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(UnityEngine.RigidbodySleepMode2D)));
			writer.WriteProperty("collisionDetectionMode", instance.collisionDetectionMode, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(UnityEngine.CollisionDetectionMode2D)));
			writer.WriteProperty("totalForce", instance.totalForce, ES3Type_Vector2.Instance);
			writer.WriteProperty("totalTorque", instance.totalTorque, ES3Type_float.Instance);
			writer.WriteProperty("excludeLayers", instance.excludeLayers, ES3Type_LayerMask.Instance);
			writer.WriteProperty("includeLayers", instance.includeLayers, ES3Type_LayerMask.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (UnityEngine.Rigidbody2D)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "position":
						instance.position = reader.Read<UnityEngine.Vector2>(ES3Type_Vector2.Instance);
						break;
					case "rotation":
						instance.rotation = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "velocity":
						instance.velocity = reader.Read<UnityEngine.Vector2>(ES3Type_Vector2.Instance);
						break;
					case "angularVelocity":
						instance.angularVelocity = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "useAutoMass":
						instance.useAutoMass = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "mass":
						instance.mass = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "sharedMaterial":
						instance.sharedMaterial = reader.Read<UnityEngine.PhysicsMaterial2D>(ES3Type_PhysicsMaterial2D.Instance);
						break;
					case "centerOfMass":
						instance.centerOfMass = reader.Read<UnityEngine.Vector2>(ES3Type_Vector2.Instance);
						break;
					case "inertia":
						instance.inertia = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "drag":
						instance.drag = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "angularDrag":
						instance.angularDrag = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "gravityScale":
						instance.gravityScale = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "bodyType":
						instance.bodyType = reader.Read<UnityEngine.RigidbodyType2D>();
						break;
					case "useFullKinematicContacts":
						instance.useFullKinematicContacts = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "isKinematic":
						instance.isKinematic = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "freezeRotation":
						instance.freezeRotation = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "constraints":
						instance.constraints = reader.Read<UnityEngine.RigidbodyConstraints2D>();
						break;
					case "simulated":
						instance.simulated = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "interpolation":
						instance.interpolation = reader.Read<UnityEngine.RigidbodyInterpolation2D>();
						break;
					case "sleepMode":
						instance.sleepMode = reader.Read<UnityEngine.RigidbodySleepMode2D>();
						break;
					case "collisionDetectionMode":
						instance.collisionDetectionMode = reader.Read<UnityEngine.CollisionDetectionMode2D>();
						break;
					case "totalForce":
						instance.totalForce = reader.Read<UnityEngine.Vector2>(ES3Type_Vector2.Instance);
						break;
					case "totalTorque":
						instance.totalTorque = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "excludeLayers":
						instance.excludeLayers = reader.Read<UnityEngine.LayerMask>(ES3Type_LayerMask.Instance);
						break;
					case "includeLayers":
						instance.includeLayers = reader.Read<UnityEngine.LayerMask>(ES3Type_LayerMask.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_Rigidbody2DArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_Rigidbody2DArray() : base(typeof(UnityEngine.Rigidbody2D[]), ES3UserType_Rigidbody2D.Instance)
		{
			Instance = this;
		}
	}
}
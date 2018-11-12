using System;
using UnityEngine;

namespace MyGame.Settings
{
	[Serializable]
	public class GameSettings : ScriptableObject
	{
		public float StepCylinderYIncrement = 0.25f;
		public float CylinderPerFrameExpandAmount = 0.04f;
	}
}

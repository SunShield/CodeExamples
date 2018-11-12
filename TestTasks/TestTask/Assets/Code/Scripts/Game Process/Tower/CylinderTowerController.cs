using MyGame.Cylinder;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.GameProcess
{
	public class CylinderTowerController : MonoBehaviour
	{
		public List<CylinderController> Cylinders = new List<CylinderController>();
		public int CylindersAmount {  get { return Cylinders.Count; } }
		public float LastCylinderScale {  get { return Cylinders[Cylinders.Count - 1].Scale; } }

		// here we add first cylinder which is actually on the field
		public void InitByFirstCylinder(CylinderController firstCylinder)
		{
			firstCylinder.Init(null); // first cylinder requires no pool though it nevel will be destroyed.
			Cylinders.Add(firstCylinder);
		}

		public void Expand(CylinderController newCylinder)
		{
			Cylinders.Add(newCylinder);
		}
	}
}

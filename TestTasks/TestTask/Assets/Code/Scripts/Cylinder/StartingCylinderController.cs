using UnityEngine;

namespace MyGame.Cylinder
{
	class StartingCylinderController : CylinderController
	{
		private readonly Vector3 StartingCylinderDefaultScale = new Vector3(1f, 0.25f, 1f);

		protected override Vector3 GetDefaultScale()
		{
			return StartingCylinderDefaultScale;
		}
	}
}

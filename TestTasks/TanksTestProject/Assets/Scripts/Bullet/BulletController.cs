using TankGame.Tank;
using UnityEngine;

namespace TankGame.Bullet
{
	public class BulletController : EnhancedMonoBehaviour
	{
		private BulletMoveController _moveController;

		public UnitType Owner { get; private set; }

		public void Init(UnitType owner)
		{
			InitComponentReferences();
			SetOwner(owner);
		}

		private void InitComponentReferences()
		{
			_moveController = GetComponent<BulletMoveController>();
		}

		private void SetOwner(UnitType owner)
		{
			Owner = owner;
		}

		public void SetMoveParams(Vector3 flyDirection, float flySpeed)
		{
			_moveController.SetParams(flyDirection, flySpeed);
		}

		public void Destroy()
		{
			SimplePool.Despawn(this.gameObject);
		}
	}
}

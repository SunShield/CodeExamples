using TankGame.Bullet;
using UnityEngine;

namespace TankGame.Tank.Shooting
{
	public class TankShootController : EnhancedMonoBehaviour
	{
		[SerializeField] private Transform DirectionShower;
		[SerializeField] private float BulletSpeed;

		private TankController _controller;
		private BulletFactory BulletFactory { get { return BulletFactory.Instance; } }

		public void Init(TankController controller)
		{
			_controller = controller;
		}

		public void Shoot()
		{
			BulletController bullet = ConstructBullet();
			SetBulletParams(bullet);
		}

		private BulletController ConstructBullet()
		{
			return BulletFactory.CreateBullet();
		}

		private void SetBulletParams(BulletController bullet)
		{
			Vector3 shootDirection = GetShootDirection();
			SetBulletPosition(bullet);
			bullet.Init(_controller.Type);
			bullet.SetMoveParams(shootDirection, BulletSpeed);
		}

		private Vector3 GetShootDirection()
		{
			Vector3 shootDirection = DirectionShower.position - Tran.position;
			return shootDirection;
		}

		private void SetBulletPosition(BulletController bullet)
		{
			bullet.Tran.position = DirectionShower.position;
		}
	}
}

using UnityEngine;

namespace TankGame.Bullet
{
	public class BulletFactory : MonoBehaviour
	{
		// Very simple singleton for monobehaviours;
		private static BulletFactory _instance;
		public static BulletFactory Instance
		{
			get
			{
				if(_instance == null)
					_instance = FindObjectOfType<BulletFactory>();
				return _instance;
			}
		}

		[SerializeField] private GameObject BulletPrefab;

		public BulletController CreateBullet()
		{
			BulletController bulletGo = SimplePool.Spawn(BulletPrefab, Vector3.zero, Quaternion.identity).GetComponent<BulletController>();
			return bulletGo;
		}
	}
}

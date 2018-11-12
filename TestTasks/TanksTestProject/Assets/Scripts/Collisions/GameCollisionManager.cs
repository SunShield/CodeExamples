using TankGame.Battle.Spawning;
using TankGame.Bullet;
using UnityEngine;

namespace TankGame.Battle.Interaction
{
	public static class CollisionConstants
	{
		public const string BulletTag = "Bullet";
		public const string WallTag = "Wall";
		public const string PlayerTag = "Player";
		public const string EnemyTag = "Enemy";
	}

	public class GameCollisionManager : MonoBehaviour
	{
		// actually should be encapsulated to separate class like MonoSingleton<T>
		private static GameCollisionManager _instance;
		public static GameCollisionManager Instance
		{
			get
			{
				if (_instance == null)
					_instance = FindObjectOfType<GameCollisionManager>();
				return _instance;
			}
		}

		public void ProcessCollisionInfo(GameObject collidedObject, GameObject objectCollidedWith)
		{
			string collidedObjectTag = collidedObject.tag;
			string objectCollidedWithTag = objectCollidedWith.tag;
			
			if (collidedObjectTag == CollisionConstants.BulletTag)
			{
				BulletController bullet = collidedObject.GetComponent<BulletController>();

				// bullet hits any object except other bullet and owner
				if (objectCollidedWithTag == CollisionConstants.WallTag) 
				{
					bullet.Destroy();
				}

				// player's bullet hits an enemy or enemy bullet hits player
				if (objectCollidedWithTag == CollisionConstants.PlayerTag && bullet.Owner == Tank.UnitType.Enemy ||
				   objectCollidedWithTag == CollisionConstants.EnemyTag && bullet.Owner == Tank.UnitType.Player)
				{
					bullet.Destroy();
					SpawnableUnit unit = objectCollidedWith.GetComponent<SpawnableUnit>();
					unit.Despawn();
				}
			}

			// player tank hits an enemy tank
			if(collidedObjectTag == CollisionConstants.PlayerTag && objectCollidedWithTag == CollisionConstants.EnemyTag)
			{
				SpawnableUnit unit = collidedObject.GetComponent<SpawnableUnit>();
				unit.Despawn();
			}
		}
	}
}

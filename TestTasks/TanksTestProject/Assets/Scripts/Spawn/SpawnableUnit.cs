using UnityEngine;

namespace TankGame.Battle.Spawning
{
	public class SpawnableUnit : MonoBehaviour
	{
		private UnitSpawner _spawner;
		private bool _hasInited = false;

		public void Init(UnitSpawner spawner)
		{
			if (!_hasInited)
			{
				_spawner = spawner;
				DoInit();
				_hasInited = true;
			}
		}

		protected virtual void DoInit()
		{

		}

		public void Despawn()
		{
			SimplePool.Despawn(this.gameObject);
			_spawner.Despawn();
		}
	}
}

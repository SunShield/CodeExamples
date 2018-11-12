using System.Collections.Generic;
using UnityEngine;

namespace TankGame.Battle.Spawning
{
	public class UnitSpawner : MonoBehaviour
	{
		[SerializeField] private SpawnableUnit UnitToSpawn;
		[SerializeField] private int ObjectsToSpawnAmount = 1;
		[SerializeField] private float SpawnDelay = 1f;
		[SerializeField] private List<Transform> SpawnPoints;
		[SerializeField] private List<int> PointsToSpawnOnStartIndexes;

		private int _currentSpawnedObjectsCount;
		private float _timeLastSpawned;
		private bool _startActionsDone;

		private bool AllObjectsSpawned { get { return ObjectsToSpawnAmount == _currentSpawnedObjectsCount; } }

		private void Update()
		{
			if (!_startActionsDone)
			{
				SpawnAllUnitsNeededOnStart();
				_startActionsDone = true;
			}

			if (!AllObjectsSpawned)
			{
				bool isSpawnTime = CheckIfSpawnTime();
				if (isSpawnTime)
				{
					Spawn();
				}
			}
		}

		private void SpawnAllUnitsNeededOnStart()
		{
			foreach(int pointToSpawnOnStartIndex in PointsToSpawnOnStartIndexes)
			{
				Spawn(pointToSpawnOnStartIndex);
			}
		}

		private bool CheckIfSpawnTime()
		{
			return Time.time - _timeLastSpawned >= SpawnDelay;
		}

		public GameObject Spawn(int spawnPointIndex = -1)
		{
			Transform randomSpawnPoint = GetSpawnPointByIndex(spawnPointIndex);
			GameObject spawnedUnitGo = SimplePool.Spawn(UnitToSpawn.gameObject, randomSpawnPoint.position, Quaternion.identity);
			SpawnableUnit spawnedUnit = spawnedUnitGo.GetComponent<SpawnableUnit>();
			spawnedUnit.Init(this);

			_currentSpawnedObjectsCount++;
			SetTimeLastSpawnedToCurrentTime();
			return spawnedUnitGo;
		}

		private Transform GetSpawnPointByIndex(int spawnPointIndex = -1)
		{
			if (spawnPointIndex == -1)
				return GetRandomSpawnPoint();
			return SpawnPoints[spawnPointIndex];
		}

		private Transform GetRandomSpawnPoint()
		{
			int randomSpawnPointIndex = UnityEngine.Random.Range(0, SpawnPoints.Count);
			Transform randomSpawnPoint = SpawnPoints[randomSpawnPointIndex];
			return randomSpawnPoint;
		}

		private void SetTimeLastSpawnedToCurrentTime()
		{
			_timeLastSpawned = Time.time;
		}

		public void Despawn()
		{
			_currentSpawnedObjectsCount--;
			_timeLastSpawned = Time.time;
		}
	}
}

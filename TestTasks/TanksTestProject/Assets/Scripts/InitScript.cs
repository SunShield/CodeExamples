using System.Collections.Generic;
using TankGame.Battle.Spawning;
using TankGame.Tank;
using UnityEngine;

public class InitScript : MonoBehaviour
{
	[SerializeField] private UnitSpawner _enemySpawner;
	[SerializeField] private UnitSpawner _playerSpawner;
	[SerializeField] private InputListener _inputListener;

	private void Start ()
	{
		// Я так и не смог придумать, как сделать это умнее.
		PlayerTankController player = _playerSpawner.Spawn(0).GetComponent<PlayerTankController>();
		_inputListener.Init(player);
	}
}

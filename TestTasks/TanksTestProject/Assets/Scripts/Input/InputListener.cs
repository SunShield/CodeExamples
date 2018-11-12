using TankGame.Tank;
using TankGame.Tank.Movement;
using UnityEngine;

public class InputListener : MonoBehaviour
{
	private PlayerTankController _playerController;

	public void Init(PlayerTankController playerController)
	{
		_playerController = playerController;
	}

	private void Update()
	{
		ProcessMovementInput();
		ProcessShootInput();
	}

	private void ProcessMovementInput()
	{
		MoveDirection currentDir = GetMoveDirectionByInput();
		_playerController.Move(currentDir);
	}

	private MoveDirection GetMoveDirectionByInput()
	{
		if(Input.GetKey(KeyCode.W))
			return MoveDirection.Up;
		if (Input.GetKey(KeyCode.D))
			return MoveDirection.Right;
		if (Input.GetKey(KeyCode.S))
			return MoveDirection.Down;
		if (Input.GetKey(KeyCode.A))
			return MoveDirection.Left;
		return MoveDirection.None;
	}

	private void ProcessShootInput()
	{
		if (Input.GetMouseButtonDown(0))
			_playerController.Shoot();
	}
}

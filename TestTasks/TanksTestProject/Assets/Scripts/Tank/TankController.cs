using TankGame.Battle.Spawning;
using TankGame.Tank.Movement;
using TankGame.Tank.Shooting;
using UnityEngine;

namespace TankGame.Tank
{
	public enum UnitType
	{
		Player = 0,
		Enemy = 1
	}

	public class TankController : SpawnableUnit
	{
		[HideInInspector] public UnitType Type { get; protected set; }

		private TankMoveController _moveController;
		private TankRotateController _rotateController;
		private TankShootController _shootController;

		protected override void DoInit()
		{
			SetUnitType();
			InitComponentReferences();
			InitControllers();
			InitEventSubscriptions();
		}

		protected virtual void SetUnitType()
		{

		}

		private void InitComponentReferences()
		{
			_moveController = GetComponent<TankMoveController>();
			_rotateController = GetComponent<TankRotateController>();
			_shootController = GetComponent<TankShootController>();
		}

		private void InitControllers()
		{
			_moveController.Init();
			_rotateController.Init();
			_shootController.Init(this);
		}

		private void InitEventSubscriptions()
		{
			_moveController.OnMoveDirectionChanged += _rotateController.RotateByMoveDirection;
		}

		public void Move(MoveDirection dir)
		{
			_moveController.Move(dir);
		}

		public void Shoot()
		{
			_shootController.Shoot();
		}
	}
}

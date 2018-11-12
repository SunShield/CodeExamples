using TankGame.Battle.Ai.CollisionDetection;
using TankGame.Battle.Ai.Movement;
using TankGame.Battle.Ai.Shooting;
using UnityEngine;

namespace TankGame.Battle.Ai
{
	public class SimpleTankAi : BaseAi
	{
		[SerializeField] private SimpleAiCollisionSystem CollisionSystemPrefab;
		private SimpleAiMovementCalculator _movementCalculator;
		private SimpleAiShootCalculator _shootCalculator;

		protected override void DoEstablish()
		{
			EstablishMovementCalculator();
			EstablishShootCalculator();
			EstablishCollisionSystem();
		}

		public override void DoMovementActions()
		{
			Controller.Move(_movementCalculator.CurrentDirection);
		}

		public override void DoShootingActions()
		{
			if(_shootCalculator.ShootNow)
			{
				Controller.Shoot();
				_shootCalculator.SetNewShootTime();
			}
		}

		#region Establish

		private void EstablishMovementCalculator()
		{
			_movementCalculator = gameObject.GetComponent<SimpleAiMovementCalculator>();
			_movementCalculator.Init();
		}

		private void EstablishShootCalculator()
		{
			_shootCalculator = gameObject.GetComponent<SimpleAiShootCalculator>();
		}

		private void EstablishCollisionSystem()
		{
			SimpleAiCollisionSystem collisionSystem = InitCollisionSystem();
			SubscribeForOnCollisionMatrixUpdated(collisionSystem);
		}

		private SimpleAiCollisionSystem InitCollisionSystem()
		{
			SimpleAiCollisionSystem collisionSystem = GameObject.Instantiate(CollisionSystemPrefab, transform);
			collisionSystem.transform.localPosition = Vector3.zero;
			collisionSystem.Init();
			return collisionSystem;
		}

		private void SubscribeForOnCollisionMatrixUpdated(SimpleAiCollisionSystem collisionSystem)
		{
			collisionSystem.OnCollisionMatrixUpdated += _movementCalculator.SetCollisionMatrixAndRenewDirectionIfNeeded;
		}

		#endregion
	}
}

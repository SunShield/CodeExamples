using TankGame.Tank.Movement;
using UnityEngine;

namespace TankGame.Battle.Ai.CollisionDetection
{
	public class SimpleAiCollisionListener : MonoBehaviour
	{
		private SimpleAiCollisionSystem _collisionSystem;
		private MoveDirection _direction;

		public void Init(SimpleAiCollisionSystem collisionSystem, MoveDirection direction)
		{
			_collisionSystem = collisionSystem;
			_direction = direction;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			FireOnCollisionEntered(collision);
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			FireOnCollisionExited(collision);
		}

		private void FireOnCollisionEntered(Collider2D collision)
		{
			if (OnCollisionEntered != null)
				OnCollisionEntered(_direction, collision.gameObject.tag);
		}

		private void FireOnCollisionExited(Collider2D collision)
		{
			if (OnCollisionExited != null)
				OnCollisionExited(_direction);
		}

		public delegate void CollisionData(MoveDirection direction, string objectCollidedWithTag);
		public event CollisionData OnCollisionEntered;
		public delegate void LeaveCollisionData(MoveDirection direction);
		public event LeaveCollisionData OnCollisionExited;
	}
}

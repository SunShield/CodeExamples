using UnityEngine;

namespace TankGame.Tank.Movement
{
	public enum MoveDirection
	{
		None = 0,
		Up = 1,
		Right = 2,
		Down = 3,
		Left = 4
	}

	public class TankMoveController : MonoBehaviour
	{
		[SerializeField] private float SpeedMultiplier = 10f;
		
		private Rigidbody2D _rb2d;

		private MoveDirection _currentDir;

		public void Init()
		{
			InitComponentReferences();
		}

		private void InitComponentReferences()
		{
			_rb2d = GetComponent<Rigidbody2D>();
		}

		public void Move(MoveDirection dir)
		{
			bool needsRecalculateMove = CheckNeedsRecalculateMove(dir);
			if (!needsRecalculateMove)
				return;
			
			SetNewMoveDirection(dir);
			SetVelocityByMoveDirection(dir);
		}

		private bool CheckNeedsRecalculateMove(MoveDirection dir)
		{
			return _currentDir != dir;
		}

		private void SetNewMoveDirection(MoveDirection dir)
		{
			_currentDir = dir;
			FireOnMoveDirectionChangedEvent(dir);
		}

		private void FireOnMoveDirectionChangedEvent(MoveDirection dir)
		{
			if (OnMoveDirectionChanged != null)
				OnMoveDirectionChanged(dir);
		}

		private void SetVelocityByMoveDirection(MoveDirection dir)
		{
			Vector3 velocityNormalized = GetNormalizedVelocityByDirection(dir);
			Vector3 finalVelocity = GetFinalVelocity(velocityNormalized);
			SetTankVelocity(finalVelocity);
		}

		private Vector3 GetNormalizedVelocityByDirection(MoveDirection dir)
		{
			switch (dir)
			{
				case MoveDirection.Up:
					return Vector3.up;
				case MoveDirection.Right:
					return Vector3.right;
				case MoveDirection.Down:
					return Vector3.down;
				case MoveDirection.Left:
					return Vector3.left;
				default:
					return Vector3.zero;
			}
		}

		private Vector3 GetFinalVelocity(Vector3 normalizedVolocity)
		{
			Vector3 finalVelocity = normalizedVolocity * SpeedMultiplier;
			return finalVelocity;
		}

		private void SetTankVelocity(Vector3 finalVelocity)
		{
			_rb2d.velocity = finalVelocity;
		}

		public delegate void MoveDirectionChanged(MoveDirection newDirection);
		public event MoveDirectionChanged OnMoveDirectionChanged;
	}
}

using System.Collections.Generic;
using TankGame.Battle.Ai.CollisionDetection;
using TankGame.Battle.Interaction;
using TankGame.Tank.Movement;
using UnityEngine;

namespace TankGame.Battle.Ai.Movement
{
	public class SimpleAiMovementCalculator : MonoBehaviour
	{
		private readonly List<MoveDirection> AllDirections = new List<MoveDirection>
		{
			MoveDirection.Up,
			MoveDirection.Right,
			MoveDirection.Down,
			MoveDirection.Left
		};
		
		[SerializeField] private float DirectionChangeDelay = 1f;

		private CollisionMatrix _collisionMatrix;
		private bool _isInited;

		public MoveDirection CurrentDirection { get; private set; }
		private float _directionChangeDelayTimer;

		private bool NeedChangeDirection { get { return _directionChangeDelayTimer == 0f; } }
		private bool IsCurrentDirectionFree
		{
			get
			{
				if (_collisionMatrix.Directions == null)
					return false;
				return CheckDirectionFree(CurrentDirection);
			}
		}

		public void Init()
		{
			InitCollisionMatrix();
			_isInited = true;
		}

		private void InitCollisionMatrix()
		{
			_collisionMatrix = new CollisionMatrix();
			_collisionMatrix.Init();
		}

		private void Update()
		{
			if (!_isInited)
				return;

			AdvanceChangeDirectionTimer();
			if (NeedChangeDirection)
				SetNewDirection();
		}

		public void SetCollisionMatrixAndRenewDirectionIfNeeded(CollisionMatrix newMatrix)
		{
			_collisionMatrix = newMatrix;
			SetNewDirectionIfNeeded();
		}

		private void SetNewDirectionIfNeeded()
		{
			if (!IsCurrentDirectionFree)
			{
				SetNewDirection();
			}
		}

		private void AdvanceChangeDirectionTimer()
		{
			_directionChangeDelayTimer -= Time.deltaTime;
			if (_directionChangeDelayTimer < 0f)
				_directionChangeDelayTimer = 0f;
		}

		private void SetNewDirection()
		{
			List<MoveDirection> allDirections = new List<MoveDirection>(AllDirections);
			allDirections.Remove(CurrentDirection); // we must change new direction

			FindFreeDirection(allDirections);
			RestoreChangeDirectionTimer();
		}

		private void FindFreeDirection(List<MoveDirection> allDirections)
		{
			while (allDirections.Count > 0)
			{
				int randomDirIndex = UnityEngine.Random.Range(0, allDirections.Count);
				MoveDirection randomDir = allDirections[randomDirIndex];

				bool directionIsFree = CheckDirectionFree(randomDir);
				if (directionIsFree)
				{
					CurrentDirection = randomDir;
					return;
				}
				else
				{
					allDirections.Remove(randomDir);
				}
			}

			CurrentDirection = MoveDirection.None;
		}

		private bool CheckDirectionFree(MoveDirection direction)
		{
			if (direction == MoveDirection.None)
				return false;
			
			string curCollidedObjectInDirectionTag = _collisionMatrix.Directions[direction];
			return curCollidedObjectInDirectionTag != CollisionConstants.WallTag && curCollidedObjectInDirectionTag != CollisionConstants.EnemyTag;
		}

		private void RestoreChangeDirectionTimer()
		{
			_directionChangeDelayTimer = DirectionChangeDelay;
		}
	}
}

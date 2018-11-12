using System.Collections.Generic;
using TankGame.Tank.Movement;
using UnityEngine;

namespace TankGame.Battle.Ai.CollisionDetection
{
	public struct CollisionMatrix
	{
		public Dictionary<MoveDirection, string> Directions;

		public void Init()
		{
			Directions = new Dictionary<MoveDirection, string>();
			Directions.Add(MoveDirection.Up, "");
			Directions.Add(MoveDirection.Right, "");
			Directions.Add(MoveDirection.Down, "");
			Directions.Add(MoveDirection.Left, "");
		}
	}

	public class SimpleAiCollisionSystem : MonoBehaviour
	{
		[SerializeField] private SimpleAiCollisionListener UpListener;
		[SerializeField] private SimpleAiCollisionListener RightListener;
		[SerializeField] private SimpleAiCollisionListener DownListener;
		[SerializeField] private SimpleAiCollisionListener LeftListener;

		public CollisionMatrix Matrix;

		#region Init

		public void Init()
		{
			InitMatrix();
			InitListeners();
			SubscribeForListenersEvents();
		}

		private void InitMatrix()
		{
			Matrix = new CollisionMatrix();
			Matrix.Init();
		}

		private void InitListeners()
		{
			UpListener.Init(this, MoveDirection.Up);
			RightListener.Init(this, MoveDirection.Right);
			DownListener.Init(this, MoveDirection.Left);
			LeftListener.Init(this, MoveDirection.Down);
		}

		private void SubscribeForListenersEvents()
		{
			SubscribeForOnCollisionEnteredEvents();
			SubscribeForOnCollisionExitedEvents();
		}

		private void SubscribeForOnCollisionEnteredEvents()
		{
			UpListener.OnCollisionEntered += OnNewCollision;
			RightListener.OnCollisionEntered += OnNewCollision;
			DownListener.OnCollisionEntered += OnNewCollision;
			LeftListener.OnCollisionEntered += OnNewCollision;
		}

		private void SubscribeForOnCollisionExitedEvents()
		{
			UpListener.OnCollisionExited += OnLeaveCollision;
			RightListener.OnCollisionExited += OnLeaveCollision;
			DownListener.OnCollisionExited += OnLeaveCollision;
			LeftListener.OnCollisionExited += OnLeaveCollision;
		}

		#endregion

		private void OnNewCollision(MoveDirection direction, string objectCollidedWithTag)
		{
			ModifyCollisionMatrixByDir(direction, objectCollidedWithTag);
		}

		private void OnLeaveCollision(MoveDirection direction)
		{
			ModifyCollisionMatrixByDir(direction, "");
		}

		private void ModifyCollisionMatrixByDir(MoveDirection direction, string objectCollidedWithTag)
		{
			Matrix.Directions[direction] = objectCollidedWithTag;

			FireOnCollisionMatrixUpdated();
		}

		private void FireOnCollisionMatrixUpdated()
		{
			if (OnCollisionMatrixUpdated != null)
				OnCollisionMatrixUpdated(Matrix);
		}

		public delegate void CollisionMatrixUpdated(CollisionMatrix newMatrix);
		public event CollisionMatrixUpdated OnCollisionMatrixUpdated;
	}
}

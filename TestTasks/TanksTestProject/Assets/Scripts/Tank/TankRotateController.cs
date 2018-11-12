using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.Tank.Movement
{
	public enum RotateDirection
	{
		Up = 1,
		Right = 2,
		Down = 3,
		Left = 4
	}

	public class TankRotateController : MonoBehaviour
	{
		private Transform _transform;

		private RotateDirection _rotateDirection;

		public void Init()
		{
			_transform = GetComponent<Transform>();
		}

		public void RotateByMoveDirection(MoveDirection direction)
		{
			SetRotateDirectionByMoveDiretion(direction);
			SetRotationByRotateDirection();
		}

		private void SetRotateDirectionByMoveDiretion(MoveDirection direction)
		{
			switch(direction)
			{
				case MoveDirection.Up:
					_rotateDirection = RotateDirection.Up;
					return;
				case MoveDirection.Right:
					_rotateDirection = RotateDirection.Right;
					return;
				case MoveDirection.Down:
					_rotateDirection = RotateDirection.Down;
					return;
				case MoveDirection.Left:
					_rotateDirection = RotateDirection.Left;
					return;
			}
		}

		private void SetRotationByRotateDirection()
		{
			Vector3 newRotation = _transform.localEulerAngles;
			switch (_rotateDirection)
			{
				case RotateDirection.Up:
					newRotation = new Vector3(0f, 0f, 0f);
					break;
				case RotateDirection.Right:
					newRotation = new Vector3(0f, 0f, 270f);
					break;
				case RotateDirection.Down:
					newRotation = new Vector3(0f, 0f, 180f);
					break;
				case RotateDirection.Left:
					newRotation = new Vector3(0f, 0f, 90f);
					break;
			}
			SetTransformEulerAngles(newRotation);
		}

		private void SetTransformEulerAngles(Vector3 newRotation)
		{
			_transform.localEulerAngles = newRotation;
		}
	}
}

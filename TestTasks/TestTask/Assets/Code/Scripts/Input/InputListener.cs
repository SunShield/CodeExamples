using UnityEngine;

namespace MyGame.GameInput
{
	public class InputListener : MonoBehaviour
	{
		private GameController _gameController;

		public void Init(GameController gameController)
		{
			_gameController = gameController;
		}

		private void Update()
		{
			if(Input.GetMouseButtonDown(0))
			{
				if (_gameController.IsGameLost)
					_gameController.RestartGame();
				else
					_gameController.StartMakingTurn();
			}

			if(Input.GetMouseButtonUp(0))
			{
				_gameController.StopMakingTurn();
			}
		}
	}
}

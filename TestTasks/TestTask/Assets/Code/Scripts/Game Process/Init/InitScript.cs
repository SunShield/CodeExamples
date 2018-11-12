using MyGame;
using MyGame.GameInput;
using UnityEngine;

public class InitScript : MonoBehaviour
{
	[SerializeField] private GameController GameController;
	[SerializeField] private InputListener InputListener;

	private void Start()
	{
		InputListener.Init(GameController);
		GameController.StartGame();
	}
}

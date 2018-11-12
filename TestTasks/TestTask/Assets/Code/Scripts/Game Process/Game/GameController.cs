using MyGame.Cylinder;
using MyGame.GameProcess;
using MyGame.GameProcess.Animation;
using MyGame.GameProcess.CylinderPlacement;
using MyGame.Settings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGame
{
	public class GameController : MonoBehaviour
	{
		[SerializeField] private GameSettings Settings;

		[SerializeField] private CylinderController DefaultCylinder;
		[SerializeField] private CylinderTowerController TowerController;
		[SerializeField] private CylinderPlacer CylinderPlacer;
		[SerializeField] private CameraPositionController CameraPositionController;
		[SerializeField] private LoseAnimator LoseAnimator;
		[SerializeField] private PerfectMoveAnimator PerfectMoveAnimator;

		private bool _actionsEnabled = true;
		public bool IsGameLost { get; private set; }

		#region Starting Game

		public void StartGame()
		{
			Init();
			SetSettings();
			SubscribeOnEvents();
			SetActionsEnabled(true);
		}

		private void Init()
		{
			CylinderPlacer.Init();
			TowerController.InitByFirstCylinder(DefaultCylinder);
			CameraPositionController.Init();
		}

		private void SetSettings()
		{
			CylinderPlacer.SetSettings(Settings.StepCylinderYIncrement, Settings.CylinderPerFrameExpandAmount);
			CameraPositionController.SetSettings(Settings.StepCylinderYIncrement);
		}

		private void SubscribeOnEvents()
		{
			CylinderPlacer.OnAutoLoseConditionFullified += LoseGame;
			LoseAnimator.OnAnimationComplete += CompleteAnimatingLose;
			PerfectMoveAnimator.OnAnimationComplete += CompleteAnimatingPerfectMove;
		}

		#endregion

		#region Game Actions

		public void StartMakingTurn()
		{
			if (!_actionsEnabled)
				return;

			CylinderPlacer.TryPlaceNewCylinder(TowerController.CylindersAmount, TowerController.LastCylinderScale);
		}

		public void StopMakingTurn()
		{
			if (!_actionsEnabled)
				return;

			CylinderPlaceArgs placeArgs = CylinderPlacer.StopPlacingCylinder();
			DoActionsBasedOnPlaceArgs(placeArgs);
		}

		private void DoActionsBasedOnPlaceArgs(CylinderPlaceArgs placeArgs)
		{
			if (placeArgs == null)
				return;

			switch(placeArgs.PlaceResult)
			{
				case PlaceResult.Regular:
					ExpandTower(placeArgs.Cylinder);
					UpdateCamPosition();
					break;
				case PlaceResult.Perfect:
					ExpandTower(placeArgs.Cylinder);
					SetCylinderPerfect(placeArgs.Cylinder);
					UpdateCamPosition();
					SetActionsEnabled(false);
					AnimatePerfectMove();
					break;
				case PlaceResult.Incorrent:
					LoseGame(placeArgs.Cylinder);
					break;
			}
		}

		private void ExpandTower(CylinderController newCylinder)
		{
			TowerController.Expand(newCylinder);
		}

		private void UpdateCamPosition()
		{
			CameraPositionController.SetCameraPosition(TowerController.CylindersAmount);
		}
		
		private void SetCylinderPerfect(CylinderController currentCylinder)
		{
			currentCylinder.MakePerfect();
		}

		private void AnimatePerfectMove()
		{
			PerfectMoveAnimator.AnimatePerfectMove(TowerController.Cylinders);
		}

		private void CompleteAnimatingPerfectMove()
		{
			SetActionsEnabled(true);
		}

		private void SetActionsEnabled(bool enable)
		{
			_actionsEnabled = enable;
		}

		#endregion

		public void LoseGame(CylinderController cylinderCausedLose)
		{
			SetActionsEnabled(false);
			AnimateLose(cylinderCausedLose);
		}

		private void AnimateLose(CylinderController cylinderCausedLose)
		{
			LoseAnimator.StartAnimateLose(cylinderCausedLose);
		}

		private void CompleteAnimatingLose(CylinderController cylinderCausedLose)
		{
			cylinderCausedLose.Destroy();
			CameraPositionController.SetCameraLoosePosition(TowerController.CylindersAmount);
			IsGameLost = true;
		}

		public void RestartGame()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}

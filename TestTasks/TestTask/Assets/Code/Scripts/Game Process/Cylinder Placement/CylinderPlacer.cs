using MyGame.Cylinder;
using System;
using UnityEngine;

namespace MyGame.GameProcess.CylinderPlacement
{
	public enum PlaceResult
	{
		Regular = 0,
		Perfect = 1,
		Incorrent = 2
	}

	public class CylinderPlaceArgs
	{
		public CylinderController Cylinder;
		public PlaceResult PlaceResult;
	}

	public class CylinderPlacer : MonoBehaviour
	{
		private const float MinCylinderScale = 0.1f;
		private const float MaxCylinderScale = 1f;
		private const float CylinderRelativeScaleToAutoLose = 1.1f;
		private const float PerfectPlaceScaleMaxDifference = 0.05f;

		[SerializeField] private CylinderFactory Factory;

		// settings
		private float _cylinderYStepIncrement;
		private float _cylinderPerFrameExpandAmount;

		// current place step data
		private int _treeCylindersAmount;
		private float _lastCylinderScale;

		private CylinderController _currentCylinder;
		private float CurrentCylinderScale { get { return _currentCylinder.Scale; } }
		private bool IsPlacingCylinder { get { return _currentCylinder != null; } }

		#region Establish

		public void Init()
		{
			Factory.Init();
		}

		public void SetSettings(float cylinderYStepIncrement, float cylinderPerFrameExpandAmount)
		{
			_cylinderYStepIncrement = cylinderYStepIncrement;
			_cylinderPerFrameExpandAmount = cylinderPerFrameExpandAmount;
		}

		#endregion

		#region Starting Placing Cylinder

		public void TryPlaceNewCylinder(int treeCylindersAmount, float lastCylinderScale)
		{
			StoreCurrentPlaceData(treeCylindersAmount, lastCylinderScale);
			CylinderController newCylinder = CreateNewCylinder();
			StoreCylinder(newCylinder);
		}

		private void StoreCurrentPlaceData(int treeCylindersAmount, float lastCylinderScale)
		{
			_treeCylindersAmount = treeCylindersAmount;
			_lastCylinderScale = lastCylinderScale;
		}

		private CylinderController CreateNewCylinder()
		{
			CylinderController newCylinder = Factory.Get();
			SetCylinderCoords(newCylinder);
			return newCylinder;
		}

		private void SetCylinderCoords(CylinderController newCylinder)
		{
			float cylinderY = GetCylinderYCoord();
			Vector3 newCylinderPos = new Vector3(0f, cylinderY, 0f);
			newCylinder.Tran.position = newCylinderPos;
		}

		private float GetCylinderYCoord()
		{
			return _treeCylindersAmount * _cylinderYStepIncrement;
		}

		private void StoreCylinder(CylinderController newCylinder)
		{
			_currentCylinder = newCylinder;
		}

		#endregion

		private void Update()
		{
			if(IsPlacingCylinder)
			{
				ExpandCylinder();
				bool autoLose = CheckAutoLoseCondition();
				if (autoLose)
					DoAutoLoseActions();
			}
		}

		#region Placing Cylinder Process

		private void ExpandCylinder()
		{
			_currentCylinder.Expand(_cylinderPerFrameExpandAmount * Time.deltaTime);
		}

		private bool CheckAutoLoseCondition()
		{
			bool autoLoseCondition = CurrentCylinderScale > _lastCylinderScale * CylinderRelativeScaleToAutoLose;
			return autoLoseCondition;
		}

		private void DoAutoLoseActions()
		{
			CylinderController currentCylinder = _currentCylinder;
			UnstoreCylinder();
			FireOnAutoLoseConditionFullified(currentCylinder);
		}

		private void FireOnAutoLoseConditionFullified(CylinderController currentCylinder)
		{
			if (OnAutoLoseConditionFullified != null)
				OnAutoLoseConditionFullified(currentCylinder);
		}

		#endregion

		#region Stopping Placing Cylinder

		public CylinderPlaceArgs StopPlacingCylinder()
		{
			if (_currentCylinder == null)
				return null;

			CylinderController currentCylinder = _currentCylinder;
			UnstoreCylinder();
			PlaceResult placeResult = GetPlaceResultByCylinderScale(currentCylinder);
			CylinderPlaceArgs placeArgs = ConstructCylinderPlaceArgs(currentCylinder, placeResult);
			return placeArgs;
		}

		private void UnstoreCylinder()
		{
			_currentCylinder = null;
		}

		private PlaceResult GetPlaceResultByCylinderScale(CylinderController currentCylinder)
		{
			float scale = currentCylinder.Scale;
			if (scale > _lastCylinderScale)
				return PlaceResult.Incorrent;
			else if (_lastCylinderScale - scale <= PerfectPlaceScaleMaxDifference)
				return PlaceResult.Perfect;
			return PlaceResult.Regular;
		}

		private CylinderPlaceArgs ConstructCylinderPlaceArgs(CylinderController currentCylinder, PlaceResult placeResult)
		{
			CylinderPlaceArgs placeArgs = new CylinderPlaceArgs
			{
				Cylinder = currentCylinder,
				PlaceResult = placeResult
			};
			return placeArgs;
		}

		#endregion

		public event Action<CylinderController> OnAutoLoseConditionFullified;
	}
}

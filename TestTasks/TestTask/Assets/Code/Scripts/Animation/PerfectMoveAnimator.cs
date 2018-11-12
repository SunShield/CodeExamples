using MyGame.Cylinder;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.GameProcess.Animation
{
	public class PerfectMoveAnimator : MonoBehaviour
	{
		private const float DelayBeforeNextCylinderAnim = 0.2f;
		private const float CylinderAnimateTime = 0.2f;
		private const int IterationsAmount = 50;

		private const float LastCylinderExpandAmount = 0.4f;
		private const float LastCylinderShrinkAmount = 0.2f;
		private const float CylinderExpandAmount = 0.3f;
		private const float CylinderShrinkMultiplier = 0.8f;
		private const float MaxScale = 1f;
		private const float MinScale = 0.1f;

		public void AnimatePerfectMove(List<CylinderController> cylinders)
		{
			StartCoroutine(AnimatePerfectMoveCoroutine(cylinders));
		}

		private IEnumerator AnimatePerfectMoveCoroutine(List<CylinderController> cylinders)
		{
			StartCoroutine(AnimateLastPlacedCylinder(cylinders[cylinders.Count - 1]));
			yield return new WaitForSeconds(DelayBeforeNextCylinderAnim);
			for(int i = cylinders.Count - 2; i >= 0; i--)
			{
				StartCoroutine(AnimateCylinder(cylinders[i], i));
				yield return new WaitForSeconds(DelayBeforeNextCylinderAnim);
			}
		}

		private IEnumerator AnimateLastPlacedCylinder(CylinderController cylinder)
		{
			for (int i = 0; i < IterationsAmount; i++)
			{
				float expandAmount = LastCylinderExpandAmount / IterationsAmount;
				cylinder.Expand(expandAmount);
				yield return new WaitForSeconds(CylinderAnimateTime / IterationsAmount);
			}

			float scaleStored = cylinder.Scale;
			for (int i = 0; i < IterationsAmount; i++)
			{
				float shrinkAmount = GetShrinkAmountForFirstCylinder(scaleStored);
				cylinder.Expand(shrinkAmount);
				yield return new WaitForSeconds(CylinderAnimateTime / IterationsAmount);
			}
		}

		private float GetShrinkAmountForFirstCylinder(float scaleStored)
		{
			float shrinkAmount = -LastCylinderShrinkAmount / IterationsAmount;
			if (scaleStored - LastCylinderShrinkAmount > MaxScale)
				shrinkAmount = -(scaleStored - MaxScale) / IterationsAmount;
			return shrinkAmount;
		}

		private IEnumerator AnimateCylinder(CylinderController cylinder, int cylinderIndex)
		{
			for (int i = 0; i < IterationsAmount; i++)
			{
				float expandAmount = CylinderExpandAmount / IterationsAmount;
				cylinder.Expand(expandAmount);
				yield return new WaitForSeconds(CylinderAnimateTime / IterationsAmount);
			}

			float scaleStored = cylinder.Scale;
			for (int i = 0; i < IterationsAmount; i++)
			{
				float shrinkAmount = GetShrinkAmountForRegularCylinder(cylinder, scaleStored);
				cylinder.Expand(shrinkAmount);
				yield return new WaitForSeconds(CylinderAnimateTime / IterationsAmount);
			}

			if (cylinderIndex == 0)
				FireOnAnimCompletedEvent();
		}

		private float GetShrinkAmountForRegularCylinder(CylinderController cylinder, float scaleStored)
		{
			float scaleDifference = cylinder.Type != CylinderType.PerfectMoved 
																? (scaleStored - (scaleStored - CylinderExpandAmount) * CylinderShrinkMultiplier) 
																: CylinderExpandAmount;
			if (scaleStored - scaleDifference < MinScale)
				scaleDifference = scaleStored - MinScale;
			float shrinkAmount = -scaleDifference / IterationsAmount;
			return shrinkAmount;
		}

		private void FireOnAnimCompletedEvent()
		{
			if (OnAnimationComplete != null)
				OnAnimationComplete();
		}

		public event Action OnAnimationComplete;
	}
}

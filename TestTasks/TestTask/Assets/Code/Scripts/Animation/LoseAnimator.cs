using MyGame.Cylinder;
using System;
using System.Collections;
using UnityEngine;

namespace MyGame.GameProcess.Animation
{
	public class LoseAnimator : MonoBehaviour
	{
		private const int IterationsNumber = 50;

		[SerializeField] private Material CylinderRecolorableMaterial;
		[SerializeField] private float AnimTime = 0.5f;
		[SerializeField] private float LoseCylinderFinalColorR = 1f;

		public void StartAnimateLose(CylinderController cylinderLostGame)
		{
			SetCylinderMaterial(cylinderLostGame);
			StartCoroutine(AnimateLoseCoroutine(cylinderLostGame));
		}

		private void SetCylinderMaterial(CylinderController cylinderLostGame)
		{
			cylinderLostGame.MeshRenderer.material = CylinderRecolorableMaterial;
		}

		private IEnumerator AnimateLoseCoroutine(CylinderController cylinderLostGame)
		{
			MeshRenderer renderer = cylinderLostGame.MeshRenderer;

			for (int i = 0; i < IterationsNumber; i++) 
			{
				Color curColor = renderer.material.color;
				Color newColor = new Color(curColor.r + LoseCylinderFinalColorR / IterationsNumber, 
										   curColor.g - 0.1f, 
										   curColor.b - 0.1f, 
										   1);
				renderer.material.color = newColor;
				yield return new WaitForSeconds(AnimTime / IterationsNumber);
			}

			FireOnAnimationCompleteEvent(cylinderLostGame);
		}

		private void FireOnAnimationCompleteEvent(CylinderController cylinderLostGame)
		{
			if (OnAnimationComplete != null)
				OnAnimationComplete(cylinderLostGame);
		}

		public event Action<CylinderController> OnAnimationComplete;
	}
}

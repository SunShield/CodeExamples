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

		private CylinderController _loseCylinderStored;

		public void StartAnimateLose(CylinderController cylinderLostGame)
		{
			_loseCylinderStored = cylinderLostGame;
			SetCylinderMaterial();
			StartCoroutine(AnimateLoseCoroutine());
		}

		private void SetCylinderMaterial()
		{
			_loseCylinderStored.MeshRenderer.material = CylinderRecolorableMaterial;
		}

		private IEnumerator AnimateLoseCoroutine()
		{
			MeshRenderer renderer = _loseCylinderStored.MeshRenderer;

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

			FireOnAnimationCompleteEvent();
		}

		private void FireOnAnimationCompleteEvent()
		{
			if (OnAnimationComplete != null)
				OnAnimationComplete(_loseCylinderStored);
		}

		public void ClearAnimation()
		{
			RestoreLoseCylinderColor();
			RestoreState();
		}

		private void RestoreLoseCylinderColor()
		{
			MeshRenderer renderer = _loseCylinderStored.MeshRenderer;
			Color defColor = Color.white;
			renderer.material.color = defColor;
		}

		private void RestoreState()
		{
			_loseCylinderStored = null;
		}

		public event Action<CylinderController> OnAnimationComplete;
	}
}

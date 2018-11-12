using UnityEngine;

namespace TankGame.Battle.Ai.Shooting
{
	public class SimpleAiShootCalculator : MonoBehaviour
	{
		[SerializeField] private Vector2 ShootDelayRange = new Vector2(0.65f, 1.2f);
		private float _shootDelayTimer;

		public bool ShootNow { get { return _shootDelayTimer == 0f; } }

		private void Update()
		{
			AdvanceShootDelayTimer();
		}

		private void AdvanceShootDelayTimer()
		{
			_shootDelayTimer -= Time.deltaTime;
			if (_shootDelayTimer < 0f)
				_shootDelayTimer = 0f;
		}

		public void SetNewShootTime()
		{
			_shootDelayTimer = UnityEngine.Random.Range(ShootDelayRange.x, ShootDelayRange.y);
		}
	}
}

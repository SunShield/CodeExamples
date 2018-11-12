using TankGame.Tank;
using UnityEngine;

namespace TankGame.Battle.Ai
{
	public class BaseAi : MonoBehaviour
	{
		protected TankController Controller;

		private bool _isEstablished;

		public void Establish(TankController newController)
		{
			if (_isEstablished)
				return;

			Controller = newController;
			DoEstablish();

			_isEstablished = true;
		}

		protected virtual void DoEstablish() { }
		public virtual void GatherData() { }
		public virtual void DoMovementActions() { }
		public virtual void DoShootingActions() { }
	}
}

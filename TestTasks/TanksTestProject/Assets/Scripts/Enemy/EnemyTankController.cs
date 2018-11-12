using TankGame.Battle.Ai;
using UnityEngine;

namespace TankGame.Tank
{
	public class EnemyTankController : TankController
	{
		[SerializeField] private BaseAi AiModule;

		protected override void SetUnitType()
		{
			Type = UnitType.Enemy;
		}

		private void Update()
		{
			EstablishAi();
			GatherAiData();
			DoAiMovementActions();
			DoAiShootingActions();
		}

		private void EstablishAi()
		{
			AiModule.Establish(this);
		}

		private void GatherAiData()
		{
			AiModule.GatherData();
		}

		private void DoAiMovementActions()
		{
			AiModule.DoMovementActions();
		}

		private void DoAiShootingActions()
		{
			AiModule.DoShootingActions();
		}
	}
}

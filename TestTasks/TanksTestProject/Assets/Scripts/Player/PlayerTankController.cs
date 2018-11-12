namespace TankGame.Tank
{
	public class PlayerTankController : TankController
	{
		protected override void SetUnitType()
		{
			Type = UnitType.Player;
		}
	}
}

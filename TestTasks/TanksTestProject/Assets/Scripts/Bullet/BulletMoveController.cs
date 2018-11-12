using UnityEngine;

public class BulletMoveController : EnhancedMonoBehaviour
{
	private Vector3 _flyDirection;
	private float _flySpeed;

	public void SetParams(Vector3 flyDirection, float flySpeed)
	{
		_flyDirection = flyDirection;
		_flySpeed = flySpeed;
	}

	private void Update()
	{
		FlyIfNeeded();
	}

	private void FlyIfNeeded()
	{
		bool flies = CheckIfFlies();
		if (!flies)
			return;

		Vector3 flyVector = CalculateFlyVector();
		DoFly(flyVector);
	}

	private bool CheckIfFlies()
	{
		return _flyDirection != Vector3.zero;
	}

	private Vector3 CalculateFlyVector()
	{
		Vector3 flyVector = _flyDirection * _flySpeed;
		return flyVector;
	}

	private void DoFly(Vector3 flyVector)
	{
		Tran.position += flyVector * Time.deltaTime;
	}
}

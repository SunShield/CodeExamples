using UnityEngine;

public class CameraPositionController : MonoBehaviour
{
	[SerializeField] private Camera Camera;
	private Transform _camTran;

	private float _cylinderYStepIncrement;
	private float _defaultY;

	public void Init()
	{
		_camTran = Camera.transform;
		_defaultY = Camera.transform.position.y;
	}

	public void SetSettings(float cylinderYStepIncrement)
	{
		_cylinderYStepIncrement = cylinderYStepIncrement;
	}

	public void SetCameraPosition(int step)
	{
		float newY = _defaultY + step * _cylinderYStepIncrement;
		Vector3 camPos = _camTran.position;
		Vector3 newPos = new Vector3(camPos.x, newY, camPos.z);
		_camTran.position = newPos;
	}

	public void SetCameraLoosePosition(int steps)
	{
		SetCameraPosition(0);
		Vector3 camPos = _camTran.position;
		Vector3 newPos = camPos - _camTran.forward * (5f + steps / 9f);
		_camTran.position = newPos;
	}
}

using UnityEngine;

public class CameraPositionController : MonoBehaviour
{
	[SerializeField] private Camera Camera;
	private Transform _camTran;

	private float _cylinderYStepIncrement;
	private Vector3 _defaultPos;
	private float DefaultY {  get { return _defaultPos.y; } }

	public void Init()
	{
		_camTran = Camera.transform;
		_defaultPos = Camera.transform.position;
	}

	public void SetSettings(float cylinderYStepIncrement)
	{
		_cylinderYStepIncrement = cylinderYStepIncrement;
	}

	public void SetCameraPosition(int step)
	{
		float newY = DefaultY + step * _cylinderYStepIncrement;
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

	public void ResetPosition()
	{
		_camTran.position = _defaultPos;
	}
}

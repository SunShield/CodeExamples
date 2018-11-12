using UnityEngine;

public class EnhancedMonoBehaviour : MonoBehaviour
{
	private Transform _transform;
	public Transform Tran
	{
		get
		{
			if (_transform == null)
				_transform = GetComponent<Transform>();
			return _transform;
		}
	}
}

using UnityEngine;

namespace MyGame.Cylinder
{
	public class CylinderFactory : MonoBehaviour
	{
		[SerializeField] private CylinderController CylinderPrefab;
		private ObjectPool _pool;

		public void Init()
		{
			_pool = new ObjectPool(CylinderPrefab.gameObject);
		}

		public CylinderController Get()
		{
			CylinderController cylinder = _pool.GetFromPool().GetComponent<CylinderController>();
			cylinder.Init(_pool);
			return cylinder;
		}
	}
}

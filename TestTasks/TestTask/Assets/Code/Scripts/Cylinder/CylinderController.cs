using UnityEngine;

namespace MyGame.Cylinder
{
	public enum CylinderType
	{
		Usual = 0,
		PerfectMoved = 1
	}

	public class CylinderController : MonoBehaviour
	{
		private ObjectPool _nativePool;

		public Transform Tran { get; private set; }
		public MeshRenderer MeshRenderer { get; private set; }

		public float Scale { get { return Tran.localScale.x; } }
		public CylinderType Type { get; private set; }

		public void Init(ObjectPool nativePool)
		{
			_nativePool = nativePool;
			InitComponentReferences();
		}

		private void InitComponentReferences()
		{
			Tran = GetComponent<Transform>();
			MeshRenderer = GetComponent<MeshRenderer>();
		}

		public void Expand(float expandValue)
		{
			Vector3 curScale = Tran.localScale;
			Vector3 newScale = new Vector3(curScale.x + expandValue, curScale.y, curScale.z + expandValue);
			Tran.localScale = newScale;
		}

		public void MakePerfect()
		{
			Type = CylinderType.PerfectMoved;
		}

		public void Destroy()
		{
			_nativePool.ReturnToPool(this.gameObject);
		}
	}
}

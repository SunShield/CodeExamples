using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
	private GameObject _prefab;
	private Stack<GameObject> _pool;

	private bool PoolIsEmpty {  get { return _pool.Count == 0; } }

	public ObjectPool(GameObject objectToPool)
	{
		_prefab = objectToPool;
		_pool = new Stack<GameObject>();
	}

	public GameObject GetFromPool()
	{
		if (PoolIsEmpty)
			AddNewPoolElement();
		GameObject element = GetElementFromPool();
		return element;
	}

	private void AddNewPoolElement()
	{
		GameObject go = GameObject.Instantiate(_prefab);
		PoolElement poolElement = go.AddComponent<PoolElement>();
		poolElement.Pool = this;

		_pool.Push(go);
	}

	private GameObject GetElementFromPool()
	{
		GameObject go = _pool.Pop();
		while(go == null) // if something bad happened with object (on scene swap, etc), it can be null even here
		{
			AddNewPoolElement();
			go = _pool.Pop();
		}
		go.SetActive(true);
		return go;
	}

	public void ReturnToPool(GameObject element)
	{
		element.SetActive(false);
		_pool.Push(element);
	}
}

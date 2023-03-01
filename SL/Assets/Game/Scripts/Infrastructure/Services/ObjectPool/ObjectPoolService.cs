using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Infrastructure.Services.ObjectPool
{
	public class ObjectPoolService
	{
		private List<GameObject> _pooledObjects = new List<GameObject>();
		private int _amountToPool;
		private GameObject _bulletPrefab;

		private void InstantiateObjectInPool()
		{
			for (int i = 0; i < _amountToPool; i++)
			{
				GameObject gameObject = (_bulletPrefab);
			}
		}
	}
}
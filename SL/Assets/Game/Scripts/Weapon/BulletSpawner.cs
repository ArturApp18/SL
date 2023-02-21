using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Services.Input;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Scripts.Weapon
{
	public class BulletSpawner : MonoBehaviour
	{
		[SerializeField] private Bullet _bullet;
		[SerializeField] private GameObject _bulletsKeeper;
		[SerializeField] private BoxCollider2D _spawnArea;
		[SerializeField] private int _bulletsPerSecond = 10;
		[SerializeField] private float _speed = 5;
		[SerializeField] private bool _useObjectPool = false;

		private ObjectPool<Bullet> _bulletPool;

		private float LastSpawnTime;
		private IInputService _inputService;

		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
			_bulletPool = new ObjectPool<Bullet>(CreatePooledObject, OnTakeFromPool, OnReturnToPool, OnDestroyObject,false, 200, 100_000);
		}

		private Bullet CreatePooledObject()
		{
			Bullet instance = Instantiate(_bullet, Vector2.zero, Quaternion.identity);
			instance.Disable += ReturnObjectToPool;
			instance.gameObject.SetActive(false);

			return instance;
		}

		private void ReturnObjectToPool(Bullet instance)
		{
			_bulletPool.Release(instance);
		}
		private void OnTakeFromPool(Bullet instance)
		{
			instance.gameObject.SetActive(true);
			SpawnBullet(instance);
		}

		private void OnReturnToPool(Bullet instance)
		{
			instance.gameObject.SetActive(false);
		}

		private void OnDestroyObject(Bullet instance)
		{
			Destroy(instance.gameObject);
		}

		private void OnGUI()
		{
			if (_useObjectPool)
			{
				GUI.Label(new Rect(10,10,200, 30), $"Total Pool Size:{_bulletPool.CountAll}");
				GUI.Label(new Rect(10,30,200, 30), $"Active:{_bulletPool.CountActive}");
			}
		}

		private void Update()
		{
			float delay = 1f / _bulletsPerSecond;
			if (LastSpawnTime + delay < Time.time)
			{
				int bulletsToSpawnInFrame = Mathf.CeilToInt(Time.deltaTime / delay);
				while (bulletsToSpawnInFrame > 0)
				{
					if (_inputService.AimAxis.x > 0.6 || _inputService.AimAxis.y > 0.6)
						_bulletPool.Get();
					

					bulletsToSpawnInFrame--;
				}

				LastSpawnTime = Time.time;
			}
		}

		private void SpawnBullet(Bullet instance)
		{
			Vector2 spawnLocation = new Vector2(
				_spawnArea.transform.position.x + Random.Range(-1 * _spawnArea.bounds.extents.x, _spawnArea.bounds.extents.x),
				_spawnArea.transform.position.y + Random.Range(-1 * _spawnArea.bounds.extents.y, _spawnArea.bounds.extents.y));

			instance.transform.position = spawnLocation;
			
			instance.Shoot(spawnLocation, _spawnArea.transform.forward,_speed);
		}
	}
}
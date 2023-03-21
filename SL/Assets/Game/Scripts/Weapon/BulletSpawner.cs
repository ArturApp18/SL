using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Services.Input;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Scripts.Weapon
{
	public class BulletSpawner : MonoBehaviour
	{
		[SerializeField] private Bullet _bullet;
		[SerializeField] private Transform _spawnArea;
		[SerializeField] private RecoilWeapon _recoilWeapon;
		[SerializeField] private ParticleSystem _impactVFX;
		[SerializeField] private GunReload _reload;
		
		[SerializeField] private int _bulletsPerSecond = 10;
		[SerializeField] private float _speed = 5;

		private ObjectPool<Bullet> _bulletPool;

		private float _lastSpawnTime;

		private IInputService _inputService;
		[SerializeField]
		private int releasedBullet = 0;
		[SerializeField]
		private AudioSource _shootSoundFX;

		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
			_bulletPool = new ObjectPool<Bullet>(CreatePooledObject, OnTakeFromPool, OnReturnToPool, OnDestroyObject, false, 200, 100_000);
			
		}


		private void Update()
		{
			if (_reload.IsReloading)
				return;
			
			float delay = 1f / _bulletsPerSecond;
			if (_lastSpawnTime + delay < Time.time)
			{
				int bulletsToSpawnInFrame = Mathf.CeilToInt(Time.deltaTime / delay);
				
				while (bulletsToSpawnInFrame > 0)
				{
					if (_inputService.AimAxis.magnitude > 0.6)
					{
						if (_reload.CurrentAmmoCount > 0)
						{
							_bulletPool.Get();
							releasedBullet++;
							_reload.CurrentAmmoCount--;
						}
						else
						{
							_reload.ReloadCoroutine();
						}
					}

					bulletsToSpawnInFrame--;
				}

				_lastSpawnTime = Time.time;
			}
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

			Debug.Log("Go");
			_shootSoundFX.Play();
			SpawnBullet(instance);
		}

		private void OnReturnToPool(Bullet instance)
		{
			instance.gameObject.SetActive(false);
			instance.transform.position = _spawnArea.position;
		}

		private void OnDestroyObject(Bullet instance)
		{
			instance.Disable -= ReturnObjectToPool;
			Destroy(instance.gameObject);
			
		}

		private void OnGUI()
		{
			GUI.Label(new Rect(10, 10, 200, 30), $"Total Pool Size:{_bulletPool.CountAll}");
			GUI.Label(new Rect(10, 30, 200, 30), $"Active:{_bulletPool.CountActive}");
		}


		private void SpawnBullet(Bullet instance)
		{
			instance.transform.position = _spawnArea.position;
			instance.transform.rotation = _spawnArea.rotation;

			
			instance.Shoot(_spawnArea.position,_spawnArea.rotation, _spawnArea.right, _speed);
			_impactVFX.Play();
			_recoilWeapon.StartRecoil();
		}
	}
}
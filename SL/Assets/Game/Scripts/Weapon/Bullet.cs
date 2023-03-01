using System;
using Game.Scripts.Enemy;
using Game.Scripts.Logic;
using UnityEngine;

namespace Game.Scripts.Weapon
{
	public class Bullet : MonoBehaviour
	{
		private const string Mob = "Mob";

		[SerializeField] private Rigidbody2D _rigidbody2D;
		[SerializeField] private TriggerObserver _triggerObserver;
		[SerializeField] private ParticleSystem _impactVFX;


		[SerializeField] private float _damage;
		[SerializeField] private float _bulletLifeTime = 5;
		[SerializeField] private float _bulletLifeTimeLeft;


		private static int _layerMask;
		public delegate void OnDisableCallBack(Bullet instance);

		public OnDisableCallBack Disable;
		[SerializeField]
		private float _PosZ;
		[SerializeField]
		private SpriteRenderer _spriteBullet;
		[SerializeField]
		private TrailRenderer _trailRenderer;

		private void Start()
		{
			_layerMask = LayerMask.NameToLayer("Hittable");
			_bulletLifeTimeLeft = _bulletLifeTime;
			_triggerObserver.TriggerEnter += TriggerEnter;
		}

		private void OnDestroy()
		{
			_triggerObserver.TriggerEnter -= TriggerEnter;
		}

		private void Update()
		{
			_PosZ = transform.position.z;
			_bulletLifeTimeLeft -= Time.deltaTime;

			if (_bulletLifeTimeLeft <= 0)
			{
				TrailClean();
				Disable?.Invoke(this);
				_bulletLifeTimeLeft = _bulletLifeTime;
			}
		}

		public void Shoot(Vector2 position, Quaternion rotation, Vector2 direction, float speed)
		{
			_rigidbody2D.velocity = Vector2.zero;

			transform.position = position;

			_trailRenderer.enabled = true;

			_rigidbody2D.velocity = direction * speed;
			
			
		}

		private void TriggerEnter(Collider2D obj)
		{
			_impactVFX.transform.right = -1 * transform.right;
			_impactVFX.Play();
			_spriteBullet.enabled = false;
			_rigidbody2D.velocity = Vector2.zero;

			Debug.Log($"{obj.gameObject.layer}  +  {obj.gameObject.name}");
			if (obj.gameObject.layer == _layerMask)
			{
				obj.transform.parent.GetComponent<IHealth>().TakeDamage(_damage);
				Debug.Log($"{obj.gameObject.name}  +  {obj.gameObject.layer}");
			}
		}

		private void OnParticleSystemStopped()
		{
			TrailClean();
			Disable?.Invoke(this);
			Debug.Log("Allo");
			_spriteBullet.enabled = true;
		}

		private void TrailClean()
		{
			_trailRenderer.Clear();
			_trailRenderer.enabled = false;
		}
	}

}
using System;
using UnityEngine;

namespace Game.Scripts.Enemy
{
	public class RetreatFromHero : MonoBehaviour
	{
		
		public float MinimalDistance = 3f;

		[SerializeField] private RotateToHero _rotateToHero;
		[SerializeField] private TriggerObserver _triggerObserver;
		[SerializeField] private float _movementSpeed;

		public Rigidbody2D Rigidbody;
		public Transform _heroTransform = null;
		[SerializeField]
		private bool TestSafeZone;
		public float MovementSpeed
		{
			get => _movementSpeed;
			set => _movementSpeed = value;
		}

		private void Start()
		{
			_triggerObserver.TriggerEnter += TriggerEnter;
			_triggerObserver.TriggerExit += TriggerExit;
		}

		private void OnDestroy()
		{
			_triggerObserver.TriggerEnter -= TriggerEnter;
			_triggerObserver.TriggerExit -= TriggerExit;
		}

		private void TriggerEnter(Collider2D obj)
		{
			_heroTransform = obj.transform;
		}

		private void TriggerExit(Collider2D obj)
		{
			_heroTransform = null;
		}

		private void Update()
		{
			if (Initialized() && SafeZoneNotReached())
				Move();

			
		}

		private void Move()
		{
			if (ChooseSide())
			{
				Rigidbody.velocity = new Vector2(-MovementSpeed, Rigidbody.velocity.y);

				if (!_rotateToHero.IsFacingRight)
				{
					_rotateToHero.Flip();
				}
			}
			else
			{
				
				Rigidbody.velocity = new Vector2(MovementSpeed, Rigidbody.velocity.y);
				if (_rotateToHero.IsFacingRight)
				{
					_rotateToHero.Flip();
				}
			}
		}

		private bool ChooseSide() =>
			transform.position.x <= _heroTransform.position.x;

		private bool Initialized() =>
			_heroTransform != null;


		private bool SafeZoneNotReached() =>
			Vector3.Distance(transform.position, _heroTransform.position) <= MinimalDistance;
	}
}
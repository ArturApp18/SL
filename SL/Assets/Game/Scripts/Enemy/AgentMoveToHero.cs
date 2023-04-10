using System;
using Game.Scripts.Infrastructure.Factories;
using UnityEngine;

namespace Game.Scripts.Enemy
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class AgentMoveToHero : Follow
	{
		private const float MinimalDistance = 0.7f;

		[SerializeField] private RotateToHero _rotateToHero;
		private Transform _heroTransform;
		private IGameFactory _gameFactory;

		public Rigidbody2D Rigidbody;
		
		[SerializeField] private float _movementSpeed;
		public float MovementSpeed
		{
			get => _movementSpeed;
			set => _movementSpeed = value;
		}

		public void Construct(Transform heroTransform) =>
			_heroTransform = heroTransform;

		private void Update()
		{
			if (Initialized() && HeroNotReached())
				Move();
		}

		private void Move()
		{
			if (ChooseSide())
			{
				Rigidbody.velocity = new Vector2(MovementSpeed, Rigidbody.velocity.y);

				if (!_rotateToHero.IsFacingRight)
				{
					_rotateToHero.Flip();
				}
			}
			else
			{
				Rigidbody.velocity = new Vector2(-MovementSpeed, Rigidbody.velocity.y);
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


		private bool HeroNotReached() =>
			Vector3.Distance(transform.position, _heroTransform.position) >= MinimalDistance;

	}

}
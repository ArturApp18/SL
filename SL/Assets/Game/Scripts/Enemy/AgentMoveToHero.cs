using Game.Scripts.Infrastructure.Factories;
using Game.Scripts.Infrastructure.Services;
using UnityEngine;

namespace Game.Scripts.Enemy
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class AgentMoveToHero : Follow
	{
		private const float MinimalDistance = 0.7f;
		
		private Transform _heroTransform;
		private IGameFactory _gameFactory;

		public Rigidbody2D Rigidbody;
		
		[SerializeField] private float _movementSpeed;

		private void Start()
		{
			_gameFactory = AllServices.Container.Single<IGameFactory>();

			if(_gameFactory.HeroGameObject != null)
				InitializeHeroTransform();
			else
				_gameFactory.HeroCreated += HeroCreated;
		}

		private void Update()
		{
			if (Initialized() && HeroNotReached())
			{
				Vector3 localScale = transform.localScale;
				
				if(transform.position.x < _heroTransform.position.x)
				{
					Rigidbody.velocity = new Vector2(_movementSpeed, Rigidbody.velocity.y);

					transform.localScale = new Vector2(-1, 1);
				}
				else
				{
					Rigidbody.velocity = new Vector2(-_movementSpeed, Rigidbody.velocity.y);
					transform.localScale = new Vector2(1, 1);
				}
			}
		}

		private bool Initialized()
		{
			return _heroTransform != null;
		}

		private void InitializeHeroTransform() =>
			_heroTransform = _gameFactory.HeroGameObject.transform;

		private void HeroCreated() =>
			InitializeHeroTransform();

		private bool HeroNotReached() =>
			Vector3.Distance(transform.position, _heroTransform.position) >= MinimalDistance;
	}
}
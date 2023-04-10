using System;
using Game.Scripts.Infrastructure.Factories;
using Game.Scripts.Infrastructure.Services;
using UnityEngine;

namespace Game.Scripts.Enemy
{
	public class RotateToHero : Follow
	{
		[SerializeField] private bool _isFacingRight;
		[SerializeField] private bool _entered;


		private Transform _heroTransform;
		private IGameFactory _gameFactory;

		public float FacingDirection { get; set; }
		public bool IsFacingRight
		{
			get
			{
				return _isFacingRight;
			}
			set
			{
				_isFacingRight = value;
			}
		}

		public void Construct(Transform heroTransform) =>
			_heroTransform = heroTransform;
		
		public void Flip()
		{
			FacingDirection = FacingDirection * -1;
			IsFacingRight = !IsFacingRight;
			transform.Rotate(0, 180, 0);
		}

	}

}
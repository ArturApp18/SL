using System;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class PhysicsMovement : MonoBehaviour
	{
		[SerializeField] private Rigidbody2D _rigidbody;
		[SerializeField] private SurfaceSlider _surfaceSlider;
		[SerializeField] private float _speed;

		public void Move(Vector2 direction)
		{
			Vector2 directionAlongSurface = _surfaceSlider.Project(direction.normalized);
			Vector2 offset = directionAlongSurface * ( _speed * Time.deltaTime );
			
			_rigidbody.MovePosition(_rigidbody.position + offset);
		}
	}

}
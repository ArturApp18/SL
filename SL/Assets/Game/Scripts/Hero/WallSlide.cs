using Game.Scripts.Services.Input;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class WallSlide : MonoBehaviour
	{
		[SerializeField] private CharacterController2D _controller;
		[SerializeField] private Rigidbody2D _rigidbody;
		
		private IInputService _input;

		public bool WallSliding;
		public float WallSlidingSpeed;

		public void Construct(IInputService input)
		{
			_input = input;
		}

		private void Update()
		{
			Slide();
		}

		private void Slide()
		{
			if (!_controller.m_Grounded && _controller.m_WallDetected && _input.Axis.x != 0)
			{
				WallSliding = true;
				_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Mathf.Clamp(_rigidbody.velocity.y, -WallSlidingSpeed, float.MaxValue));
			}
		}
	}
}
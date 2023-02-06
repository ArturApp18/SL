using System;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Services.Input;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class HeroFriction : MonoBehaviour
	{
		[SerializeField]
		private Rigidbody2D _rigidBody;
		[SerializeField]
		private float frictionAmount;

		private IInputService _inputService;

		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
		}

		private void FixedUpdate()
		{
			float amount = CalculateAmount();
			SetDirection(ref amount);
			AppliesForceDirection(amount);
		}

		private float CalculateAmount() =>
			Mathf.Min(Mathf.Abs(_rigidBody.velocity.x), Mathf.Abs(frictionAmount));

		private void SetDirection(ref float amount) =>
			amount *= Mathf.Sign(_rigidBody.velocity.x);

		private void AppliesForceDirection(float amount) =>
			_rigidBody.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
	}
}
using System;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Services.Input;
using UnityEngine;

namespace Game.Scripts.PlatformLogic
{
	public class TransformingPlatform : MonoBehaviour
	{
		private static readonly int Transform = Animator.StringToHash("Transform");

		[SerializeField] private Animator _animator;
		[SerializeField] private float _trasnformationProgress;
		[SerializeField] private float _scale;
		[SerializeField] private bool _startTransforming;

		private IInputService _inputService;

		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
		}

		private void Start()
		{
			_animator.SetFloat(Transform, _trasnformationProgress);
		}

		private void Update()
		{
			if (_inputService.IsActionButton())
			{
				_startTransforming = true;
			}
			else
			{
				_startTransforming = false;
			}
		}

		private void FixedUpdate()
		{
			if (_startTransforming)
			{
				Transformation(_scale);
			}
			else
			{
				Transformation(-_scale);
			}
		}

		private void Transformation(float scale)
		{
			_trasnformationProgress = Math.Clamp(_trasnformationProgress + Time.deltaTime * scale, -1, 5); 
			_animator.SetFloat(Transform, _trasnformationProgress);
		}

		private bool BorderCheck() =>
			_trasnformationProgress < 1.1 && -1.1 < _trasnformationProgress;
	}
}
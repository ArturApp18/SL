using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Services.Input;
using Game.Scripts.Weapon;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class HeroAim : MonoBehaviour
	{
		[SerializeField] private Transform _aim;
		[SerializeField] private Transform _aimTarget;
		[SerializeField] private CharacterController2D _controller;
		[SerializeField] private HeroFlip heroFlip;
		[SerializeField] private AimAssist _aimAssist;
		[SerializeField] private float _returnTime;
		[SerializeField] private float _aimSpeed;
		[SerializeField] private float _thresHold;


		private IInputService _inputService;
		public Vector3 _degrece;
		[SerializeField]
		private float Test;
		[SerializeField]
		private Vector2 Test2;
		[SerializeField]
		private Transform _target;
		[SerializeField]
		private LayerMask _targetMask;
		[SerializeField]
		private LineRenderer _lineRenderer;
		[SerializeField]
		private Vector3 targetPosition;
		[SerializeField]
		private Vector2 Test3;
		private Vector2 _inputDirection;

		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
			_targetMask = 1 << LayerMask.NameToLayer("Hittable");
		}

		private void Update()
		{
			if (_aim != null)
			{
				_inputDirection = new Vector2(_inputService.AimAxis.x, _inputService.AimAxis.y);
				Vector3 aimPosition = _aim.position;
				Vector3 aimDirection = _aim.right;
				RaycastHit2D hit = Physics2D.Raycast(aimPosition, aimDirection, 20, _targetMask);
				Vector3 endPosition = hit ? hit.point : aimPosition + aimDirection * 100;
				_lineRenderer.SetPositions(new Vector3[] { aimPosition, endPosition });
			}
		}

		private void FixedUpdate()
		{
			Test2 = _inputDirection;
			Aim();
		}

		private void Aim()
		{
			Vector3 angle = _aim.localEulerAngles;
			_degrece = angle;
			_degrece.z = angle.z;
			//Test3 = Mathf.Clamp(Mathf.Atan2(_inputService.AimAxis.x, _inputService.AimAxis.y) / Mathf.PI + 90, -40, 60);
			if (HandlerIdle())
			{
				BackToHomeRotation(angle);
			}
			else
			{
				_target = GetTarget();
				HandleAim(_target);
			}
		}

		private void HandleAim(Transform target)
		{
			
			if (target != null)
			{
				Vector2 directionTarget = target.position - _aim.position;
				if (Vector2.Distance(_inputDirection, directionTarget.normalized) < _thresHold)
				{
					Test3 = directionTarget.normalized;
					RotateTowardsTarget(directionTarget.normalized);
				}
				else
				{
					Vector2 direction = _inputDirection;
					RotateTowardsTarget(direction);
				}
			}
			else
			{
				Vector2 direction = _inputDirection;
				RotateTowardsTarget(direction);
			}
		}

		private void BackToHomeRotation(Vector3 angle)
		{
			Vector3 homeRotation;

			if (angle.z > 180)
			{
				homeRotation = new Vector3(0, 0, 359.9f);
			}
			else
			{
				homeRotation = Vector3.zero;
			}

			_aim.localEulerAngles = Vector3.Slerp(angle, homeRotation, Time.deltaTime * _returnTime);
		}

		private float LookingDirection()
		{
			if (!heroFlip.IsFacingRight)
				return 180;
			else
				return -180;
		}

		private bool HandlerIdle()
		{
			return _inputService.AimAxis.x == 0 && _inputService.AimAxis.y == 0;
		}

		private Transform GetTarget()
		{
			RaycastHit2D hit2D = Physics2D.Raycast(_aim.position, _aim.right, 10, _targetMask);


			if (hit2D != null)
			{
				targetPosition = hit2D.point;
				return hit2D.transform;
			}

			return null;
		}

		private void RotateTowardsTarget(Vector2 direction)
		{
			float angle = Mathf.Atan2(direction.x, direction.y) * LookingDirection() / Mathf.PI + 90;
			Quaternion targetRotation;
			if (heroFlip.IsFacingRight)
			{
				targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
			}
			else
			{
				targetRotation = Quaternion.Euler(new Vector3(0, 180, angle));
			}
			_aim.rotation = Quaternion.Lerp(_aim.rotation, targetRotation, Time.deltaTime * _aimSpeed);
		}
	}
}
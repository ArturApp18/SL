using System;
using Game.Scripts.Enemy;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class LedgeDetection : MonoBehaviour
	{
		[SerializeField] private float radius;
		[SerializeField] private LedgeClimb _characterLedgeClimb;
		[SerializeField] private CharacterController2D _character;
		[SerializeField] private bool _canDetected;
		[SerializeField] private TriggerObserver _triggerObserver;
		[SerializeField] private Transform upperLedgeCheck;
		[SerializeField] private float _raycastDistance;

		private void Start()
		{
			_triggerObserver.TriggerEnter += TriggerEnter;
			_triggerObserver.TriggerExit += TriggerExit;
		}

		private void Update()
		{
			if (_canDetected)
			{
				_characterLedgeClimb._ledgeDetected = Physics2D.OverlapCircle(transform.position, radius, _character.MWhatIsLedge);
				_characterLedgeClimb._upperLedgeDetected = Physics2D.Raycast(upperLedgeCheck.position, transform.right, _raycastDistance, _character.MWhatIsWall);
			}
		}

		private void OnDisable()
		{
			_triggerObserver.TriggerEnter -= TriggerEnter;
			_triggerObserver.TriggerExit += TriggerExit;
		}

		private void TriggerEnter(Collider2D obj)
		{
			_canDetected = false;
		}

		private void TriggerExit(Collider2D obj)
		{
			_canDetected = true;
		}

		private void OnDrawGizmos()
		{
			Gizmos.DrawWireSphere(transform.position, radius);
			Gizmos.DrawLine(upperLedgeCheck.position, new Vector2(upperLedgeCheck.position.x + _raycastDistance, upperLedgeCheck.position.y));
		}
	}
}
using System.Collections;
using UnityEngine;

namespace Game.Scripts.Hero
{
	public class LedgeClimb : MonoBehaviour
	{
		[SerializeField] private Vector2 _offset1; // A collider that will be disabled when crouching
		[SerializeField] private Vector2 _offset2;
		[SerializeField] private Vector2 _climbBegunPositiion; // A collider that will be disabled when crouching
		[SerializeField] private Vector2 _climbOverPosition;
		[SerializeField] private Transform _ledgeCheck;
		[SerializeField] private Transform _heroTransform;
		[SerializeField] private HeroAnimator _animator;
		[SerializeField] private bool _canClimb;
		[SerializeField] private Rigidbody2D _rigidbody2D;
		[SerializeField] private WallDetection _wallDetection;
		[SerializeField] private HeroFlip _heroFlip;
		[SerializeField] private HeroMove _heroMove;


		private bool CanGrabLedge = true;
		
		public bool _ledgeDetected;
		public bool _upperLedgeDetected;

		private void Update()
		{
			CheckForLedge();
		}

		public IEnumerator FinishLedgeClimb()
		{
			_canClimb = false;

			_rigidbody2D.velocity = Vector2.zero;
			_heroTransform.position = _climbOverPosition;
			_animator.StartLedge(_canClimb);
			_heroFlip.CanFlip = true;
			Debug.Log("456");
			_heroMove.CanMove = true;
			yield return new WaitForSeconds(1);

			CanGrabLedge = true;
		}


		private void CheckForLedge()
		{
			if (_ledgeDetected && CanGrabLedge && _wallDetection && !_upperLedgeDetected)
			{
				CanGrabLedge = false;

				Vector2 ledgePosition = _ledgeCheck.position;

				if (_heroFlip.IsFacingRight)
				{
					_climbBegunPositiion = ledgePosition + _offset1;
					_climbOverPosition = ledgePosition + _offset2;
				}
				else
				{
					_climbBegunPositiion = ledgePosition + new Vector2(-_offset1.x, _offset1.y);
					_climbOverPosition = ledgePosition + new Vector2(-_offset2.x, _offset2.y);
				}

				_canClimb = true;
				_animator.StartLedge(_canClimb);
			}

			if (_canClimb)
			{
				_heroMove.CanMove = false;
				_heroTransform.position = _climbBegunPositiion;
			}
		}
	}
}
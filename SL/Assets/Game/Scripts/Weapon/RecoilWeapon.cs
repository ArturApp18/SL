using System;
using System.Collections;
using Game.Scripts.Hero;
using UnityEngine;

namespace Game.Scripts.Weapon
{
	public class RecoilWeapon : MonoBehaviour
	{
		[SerializeField] private float _recoilDuration = 0.2f;
		[SerializeField] private float _recoilDistance = 0.2f;
		[SerializeField] private float _returnSpeed;
		[SerializeField] private float _YOffset;

		[SerializeField] private HeroFlip _heroFlip;

		public Vector3 _originalPosition;
		public Quaternion _originalRotation;

		public bool _isRecoiling = false;

		private void Start()
		{
			_originalPosition = transform.localPosition;
		}

		public void StartRecoil()
		{
			if (!_isRecoiling)
			{
				StartCoroutine(Recoil());
			}
		}

		private IEnumerator Recoil()
		{
			_isRecoiling = true;
			_returnSpeed = _recoilDistance / _recoilDuration;

			

			Vector3 recoilPos = OriginalPosition();

			float elapsedTime = 0;
			while (elapsedTime < _recoilDuration)
			{
				transform.localPosition = Vector3.Lerp(_originalPosition, recoilPos, elapsedTime / _recoilDuration);
				elapsedTime += Time.deltaTime;
				yield return null;
			}

			elapsedTime = 0;
			while (elapsedTime < _recoilDuration)
			{
				transform.localPosition = Vector3.Lerp(recoilPos, _originalPosition, elapsedTime / _recoilDuration);
				elapsedTime += Time.deltaTime * _returnSpeed;
				yield return null;
			}

			transform.localPosition = _originalPosition;
			_isRecoiling = false;

		}

		private Vector3 OriginalPosition()
		{
			float recoilDirection = _heroFlip.IsFacingRight ? 1f : -1f;
			return _originalPosition - transform.right * (recoilDirection * _recoilDistance);
		}
	}
}
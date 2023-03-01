using System;
using System.Collections;
using Game.Scripts.Hero;
using UnityEngine;

namespace Game.Scripts.Weapon
{
	public class GunReload : MonoBehaviour
	{
		[SerializeField] private HeroAnimator _animator;
		
		[SerializeField] private int _maxBulletAmount;
		[SerializeField] private float _reeloadTime;
		[SerializeField] private int _currentAmmoCount;

		private bool _isReloading = false;
		
		public int CurrentAmmoCount
		{
			get
			{
				return _currentAmmoCount;
			}
			set
			{
				_currentAmmoCount = value;
			}
		}
		public bool IsReloading
		{
			get
			{
				return _isReloading;
			}
		}

		private void Start()
		{
			_currentAmmoCount = _maxBulletAmount;
		}

		public void ReloadCoroutine()
		{
			StartCoroutine(Reload());
		}
		
		private IEnumerator Reload() {
			_isReloading = true;
			_animator.PlayReload();
			Debug.Log("Reloading...");
			yield return new WaitForSeconds(_reeloadTime);
			_currentAmmoCount = _maxBulletAmount;
			_isReloading = false;
			_animator.StopReload();
		}
	}
}
using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.PlatformLogic
{
	public class BlastSpawner : MonoBehaviour
	{
		[SerializeField] private GameObject _projectilePrefab;
		[SerializeField] private float _cooldowm;
		[SerializeField] private bool isActivated;

		private Coroutine _blastCoroutine;

		private void Update()
		{
			if (!isActivated)
			{
				_blastCoroutine = StartCoroutine(BlastCoroutine());
			}
		}

		private IEnumerator BlastCoroutine()
		{
			isActivated = true;
			Instantiate(_projectilePrefab, transform.position, _projectilePrefab.transform.rotation);
			yield return new WaitForSeconds(_cooldowm);
			isActivated = false;
		}
	}
}
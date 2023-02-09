using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.PlatformLogic
{
	public class BlastSpawner : MonoBehaviour
	{
		private Coroutine _blastCoroutine;
		[SerializeField] private GameObject _projectilePrefab;
		[SerializeField] private float _cooldowm;
		[SerializeField] private bool isActivated;


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
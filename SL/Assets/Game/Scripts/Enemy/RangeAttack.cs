using UnityEngine;

namespace Game.Scripts.Enemy
{
	public class RangeAttack : MonoBehaviour
	{

		[SerializeField] private GameObject projectilePrefab;
		[SerializeField] private Transform[] shootingPoints;
		[SerializeField] private TriggerObserver _triggerObserver;

		[SerializeField] private float minTimeBetweenShoot = 1f;
		[SerializeField] private bool _entered;
		[SerializeField] private float _timer;

		private void Start()
		{
			_triggerObserver.TriggerEnter += TriggerEnter;
			_triggerObserver.TriggerExit += TriggerExit;
		}

		private void OnDestroy()
		{
			_triggerObserver.TriggerEnter -= TriggerEnter;
			_triggerObserver.TriggerExit -= TriggerExit;
		}

		private void TriggerEnter(Collider2D obj)
		{
			_entered = true;
		}

		private void TriggerExit(Collider2D obj)
		{
			_entered = false;
		}

		private void Update()
		{
			if (_entered)
			{
				_timer += Time.deltaTime;
				if (_timer > minTimeBetweenShoot)
				{
					_timer = 0;
					Shoot();
				}
			}
		}

		private void Shoot()
		{
			for (int i = 0; i < shootingPoints.Length; i++)
			{
				Instantiate(projectilePrefab, shootingPoints[i].position, shootingPoints[i].rotation);
			}
		}
	}
}
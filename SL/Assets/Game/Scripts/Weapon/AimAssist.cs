using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Weapon
{
	public class AimAssist : MonoBehaviour
	{
		[SerializeField] private List<Transform> _targets;
		[SerializeField] private float _lockThreshold = 0.3f;
		private Transform GetClosestTarget(Vector3 direction)
		{
			float closestDistance = Mathf.Infinity;
			Transform closestTarget = null;

			foreach (Transform target in _targets)
			{
				Vector3 targetDirection = target.position - transform.position;
				float angle = Vector3.Angle(direction, targetDirection);
				float distance = Vector3.Distance(transform.position, target.position);

				if (angle <= _lockThreshold && distance < closestDistance)
				{
					closestDistance = distance;
					closestTarget = target;
				}
			}

			return closestTarget;
		}
	}
}
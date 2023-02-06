using UnityEngine;

namespace Game.Scripts.CameraLogic
{
	public class CameraBounds : MonoBehaviour
	{

		[SerializeField] private float _leftLimit;
		[SerializeField] private float _rightLimit;
		[SerializeField] private float _bottonLimit;
		[SerializeField] private float _upperLimit;

		private void LateUpdate()
		{
			transform.position = new Vector3(
				Mathf.Clamp(transform.position.x, _leftLimit, _rightLimit),
				Mathf.Clamp(transform.position.y, _bottonLimit, _upperLimit),
				transform.position.z
			);
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(new Vector2(_leftLimit, _upperLimit), new Vector2(_rightLimit, _upperLimit));
			Gizmos.DrawLine(new Vector2(_leftLimit, _bottonLimit), new Vector2(_rightLimit, _bottonLimit));
			Gizmos.DrawLine(new Vector2(_leftLimit, _upperLimit), new Vector2(_leftLimit, _bottonLimit));
			Gizmos.DrawLine(new Vector2(_rightLimit, _upperLimit), new Vector2(_rightLimit, _bottonLimit));
		}
	}
}
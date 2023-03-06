using UnityEngine;

namespace Game.Scripts.CameraLogic
{
	public class ParallaxLayer : MonoBehaviour
	{
		[SerializeField, Range(0,1)]  private float multiplier = 0.0f;
		[SerializeField, Range(1,10)] private float _speed;
		[SerializeField] private bool horizontalOnly = true;

		private Transform cameraTransform;

		private Vector3 startCameraPos;
		private Vector3 startPos;

		private void Start()
		{
			cameraTransform = Camera.main.transform;
			startCameraPos = cameraTransform.position;
			startPos = transform.position;
		}


		private void LateUpdate()
		{
			var position = startPos;
			if (horizontalOnly)
				position.x += multiplier * (cameraTransform.position.x - startCameraPos.x);
			else
				position += multiplier * (cameraTransform.position - startCameraPos);

			transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * _speed);
		}
	}
}
using UnityEngine;

namespace Game.Scripts
{
	public class CloakAnimation : MonoBehaviour
	{
		public Transform anchorPoint;
		public float springConstant = 10f;
		public float damping = 1f;
		public float cloakArea = 2f;
		public float maxStretch = 2f;
		public float mass = 1f;

		private Rigidbody2D rb;
		private SpringJoint2D springJoint;

		private void Start()
		{
			rb = GetComponent<Rigidbody2D>();
			springJoint = gameObject.AddComponent<SpringJoint2D>();
			springJoint.connectedBody = anchorPoint.GetComponent<Rigidbody2D>();
			springJoint.frequency = springConstant;
			springJoint.dampingRatio = damping;
			springJoint.distance = 0f;
			springJoint.autoConfigureDistance = false;
		}

		private void Update()
		{
			Vector2 windForce = Vector2.up * Mathf.Sin(Time.time) * cloakArea;
			rb.AddForce(windForce);
			float stretch = Mathf.Clamp01(springJoint.distance / maxStretch);
			springJoint.connectedAnchor = new Vector2(0, -stretch);
		}

		private void FixedUpdate()
		{
			rb.mass = mass;
		}
	}
}
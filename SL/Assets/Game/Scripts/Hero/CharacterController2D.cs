using System.Collections;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Services.Input;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Hero
{
	public class CharacterController2D : MonoBehaviour
	{

		[Range(0, 1)] [SerializeField]
		private float m_CrouchSpeed = .36f; // Amount of maxSpeed applied to crouching movement. 1 = 100%

		[SerializeField] private bool m_AirControl = false; // Whether or not a player can steer while jumping;

		[SerializeField] private LayerMask m_WhatIsGround; // A mask determining what is ground to the character
		[SerializeField] private LayerMask m_WhatIsWall; // A mask determining what is ground to the character
		[SerializeField] private Transform m_GroundCheck; // A position marking where to check if the player is grounded.
		[SerializeField] private Transform m_WallCheckUp; // A position marking where to check if the player is grounded.
		[SerializeField] private Transform m_CeilingCheck; // A position marking where to check for ceilings

		[SerializeField] private Collider2D m_CrouchDisableCollider;



		public float k_GroundedRadius = 0.2f; // Radius of the overlap circle to determine if grounded
		public float k_WallCheckRayDistance = 0.2f; // Radius of the overlap circle to determine if grounded
		public bool m_Grounded; // Whether or not the player is grounded.
		public float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up

		private bool m_wasCrouching = false;

		[SerializeField] private Rigidbody2D rigidbody2D;

		public Transform MGroundCheck
		{
			get
			{
				return m_GroundCheck;
			}
			set
			{
				m_GroundCheck = value;
			}
		}
		public LayerMask MWhatIsGround
		{
			get
			{
				return m_WhatIsGround;
			}
			set
			{
				m_WhatIsGround = value;
			}
		}
		public LayerMask MWhatIsWall
		{
			get
			{
				return m_WhatIsWall;
			}
			set
			{
				m_WhatIsWall = value;
			}
		}
		[Header("Events")] [Space] public UnityEvent OnLandEvent;


		[System.Serializable]
		public class BoolEvent : UnityEvent<bool> { }

		public BoolEvent OnCrouchEvent;

		private void Awake()
		{
			AllServices.Container.Single<IInputService>();
			GetComponent<Rigidbody2D>();

			if (OnLandEvent == null)
				OnLandEvent = new UnityEvent();

			if (OnCrouchEvent == null)
				OnCrouchEvent = new BoolEvent();
		}

		private void Update()
		{
			IsGrounded();
		}

		

		private bool IsWalled()
		{
			return Physics2D.OverlapCircle(m_WallCheckUp.position, k_WallCheckRayDistance, MWhatIsWall);
		}

		private void IsGrounded()
		{
			bool wasGrounded = m_Grounded;
			m_Grounded = false;

			Collider2D[] colliders = Physics2D.OverlapCircleAll(MGroundCheck.position, k_GroundedRadius, MWhatIsGround);
			for (int i = 0; i < colliders.Length; i++)
			{
				if (colliders[i].gameObject != gameObject)
				{
					m_Grounded = true;
					if (!wasGrounded)
						OnLandEvent.Invoke();
				}
			}
		}


		public void Move(bool crouch)
		{
			if (!crouch)
			{
				// If the character has a ceiling preventing them from standing up, keep them crouching
				if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, MWhatIsGround))
				{
					crouch = true;
				}
			}

			//only control the player if grounded or airControl is turned on
			if (m_Grounded || m_AirControl)
			{
				// If crouching
				if (crouch)
				{
					if (!m_wasCrouching)
					{
						m_wasCrouching = true;
						OnCrouchEvent.Invoke(true);
					}

					// Reduce the speed by the crouchSpeed multiplier
					rigidbody2D.velocity *= m_CrouchSpeed;

					// Disable one of the colliders when crouching
					if (m_CrouchDisableCollider != null)
						m_CrouchDisableCollider.enabled = false;
				}
				else
				{
					// Enable the collider when not crouching
					if (m_CrouchDisableCollider != null)
						m_CrouchDisableCollider.enabled = true;

					if (m_wasCrouching)
					{
						m_wasCrouching = false;
						OnCrouchEvent.Invoke(false);
					}
				}
				
			}
		}

		/*private void OnDrawGizmos()
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawSphere(MGroundCheck.position, k_GroundedRadius);
			Gizmos.DrawSphere(m_CeilingCheck.position, k_CeilingRadius);
		}*/
	}

}
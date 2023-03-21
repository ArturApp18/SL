using Game.Scripts.Enemy;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Services.Input;
using Game.Scripts.UI.Elements;
using UnityEngine;

namespace Game.Scripts.Logic
{
	public class InteractionTrigger : MonoBehaviour
	{
		[SerializeField] private TriggerObserver _triggerObserver;
		[SerializeField] private bool _inActiveZone;

		private PopUpActionButton _popUpActionButton;
		private IInputService _input;


		public bool Active;

		public void Construct(PopUpActionButton popUpActionButton)
		{
			_popUpActionButton = popUpActionButton;
		}
		private void Awake()
		{
			_input = AllServices.Container.Single<IInputService>();
		}

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

		private void Update()
		{
			if (_inActiveZone)
			{
				if (_input.IsActionButton())
				{
					Active = true;
				}
				else
				{
					Active = false;
				}
			}
		}

		private void TriggerEnter(Collider2D obj)
		{
			_inActiveZone = true;
			Debug.Log(obj.name);
			_popUpActionButton.Enable();
		}

		private void TriggerExit(Collider2D obj)
		{
			_popUpActionButton.Disable();
			Debug.Log(obj.name);
			_inActiveZone = false;
		}
	}
}
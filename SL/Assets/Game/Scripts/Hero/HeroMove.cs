using Game.Scripts.Data;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Infrastructure.Services.PersistentProgress;
using Game.Scripts.Services.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Hero
{
	public class HeroMove : MonoBehaviour, ISavedProgress
	{
		[SerializeField] private Rigidbody2D _rigidbody;
		[SerializeField] private CharacterController2D _characterController;

		private IInputService _inputService;

		public float MovementSpeed;

		private bool _jump;
		private bool _crouch = false;

		public float _horizontalMove;

		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
		}

		private void Update()
		{
			_horizontalMove = _inputService.Axis.x * MovementSpeed;
		}

		private void FixedUpdate()
		{
			_characterController.Move(_horizontalMove * Time.deltaTime, _crouch, _jump);
			_jump = false;
		}

		public void UpdateProgress(PlayerProgress progress) =>
			progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());

		private static string CurrentLevel() =>
			SceneManager.GetActiveScene().name;

		public void LoadProgress(PlayerProgress progress)
		{
			if(CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
			{
				Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
				if(savedPosition != null)
					Warp(to: savedPosition);
			}
		}

		private void Warp(Vector3Data to)
		{
			transform.position = to.AsUnityVector();
		}
	}
}
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Elements
{
	public class PopUpActionButton : MonoBehaviour
	{
		[SerializeField] private Image _image;

		public void Enable()
		{
			_image.enabled = true;
		}

		public void Disable()
		{
			_image.enabled = false;
		}
	}
}
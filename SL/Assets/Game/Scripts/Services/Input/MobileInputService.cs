using UnityEngine;

namespace Game.Scripts.Services.Input
{
	public class MobileInputService : InputService
	{
		public override Vector2 Axis => SimpleInputAxis();
		public override Vector2 AimAxis => SimpleInputAimAxis();
	}
}
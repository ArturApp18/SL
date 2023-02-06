using UnityEngine;

namespace Game.Scripts.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        private const string Attack = "Fire";
        private const string Jump = "Jump";

        public abstract Vector2 Axis { get; }

        public bool IsAttackButtonUp() =>
            SimpleInput.GetButton(Attack);

        public bool IsJumpButton() =>
            SimpleInput.GetButton(Jump);

        public bool IsJumpButtonDown() =>
            SimpleInput.GetButtonDown(Jump);

        public bool IsJumpButtonUp() =>
            SimpleInput.GetButtonUp(Jump);

        protected static Vector2 SimpleInputAxis() => 
            new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}
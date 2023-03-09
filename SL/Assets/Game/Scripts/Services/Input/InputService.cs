using UnityEngine;

namespace Game.Scripts.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        
        protected const string HorizontalAim = "HorizontalAim";
        protected const string VerticalAim = "VerticalAim";
        
        private const string Attack = "Fire";
        private const string Jump = "Jump";
        private const string Action = "Fire3";

        public abstract Vector2 Axis { get; }
        public abstract Vector2 AimAxis { get; }

    
        public bool IsActionButton() =>
            SimpleInput.GetButton(Action);
        public bool IsAttackButton() =>
            SimpleInput.GetButton(Attack);

        public bool IsJumpButton() =>
            SimpleInput.GetButton(Jump);

        public bool IsJumpButtonDown() =>
            SimpleInput.GetButtonDown(Jump);

        public bool IsJumpButtonUp() =>
            SimpleInput.GetButtonUp(Jump);

        
        protected static Vector2 SimpleInputAxis() => 
            new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
        
        protected static Vector2 SimpleInputAimAxis() => 
            new Vector2(SimpleInput.GetAxis(HorizontalAim), SimpleInput.GetAxis(VerticalAim));
    }
}
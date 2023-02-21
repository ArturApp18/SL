using UnityEngine;

namespace Game.Scripts.Services.Input
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis
        {
            get
            {
                Vector2 axis = SimpleInputAxis();

                if (axis == Vector2.zero) 
                    axis = UnityAxis();

                return axis;
            }
        }
        
        public override Vector2 AimAxis
        {
            get
            {
                Vector2 axis = SimpleInputAimAxis();

                if (axis == Vector2.zero) 
                    axis = UnityAimAxis();

                return axis;
            }
        }

        private static Vector2 UnityAxis() => 
            new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
        
        private static Vector2 UnityAimAxis() => 
            new Vector2(UnityEngine.Input.GetAxis(HorizontalAim), UnityEngine.Input.GetAxis(VerticalAim));
    }
}
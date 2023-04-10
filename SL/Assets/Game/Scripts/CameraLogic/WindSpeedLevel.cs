using UnityEngine;

namespace Game.Scripts.CameraLogic
{
    public class WindSpeedLevel : MonoBehaviour
    {
        private static readonly int Speed = Shader.PropertyToID("_WindSpeed");
        
        public Material[] Materials;
        public float WindSpeed;
        
        private void Update()
        {
            foreach (Material material in Materials)
            {
                material.SetFloat(Speed, WindSpeed);
            }
        
        }
    }
}

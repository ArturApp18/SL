using UnityEngine;

namespace Game.Scripts.StaticData
{
	[CreateAssetMenu(fileName = "HeroData", menuName = "StaticData/Hero")]
	public class HeroStaticData : ScriptableObject
	{
		public GameObject Prefab;

		[Range(1, 100)]
		public int Hp;

		[Range(1f, 30f)]
		public float Damage;


		[Range(1f, 15f)]
		public float MoveSpeed;


		[Range(0.5f, 1f)]
		public float EffectiveDistance = 0.666f;

		[Range(0.5f, 1f)]
		public float Cleavage;
		public float JumpForce;
		public float DoubleJumpForce;
		public float AimSpeed;
		public float AimReturnTime;
		public float AimTresHold;
	}
}
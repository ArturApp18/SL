using System;
using Game.Scripts.Enemy;
using Game.Scripts.Hero;
using Game.Scripts.Logic;
using UnityEngine;

namespace Game.Scripts.PlatformLogic
{
	public class AttackColumn : MonoBehaviour
	{
		[SerializeField] private float _damage = 7f;
		private void OnCollisionEnter2D(Collision2D col)
		{
			
			col.transform.GetComponent<IHealth>().TakeDamage(_damage);
			col.transform.GetComponent<KnockBackEffect>().KnockBack(col.transform);
		}
	}
}
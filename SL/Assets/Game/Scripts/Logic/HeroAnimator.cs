using System;
using UnityEditor.Animations;
using UnityEngine;

namespace Game.Scripts.Hero
{
   public class HeroAnimator : MonoBehaviour
  {
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] public Animator _animator;

    private static readonly int MoveHash = Animator.StringToHash("Walking");
    private static readonly int AttackHash = Animator.StringToHash("AttackNormal");
    private static readonly int HitHash = Animator.StringToHash("Hit");
    private static readonly int DieHash = Animator.StringToHash("Die");
    private static readonly int IsRun = Animator.StringToHash("isRun");

    private void Update()
    {
      _animator.SetFloat(MoveHash, _rigidbody.velocity.magnitude, 0.1f, Time.deltaTime);
      if (_animator.GetFloat(MoveHash) > 0.5)
      {
        _animator.SetBool(IsRun, true);
      }
      else
      {
        _animator.SetBool(IsRun, false);
      }
    }

    public void PlayHit()
    {
      _animator.SetTrigger(HitHash);
    }

    public void PlayAttack()
    {
      _animator.SetTrigger(AttackHash);
    }

    public void PlayDeath()
    {
      _animator.SetTrigger(DieHash);
    }
    
  }
}
using Game.Scripts.Infrastructure.Services;
using UnityEngine;

namespace Game.Scripts.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        Vector2 AimAxis { get; }
        
        bool IsActionButton();
        bool IsAttackButton();
        bool IsJumpButtonDown();
        bool IsJumpButton();
        bool IsJumpButtonUp();
    }
}
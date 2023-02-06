using Game.Scripts.Infrastructure.Services;
using UnityEngine;

namespace Game.Scripts.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }

        bool IsAttackButtonUp();
        bool IsJumpButtonDown();
        bool IsJumpButton();
        bool IsJumpButtonUp();
    }
}
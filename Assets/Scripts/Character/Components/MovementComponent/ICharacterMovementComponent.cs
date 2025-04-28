using UnityEngine;

namespace Arenar.Character
{
    public interface ICharacterMovementComponent : ICharacterComponent
    {
        void Move(Vector2 movement);

        void Jump();
    }
}
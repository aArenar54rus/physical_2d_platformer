namespace Arenar.Character
{
    public interface ICharacterRaycastComponent : ICharacterComponent
    {
        bool IsGrounded { get; }
        
        float GroundAngle { get; }
    }
}
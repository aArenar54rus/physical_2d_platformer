namespace Arenar.Character
{
    public interface ICharacterControlComponent : ICharacterComponent
    {
        void SetControlStatus(bool status);
    }
}
using UnityEngine;

namespace Arenar.Character
{
    [CreateAssetMenu(fileName = "PlayerCharacterData", menuName = "Character Data/Player Character Data", order = 51)]
    public class CharacterData : ScriptableObject
    {
        [SerializeField]
        private int health = 3;
        [SerializeField]
        private float speed;
        [SerializeField]
        private float maxHorizontalSpeed;
        [SerializeField]
        private float jumpForce;


        public int Health => health;
        public float Speed => speed;
        public float MaxHorizontalSpeed => maxHorizontalSpeed;
        public float JumpForce => jumpForce;
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Arenar.Character
{
    public class CollisionObserver : MonoBehaviour
    {
        private const string GROUND_LAYER = "Ground";
        private const string CHARACTER_LAYER = "Character";


        public event Action<ICharacterEntity> onTriggerCharacter;
        
        
        [SerializeField]
        private float maxGroundAngle = 50f;
        
        private List<ContactPoint2D> groundContacts = new List<ContactPoint2D>();


        public List<ContactPoint2D> GroundContacts => groundContacts;


        private void OnCollisionEnter2D(Collision2D collision)
        {
            UpdateContacts(collision);
            
            if (collision.gameObject.CompareTag(CHARACTER_LAYER))
                onTriggerCharacter?.Invoke(collision.gameObject.GetComponent<ICharacterEntity>());
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            UpdateContacts(collision);
        }
        
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag(GROUND_LAYER))
                return;
            groundContacts.Clear();
        }
        
        private void UpdateContacts(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag(GROUND_LAYER))
                return;

            groundContacts.Clear();
            foreach (var contact in collision.contacts)
            {
                float angle = Vector2.Angle(contact.normal, Vector2.up);
                if (angle <= maxGroundAngle)
                    groundContacts.Add(contact);
            }
        }
    }
}
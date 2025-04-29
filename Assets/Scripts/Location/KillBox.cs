using Arenar.Character;
using System;
using UnityEngine;

namespace Arenar.Location
{
    public class KillBox : MonoBehaviour
    {
        private const string CHARACTER_LAYER = "Character";


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(CHARACTER_LAYER))
            {
                var entity = collision.gameObject.GetComponent<ICharacterEntity>();
                if (entity == null)
                    return;

                if (entity.TryGetCharacterComponent<ICharacterLiveComponent>(out var component))
                    component.GetDamage(Int32.MaxValue);
            }
        }
    }
}
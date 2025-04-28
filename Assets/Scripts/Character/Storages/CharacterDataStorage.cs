using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Arenar.Character
{
    [Serializable]
    public class CharacterDataStorage
    {
        [SerializeField]
        private CharacterData characterData;
        [SerializeField]
        private SpriteRenderer characterSpriteRenderer;
        [SerializeField]
        private Rigidbody2D characterRigidbody2D;
        [SerializeField]
        private Collider2D collider2D;
        [SerializeField]
        private CollisionObserver collisionObserver;


        public CharacterData CharacterData => characterData;
        public SpriteRenderer CharacterSpriteRenderer => characterSpriteRenderer;
        public Collider2D Collider2D => collider2D;
        public Rigidbody2D CharacterRigidbody2D => characterRigidbody2D;
        public CollisionObserver CollisionObserver => collisionObserver;
    }
}
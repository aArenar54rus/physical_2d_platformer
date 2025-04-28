using Arenar.Character;
using System;
using UnityEngine;

namespace Arenar.Location
{
    public class EnemiesSpawnPoints : MonoBehaviour
    {
        [SerializeField]
        private SpawnPoint[] spawnPoints;


        public SpawnPoint[] SpawnPoints => spawnPoints;
        
        
        
        [Serializable]
        public class SpawnPoint
        {
            [SerializeField]
            private Transform spawnPoint;
            [SerializeField]
            private CharacterType enemyType;
            
            
            public Transform Point => spawnPoint;
            public CharacterType EnemyType
            {
                get
                {
                    if (enemyType == CharacterType.None || enemyType == CharacterType.Player)
                        enemyType = CharacterType.RedEnemy;

                    return enemyType;
                }
            }
        }
    }
}
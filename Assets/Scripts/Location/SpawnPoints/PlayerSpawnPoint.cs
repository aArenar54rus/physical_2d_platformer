using UnityEngine;

namespace Arenar.Location
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        [SerializeField]
        private Transform spawnPoint;


        public Transform Point
        {
            get
            {
                if (spawnPoint == null)
                    spawnPoint = this.transform;

                return spawnPoint;
            }
        }
    }
}
using UnityEngine;

namespace Game.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject treasurePrefab;
        [SerializeField] private int numOfTreasures;

        private void Start()
        {
            SpawnTreasure();
        }

        private void SpawnTreasure()
        {
            for (int i = 0; i < numOfTreasures; i++)
            {
                GameObject treasure = PoolManager.Spawn(treasurePrefab, Utils.GetRandomPoint());
            }
        }

        private void Update()
        {
            
        }
    }
}
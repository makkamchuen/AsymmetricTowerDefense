using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject treasurePrefab;
        [SerializeField] private int numOfTotalTreasures;
        [SerializeField] private TMP_Text treasureText;
        [SerializeField] private Player player;
        [SerializeField] private GameObject[] minions;
        [SerializeField] private Button menuButton;
        [SerializeField] private Canvas menuCanvas;

        private void Start()
        {
            menuButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            menuCanvas.gameObject.SetActive(false);
            player.gameObject.SetActive(true);
            foreach (GameObject minion in minions)
            {
                minion.gameObject.SetActive(true);
            }
            SpawnTreasure();
        }

        private void SpawnTreasure()
        {
            for (int i = 0; i < numOfTotalTreasures * 2; i++)
            {
                PoolManager.Spawn(treasurePrefab, Utils.GetRandomPoint());
            }
        }
    
        private void Update()
        {
            treasureText.SetText(player.GetNumOfTreasureCollected() + " / " + numOfTotalTreasures);
        }
    }
}
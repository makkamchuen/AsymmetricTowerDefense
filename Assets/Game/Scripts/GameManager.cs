using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.Win32.SafeHandles;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject treasurePrefab;
        [SerializeField] private int rewardAmountMax;
        [SerializeField] private TMP_Text rewardText;
        [SerializeField] private Player player;
        [SerializeField] private GameObject gameStatusDisplay;
        [SerializeField] private TMP_Text gameResultText;
        [SerializeField] private Button playAgainButton;
        [SerializeField] private SpawnContent[] spawnContents;
        [SerializeField] private int minSpawnDistanceFromPlayer = 20;
        [SerializeField] private int maxSpawnDistanceFromPlayer = 40;
        private float[] cooldown;
        private void Start()
        {
            cooldown = new float[spawnContents.Length];
            player.gameObject.SetActive(true);
            player.RewardAmountMax = rewardAmountMax;
            playAgainButton.onClick.AddListener(StartGame);
            Statistic.Reset(player);
        }

        public int RewardAmountMax => rewardAmountMax;

        private void Update()
        {

            rewardText.SetText(player.GetRewardAmountCollected() + " / " + rewardAmountMax);
            if (player.GetHealth().GetIsDead())
            {
                EndGame();
            }

            for (int i = 0; i < cooldown.Length; i += 1)
            {
                if (!spawnContents[i].enabled /*|| spawnContents[i].spawnedCountDoNotTouch >= spawnContents[i].maxNumber*/ )
                    continue;

                if (cooldown[i] == 0)
                {
                    GameObject gameObject = PoolManager.Spawn(spawnContents[i].prefab, Utils.GetRandomPoint(player.transform.position, minSpawnDistanceFromPlayer, maxSpawnDistanceFromPlayer));
                    gameObject.transform.LookAt(Utils.GetRandomPoint());
                    cooldown[i] = spawnContents[i].cooldown;
                    // spawnContents[i].spawnedCountDoNotTouch += 1;
                }
                else
                {
                    cooldown[i] -= Time.deltaTime;
                    if (cooldown[i] <= 0)
                    {
                        cooldown[i] = 0;
                    }
                }
            }
        }

        private void StartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Statistic.Reset(player);
        }

        private void EndGame()
        {
            if (!gameStatusDisplay.activeSelf)
            {
                gameStatusDisplay.SetActive(true);
                var gameObjects = new List<GameObject>();
                gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Minion"));
                gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
                foreach (GameObject gameObject in gameObjects)
                {
                    gameObject.SetActive(false);
                }
                player.GetStatus().SetGameFinishStatus();
                SetGameResultLabel();
            }
        }

        private void SetGameResultLabel()
        {
            TimeSpan playTimeSpan = DateTime.Now.Subtract(Statistic.PlayStartTime);
            string playTime = "";
            if (playTimeSpan.Hours > 0)
                playTime += playTimeSpan.Hours + "h ";
            if (playTimeSpan.Hours > 0 || playTimeSpan.Minutes > 0)
                playTime += playTimeSpan.Minutes + "m ";
            playTime += playTimeSpan.Seconds + "s";

            int xDistance = Convert.ToInt32((player.gameObject.transform.position.x - Statistic.StartPosition.x) / 2);
            string gameResult = String.Format("{0}\n{1}m\n{2}", playTime, xDistance, Statistic.EnemyKilled);
            gameResultText.SetText(gameResult);
        }

        [Serializable]
        class SpawnContent
        {
            public GameObject prefab;
            public int cooldown;
            [Range(1, 100)]
            // public int maxNumber = 1;
            public bool enabled;
            // public int spawnedCountDoNotTouch = 0;
        }
    }
}
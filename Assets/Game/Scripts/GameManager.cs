using System;
using System.Collections.Generic;
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

        private float[] cooldown;
        private static int currentLevel = 1;
        [SerializeField] private GameObject treasurePrefab;
        [SerializeField] private TMP_Text rewardText;
        [SerializeField] public int maxReward = 40;
        [SerializeField] private Player player;
        [SerializeField] private GameObject gameStatusDisplay;
        [SerializeField] private TMP_Text gameStatusText;
        [SerializeField] private Button playAgainButton;
        [SerializeField] private int minSpawnDistanceFromPlayer = 20;
        [SerializeField] private int maxSpawnDistanceFromPlayer = 40;
        [SerializeField] private SpawnContent[] spawnContents;

        public static GameManager Instance;

        private void Start()
        {
            Instance = this;
            cooldown = new float[spawnContents.Length];
            player.gameObject.SetActive(true);
            playAgainButton.onClick.AddListener(StartGame);
        }

        public static void IncrementCurrentLevel()
        {
            currentLevel++;
            if (currentLevel > 100) currentLevel = 100;
        }

        public static int CurrentLevel => currentLevel;

        private void Update()
        {

            rewardText.SetText(player.GetRewardAmountCollected() + " / " + maxReward);
            if (player.GetHealth().GetIsDead())
            {
                EndGame("Game Over!");
            }
            /* if (player.GetRewardAmountCollected() >= maxReward)
            {
                EndGame("Victory!");
            }
            else if (player.GetHealth().GetIsDead())
            {
                EndGame("Game Over!");
            } */

            for (int i = 0; i < cooldown.Length; i += 1)
            {
                if (!spawnContents[i].enabled || spawnContents[i].spawnedCountDoNotTouch >= spawnContents[i].maxNumber)
                    continue;

                if (cooldown[i] == 0)
                {
                    GameObject gameObject = PoolManager.Spawn(spawnContents[i].prefab, Utils.GetRandomPoint(player.transform.position, minSpawnDistanceFromPlayer, maxSpawnDistanceFromPlayer));
                    gameObject.transform.LookAt(Utils.GetRandomPoint());
                    cooldown[i] = spawnContents[i].cooldown;
                    spawnContents[i].spawnedCountDoNotTouch += 1;
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
        }

        private void EndGame(string statusText)
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
            gameStatusText.SetText(statusText);
        }

        [Serializable]
        class SpawnContent
        {
            public GameObject prefab;
            public int cooldown;
            [Range(1, 100)]
            public int maxNumber = 1;
            public bool enabled;
            public int spawnedCountDoNotTouch = 0;
        }
    }
}
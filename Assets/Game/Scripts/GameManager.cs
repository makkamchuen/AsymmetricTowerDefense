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
        public static int currentLevel = 1;
        [SerializeField] private GameObject treasurePrefab;
        [SerializeField] private int rewardReqToWin;
        [SerializeField] private TMP_Text rewardText;
        [SerializeField] private Player player;
        [SerializeField] private GameObject gameStatusDisplay;
        [SerializeField] private TMP_Text gameStatusText;
        [SerializeField] private Button playAgainButton;
        [SerializeField] private SpawnContent[] spawnContents;
        private float[] cooldown;
        private Timer timer;
        private void Start()
        {
            cooldown = new float[spawnContents.Length];
            player.gameObject.SetActive(true);
            playAgainButton.onClick.AddListener(StartGame);
            timer = GameObject.Find("Timer").GetComponent(typeof(Timer)) as Timer;
            timer.CountCompleted += () =>
            {
                EndGame("Time's up!");
            };
        }


        private void Update()
        {
            currentLevel = timer.StartTimeInSecs - timer.TimeRemainingInSec + 1;
            if (currentLevel > 100) currentLevel = 100; 
            
            rewardText.SetText(player.GetRewardAmountCollected() + " / " + rewardReqToWin);
            if (player.GetRewardAmountCollected() >= rewardReqToWin)
            {
                EndGame("Victory!");
            }
            else if (player.GetHealth().GetIsDead())
            {
                EndGame("Game Over!");
            }

            for (int i = 0; i < cooldown.Length; i += 1)
            {
                if (!spawnContents[i].enabled)
                    continue;
                
                if (cooldown[i] == 0)
                {
                    GameObject gameObject = PoolManager.Spawn(spawnContents[i].prefab, Utils.GetRandomPoint());
                    gameObject.transform.LookAt(Utils.GetRandomPoint());
                    cooldown[i] = spawnContents[i].cooldown;
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
            GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion");
            foreach (GameObject minion in minions)
            {
                minion.SetActive(false);
            }
            player.GetStatus().SetGameFinishStatus();
            gameStatusText.SetText(statusText);
            timer.StopTimer();
        }

        [Serializable]
        class SpawnContent
        {
            public GameObject prefab;
            public int cooldown;
            public bool enabled;
        }

    }
    
    
}
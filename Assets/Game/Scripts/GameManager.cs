using System;
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
        [SerializeField] private int numOfTotalTreasures;
        [SerializeField] private TMP_Text treasureText;
        [SerializeField] private Player player;
        [SerializeField] private GameObject gameStatusDisplay;
        [SerializeField] private TMP_Text gameStatusText;
        [SerializeField] private Button playAgainButton;
        [SerializeField] private SkillData[] spawnSkills;
        private float[] cooldown;

        private void Start()
        {
            cooldown = new float[spawnSkills.Length];
            player.gameObject.SetActive(true);
            SpawnTreasure();
            playAgainButton.onClick.AddListener(StartGame);
        }

        private void SpawnTreasure()
        {
            for (int i = 0; i < numOfTotalTreasures * 3; i++)
            {
                PoolManager.Spawn(treasurePrefab, Utils.GetRandomPoint());
            }
        }
    
        private void Update()
        {
            treasureText.SetText(player.GetNumOfTreasureCollected() + " / " + numOfTotalTreasures);
            if (player.GetNumOfTreasureCollected() >= numOfTotalTreasures)
            {
                EndGame("Victory!");
            }
            else if (player.GetHealth().GetIsDead())
            {
                EndGame("Game Over!");
            }

            for (int i = 0; i < cooldown.Length; i += 1)
            {
                if (cooldown[i] == 0)
                {
                    spawnSkills[i].Cast(null, Utils.GetRandomPoint());
                    cooldown[i] = spawnSkills[i].GetCoolDown();
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
        }
    }
}
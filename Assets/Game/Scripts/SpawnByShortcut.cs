﻿using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts;
using UnityEngine;
using UnityEngine.AI;

public class SpawnByShortcut : MonoBehaviour
{

    [SerializeField] private Player player;
    [SerializeField] private SpawnContentByShortcut[] spawnContents;
    private SpawnContentByShortcut contentToSpawn;

    void Update()
    {

        if (contentToSpawn != null && !Input.GetKeyDown(contentToSpawn.Shortcut))
        {
            contentToSpawn = null;
        }

        if (contentToSpawn == null)
        {
            foreach (SpawnContentByShortcut spawnContent in spawnContents)
            {
                spawnContent.timePassedDoNotTouch -= Time.deltaTime;
                if (Input.GetKeyDown(spawnContent.Shortcut) && player.GetRewardAmountCollected() >= spawnContent.RewardCost && spawnContent.timePassedDoNotTouch <= 0f)
                {
                    contentToSpawn = spawnContent;
                    spawnContent.timePassedDoNotTouch = spawnContent.CoolDown;
                    GameObject newInstance = PoolManager.Spawn(contentToSpawn.Prefab, player.GetMover().GetNavMeshAgent().destination);
                    if (!String.IsNullOrEmpty(contentToSpawn.SpawnSound))
                    {
                        FMODUnity.RuntimeManager.PlayOneShotAttached(contentToSpawn.SpawnSound, newInstance);
                    }
                    player.DecrementRewardCollected(contentToSpawn.RewardCost);
                }
            }
        }
    }
}
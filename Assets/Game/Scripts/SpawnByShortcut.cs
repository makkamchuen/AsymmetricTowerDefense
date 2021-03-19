using System;
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
    private SpawnContentByShortcut previewToSpawn;
    private Plane plane = new Plane(Vector3.up, 0);
    private Vector3 destinationToSpawn;

    void Update()
    {
        
        float distance;
        
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        NavMeshAgent agent = player.GetMover().GetNavMeshAgent();
        
        
        if (previewToSpawn != null && !Input.GetKey(previewToSpawn.Shortcut) && plane.Raycast(ray, out distance))
        {
            player.GetMover().MoveTo(ray.GetPoint(distance));
            destinationToSpawn = player.GetMover().GetNavMeshAgent().destination;
            contentToSpawn = previewToSpawn;
            previewToSpawn = null;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);


        }
        


        if (contentToSpawn != null  && agent.isOnNavMesh && agent.isStopped  && GetDistance(agent.destination, destinationToSpawn) < 0.5)
        {
            GameObject newInstance = PoolManager.Spawn(contentToSpawn.Prefab, destinationToSpawn);
            if (!String.IsNullOrEmpty(contentToSpawn.SpawnSound))
            {
                FMODUnity.RuntimeManager.PlayOneShotAttached(contentToSpawn.SpawnSound, newInstance);
            }
            player.DecrementRewardCollected(contentToSpawn.RewardCost);


            destinationToSpawn = new Vector3();
            contentToSpawn = null;
        }
       
        // while hero walks toward the location before spawn, and users click mouse to intercept, cancel the span.
        if (!agent.destination.Equals(destinationToSpawn) && agent.isOnNavMesh && !agent.isStopped && !destinationToSpawn.Equals(new Vector3()))
        {
            destinationToSpawn = new Vector3();
            contentToSpawn = null;
        }


        foreach (SpawnContentByShortcut spawnContent in spawnContents)
        {

            if (Input.GetKeyDown(spawnContent.Shortcut) && player.GetRewardAmountCollected() >=  spawnContent.RewardCost)
            {
                previewToSpawn = spawnContent;
                Cursor.SetCursor(previewToSpawn.Cursor, Vector2.zero, CursorMode.Auto);
            }
        }

    }
    
    private double GetDistance(Vector3 pt1, Vector3 pt2)
    {
        return Math.Sqrt(Math.Pow(pt1.x - pt2.x, 2) + Math.Pow(pt1.z - pt2.z, 2));
    }
    
    
}
using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts;
using UnityEngine;

public class SpawnByShortcut : MonoBehaviour
{

    [SerializeField] private SpawnContentByShortcut[] spawnContents;
    private bool mouseEntered;
    private SpawnContentByShortcut contentToSpawn;
    private Plane plane = new Plane(Vector3.up, 0);


    void Update()
    {
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (contentToSpawn != null &&  !Input.GetKey(contentToSpawn.Shortcut) && plane.Raycast(ray, out distance))
        {
            GameObject newInstance = PoolManager.Spawn(contentToSpawn.Prefab, ray.GetPoint(distance));

            if (!String.IsNullOrEmpty(contentToSpawn.SpawnSound))
            {
                FMODUnity.RuntimeManager.PlayOneShotAttached(contentToSpawn.SpawnSound, newInstance);
            }

            contentToSpawn = null;
        }

        foreach (SpawnContentByShortcut spawnContent in spawnContents)
        {

            if (Input.GetKeyDown(spawnContent.Shortcut) && mouseEntered)
                contentToSpawn = spawnContent;
        }
        if (contentToSpawn != null)
        {
            Cursor.SetCursor(contentToSpawn.Cursor, Vector2.zero, CursorMode.Auto);
        }
        else
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

    }

    private void OnMouseEnter()
    {
        mouseEntered = true;
    }

    private void OnMouseExit()
    {
        mouseEntered = false;
    }
    
}
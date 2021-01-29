using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] 
    Sprite sprite;

    private bool mouseEntered;
    private bool canSpawn;
    private Texture2D cursor;
    public string shortcut;
    public GameObject prefab;
    private Plane plane = new Plane(Vector3.up, 0);

    void Start()
    {
        
        cursor = sprite.texture;
    }
    void Update()
    {
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (canSpawn && !Input.GetKey(shortcut) && plane.Raycast(ray, out distance))
        {
            GameObject newInstance = PoolManager.Spawn(prefab);

            newInstance.transform.position = ray.GetPoint(distance);
        }
            
        canSpawn = Input.GetKey(shortcut) && mouseEntered;
        if (canSpawn)
            Cursor.SetCursor (cursor, Vector2.zero, CursorMode.Auto);
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

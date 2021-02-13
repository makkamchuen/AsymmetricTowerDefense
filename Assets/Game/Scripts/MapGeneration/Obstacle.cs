using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
        
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.z * 100) * -1 + 1000;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

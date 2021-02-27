
using System;
using UnityEngine;

public class Reward : MonoBehaviour
{

    private int amount;

    public int Amount
    {
        get
        {
            return amount;
        }
        set
        {
            amount = value;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().IncrementRewardCollected(this.amount);
            Destroy(gameObject);
        }

    }

}

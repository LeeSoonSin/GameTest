using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadTile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Shooter"))
        {
            collision.gameObject.GetComponent<Shooter>().Die();
        }
        if(collision.gameObject.CompareTag("Infighter"))
        {
            collision.gameObject.GetComponent<Infighter>().Die();
        }
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerStat>().Die();
        }
    }
    
}

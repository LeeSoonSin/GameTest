using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadTile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Infighter"))
        {
            collision.GetComponent<Infighter>().Die();
        }
        if(collision.gameObject.CompareTag("Shooter"))
        {
            collision.GetComponent<Shooter>().Die();
        }
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStat>().Die();
        }
    }
}

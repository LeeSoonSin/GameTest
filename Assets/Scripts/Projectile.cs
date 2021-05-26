using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int moveSpeed = 5;
    private Rigidbody2D projectileRigid;
    private int damage;

    private int speed;

    private void Awake()
    {
        projectileRigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        projectileRigid.velocity = new Vector2(speed, projectileRigid.velocity.y);
    }

    public void InstantiateBullet(GameObject Target, int _damage)
    {
        damage = _damage;
        if (Target.transform.position.x > transform.position.x)
        {
            speed = moveSpeed;
        }
        else
        {
            speed = -moveSpeed;
        }
        Destroy(gameObject, 4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStat playerstat = collision.GetComponent<PlayerStat>();
            Debug.Log("Hit");
            playerstat.PlayerDamaged(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}

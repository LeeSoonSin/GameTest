using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D projectileRigid;
    public float speed;
    // Start is called before the first frame update
    private void Awake()
    {
        projectileRigid = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        speed = 2f;
        projectileRigid.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit");
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        Destroy(gameObject, 4f);
    }
}

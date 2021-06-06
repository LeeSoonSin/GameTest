using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public Transform left;
    public Transform right;
    public Transform player;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        HP = 700;
        currentHP = HP;
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Event()
    {
        int pattern = Random.Range(0, 6);

        if(pattern == 0)
        {
            if(transform.position.x >= player.position.x)
            {

            }
        }
    }
    protected override void Die()
    {
        //gameManager.dead = true;
        gameManager.MonsterCount[gameManager.buildIndex - 1] -= 1;
        Destroy(gameObject);
    }

}

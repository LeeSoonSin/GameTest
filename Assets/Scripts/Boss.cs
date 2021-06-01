using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public Transform left;
    public Transform right;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        HP = 700;
        currentHP = HP;
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
}

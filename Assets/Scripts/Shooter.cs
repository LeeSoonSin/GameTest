using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Mob
{
    public GameObject projectiles;
    public Transform pos;
    
    protected override void Start()
    {
        base.Start();
        HP = 70;
        currentHP = HP;
        Atk = 20;
        circleCollider2D.radius = 5f;
        Distance = 3f;
    }

    protected override void Attack()
    {
        
        if(currentTime <= 0)
        {
            IsAttack = true;
            Instantiate(projectiles, pos.position, pos.rotation).GetComponent<Projectile>().InstantiateBullet(target, Atk);            
            currentTime = coolTime;
        }
        else
        {
            IsAttack = false;
        }
        
        currentTime -= Time.deltaTime;
    }

    
}

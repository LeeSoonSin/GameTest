using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Mob
{
    public GameObject projectiles;
    public Transform pos;
    GameManager gameManager;
    
    protected override void Start()
    {
        base.Start();
        HP = 70;
        currentHP = HP;
        Atk = 20;
        circleCollider2D.radius = 5f;
        Distance = 3f;
        gameManager = FindObjectOfType<GameManager>();
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
    protected override void Die()
    {
        Debug.Log("����");
        //gameManager.dead = true;
        gameManager.MonsterCount[gameManager.buildIndex - gameManager.RoundNumber] -= 1;
        Destroy(gameObject);
    }

}

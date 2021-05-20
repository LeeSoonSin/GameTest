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
        circleCollider2D.radius = 5f;
        Distance = 3f;
    }

    protected override void Move()
    {
        if (IsTracing)
        {
            Vector3 playerPos = target.transform.position;
            if (transform.position.x - playerPos.x > Distance)
            {
                speed = -moveSpeed;
            }
            else if (playerPos.x - transform.position.x > Distance)
            {
                speed = moveSpeed;
            }
            else
            {
                speed = 0;
                Attack();
                // 몬스터 공격
            }
        }
        MobRigid.velocity = new Vector2(speed, MobRigid.velocity.y);
    }

    protected void Attack()
    {
        
        if(currentTime <= 0)
        {
            IsAttack = true;
            Instantiate(projectiles, pos.position, pos.rotation).GetComponent<Projectile>().InstantiateBullet(target);
            currentTime = coolTime;
        }
        else
        {
            IsAttack = false;
        }
        
        currentTime -= Time.deltaTime;
    }
}

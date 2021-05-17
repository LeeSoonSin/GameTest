using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Mob
{
    public GameObject projectile;
    public Transform pos;

    void Start()
    {
        circleCollider2D.radius = 5f;
        coolTime = 2f;
        Distance = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Move()
    {
        

        if (IsTracing)
        {
            Vector3 playerPos = target.transform.position;

            if (transform.position.x - playerPos.x > Distance)
            {
                moveSpeed = -Speed;
            }
            else if (playerPos.x - transform.position.x > Distance)
            {
                moveSpeed = Speed;
            }
            else
            {
                moveSpeed = 0;
                Attack();
                // 몬스터 공격
            }
        }

        MobRigid.velocity = new Vector2(moveSpeed, MobRigid.velocity.y);
    }

    protected void Attack()
    {
        
        if(currentTime <= 0)
        {
            IsAttack = true;
            GameObject projectileCopy = Instantiate(projectile, pos.position, pos.rotation);
            projectileCopy.transform.Translate()
            currentTime = coolTime;
        }
        else
        {
            IsAttack = false;
        }
        
        currentTime -= Time.deltaTime;
    }
}

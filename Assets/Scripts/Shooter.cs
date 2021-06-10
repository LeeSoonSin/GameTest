using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Mob
{
    public GameObject projectiles;
    public Transform pos;
    Animator anim;

    Rigidbody2D shooterRigid;

    protected override void Start()
    {
        base.Start();
        HP = 70;
        currentHP = HP;
        moveSpeed = 3;
        Atk = 5;
        circleCollider2D.radius = 3f;
        Distance = 3f;
        anim = GetComponent<Animator>();
        shooterRigid = gameObject.GetComponent<Rigidbody2D>();
    }
    protected override void Move()
    {
        if (IsTracing)
        {

            Vector3 playerPos = target.transform.position;
            StopCoroutine(ChangeMovement());
            isChange = false;
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
                anim.SetTrigger("Attack");
                Attack();
            }
        }
        else
        {
            if (!isChange)
            {
                StartCoroutine(ChangeMovement());
                isChange = true;
            }

        }
        shooterRigid.velocity = new Vector2(speed, shooterRigid.velocity.y);
    }
    protected override void MoveAni()
    {
        if (speed != 0)
        {
            anim.SetBool("isWalk", true);
        }
        else
        {
            anim.SetBool("isWalk", false);
        }
    }
    protected override void Attack()
    {

        if (currentTime <= 0)
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

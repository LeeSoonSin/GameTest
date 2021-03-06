using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infighter : Mob
{
    public Transform pos;
    public Vector2 boxSize;
    Animator anim;

    Rigidbody2D infighterRigid;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        moveSpeed = 4;
        HP = 50;
        currentHP = HP;
        Atk = 5;
        circleCollider2D.radius = 2f;
        moveSpeed = 4;
        Distance = 1f;
        anim = GetComponent<Animator>();
        infighterRigid = gameObject.GetComponent<Rigidbody2D>();
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
                Attack();
            }
        }
        else
        {
            if(!isChange)
            {
                StartCoroutine(ChangeMovement());
                isChange = true;
                anim.SetBool("Attack", false);
            }
            
        }
        infighterRigid.velocity = new Vector2(speed, infighterRigid.velocity.y);
    }
    protected override void MoveAni()
    {
        if(speed != 0)
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
        anim.SetBool("Attack", true);
        if (currentTime <= 0)
        {
            IsAttack = true;
            currentTime = coolTime;
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
            //anim.SetBool("Attack", true);
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.CompareTag("Player"))
                {
                    PlayerStat playerStat = collider.GetComponent<PlayerStat>();
                    playerStat.PlayerDamaged(Atk);
                    Rigidbody2D playerRigid = collider.GetComponent<Rigidbody2D>();
                    playerRigid.AddForce(new Vector2((collider.transform.position.x - transform.position.x)*5f, 7.5f), ForceMode2D.Impulse);
                    break;
                }
            }
        }
        else
        {
            IsAttack = false;
        }

        currentTime -= Time.deltaTime;
    }
    public override void Slow()
    {
        if (moveSpeed > 2 && foxBullet.isInfighter == true)
        {
            DeBuff.SetActive(true);
            moveSpeed -= 2;
            foxBullet.isFoxBallHit = true;
            Debug.Log("????????");
            foxBullet.isInfighter = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infighter : Mob
{
    public Transform pos;
    public Vector2 boxSize;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        moveSpeed = 4;
        HP = 50;
        currentHP = HP;
        Atk = 10;
        circleCollider2D.radius = 3.5f;
        moveSpeed = 4;
        Distance = 1f;
    }

    protected override void Attack()
    {
        if (currentTime <= 0)
        {
            IsAttack = true;
            currentTime = coolTime;
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
}

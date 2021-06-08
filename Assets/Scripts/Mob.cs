using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mob : Enemy
{
    Rigidbody2D MobRigid;
    public CircleCollider2D circleCollider2D;

    [SerializeField]
    protected int moveSpeed; //속력
    [SerializeField]
    protected int speed; //변속

    [SerializeField]
    protected bool IsTracing;

    [SerializeField]
    protected float Distance; //공격 사거리

    protected float coolTime = 2f;//공격 쿨타임
    protected float currentTime;
    [SerializeField]
    protected bool IsAttack;

    FoxBullet foxBullet;

    void Awake()
    {
        circleCollider2D = transform.GetChild(0).gameObject.GetComponent<CircleCollider2D>();
        MobRigid = gameObject.GetComponent<Rigidbody2D>();
    }
    protected virtual void Start()
    {
        StartCoroutine(ChangeMovement());
        IsTracing = false;
        foxBullet = GetComponent<FoxBullet>();
    }

    void FixedUpdate()
    {
        Move();
        //플랫폼 체크
        if (speed != 0)
        {
            Vector2 front = new Vector2(MobRigid.position.x + (Mathf.Abs(speed) / speed), MobRigid.position.y);
            Debug.DrawRay(front, Vector3.down, Color.red);
            RaycastHit2D rayHit = Physics2D.Raycast(front, Vector3.down, 1, LayerMask.GetMask("Wall"));
            if (rayHit.collider == null)
            {
                speed *= -1;
            }
        }
    }

    IEnumerator ChangeMovement()
    {
        speed = Random.Range(-1, 2)*moveSpeed;
        float randomTime = Random.Range(2f, 4f);

        yield return new WaitForSeconds(randomTime);
        StartCoroutine(ChangeMovement());
    }

    protected virtual void Move() 
    {
        if (IsTracing)
        {
            Vector3 playerPos = target.transform.position;

            if (transform.position.x - playerPos.x > Distance )
            {
                speed = -moveSpeed;
            }
            else if(playerPos.x - transform.position.x > Distance)
            {
                speed = moveSpeed;
            }
            else
            {
                speed = 0;
                Attack();
            }
        }

        MobRigid.velocity = new Vector2(speed, MobRigid.velocity.y);
    }

    protected virtual void Attack()
    {
    }

    public void Slow()
    {
        if(moveSpeed > 2)
        {
            moveSpeed -= 2;
            foxBullet.isFoxBallHit = true;
            Debug.Log("맞았어요");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            target = collision.gameObject;
            IsTracing = true;
            StopCoroutine(ChangeMovement());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            IsTracing = false;
            StartCoroutine(ChangeMovement());
        }
    }
    protected override void Die()
    {
        GameManager.instance.MonsterCount[GameManager.instance.buildIndex - GameManager.instance.RoundNumber] -= 1;
        Destroy(gameObject);
    }
}

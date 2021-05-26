using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mob : Enemy
{
    public Rigidbody2D MobRigid;
    public CircleCollider2D circleCollider2D;

    protected int moveSpeed = 3; //속력
    [SerializeField]
    protected int speed; //변속

    [SerializeField]
    protected bool IsTracing;

    [SerializeField]
    protected float Distance; //공격 사거리

    protected float coolTime = 2f;//공격 쿨타임
    protected float currentTime;
    protected bool IsAttack;

    void Awake()
    {
        circleCollider2D = transform.GetChild(0).gameObject.GetComponent<CircleCollider2D>();
        MobRigid = GetComponent<Rigidbody2D>();
    }
    protected virtual void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ChangeMovement());
        IsTracing = false;
    }
    void FixedUpdate()
    {
        Move();

        //플랫폼 체크
        Vector2 front = new Vector2(MobRigid.position.x + speed, MobRigid.position.y);
        Debug.DrawRay(front, Vector3.down, Color.red);
        RaycastHit2D rayHit = Physics2D.Raycast(front, Vector3.down, 1, LayerMask.GetMask("Wall"));
        if(rayHit.collider == null)
        {
            speed *= -1;
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
                // 몬스터 공격
            }
        }

        MobRigid.velocity = new Vector2(speed, MobRigid.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
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
}

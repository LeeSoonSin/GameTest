using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    PlayerStat playerStat;
    public float jumpPower;
    public float maxSpeed;
    SpriteRenderer spriteRenderer;
    Animator anim;

    private float curTime;
    public float coolTime = 0.5f;
    public Transform pos;
    public Vector2 boxSize;
    public int transanimal;
    public float transDelayTime = 1f; // 1초마다 분노게이지 1씩깍임
    float transTimer = 0f;
    public bool notActive; // 이 함수가 참일때는 활동 못함.(특히 점프일때 다른 스킬이나 그런거 못함.)

    public State state;
    private float DefcoolTime = 1f;
    private float DefCurTime;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        playerStat = GameObject.Find("Player").GetComponent<PlayerStat>();
    }
    //Stop Speed
    void Update()
    {
        Attack(); //근접공격
        Jump(); //점프
        TransAnimal(); //수인화
        Guard(); //방어
        if (Input.GetButtonUp("Horizontal"))
        {
            playerRigidbody.velocity = new Vector2 (playerRigidbody.velocity.normalized.x * 0.5f, playerRigidbody.velocity.y);
        }
        Animation();
        BuffSkill();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
    void FixedUpdate()
    {
        MoveDirection();
        LandingPlatform();
    }

    private void LandingPlatform()
    {
        //Landing Platform
        if (playerRigidbody.velocity.y < 0)
        {
            Debug.DrawRay(transform.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector3.down, 1, LayerMask.GetMask("Wall"));//1은 레이의 길이임

            if (rayHit.collider != null) // 빔을 맞지 않았다면? 점프했을때.
            {
                if (rayHit.distance < 0.5f)
                {
                    anim.SetBool("isJumping", false);
                    notActive = false;
                }
            }
        }
    } // 요것은 점프낙하 레이여!
    private void MoveDirection()
    {
        //Move Speed
        float horizontal = Input.GetAxisRaw("Horizontal");
        playerRigidbody.AddForce(Vector2.right * horizontal, ForceMode2D.Impulse);
        //최대속도 구현하기. velocity는 그 오브젝트의 속도를 말함.
        if (horizontal < 0)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else if (horizontal > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (playerRigidbody.velocity.x > maxSpeed)
        {
            playerRigidbody.velocity = new Vector2(maxSpeed, playerRigidbody.velocity.y);
        }
        if (playerRigidbody.velocity.x < (-1) * maxSpeed)
        {
            playerRigidbody.velocity = new Vector2((-1) * maxSpeed, playerRigidbody.velocity.y);
        }
    }
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (curTime <= 0 && notActive == false)
            {
                notActive = true;
                maxSpeed = 0;
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.tag == "Enemy")
                    {
                        Debug.Log("몬스터 피깍는 함수 만들어주세용:)");
                        Invoke("atkWait", 1.1f);
                        break;
                    }
                }
                anim.SetTrigger("atk");
                curTime = coolTime;
                Invoke("atkWait", 1.1f);
            }
        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }  //근접공격 (공격하면서 바라보는 방향으로 이동하는거 아직 구현 못함.)
    private void TransAnimal()
    {
        if (Input.GetKeyDown(KeyCode.F) && transanimal == 1 && playerStat.currentRage == 100)
        {
            Debug.Log("구미호로 변신!");
            anim.SetBool("isTrans", true);
            transanimal = 0;
        }
        if(Input.GetKeyDown(KeyCode.F) && transanimal == 0 && playerStat.currentRage < 100)
        {
            anim.SetBool("isTrans", false);
            transanimal = 1;
        }
        if(transanimal == 0) // 분노게이지가 줄어드는 조건식
        {
            transTimer += Time.deltaTime;
            if(transTimer >= transDelayTime)
            {
                transTimer = 0f;
                playerStat.currentRage -= 1;
                if (playerStat.currentRage < 30)
                {
                    transanimal = 1;
                    anim.SetBool("isTrans", false);
                }
            }
        }

    } //수인화, 1초마다 분노게이지 1씩깍임
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping") && notActive == false)
        {
            playerRigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
            notActive = true;
        }
    }
    private void Guard()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(!state.IsBlock)
            {
                DefCurTime = DefcoolTime;
                state.IsBlock = true;
                state.IsDamaged = false;
                state.StartCoroutine(state.GuardCoroutine());
            }
        }

        if(state.IsBlock)
        {
            if(!state.IsDamaged)
            {
                if(DefCurTime <= 0)
                {
                    state.IsDamaged = true;
                }
                DefCurTime -= Time.deltaTime;
            }
        }
    }

    private void Animation()
    {
        //Animation
        if (Mathf.Abs(playerRigidbody.velocity.x) < 0.3)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }
    } // 애니메이션 true flase 모음
    private void BuffSkill() // 스킬 버프
    {
        if(Input.GetKeyDown(KeyCode.W) && notActive == false)
        {
            anim.SetBool("isBuff", true);
        }
    }
    private void atkWait()
    {
        notActive = false;
        maxSpeed = 5;
    }
}

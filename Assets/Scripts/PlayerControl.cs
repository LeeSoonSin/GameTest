using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    public AudioClip clip;
    public AudioClip atkSfx;
    public AudioClip hitSfx;
    Animator anim;
    SpriteRenderer spriteRenderer;
    public string currentMapName;

    //Move 관련 변수
    Rigidbody2D playerRigidbody;
    PlayerStat playerStat;
    private float jumpPower = 18f;
    public float maxSpeed = 5f;
    [SerializeField]
    private float mem_horizontal = 0;

    //상태 관련 변수
    public bool IsDamaged = true;
    [SerializeField]
    private bool IsGuard = false;
    [SerializeField]
    private bool IsAttack = false;
    public bool transanimal = false;
    public bool notActive = false; // 이 함수가 참일때는 활동 못함.(특히 점프일때 다른 스킬이나 그런거 못함.)

    [SerializeField]
    //Attack 관련 변수
    private float curTime = 0;
    private float coolTime = 1.1f;
    public Transform pos;
    public Vector2 boxSize;
    
    //Transanimal 관련 변수
    private float transDelayTime = 1f; // 1초마다 분노게이지 1씩깍임
    float transTimer = 0f;

    //Guard 관련 변수
    private float GuardDelay = 5f;
    private float HoldGuardTime = 1f; //가드 유지 시간
    private float GuardCurTime;
    public GameObject blueHalfGuard = null;
    public GameObject redHalfGuard = null;
    public GameObject blueGuard = null;
    public GameObject redGuard = null;

    //Buff 관련 변수
    private bool isBuffcoolTime = false;
    public GameObject Energy = null;


    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        //Energy = GetComponent<GameObject>();
    }
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        playerStat = GameObject.Find("Player2").GetComponent<PlayerStat>();
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
        HealingSkill();
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

            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector3.down, 3, LayerMask.GetMask("Wall"));//1은 레이의 길이임

            if (rayHit.collider != null) // 빔을 맞지 않았다면? 점프했을때.
            {
                if (rayHit.distance < 1.3f)
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

        if (!IsAttack || !IsGuard)
        {
            mem_horizontal = horizontal;

            //최대속도 구현하기. velocity는 그 오브젝트의 속도를 말함.
            if (horizontal < 0)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else if (horizontal > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        if (playerRigidbody.velocity.x > maxSpeed)
        {
            playerRigidbody.velocity = new Vector2(maxSpeed, playerRigidbody.velocity.y);
        }

        if (playerRigidbody.velocity.x < (-1) * maxSpeed)
        {
            playerRigidbody.velocity = new Vector2((-1) * maxSpeed, playerRigidbody.velocity.y);
        }

        if (playerRigidbody.velocity.normalized.x == 0)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }
    }
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (curTime <= 0 && notActive == false)
            {
                SoundManager.instance.SFXPlay("AttackSfx", atkSfx);
                notActive = true;
                IsAttack = true;
                maxSpeed = 0;
                curTime = coolTime;
                AttackRange();
                Invoke("AttackRange", 0.5f);

                anim.SetTrigger("atk");
                Invoke("atkWait", 1.1f);
            }
        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }  //근접공격 (공격하면서 바라보는 방향으로 이동하는거 아직 구현 못함.)

    void AttackRange()  
    {
        transform.Translate(new Vector2(0.25f, 0));
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.CompareTag("Shooter"))
            {
                SoundManager.instance.SFXPlay("HitSfx", hitSfx);
                Shooter shooter = collider.GetComponent<Shooter>();
                shooter.EnemyDamaged(playerStat.Atk);
                Invoke("atkWait", 1.1f); // 이동불가 + 행동 활성화
                break;
            }
            else if (collider.CompareTag("Infighter"))
            {
                SoundManager.instance.SFXPlay("HitSfx", hitSfx);
                Infighter infighter = collider.GetComponent<Infighter>();
                infighter.EnemyDamaged(playerStat.Atk);
                Invoke("atkWait", 1.1f); // 이동불가 + 행동 활성화
                break;
            }
            else if (collider.CompareTag("Boss"))
            {
                SoundManager.instance.SFXPlay("HitSfx", hitSfx);
                Boss boss = collider.GetComponent<Boss>();
                boss.EnemyDamaged(playerStat.Atk);
                Invoke("atkWait", 1.1f); // 이동불가 + 행동 활성화
                break;
            }
        }
    }

    private void TransAnimal()
    {
        if (Input.GetKeyDown(KeyCode.F) && transanimal && playerStat.currentRage > 10)
        {
            Debug.Log("구미호로 변신!");
            anim.SetBool("isTrans", true);
            transanimal = false;
        }
        else if(Input.GetKeyDown(KeyCode.F) && !transanimal && playerStat.currentRage < 100)
        {
            anim.SetBool("isTrans", false);
            transanimal = true;
        }
        if(!transanimal) // 분노게이지가 줄어드는 조건식
        {
            transTimer += Time.deltaTime;
            if(transTimer >= transDelayTime)
            {
                transTimer = 0f;
                playerStat.currentRage -= 1;
                if (playerStat.currentRage < 1)
                {
                    transanimal = true;
                    anim.SetBool("isTrans", false);
                }
            }
        }

    } //수인화, 1초마다 분노게이지 1씩깍임
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping") && notActive == false)
        {
            SoundManager.instance.SFXPlay("JumpSfx", clip);
            playerRigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
            notActive = true;
        }
    }
    private void Guard()
    {
        if(Input.GetKeyDown(KeyCode.S) && !notActive)
        {
            if(!IsGuard)
            {
                anim.SetBool("isGuard", true);
                GuardCurTime = HoldGuardTime;
                IsGuard = true;
                IsDamaged = false;
                notActive = true;
                maxSpeed = 0f;
                if(transanimal == false)//방어강화 스킬 얻지 못했을때 조건 추가하기.
                {
                    redHalfGuard.SetActive(true);
                }
                else if(transanimal)//방어강화 스킬 얻지 못했을때 조건 추가하기.
                {
                    blueHalfGuard.SetActive(true);
                }
                /*else if (transanimal && 방어 강화 스킬 얻었을때) 
                {
                    blueGuard.SetActive(true);
                }
                else if (transanimal == false && 방어 강화 스킬 얻었을때) 
                {
                    redGuard.SetActive(true);
                }*/
                StartCoroutine(GuardCoroutine());
            }
        }
        if(IsGuard)
        {
            if(!IsDamaged)
            {
                if(GuardCurTime <= 0)
                {
                    IsDamaged = true;
                    notActive = false;
                    maxSpeed = 5f;
                    anim.SetBool("isGuard", false);
                    redHalfGuard.SetActive(false);
                    blueHalfGuard.SetActive(false);
                }
                GuardCurTime -= Time.deltaTime;
            }
        }
    }

    IEnumerator GuardCoroutine()
    {
        yield return new WaitForSeconds(GuardDelay);
        IsGuard = false;
        redHalfGuard.SetActive(false);
        blueHalfGuard.SetActive(false);
        anim.SetBool("isGuard", false);
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
        if(Input.GetKeyDown(KeyCode.W) && notActive == false && isBuffcoolTime == false)
        {
            isBuffcoolTime = true;
            anim.SetBool("isBuff", true);
            playerStat.Atk += 10;
            Invoke("CancelBuff", 60f);
            Energy.SetActive(true);
        }
    }
    void CancelBuff()
    {
        anim.SetBool("isBuff", false);
        playerStat.Atk -= 10;
        playerStat.Def -= 20;
        Energy.SetActive(false);
        Invoke("ReturnBuff", 30f);
    }
    void ReturnBuff()
    {
        playerStat.Def += 20;
        isBuffcoolTime = false;
    }
    private void atkWait()
    {
        notActive = false;
        IsAttack = false;
        maxSpeed = 5f;
    }
    private void HealingSkill()
    {
        if(Input.GetKeyDown(KeyCode.R) && notActive == false)
        {
            notActive = true;
            maxSpeed = 0f;
            StartCoroutine(Heal());
            GameObject healing = Instantiate(Resources.Load<GameObject>("healing"), transform.position, Quaternion.identity);
            Destroy(healing, 5f);
            Invoke("atkWait", 5f);
        }
    }
    IEnumerator Heal()
    {
        for(int i = 0; i < 5; i++)
        {
            playerStat.currentHP += 5;
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }
}

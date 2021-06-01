using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Animator anim;
    SpriteRenderer spriteRenderer;
    public string currentMapName;

    //Move ���� ����
    Rigidbody2D playerRigidbody;
    PlayerStat playerStat;
    private float jumpPower = 18f;
    public float maxSpeed = 5f;
    [SerializeField]
    private float mem_horizontal = 0;

    //���� ���� ����
    public bool IsDamaged = true;
    [SerializeField]
    private bool IsGuard = false;
    [SerializeField]
    private bool IsAttack = false;
    public bool transanimal = false;
    public bool notActive = false; // �� �Լ��� ���϶��� Ȱ�� ����.(Ư�� �����϶� �ٸ� ��ų�̳� �׷��� ����.)

    [SerializeField]
    //Attack ���� ����
    private float curTime = 0;
    private float coolTime = 1.1f;
    public Transform pos;
    public Vector2 boxSize;
    
    //Transanimal ���� ����
    private float transDelayTime = 1f; // 1�ʸ��� �г������ 1������
    float transTimer = 0f;

    //Guard ���� ����
    private float GuardDelay = 5f;
    private float HoldGuardTime = 1f; //���� ���� �ð�
    private float GuardCurTime;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        playerStat = GameObject.Find("Player").GetComponent<PlayerStat>();
    }
    //Stop Speed
    void Update()
    {
        Attack(); //��������
        Jump(); //����
        TransAnimal(); //����ȭ
        Guard(); //���
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

            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector3.down, 1, LayerMask.GetMask("Wall"));//1�� ������ ������

            if (rayHit.collider != null) // ���� ���� �ʾҴٸ�? ����������.
            {
                if (rayHit.distance < 0.5f)
                {
                    anim.SetBool("isJumping", false);
                    notActive = false;
                }
            }
        }
    } // ����� �������� ���̿�!
    private void MoveDirection()
    {
        //Move Speed
        float horizontal = Input.GetAxisRaw("Horizontal");
        playerRigidbody.AddForce(Vector2.right * horizontal, ForceMode2D.Impulse);

        if (!IsAttack || !IsGuard)
        {
            mem_horizontal = horizontal;

            //�ִ�ӵ� �����ϱ�. velocity�� �� ������Ʈ�� �ӵ��� ����.
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
                
                notActive = true;
                IsAttack = true;
                maxSpeed = 0;
                curTime = coolTime;
                //Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                //foreach (Collider2D collider in collider2Ds)
                //{
                //    if (collider.CompareTag("Shooter"))
                //    {
                //        Shooter shooter = collider.GetComponent<Shooter>();
                //        shooter.EnemyDamaged(playerStat.Atk);
                //        break;
                //    }
                //}
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
    }  //�������� (�����ϸ鼭 �ٶ󺸴� �������� �̵��ϴ°� ���� ���� ����.)

    void AttackRange()
    {
        transform.Translate(new Vector2(0.25f, 0));
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.CompareTag("Shooter"))
            {
                Shooter shooter = collider.GetComponent<Shooter>();
                shooter.EnemyDamaged(playerStat.Atk);
                Invoke("atkWait", 1.1f); // �̵��Ұ� + �ൿ Ȱ��ȭ
                break;
            }
        }
    }

    private void TransAnimal()
    {
        if (Input.GetKeyDown(KeyCode.F) && transanimal && playerStat.currentRage == 100)
        {
            Debug.Log("����ȣ�� ����!");
            anim.SetBool("isTrans", true);
            transanimal = false;
        }
        if(Input.GetKeyDown(KeyCode.F) && !transanimal && playerStat.currentRage < 100)
        {
            anim.SetBool("isTrans", false);
            transanimal = true;
        }
        if(!transanimal) // �г�������� �پ��� ���ǽ�
        {
            transTimer += Time.deltaTime;
            if(transTimer >= transDelayTime)
            {
                transTimer = 0f;
                playerStat.currentRage -= 1;
                if (playerStat.currentRage < 30)
                {
                    transanimal = true;
                    anim.SetBool("isTrans", false);
                }
            }
        }

    } //����ȭ, 1�ʸ��� �г������ 1������
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
        if(Input.GetKeyDown(KeyCode.S) && !notActive)
        {
            if(!IsGuard)
            {
                GuardCurTime = HoldGuardTime;
                IsGuard = true;
                IsDamaged = false;
                notActive = true;
                maxSpeed = 0f;
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
                }
                GuardCurTime -= Time.deltaTime;
            }
        }
    }

    IEnumerator GuardCoroutine()
    {
        yield return new WaitForSeconds(GuardDelay);
        IsGuard = false;
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
    } // �ִϸ��̼� true flase ����
    private void BuffSkill() // ��ų ����
    {
        if(Input.GetKeyDown(KeyCode.W) && notActive == false)
        {
            anim.SetBool("isBuff", true);
        }
    }
    private void atkWait()
    {
        notActive = false;
        IsAttack = false;
        maxSpeed = 5f;
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    public float transDelayTime = 1f; // 1�ʸ��� �г������ 1������
    float transTimer = 0f;

    private void Start()
    {
        playerStat = GameObject.Find("Player").GetComponent<PlayerStat>();
    }
    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    //Stop Speed
    void Update()
    {
        Attack(); //��������
        Jump();
        TransAnimal();
        if (Input.GetButtonUp("Horizontal"))
        {
            playerRigidbody.velocity = new Vector2 (playerRigidbody.velocity.normalized.x * 0.5f, playerRigidbody.velocity.y);
        }
        Animation();
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
                }
            }
        }
    } // ����� �������� ���̿�!
    private void MoveDirection()
    {
        //Move Speed
        float horizontal = Input.GetAxisRaw("Horizontal");
        playerRigidbody.AddForce(Vector2.right * horizontal, ForceMode2D.Impulse);
        //�ִ�ӵ� �����ϱ�. velocity�� �� ������Ʈ�� �ӵ��� ����.
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
            Debug.Log("�����ѹ��Ѱ�?");
            if (curTime <= 0)
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.tag == "Enemy")
                    {
                        Debug.Log("���� �Ǳ�� �Լ� ������ּ���:)");
                    }
                }
                anim.SetTrigger("atk");
                curTime = coolTime;
            }
        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }  //��������
    private void TransAnimal()
    {
        if (Input.GetKeyDown(KeyCode.F) && transanimal == 1 && playerStat.currentRage == 100)
        {
            Debug.Log("����ȣ�� ����!");
            anim.SetBool("isTrans", true);
            transanimal = 0;
        }
        if(Input.GetKeyDown(KeyCode.F) && transanimal == 0 && playerStat.currentRage < 100)
        {
            anim.SetBool("isTrans", false);
            transanimal = 1;
        }
        if(transanimal == 0) // �г�������� �پ��� ���ǽ�
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

    } //����ȭ, 1�ʸ��� �г������ 1������
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
        {
            playerRigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
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
    } // �ִϸ��̼� true flase ����
}

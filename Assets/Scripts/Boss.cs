using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : Enemy
{
    public GameObject left;
    public GameObject right;
    public GameObject pricklePos;
    public GameObject Warning;
    public AudioClip fistSfx;

    private float ReadySpeed = 3f;
    private float AttackSpeed;

    [SerializeField]
    private bool isAttack = false;

    [SerializeField]
    private bool isHit = false;

    [SerializeField]
    private bool isDead = false;
    private int pattern;

    // Start is called before the first frame update
    void Start()
    {
        HP = 700;
        currentHP = HP;
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            Event();
        }
    }

    private void Event()
    {
        if (!isAttack)
        {
            pattern = Random.Range(0, 6);

            if (pattern == 0)
            {
                Atk = 20;
                AttackSpeed = 15f;
                if (transform.localPosition.x >= target.transform.localPosition.x)
                {
                    Vector2 pos = new Vector2(right.transform.position.x, right.transform.position.y);
                    StartCoroutine(HorizonReady(pos, right));
                    isAttack = true;
                }
                else
                {
                    Vector2 pos = new Vector2(left.transform.position.x, left.transform.position.y);
                    StartCoroutine(HorizonReady(pos, left));
                    isAttack = true;
                }
            }
            else if(pattern == 1)
            {
                Atk = 15;
                AttackSpeed = 25f;
                Vector2 pos_l = new Vector2(left.transform.position.x, left.transform.position.y);
                Vector2 pos_r = new Vector2(right.transform.position.x, right.transform.position.y);
                StartCoroutine(VerticalReady(pos_l, left));
                StartCoroutine(VerticalReady(pos_r, right));
                isAttack = true;
            }
            else if(pattern == 2 || pattern == 3)
            {
                Atk = 10;
                AttackSpeed = 25f;
                Vector2 pos_l = new Vector2(left.transform.position.x, left.transform.position.y);
                Vector2 pos_r = new Vector2(right.transform.position.x, right.transform.position.y);
                if(target.transform.position.x >= transform.position.x)
                {
                    StartCoroutine(VerticalEachReady(pos_r, right));
                }
                else
                {
                    StartCoroutine(VerticalEachReady(pos_l, left));
                }
                isAttack = true;
            }
            else if(pattern == 4)
            {
                //화염방사
            }
            else if(pattern == 5)
            {
                Atk = 25;
                AttackSpeed = 25f;
                Warning.transform.position = new Vector2(target.transform.position.x, Warning.transform.position.y);
                pricklePos.transform.position = new Vector2(target.transform.position.x, pricklePos.transform.position.y);
                Vector2 pos = new Vector2(pricklePos.transform.position.x, pricklePos.transform.position.y);
                StartCoroutine(PrickleAttack(pos, pricklePos));
                isAttack = true;
            }
        }
    }

    IEnumerator HorizonReady(Vector2 pos, GameObject fist) //수평 공격 준비
    {
        Vector2 playerPos = new Vector2(fist.transform.position.x, target.transform.position.y);
        while (true)
        {
            fist.transform.position = Vector2.Lerp(fist.transform.position, playerPos, ReadySpeed * Time.deltaTime);

            if (fist.transform.position.y - playerPos.y < float.Epsilon + 0.1f)
            {
                StartCoroutine(HorizonAttack(pos, fist));
                break;
            }
            yield return null;
        }
    }

    IEnumerator HorizonAttack(Vector2 pos, GameObject fist) //수평 공격
    {
        float distance = transform.position.x - fist.transform.position.x;
        Vector2 vector2 = new Vector2(fist.transform.position.x + distance * 2, fist.transform.position.y);
        isHit = true;
        while (true)
        {
            fist.transform.position = Vector2.MoveTowards(fist.transform.position, vector2, AttackSpeed * Time.deltaTime);

            if(Mathf.Abs(fist.transform.position.x - vector2.x) < float.Epsilon + 0.1f)
            {
                
                StartCoroutine(Return(pos, fist));
                break;
            }
            yield return null;
        }
    }

    IEnumerator VerticalReady(Vector2 pos, GameObject fist) //수직 공격 준비
    {
        Vector2 vector2 = new Vector2(fist.transform.position.x, fist.transform.position.y + 1f);
        while (true)
        {
            fist.transform.position = Vector2.Lerp(fist.transform.position, vector2, ReadySpeed * Time.deltaTime);
            if (vector2.y - fist.transform.position.y < float.Epsilon + 0.1f)
            {
                StartCoroutine(VerticalAttack(pos, fist));
                break;
            }
            yield return null;
        }
    }

    IEnumerator VerticalEachReady(Vector2 pos, GameObject fist) //수직 개인 공격 준비
    {
        Vector2 vector2 = new Vector2(target.transform.position.x, fist.transform.position.y);
        while (true)
        {
            fist.transform.position = Vector2.Lerp(fist.transform.position, vector2, ReadySpeed * Time.deltaTime);
            if (Mathf.Abs(vector2.x - fist.transform.position.x) < float.Epsilon + 0.1f)
            {
                StartCoroutine(VerticalAttack(pos, fist));
                break;
            }
            yield return null;
        }
    }

    IEnumerator VerticalAttack(Vector2 pos, GameObject fist) //수직 공격
    {
        Vector2 vector2 = new Vector2(fist.transform.position.x, -0.3f);
        isHit = true;
        while (true)
        {
            fist.transform.position = Vector2.MoveTowards(fist.transform.position, vector2, AttackSpeed * Time.deltaTime);
            if(fist.transform.position.y - vector2.y < float.Epsilon + 0.1f)
            {
                StartCoroutine(Return(pos, fist));
                break;
            }
            yield return null;
        }
    }

    IEnumerator Return(Vector2 pos, GameObject fist)
    {
        isHit = false;
        while (true)
        {
            fist.transform.position = Vector2.Lerp(fist.transform.position, pos, ReadySpeed * Time.deltaTime);

            if (pos.y - fist.transform.position.y < float.Epsilon + 0.1f)
            {
                fist.transform.position = new Vector3(pos.x, pos.y, 0);
                yield return new WaitForSeconds(2f);
                isAttack = false;
                break;
            }
            yield return null;
        }
    } //원위치

    IEnumerator PrickleAttack(Vector2 pos, GameObject prickle)
    {
        Warning.SetActive(true);
        yield return new WaitForSeconds(1f);
        Warning.SetActive(false);
        prickle.SetActive(true);
        isHit = true;
        while (true)
        {
            prickle.transform.position = Vector2.MoveTowards(prickle.transform.position, Warning.transform.position, AttackSpeed * Time.deltaTime);
            if(Warning.transform.position.y - prickle.transform.position.y < float.Epsilon + 0.1f)
            {
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(PrickleReturn(pos, prickle));
                break;
            }
            yield return null;
        }
    } //가시 공격

    IEnumerator PrickleReturn(Vector2 pos, GameObject prickle)
    {
        isHit = false;
        while (true)
        {
            prickle.transform.position = Vector2.MoveTowards(prickle.transform.position, pos, AttackSpeed * Time.deltaTime);

            if (prickle.transform.position.y - pos.y< float.Epsilon + 0.1f)
            {
                prickle.transform.position = new Vector3(pos.x, pos.y, 0);
                prickle.SetActive(false);
                yield return new WaitForSeconds(2f);
                isAttack = false;
                break;
            }
            yield return null;
        }
    } //가시 원위치


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(isHit)
            {
                SoundManager.instance.SFXPlay("BossFist", fistSfx);
                PlayerStat playerStat = collision.GetComponent<PlayerStat>();
                playerStat.PlayerDamaged(Atk);
            }
            
        }
    }
    protected override void Die()
    {
        isDead = true;
        GameManager.instance.MonsterCount[GameManager.instance.buildIndex - 1] -= 1;
        StartCoroutine(DeadScene());
    }

    IEnumerator DeadScene()
    {
        Color color = gameObject.GetComponent<Color>();
        while(color.a > 0f)
        {
            color.a -= Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene("EndStory");
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    public GameObject bullet;
    public GameObject foxFireball;
    public Transform pos;
    public float cooltime;
    private float curtime;

    Animator anim;
    PlayerControl playerControl;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = GameObject.Find("Player2").GetComponent<PlayerControl>();
        anim = GameObject.Find("Player2").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ArrowSkill();
        FoxfireSkill();
    }
    void ArrowSkill()
    {
        if (curtime <= 0)
        {
            if (Input.GetKey(KeyCode.Q) && playerControl.notActive == false && playerControl.transanimal == false)
            {
                anim.SetBool("isArrow", true);
                Invoke("InstaniBullet", 1.6f);
            }
            curtime = cooltime;
        }
        curtime -= Time.deltaTime;
    }
    void FoxfireSkill()
    {
        if (curtime <= 0)
        {
            if (Input.GetKey(KeyCode.E) && playerControl.notActive == false && playerControl.transanimal == false)
            {
                anim.SetBool("isFoxfire", true);
                Invoke("InstaniFoxFireball", 1.6f);
            }
            curtime = cooltime;
        }
        curtime -= Time.deltaTime;
    }
    private void InstaniBullet()
    {
        Instantiate(bullet, pos.position, transform.rotation);//���ʹϾ��� �ڱ�״�� ������ ��
        Invoke("Isarrowfalse", 0.5f);
        
    }//�ذ� �����
    private void InstaniFoxFireball()
    {
        Instantiate(foxFireball, pos.position, transform.rotation);//���ʹϾ��� �ڱ�״�� ������ ��
        Invoke("IsFoxfirefalse", 0.5f);

    }//�ذ� �����
    private void Isarrowfalse()
    {
        anim.SetBool("isArrow", false);
    }//���� �����
    private void IsFoxfirefalse()
    {
        anim.SetBool("isFoxfire", false);
    }//���� �����
}

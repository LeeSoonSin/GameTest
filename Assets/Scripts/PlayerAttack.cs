using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    public GameObject bullet;
    public Transform pos;
    public float cooltime;
    private float curtime;

    Animator anim;
    Move move;
    // Start is called before the first frame update
    void Start()
    {
        move = GameObject.Find("Player").GetComponent<Move>();
        anim = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(curtime <= 0)
        {
            if (Input.GetKey(KeyCode.Q) && move.notActive == false)
            {
                anim.SetBool("isArrow", true);
                Invoke("InstaniBullet", 1.6f);
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
    private void Isarrowfalse()
    {
        anim.SetBool("isArrow", false);
    }//���� �����
}

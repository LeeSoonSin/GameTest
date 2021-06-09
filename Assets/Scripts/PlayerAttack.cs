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
        UIManager.instance.SetSkill(curtime, cooltime);
    }
    void ArrowSkill()
    {

        if (Input.GetKey(KeyCode.Q) && playerControl.notActive == false && playerControl.transanimal == false && GameManager.instance.SelectedCard[0])
        {
            if (curtime >= cooltime)
            {
                anim.SetBool("isArrow", true);
                Invoke("InstaniBullet", 1f);
                curtime = 0;
            }
        }
        curtime += Time.deltaTime;
    }
    void FoxfireSkill()
    {

        if (Input.GetKey(KeyCode.E) && playerControl.notActive == false && playerControl.transanimal == false && GameManager.instance.SelectedCard[3])
        {
            if (curtime >= cooltime)
            {
                anim.SetBool("isFoxfire", true);
                Invoke("InstaniFoxFireball", 1f);
                curtime = 0;
            }
        }
        curtime += Time.deltaTime;
    }
    private void InstaniBullet()
    {
        Instantiate(bullet, pos.position, transform.rotation);//쿼터니언은 자기그대로 고유의 값
        Invoke("Isarrowfalse", 0.5f);

    }//밑과 연계됨
    private void InstaniFoxFireball()
    {
        Instantiate(foxFireball, pos.position, transform.rotation);//쿼터니언은 자기그대로 고유의 값
        Invoke("IsFoxfirefalse", 0.5f);

    }//밑과 연계됨
    private void Isarrowfalse()
    {
        anim.SetBool("isArrow", false);
    }//위와 연계됨
    private void IsFoxfirefalse()
    {
        anim.SetBool("isFoxfire", false);
    }//위와 연계됨
}

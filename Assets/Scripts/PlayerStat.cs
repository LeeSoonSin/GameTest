using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public int HP = 100; //ü��
    public int currentHP; //���� ü��
    public int Atk; //���ݷ�
    public int FireBall; //��ų
    public int Def = 50; //����
    public int Rage = 100; //�г������
    public int currentRage; //���� �г������
    public int GuardRange;

    PlayerControl playerControl;

    private void Awake()
    {
        playerControl = GetComponent<PlayerControl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHP = HP;
        currentRage = 20;
    }

    public void PlayerDamaged(int damage)
    {
        if(!playerControl.IsDamaged)
        {
            playerControl.IsDamaged = true;
            playerControl.maxSpeed = 5f;
            if(damage < GuardRange)
            {
                damage = GuardRange;
            }
            currentHP -= (damage - GuardRange);
            UIManager.instance.SetHP(currentHP, HP);
            return;
        }
        
        currentHP -= damage + (50 - Def)*2/5;
        UIManager.instance.SetHP(currentHP, HP);
        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("�������");
        GameManager.instance.Dead = true;
        Destroy(gameObject);
    }
}

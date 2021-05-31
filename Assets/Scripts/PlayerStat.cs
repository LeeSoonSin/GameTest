using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public int HP = 100; //ü��
    public int currentHP; //���� ü��
    public int Atk; //���ݷ�
    public int Def; //����
    public int Luck; //��
    public int Rage = 50; //�г������
    public int currentRage; //���� �г������

    private bool dead = false;

    public State state;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = HP;
        Def = 0;
        currentRage = 100;
    }

    public void PlayerDamaged(int damage)
    {
        if(!state.IsDamaged)
        {
            state.IsDamaged = true;
            return;
        }

        currentHP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        dead = true;
        Debug.Log("�������");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP; //ü��
    public int currentHP;
    public int Atk; //���ݷ�
    protected string Name; //�̸�
    public GameObject target;

    private bool dead = false;

    public void EnemyDamaged(int damage)
    {
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
        Destroy(gameObject);
    }
}

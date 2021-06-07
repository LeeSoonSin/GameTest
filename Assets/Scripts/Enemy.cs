using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Enemy : MonoBehaviour//MonoBehaviour
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
        if (currentHP <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {

    }
}

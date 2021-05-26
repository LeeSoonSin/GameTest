using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP; //체력
    public int currentHP;
    public int Atk; //공격력
    protected string Name; //이름
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
        Debug.Log("으앙쥬금");
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour//MonoBehaviour
{
    public Image HPBar;
    public int HP; //ü��
    public int currentHP;
    public int Atk; //���ݷ�
    protected string Name; //�̸�
    public GameObject target;

    public void EnemyDamaged(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    protected void SetHP(int curHP)
    {
        HPBar.fillAmount = (float)curHP / HP;
    }

    public virtual void Die()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public int HP = 100; //체력
    public int currentHP; //현재 체력
    public int Atk; //공격력
    public int FireBall; //스킬
    public int Def = 50; //방어력
    public int Rage = 100; //분노게이지
    public int currentRage; //현재 분노게이지
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
        Debug.Log("으앙쥬금");
        GameManager.instance.Dead = true;
        Destroy(gameObject);
    }
}

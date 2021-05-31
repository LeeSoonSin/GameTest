using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public int HP = 100; //체력
    public int currentHP; //현재 체력
    public int Atk; //공격력
    public int Def; //방어력
    public int Luck; //운
    public int Rage = 50; //분노게이지
    public int currentRage; //현재 분노게이지

    private bool dead = false;

    PlayerControl playerControl;

    private void Awake()
    {
        playerControl = GetComponent<PlayerControl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHP = HP;
        Def = 0;
        currentRage = 0;
    }

    public void PlayerDamaged(int damage)
    {
        if(!playerControl.IsDamaged)
        {
            playerControl.IsDamaged = true;
            playerControl.notActive = false;
            playerControl.maxSpeed = 5f;
            return;
        }

        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        dead = true;
        Debug.Log("으앙쥬금");
    }
}

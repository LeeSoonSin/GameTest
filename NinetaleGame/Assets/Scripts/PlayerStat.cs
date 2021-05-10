using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public float HP; //체력
    public float Atk; //공격력
    public float Def; //방어력
    public float Luck; //운
    public float Rage; //분노게이지
    public float XP; //경험치
    public int Lv; //레벨

    private float[] MAX_XP = new float[5]; //최대 경험치

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(HP <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        
    }
}

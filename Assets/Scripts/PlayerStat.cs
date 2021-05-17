using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public float HP; //체력
    public float currentHP; //현재 체력
    public float Atk; //공격력
    public float Def; //방어력
    public float Luck; //운
    public float Rage; //분노게이지
    public float currentRage; //현재 분노게이지


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
        Debug.Log("으앙쥬금");
    }
}

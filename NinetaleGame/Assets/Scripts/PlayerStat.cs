using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public float HP; //ü��
    public float Atk; //���ݷ�
    public float Def; //����
    public float Luck; //��
    public float Rage; //�г������
    public float XP; //����ġ
    public int Lv; //����

    private float[] MAX_XP = new float[5]; //�ִ� ����ġ

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public float HP; //ü��
    public float currentHP; //���� ü��
    public float Atk; //���ݷ�
    public float Def; //����
    public float Heal; //ü��ȸ��
    public float Rage; //�г������
    public float currentRage; //���� �г������


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(currentHP <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("�������");
    }
}

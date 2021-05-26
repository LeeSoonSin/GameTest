using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public bool Transformation = false; //����ȭ ����
    public bool IsBlock = false; //��� ����
    public bool IsDamaged = true; //���� ���� (false�� ��� ����)

    private float delayTime = 5f;
    
    public IEnumerator GuardCoroutine() //��� ��Ÿ��
    {
        yield return new WaitForSeconds(delayTime);
        IsBlock = false;
    }
}

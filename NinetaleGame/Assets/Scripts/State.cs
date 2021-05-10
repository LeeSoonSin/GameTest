using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public bool Transformation; //����ȭ ����
    public bool IsBlock; //��� ����
    public bool IsDamaged; //�ǰ� ����

    private float delayTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Transformation = false;
        IsBlock = false;
        IsDamaged = false;
    }

    private void Update()
    {
        
    }

    IEnumerator BlockingCoroutine()
    {
        yield return new WaitForSeconds(delayTime);
        IsDamaged = false;
    }
}

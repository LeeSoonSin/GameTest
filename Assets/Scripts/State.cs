using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public bool Transformation; //수인화 상태
    public bool IsBlock; //방어 상태
    public bool IsDamaged; //피격 상태

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

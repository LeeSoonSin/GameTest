using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public bool Transformation = false; //수인화 상태
    public bool IsBlock = false; //방어 상태
    public bool IsDamaged = true; //무적 상태 (false일 경우 무적)

    private float delayTime = 5f;
    
    public IEnumerator GuardCoroutine() //방어 쿨타임
    {
        yield return new WaitForSeconds(delayTime);
        IsBlock = false;
    }
}

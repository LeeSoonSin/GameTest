using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    public GameObject bullet;
    public Transform pos;
    public float cooltime;
    private float curtime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(curtime <= 0)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                Instantiate(bullet, pos.position, transform.rotation);//쿼터니언은 자기그대로 고유의 값
            }
            curtime = cooltime;
        }
        curtime -= Time.deltaTime;
    }
}

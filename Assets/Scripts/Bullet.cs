using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask isLayer;


    // Start is called before the first frame update
    void Start()
    {

        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);
        if(ray.collider != null)
        {
            if(ray.collider.tag == "Enemy")
            {
                //파티클이 뿜뿜Instantiate(프리팹, 포지션, 로테이션) as GameObject;
                Debug.Log("명중!");
                GameObject boom = Instantiate(Resources.Load<GameObject>("boom"), transform.position, Quaternion.identity); //프리팹에 있는거 갖고 오는 코드;
                Destroy(boom, 1);
            }
            Destroy(gameObject);
        }
        if(transform.rotation.y == 0)
        {
            transform.Translate(transform.right * speed * Time.deltaTime); // 총알이 오른쪽 봤을때 안보임 ㅠ 씬에서는 보이는데
        }
        else
        {
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
        }
    }
}

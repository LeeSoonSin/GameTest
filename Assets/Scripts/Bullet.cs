using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask isLayer;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", 2); //일정시간이 지난 후 특정 함수를 호출 할 수 있음.(실행할 함수명, 지연시간)
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);
        if(ray.collider != null)
        {
            if(ray.collider.tag == "Enemy")
            {
                Debug.Log("명중!");
            }
            DestroyBullet();
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
    
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}

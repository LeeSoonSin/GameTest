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
        Invoke("DestroyBullet", 2); //�����ð��� ���� �� Ư�� �Լ��� ȣ�� �� �� ����.(������ �Լ���, �����ð�)
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);
        if(ray.collider != null)
        {
            if(ray.collider.tag == "Enemy")
            {
                Debug.Log("����!");
            }
            DestroyBullet();
        }
        if(transform.rotation.y == 0)
        {
            transform.Translate(transform.right * speed * Time.deltaTime); // �Ѿ��� ������ ������ �Ⱥ��� �� �������� ���̴µ�
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

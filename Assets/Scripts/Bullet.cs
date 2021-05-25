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
                //��ƼŬ�� �ջ�Instantiate(������, ������, �����̼�) as GameObject;
                Debug.Log("����!");
                GameObject boom = Instantiate(Resources.Load<GameObject>("boom"), transform.position, Quaternion.identity); //�����տ� �ִ°� ���� ���� �ڵ�;
                Destroy(boom, 1);
            }
            Destroy(gameObject);
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
}

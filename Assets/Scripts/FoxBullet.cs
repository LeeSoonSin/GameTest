using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class FoxBullet : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask isLayer;

    Shooter shooter;
    Infighter infighter;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
        shooter = GameObject.Find("Shooter").GetComponent<Shooter>();
        infighter = GameObject.Find("infighter").GetComponent<Infighter>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);
        if (ray.collider != null)
        {
            if (ray.collider.CompareTag("Shooter"))
            {
                //��ƼŬ�� �ջ�Instantiate(������, ������, �����̼�) as GameObject;
                Debug.Log("���� ����!");
                GameObject boom = Instantiate(Resources.Load<GameObject>("foxBoom"), transform.position, Quaternion.identity); //�����տ� �ִ°� ���� ���� �ڵ�;
                Destroy(boom, 1);
                shooter.Slow();
            }
            else if (ray.collider.CompareTag("Infighter"))
            {
                //��ƼŬ�� �ջ�Instantiate(������, ������, �����̼�) as GameObject;
                Debug.Log("�������� ����!");
                GameObject boom = Instantiate(Resources.Load<GameObject>("foxBoom"), transform.position, Quaternion.identity); //�����տ� �ִ°� ���� ���� �ڵ�;
                Destroy(boom, 1);
                infighter.Slow();
            }

            Destroy(gameObject);
        }
        if (transform.rotation.y == 0)
        {
            transform.Translate(transform.right * speed * Time.deltaTime); // �Ѿ��� ������ ������ �Ⱥ��� �� �������� ���̴µ�
        }
        else
        {
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
        }
    }
}

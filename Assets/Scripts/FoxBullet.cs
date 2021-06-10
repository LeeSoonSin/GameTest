using System.Collections;
using System.Collections.Generic;
//using System.Security.Cryptography;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class FoxBullet : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask isLayer;
    public bool isFoxBallHit  = false;

    public bool isShooter = false;
    public bool isInfighter = false;

    Shooter shooter;
    Infighter infighter;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);
        if (ray.collider != null)
        {
            if (ray.collider.CompareTag("Shooter") && isFoxBallHit == false)
            {
                shooter = ray.collider.GetComponent<Shooter>();
                //��ƼŬ�� �ջ�Instantiate(������, ������, �����̼�) as GameObject;
                ray.collider.gameObject.transform.GetChild(3).gameObject.SetActive(true);
                shooter.EnemyDamaged(30);
                Debug.Log("���� ����!");
                GameObject boom = Instantiate(Resources.Load<GameObject>("foxBoom"), transform.position, Quaternion.identity); //�����տ� �ִ°� ���� ���� �ڵ�;
                Destroy(boom, 1);
                isShooter = true;
                //shooter.Slow();
            }
            else if (ray.collider.CompareTag("Infighter") && isFoxBallHit == false)
            {
                infighter = ray.collider.GetComponent<Infighter>();
                //��ƼŬ�� �ջ�Instantiate(������, ������, �����̼�) as GameObject;
                ray.collider.gameObject.transform.GetChild(2).gameObject.SetActive(true);
                infighter.EnemyDamaged(30);
                Debug.Log("�������� ����!");
                GameObject boom = Instantiate(Resources.Load<GameObject>("foxBoom"), transform.position, Quaternion.identity); //�����տ� �ִ°� ���� ���� �ڵ�;
                Destroy(boom, 1);
                isInfighter = true;
                //infighter.Slow();
            }

            Destroy(gameObject);
            isFoxBallHit = false;
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

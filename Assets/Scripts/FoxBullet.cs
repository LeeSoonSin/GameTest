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
                //파티클이 뿜뿜Instantiate(프리팹, 포지션, 로테이션) as GameObject;
                Debug.Log("슈터 명중!");
                GameObject boom = Instantiate(Resources.Load<GameObject>("foxBoom"), transform.position, Quaternion.identity); //프리팹에 있는거 갖고 오는 코드;
                Destroy(boom, 1);
                shooter.Slow();
            }
            else if (ray.collider.CompareTag("Infighter"))
            {
                //파티클이 뿜뿜Instantiate(프리팹, 포지션, 로테이션) as GameObject;
                Debug.Log("인파이터 명중!");
                GameObject boom = Instantiate(Resources.Load<GameObject>("foxBoom"), transform.position, Quaternion.identity); //프리팹에 있는거 갖고 오는 코드;
                Destroy(boom, 1);
                infighter.Slow();
            }

            Destroy(gameObject);
        }
        if (transform.rotation.y == 0)
        {
            transform.Translate(transform.right * speed * Time.deltaTime); // 총알이 오른쪽 봤을때 안보임 ㅠ 씬에서는 보이는데
        }
        else
        {
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
        }
    }
}

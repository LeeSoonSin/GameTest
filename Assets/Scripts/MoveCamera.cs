using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveCamera : MonoBehaviour
{
    public Transform target;


    public Vector2 center;
    public Vector2 size;

    float height;
    float width;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "InGame")
        {
            center.x = 7.5f; center.y = 0f;
            size.x = 43.5f; size.y = 15;
        }
        else if(SceneManager.GetActiveScene().name == "2Stage")
        {
            center.x = 25f; center.y = 0f;
            size.x = 80f; size.y = 15;
        }
        else if (SceneManager.GetActiveScene().name == "3Stage")
        {
            center.x = 19; center.y = 0f;
            size.x = 50; size.y = 45;
        }
        else if (SceneManager.GetActiveScene().name == "4Stage")
        {
            center.x = 9.5f; center.y = -8;
            size.x = 50; size.y = 45;
        }
        else if (SceneManager.GetActiveScene().name == "5Stage")
        {
            center.x = 28.5f; center.y = 7f;
            size.x = 87; size.y = 30;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = new Vector3(target.position.x, target.position.y + 1.5f, target.position.z);
        //transform.position = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = Vector3.Lerp(transform.position, pos, 0.25f);
        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
}

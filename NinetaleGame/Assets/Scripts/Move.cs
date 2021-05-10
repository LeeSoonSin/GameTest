using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    public float maxSpeed;
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    //Stop Speed
    void Update()
    {
        if(Input.GetButtonUp("Horizontal"))
        {
            
            playerRigidbody.velocity = new Vector2 (playerRigidbody.velocity.normalized.x * 0.5f, playerRigidbody.velocity.y);
        }

        if (Input.GetButton("Horizontal"))
        {

            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

    }
    void FixedUpdate()
    {
        //Move Speed
        float horizontal = Input.GetAxisRaw("Horizontal");

        playerRigidbody.AddForce(Vector2.right * horizontal, ForceMode2D.Impulse);
        //최대속도 구현하기. velocity는 그 오브젝트의 속도를 말함.
        if(playerRigidbody.velocity.x > maxSpeed)
        {
            playerRigidbody.velocity = new Vector2(maxSpeed, playerRigidbody.velocity.y);
        }
        if (playerRigidbody.velocity.x < (-1) * maxSpeed)
        {
            playerRigidbody.velocity = new Vector2((-1) * maxSpeed, playerRigidbody.velocity.y);
        }
    }
}

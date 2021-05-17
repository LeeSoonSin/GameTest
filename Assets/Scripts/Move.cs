using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    public float jumpPower;
    public float maxSpeed;
    SpriteRenderer spriteRenderer;
    Animator anim;
    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    //Stop Speed
    void Update()
    {
        if(Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
        {
            playerRigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }
        if(Input.GetButtonUp("Horizontal"))
        {
            
            playerRigidbody.velocity = new Vector2 (playerRigidbody.velocity.normalized.x * 0.5f, playerRigidbody.velocity.y);
        }

        if (Input.GetButton("Horizontal"))
        {

            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
        //Animation
        if(Mathf.Abs(playerRigidbody.velocity.x) < 0.3)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
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
        //Landing Platform
        if(playerRigidbody.velocity.y < 0)
        {
            Debug.DrawRay(playerRigidbody.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(playerRigidbody.position, Vector3.down, 1, LayerMask.GetMask("Platform"));//1은 레이의 길이임

            if (rayHit.collider != null) // 빔을 맞지 않았다면? 점프했을때.
            {
                if (rayHit.distance < 05f)
                {
                    anim.SetBool("isJumping", false);
                }

            }
        }
    }
}

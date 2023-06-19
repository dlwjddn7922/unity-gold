using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    Animator anim;
    
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //이동
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        if(rigid.velocity.x > maxSpeed)//오른쪽
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }else if(rigid.velocity.x < maxSpeed * (-1)) //왼쪽
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        //방향전환
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        //애니메이션
        if(Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            anim.SetBool("isWalking", false);
        }else
        {
            anim.SetBool("isWalking", true);
        }

        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    [SerializeField] private List<Sprite> center;
    [SerializeField] private List<Sprite> left;
    [SerializeField] private List<Sprite> right;
    enum Direction //애니메이션의 상태값
    {
        Center,
        Left,
        Right
    }
    private Direction dir = Direction.Center;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        GetComponent<SpriteAnimation>().SetSprite(center, 0.2f);
        Invoke("Think", 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //지형체크
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            nextMove = nextMove * -1;
            CancelInvoke();
            Invoke("Think", 5);
        }
        if (rigid.position.x == 0 && dir != Direction.Center)
        {
            dir = Direction.Center;
            GetComponent<SpriteAnimation>().SetSprite(center, 0.2f);
        }//오른쪽으로 이동시 애니메이션
        else if (rigid.position.x > 0 && dir != Direction.Right)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            dir = Direction.Right;
            GetComponent<SpriteAnimation>().SetSprite(right, 0.2f);

        }//왼쪽으로 이동시 애니메이션
        else if (rigid.position.x < 0 && dir != Direction.Left)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            dir = Direction.Left;
            GetComponent<SpriteAnimation>().SetSprite(left, 0.2f);
        }
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);

        float nextThinkTime = Random.Range(2f, 6f);
        Invoke("Think", nextThinkTime);

    }
}

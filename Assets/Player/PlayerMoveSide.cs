using UnityEngine;

public class PlayerMoveSide : MonoBehaviour
{
    public float speed = 5f;

    Rigidbody2D rbody;
    SpriteRenderer sr;
    float moveX;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        if (!InputBinder.isInputEnabled)
        {
            moveX = 0f;
            return;
        }
        if (moveX > 0)
            sr.flipX = false;
        else if (moveX < 0)
            sr.flipX = true;
    }

    void FixedUpdate()
    {
        rbody.linearVelocity = new Vector2(moveX * speed, rbody.linearVelocity.y);
    }
}


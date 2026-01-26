using UnityEngine;

public class PlayerMoveSide : MonoBehaviour
{
    public float speed = 5f;

    Rigidbody2D rbody;
    float moveX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");

        if(moveX > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if(moveX < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void FixedUpdate()
    {
        rbody.linearVelocity = new Vector2(moveX * speed, rbody.linearVelocity.y);
    }
}


using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    Animator animator;

    float axisH = 0f;
    public float speed = 3f;

    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerWalk";

    string nowAnime;
    string oldAnime;

    SpriteRenderer sr;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        nowAnime = stopAnime;
        oldAnime = stopAnime;

        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        axisH = Input.GetAxisRaw("Horizontal");

        if (axisH > 0)
            sr.flipX = false;
        else if (axisH < 0)
            sr.flipX = true;
    }

    void FixedUpdate()
    {
        rbody.linearVelocity = new Vector2(axisH * speed, rbody.linearVelocity.y);

        if (axisH == 0)
            nowAnime = stopAnime;
        else
            nowAnime = moveAnime;

        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);
        }
    }
}
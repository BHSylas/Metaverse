using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 3.0f;

    public string downAnime = "PlayerDown";
    public string upAnime = "PlayerUp";
    public string leftAnime = "PlayerLeft";
    public string rightAnime = "PlayerRight";


    string nowAnimation;
    string oldAnimation;

    float axisH;
    float axisV;
    public float angleZ = -90f;

    Rigidbody2D rbody;
    bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        oldAnimation = downAnime;

    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving == false)
        {
            axisH = Input.GetAxisRaw("Horizontal");
            axisV = Input.GetAxisRaw("Vertical");
        }

        Vector2 fromPt = transform.position;
        Vector2 toPt = new Vector2(fromPt.x + axisH, fromPt.y + axisV);
        angleZ = GetAngle(fromPt, toPt);

        if(angleZ >= -45.0f && angleZ < 45.0f)
        {
            nowAnimation = rightAnime;
        }
        else if(angleZ >= 45.0f && angleZ < 135.0f)
        {
            nowAnimation = upAnime;
        }
        else if(angleZ >= -135.0f && angleZ < -45.0f)
        {
            nowAnimation = downAnime;
        }
        else
        {
            nowAnimation = leftAnime;
        }
        if(nowAnimation != oldAnimation)
        {
            oldAnimation = nowAnimation;
            GetComponent<Animator>().Play(nowAnimation);
        }

        Debug.Log($"H:{axisH}, V:{axisV}");


    }

    void FixedUpdate()
    {
        rbody.linearVelocity = new Vector2(axisH, axisV) * speed;
    }

    public void SetAxis(float h, float v)
    {
        axisH = h;
        axisV = v;
        if(axisH ==0 && axisV ==0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }

    float GetAngle(Vector2 p1, Vector2 p2)
    {
        float angle;
        if(axisH != 0 || axisV != 0)
        {
            float dx = p2.x - p1.x;
            float dy = p2.y - p1.y;
            float rad = Mathf.Atan2(dy, dx);
            angle = rad*Mathf.Rad2Deg;
        }

        else
        {
            angle = angleZ;
        }
        return angle;
    }
}

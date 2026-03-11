using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;

    public float minX;
    public float maxX;
    void Start()
    {
        if (player == null) return;

        float startX = Mathf.Clamp(player.position.x, minX, maxX);

        transform.position = new Vector3(
            startX,
            transform.position.y,
            transform.position.z
        );
    }

    void LateUpdate()
    {
        if (player == null) return;

        float targetX = Mathf.Clamp(player.position.x, minX, maxX);

        Vector3 target = new Vector3(
            targetX,
            transform.position.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);


    }
}
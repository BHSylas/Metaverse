using UnityEngine;

public class RedballisLie : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("RedSphere active = " + gameObject.activeInHierarchy);
        Debug.Log("RedSphere pos = " + transform.position);

        var cams = Camera.allCameras;
        Debug.Log("Camera count = " + cams.Length);
        foreach (var cam in cams)
            Debug.Log(cam.name);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

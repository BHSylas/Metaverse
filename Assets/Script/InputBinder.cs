using UnityEngine;

public class InputBinder : MonoBehaviour
{
    public static bool isInputEnabled = true;

    public void NeverMove()
    {
        isInputEnabled = false;

#if UNITY_WEBGL && !UNITY_EDITOR
        WebGLInput.captureAllKeyboardInput = false;
#endif
    }

    public void Moving()
    {
        isInputEnabled = true;

#if UNITY_WEBGL && !UNITY_EDITOR
        WebGLInput.captureAllKeyboardInput = true;
#endif
    }
}
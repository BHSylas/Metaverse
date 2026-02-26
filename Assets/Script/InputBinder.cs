using UnityEngine;

public class InputBinder : MonoBehaviour
{
    public static bool isInputEnabled = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void NeverMove()
    {
        isInputEnabled = false;
    }

    public void Moving()
    {
        isInputEnabled = true;
    }
}

using System.Collections;
using UnityEngine;

public class ControlGuide : MonoBehaviour
{
    public GameObject guideUI;
    public float showTime = 3f;

    void Start()
    {
        StartCoroutine(ShowGuide());
    }

    IEnumerator ShowGuide()
    {
        guideUI.SetActive(true);

        yield return new WaitForSeconds(showTime);

        guideUI.SetActive(false);
    }
}
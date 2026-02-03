using UnityEngine;
using System.Collections;


public class nukimpyo : MonoBehaviour
{
    public GameObject alertMark;
    public float showDuration = 2f;


    Coroutine hideCoroutine;

    void Start()
    {
        if (alertMark == null)
        {
            Debug.LogError("alertMark is NULL");
        }
        else
        {
            alertMark.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (!other.CompareTag("Player")) return;
        
        alertMark.SetActive(true);

        if(hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }

        hideCoroutine = StartCoroutine(HideAfterTime());

    }

    IEnumerator HideAfterTime()
    {
        yield return new WaitForSeconds(showDuration);
        alertMark.SetActive(false);
        hideCoroutine = null;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        if(hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
            hideCoroutine = null;
        }

        alertMark.SetActive(false);
        
    }


}

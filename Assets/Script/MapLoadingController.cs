using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MapLoadingController : MonoBehaviour
{
    public static string nextScene;

    public float loadingDelay = 2f;

    void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(loadingDelay);

        SceneManager.LoadScene(nextScene);
    }
}
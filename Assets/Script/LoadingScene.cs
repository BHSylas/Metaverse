using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{

    public float waitTime = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoadNextScene());

    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(waitTime);

        string nextScene = PlayerPrefs.GetString("NextScene");

        if(string.IsNullOrEmpty(nextScene))
        {
            Debug.LogError("NextScene ¾øÀ½");
            yield break;
        }

        SceneManager.LoadScene(nextScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
